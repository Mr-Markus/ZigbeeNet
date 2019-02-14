using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.DAO;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Clusters.General;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZDO.Command;

namespace ZigBeeNet.ZCL
{
    public abstract class ZclCluster
    {
        ///**
        // * The logger
        // */
        //private Logger logger = LoggerFactory.getLogger(ZclCluster.class);

        /**
         * The {@link ZigBeeNetworkManager} to which this device belongs
         */
        private ZigBeeNetworkManager _zigbeeManager;

        /**
         * The {@link ZigBeeEndpoint} to which this cluster belongs
         */
        private ZigBeeEndpoint _zigbeeEndpoint;

        /**
         * The ZCL cluster ID for this cluster
         */
        protected ushort _clusterId;

        /**
         * The name of this cluster
         */
        protected string _clusterName;

        /**
         * Defines if the remote is a client (true) or server (false)
         * The definition of the direction is based on the remote being the server. If it is really
         * a server, then we need to reverse direction
         */
        private bool _isClient = false;

        /**
         * The list of supported attributes in the remote device for this cluster.
         * After initialisation, the list will contain an empty list. Once a successful call to
         * {@link #discoverAttributes()} has been made, the list will reflect the attributes supported by the remote device.
         */
        private readonly List<ushort> _supportedAttributes = new List<ushort>();

        /**
         * The list of supported commands that the remote device can generate
         */
        private readonly List<byte> _supportedCommandsReceived = new List<byte>();

        /**
         * The list of supported commands that the remote device can receive
         */
        private readonly List<byte> _supportedCommandsGenerated = new List<byte>();

        /**
         * Set of listeners to receive notifications when an attribute updates its value
         */
        private readonly List<IZclAttributeListener> _attributeListeners = new List<IZclAttributeListener>();

        /**
         * Set of listeners to receive notifications when a command is received
         */
        private readonly List<IZclCommandListener> _commandListeners = new List<IZclCommandListener>();

        /**
         * Map of attributes supported by the cluster. This contains all attributes, even if they are not supported by the
         * remote device. To check what attributes are supported by the remove device, us the {@link #discoverAttributes()}
         * method followed by the {@link #getSupportedAttributes()} method.
         */
        protected Dictionary<ushort, ZclAttribute> _attributes;

        /**
         * The {@link ZclAttributeNormalizer} is used to normalize attribute data types to ensure that data types are
         * consistent with the ZCL definition. This ensures that the application can rely on consistent and deterministic
         * data type when listening to attribute updates.
         */
        private readonly ZclAttributeNormalizer _normalizer;

        /**
         * If this cluster requires all frames to have APS security applied, then this will be true. Any frames not secured
         * with the link key will be rejected and all frames sent will use APS encryption.
         */
        private bool apsSecurityRequired = false;

        /**
         * Abstract method called when the cluster starts to initialise the list of attributes defined in this cluster by
         * the cluster library
         *
         * @return a {@link Map} of all attributes this cluster is known to support
         */
        protected abstract Dictionary<ushort, ZclAttribute> InitializeAttributes();

        public ZclCluster(ZigBeeEndpoint zigbeeEndpoint, ushort clusterId, string clusterName)
        {
            _attributes = InitializeAttributes();

            this._zigbeeEndpoint = zigbeeEndpoint;
            this._clusterId = clusterId;
            this._clusterName = clusterName;
            this._normalizer = new ZclAttributeNormalizer();
        }

        protected Task<CommandResult> Send(ZclCommand command)
        {
            //command.DestinationAddress = _zigbeeEndpoint.GetEndpointAddress();
            if (IsClient())
            {
                command.CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
            }

            return _zigbeeEndpoint.SendTransaction(command, new ZclTransactionMatcher());
        }

        /**
         * Read an attribute
         *
         * @param attribute the attribute to read
         * @return command future
         */
        public Task<CommandResult> Read(ushort attribute)
        {
            ReadAttributesCommand command = new ReadAttributesCommand();

            command.ClusterId = _clusterId;
            command.Identifiers =new List<ushort>(new[] { attribute });
            command.DestinationAddress = _zigbeeEndpoint.GetEndpointAddress();

            return Send(command);
        }

        /**
         * Read an attribute
         *
         * @param attribute the {@link ZclAttribute} to read
         * @return command future
         */
        public Task<CommandResult> Read(ZclAttribute attribute)
        {
            return Read(attribute.Id);
        }

        /**
         * Write an attribute
         *
         * @param attribute the attribute ID to write
         * @param dataType the {@link ZclDataType} of the object
         * @param value the value to set (as {@link Object})
         * @return command future {@link CommandResult}
         */
        public Task<CommandResult> Write(ushort attribute, ZclDataType dataType, object value)
        {
            //logger.debug("{}: Writing cluster {}, attribute {}, value {}, as dataType {}", zigbeeEndpoint.getIeeeAddress(),
            //        clusterId, attribute, value, dataType);

            WriteAttributesCommand command = new WriteAttributesCommand();

            command.ClusterId = _clusterId;
            WriteAttributeRecord attributeIdentifier = new WriteAttributeRecord();
            attributeIdentifier.AttributeIdentifier = attribute;
            attributeIdentifier.AttributeDataType = dataType;
            attributeIdentifier.AttributeValue = value;

            command.Records = new List<WriteAttributeRecord>(new[] { attributeIdentifier });
            command.DestinationAddress = _zigbeeEndpoint.GetEndpointAddress();

            return Send(command);
        }

        /**
         * Write an attribute
         *
         * @param attribute the {@link ZclAttribute} to write
         * @param value the value to set (as {@link Object})
         * @return command future {@link CommandResult}
         */
        public Task<CommandResult> Write(ZclAttribute attribute, object value)
        {
            return Write(attribute.Id, attribute.ZclDataType, value);
        }

        /**
         * Read an attribute
         *
         * @param attribute the {@link ZclAttribute} to read
         * @return
         */
        protected object ReadSync(ZclAttribute attribute)
        {
            //logger.debug("readSync request: {}", attribute);
            CommandResult result;
            try
            {
                result = Read(attribute).Result;
            }
            catch (TaskCanceledException e) // TODO: Check if this is the right exception to catch here
            {
                //logger.debug("readSync interrupted");
                return null;
            }
            catch (Exception e)
            {
                //logger.debug("readSync exception ", e);
                return null;
            }

            if (!result.IsSuccess())
            {
                return null;
            }

            ReadAttributesResponse response = result.GetResponse<ReadAttributesResponse>();
            if (response.Records != null && response.Records[0].Status == ZclStatus.SUCCESS)
            {
                ReadAttributeStatusRecord attributeRecord = response.Records[0];
                return _normalizer.NormalizeZclData(attribute.ZclDataType, attributeRecord.AttributeValue);
            }

            return null;
        }

        /**
         * Configures the reporting for the specified attribute ID for analog attributes.
         * <p>
         * <b>minInterval</b>:
         * The minimum reporting interval field is 16 bits in length and shall contain the
         * minimum interval, in seconds, between issuing reports of the specified attribute.
         * If minInterval is set to 0x0000, then there is no minimum limit, unless one is
         * imposed by the specification of the cluster using this reporting mechanism or by
         * the applicable profile.
         * <p>
         * <b>maxInterval</b>:
         * The maximum reporting interval field is 16 bits in length and shall contain the
         * maximum interval, in seconds, between issuing reports of the specified attribute.
         * If maxInterval is set to 0xffff, then the device shall not issue reports for the specified
         * attribute, and the configuration information for that attribute need not be
         * maintained.
         * <p>
         * <b>reportableChange</b>:
         * The reportable change field shall contain the minimum change to the attribute that
         * will result in a report being issued. This field is of variable length. For attributes
         * with 'analog' data type the field has the same data type as the attribute. The sign (if any) of the reportable
         * change field is ignored.
         *
         * @param attribute the {@link ZclAttribute} to configure reporting
         * @param minInterval the minimum reporting interval
         * @param maxInterval the maximum reporting interval
         * @param reportableChange the minimum change required to report an update
         * @return command future {@link CommandResult}
         */
        public Task<CommandResult> SetReporting(ZclAttribute attribute, ushort minInterval, ushort maxInterval, object reportableChange)
        {

            ConfigureReportingCommand command = new ConfigureReportingCommand();
            command.ClusterId = _clusterId;

            AttributeReportingConfigurationRecord record = new AttributeReportingConfigurationRecord();
            record.Direction = 0;
            record.AttributeIdentifier = attribute.Id;
            record.AttributeDataType = attribute.ZclDataType;
            record.MinimumReportingInterval = minInterval;
            record.MaximumReportingInterval = maxInterval;
            record.ReportableChange = reportableChange;
            record.TimeoutPeriod = 0;
            command.Records = new List<AttributeReportingConfigurationRecord>(new[] { record });
            command.DestinationAddress = _zigbeeEndpoint.GetEndpointAddress();

            return Send(command);
        }

        /**
         * Configures the reporting for the specified attribute ID for discrete attributes.
         * <p>
         * <b>minInterval</b>:
         * The minimum reporting interval field is 16 bits in length and shall contain the
         * minimum interval, in seconds, between issuing reports of the specified attribute.
         * If minInterval is set to 0x0000, then there is no minimum limit, unless one is
         * imposed by the specification of the cluster using this reporting mechanism or by
         * the applicable profile.
         * <p>
         * <b>maxInterval</b>:
         * The maximum reporting interval field is 16 bits in length and shall contain the
         * maximum interval, in seconds, between issuing reports of the specified attribute.
         * If maxInterval is set to 0xffff, then the device shall not issue reports for the specified
         * attribute, and the configuration information for that attribute need not be
         * maintained.
         *
         * @param attribute the {@link ZclAttribute} to configure reporting
         * @param minInterval the minimum reporting interval
         * @param maxInterval the maximum reporting interval
         * @return command future {@link CommandResult}
         */
        public Task<CommandResult> SetReporting(ZclAttribute attribute, ushort minInterval, ushort maxInterval)
        {
            return SetReporting(attribute, minInterval, maxInterval, null);
        }

        /**
         * Gets the reporting configuration for an attribute
         *
         * @param attribute the {@link ZclAttribute} on which to enable reporting
         * @return command future {@link CommandResult}
         */
        public Task<CommandResult> GetReporting(ZclAttribute attribute)
        {
            ReadReportingConfigurationCommand command = new ReadReportingConfigurationCommand();
            command.ClusterId = _clusterId;
            AttributeRecord record = new AttributeRecord();
            record.AttributeIdentifier = attribute.Id;
            record.Direction = 0;
            command.Records = new List<AttributeRecord>(new[] { record });
            command.DestinationAddress = _zigbeeEndpoint.GetEndpointAddress();

            return Send(command);
        }

        /**
         * Gets all the attributes supported by this cluster This will return all
         * attributes, even if they are not actually supported by the device. The
         * user should check to see if this is implemented.
         *
         * @return {@link Set} containing all {@link ZclAttributes} available in this cluster
         */
        public IEnumerable<ZclAttribute> GetAttributes()
        {
            IEnumerable<ZclAttribute> attr = new List<ZclAttribute>(_attributes.Values);
            return attr;
        }

        /**
         * Gets an attribute from the attribute ID
         *
         * @param id
         *            the attribute ID
         * @return the {@link ZclAttribute}
         */
        public ZclAttribute GetAttribute(ushort id)
        {
            return _attributes[id];
        }

        /**
         * Gets the cluster ID for this cluster
         *
         * @return the cluster ID as {@link Integer}
         */
        public ushort GetClusterId()
        {
            return _clusterId;
        }

        /**
         * Gets the cluster name for this cluster
         *
         * @return the cluster name as {@link String}
         */
        public string GetClusterName()
        {
            return _clusterName;
        }

        /**
         * Returns the ZigBee address of this cluster
         *
         * @return the {@link ZigBeeEndpointAddress} of the cluster
         */
        public ZigBeeEndpointAddress GetZigBeeAddress()
        {
            return _zigbeeEndpoint.GetEndpointAddress();
        }

        /**
         * Sets the server flag for this cluster. This means the cluster is listed
         * in the devices input cluster list
         *
         */
        public void SetServer()
        {
            _isClient = false;
        }

        /**
         * Gets the state of the server flag. If the cluster is a server this will
         * return true
         *
         * @return true if the cluster can act as a server
         */
        public bool IsServer()
        {
            return !_isClient;
        }

        /**
         * Sets the client flag for this cluster. This means the cluster is listed
         * in the devices output cluster list
         *
         */
        public void SetClient()
        {
            _isClient = true;
        }

        /**
         * Gets the state of the client flag. If the cluster is a client this will
         * return true
         *
         * @return true if the cluster can act as a client
         */
        public bool IsClient()
        {
            return _isClient;
        }

        /**
         * Sets APS security requirement on or off for this cluster. If APS security is required, all outgoing frames will
         * be APS secured, and any incoming frames without APS security will be ignored.
         *
         * @param requireApsSecurity true if APS security is required for this cluster
         */
        public void GetApsSecurityRequired(bool requireApsSecurity)
        {
            this.apsSecurityRequired = requireApsSecurity;
        }

        /**
         * If APS security is required, all outgoing frames will
         * be APS secured, and any incoming frames without APS security will be ignored.
         *
         * @return true if APS security is required for this cluster
         */
        public bool GetApsSecurityRequired()
        {
            return apsSecurityRequired;
        }

        /**
         * Adds a binding from the cluster to the destination {@link ZigBeeEndpoint}.
         *
         * @param address the destination {@link IeeeAddress}
         * @param endpointId the destination endpoint ID
         * @return Command future
         */
        public Task<CommandResult> Bind(IeeeAddress address, ushort networkAddress, byte endpointId)
        {
            BindRequest command = new BindRequest();
            command.SrcAddress = _zigbeeEndpoint.GetIeeeAddress();
            command.SrcEndpoint = _zigbeeEndpoint.EndpointId;
            command.BindCluster = _clusterId;
            command.DstAddrMode = 3; // 64 bit addressing
            command.DstAddress = address;
            command.DestinationAddress = new ZigBeeEndpointAddress(networkAddress, endpointId);
            command.DstEndpoint = endpointId;
            return _zigbeeEndpoint.SendTransaction(command, new BindRequest());
        }

        /**
         * Removes a binding from the cluster to the destination {@link ZigBeeEndpoint}.
         *
         * @param address the destination {@link IeeeAddress}
         * @param endpointId the destination endpoint ID
         * @return Command future
         */
        public Task<CommandResult> Unbind(IeeeAddress address, byte endpointId)
        {
            UnbindRequest command = new UnbindRequest();
            command.DestinationAddress = new ZigBeeEndpointAddress(_zigbeeEndpoint.GetEndpointAddress().Address);
            command.SrcAddress = _zigbeeEndpoint.GetIeeeAddress();
            command.SrcEndpoint = _zigbeeEndpoint.EndpointId;
            command.BindCluster = _clusterId;
            command.DstAddrMode = 3; // 64 bit addressing
            command.DstAddress = address;
            command.DstEndpoint = endpointId;
            return _zigbeeEndpoint.SendTransaction(command, new UnbindRequest());
        }

        /**
         * Sends a default response to the client
         *
         * @param transactionId the transaction ID to use in the response
         * @param commandIdentifier the command identifier to which this is a response
         * @param status the {@link ZclStatus} to send in the response
         */
        public void SendDefaultResponse(byte transactionId, byte commandIdentifier, ZclStatus status)
        {
            DefaultResponse defaultResponse = new DefaultResponse();
            defaultResponse.TransactionId = transactionId;
            defaultResponse.CommandIdentifier = commandIdentifier;
            defaultResponse.DestinationAddress = _zigbeeEndpoint.GetEndpointAddress();
            defaultResponse.ClusterId = _clusterId;
            defaultResponse.StatusCode = status;

            _zigbeeEndpoint.SendTransaction(defaultResponse);
        }

        /**
         * Gets the list of attributes supported by this device.
         * After initialisation, the list will contain all known standard attributes, so is not customised to the specific
         * device. Once a successful call to {@link #discoverAttributes()} has been made, the list will reflect the
         * attributes supported by the remote device.
         *
         * @return {@link Set} of {@link Integer} containing the list of supported attributes
         */
        public IEnumerable<ushort> GetSupportedAttributes()
        {
            lock (_supportedAttributes)
            {
                if (_supportedAttributes.Count == 0)
                {
                    return new List<ushort>(_attributes.Keys);
                }

                return _supportedAttributes;
            }
        }

        /**
         * Checks if the cluster supports a specified attribute ID.
         * Note that if {@link #discoverAttributes(boolean)} has not been called, this method will return false.
         *
         * @param attributeId the attribute to check
         * @return true if the attribute is known to be supported, otherwise false
         */
        public bool IsAttributeSupported(ushort attributeId)
        {
            lock (_supportedAttributes)
            {
                return _supportedAttributes.Contains(attributeId);
            }
        }

        /**
         * Discovers the list of attributes supported by the cluster on the remote device.
         * <p>
         * If the discovery has already been completed, and rediscover is false, then the future will complete immediately
         * and the user can use existing results. Normally there should not be a need to set rediscover to true.
         * <p>
         * This method returns a future to a boolean. Upon success the caller should call {@link #getSupportedAttributes()}
         * to get the list of supported attributes.
         *
         * @param rediscover true to perform a discovery even if it was previously completed
         * @return {@link Future} returning a {@link Boolean}
         */
        public Task<bool> DiscoverAttributes(bool rediscover)
        {
            return Task.Run(() => {
                // Synchronise the request to avoid multiple simultaneous requests to this update the list on this
                // cluster which would cause errors consolidating the responses
                lock (_supportedAttributes)
                {
                    // If we don't want to rediscover, and we already have the list of attributes, then return
                    if (!rediscover && !(_supportedAttributes == null || _supportedAttributes.Count == 0))
                    {
                        return true;
                    }

                    ushort index = 0;
                    bool complete = false;
                    List<AttributeInformation> attributes = new List<AttributeInformation>();

                    do
                    {
                        DiscoverAttributesCommand command = new DiscoverAttributesCommand();
                        command.ClusterId = _clusterId;
                        command.DestinationAddress = _zigbeeEndpoint.GetEndpointAddress();
                        command.StartAttributeIdentifier = index;
                        command.MaximumAttributeIdentifiers = 10;

                        CommandResult result = Send(command).Result;
                        if (result.IsError())
                        {
                            return false;
                        }

                        DiscoverAttributesResponse response = (DiscoverAttributesResponse)result.GetResponse();
                        complete = response.DiscoveryComplete;
                        if (response.AttributeInformation != null)
                        {
                            attributes.AddRange(response.AttributeInformation);
                            index = (ushort)(attributes.Max().Identifier + 1);
                        }
                    } while (!complete);

                    _supportedAttributes.Clear();
                    foreach (AttributeInformation attribute in attributes)
                    {
                        _supportedAttributes.Add(attribute.Identifier);
                    }
                }
                return true;
            });
        }

        /**
         * Returns a list of all the commands the remote device can receive. This will be an empty list if the
         * {@link #discoverCommandsReceived()} method has not completed.
         *
         * @return a {@link Set} of command IDs the device supports
         */
        public IEnumerable<byte> GetSupportedCommandsReceived()
        {
            lock (_supportedCommandsReceived)
            {
                return new List<byte>(_supportedCommandsReceived);
            }
        }

        /**
         * Checks if the cluster supports a specified received command ID.
         * Note that if {@link #discoverCommandsReceived(boolean)} has not been called, this method will return false.
         *
         * @param commandId the attribute to check
         * @return true if the command is known to be supported, otherwise false
         */
        public bool IsReceivedCommandSupported(byte commandId)
        {
            lock (_supportedCommandsReceived)
            {
                return _supportedCommandsReceived.Contains(commandId);
            }
        }

        /**
         * Discovers the list of commands received by the cluster on the remote device. If the discovery is successful,
         * users should call {@link ZclCluster#getSupportedCommandsReceived()} to get the list of supported commands.
         * <p>
         * If the discovery has already been completed, and rediscover is false, then the future will complete immediately
         * and the user can use existing results. Normally there should not be a need to set rediscover to true.
         *
         * @param rediscover true to perform a discovery even if it was previously completed
         * @return Command future {@link Boolean} with the success of the discovery
         */
        public Task<bool> DiscoverCommandsReceived(bool rediscover)
        {
            return Task.Run(() => {
                // Synchronise the request to avoid multiple simultaneous requests to this update the list on this
                // cluster which would cause errors consolidating the responses
                lock(_supportedCommandsReceived) {
                    // If we don't want to rediscover, and we already have the list of attributes, then return
                    if (!rediscover && !(_supportedCommandsReceived == null || _supportedCommandsReceived.Count == 0))
                    {
                        return true;
                    }

                    byte index = 0;
                    bool complete = false;
                    List<byte> commands = new List<byte>();

                    do
                    {
                        DiscoverCommandsReceived command = new DiscoverCommandsReceived();
                        command.ClusterId = _clusterId;
                        command.DestinationAddress = _zigbeeEndpoint.GetEndpointAddress();
                        command.StartCommandIdentifier = index;
                        command.MaximumCommandIdentifiers = 20;

                        CommandResult result = Send(command).Result;
                        if (result.IsError())
                        {
                            return false;
                        }

                        DiscoverCommandsReceivedResponse response = (DiscoverCommandsReceivedResponse)result.GetResponse();
                        complete = response.DiscoveryComplete;
                        if (response.CommandIdentifiers != null)
                        {
                            commands.AddRange(response.CommandIdentifiers);
                            index = (byte)(commands.Max() + 1);
                        }
                    } while (!complete);

                    _supportedCommandsReceived.Clear();
                    _supportedCommandsReceived.AddRange(commands);
                }
                return true;
            });
        }

        /**
         * Returns a list of all the commands the remote device can generate. This will be an empty list if the
         * {@link #discoverCommandsGenerated()} method has not completed.
         *
         * @return a {@link Set} of command IDs the device supports
         */
        public IEnumerable<byte> GetSupportedCommandsGenerated()
        {
            lock (_supportedCommandsGenerated)
            {
                return new List<byte>(_supportedCommandsGenerated);
            }
        }

        /**
         * Checks if the cluster supports a specified generated command ID.
         * Note that if {@link #discoverCommandsGenerated(boolean)} has not been called, this method will return false.
         *
         * @param commandId the attribute to check
         * @return true if the command is known to be supported, otherwise false
         */
        public bool IsGeneratedCommandSupported(byte commandId)
        {
            lock (_supportedCommandsGenerated)
            {
                return _supportedCommandsGenerated.Contains(commandId);
            }
        }

        /**
         * Discovers the list of commands generated by the cluster on the remote device If the discovery is successful,
         * users should call {@link ZclCluster#getSupportedCommandsGenerated()} to get the list of supported commands.
         * <p>
         * If the discovery has already been completed, and rediscover is false, then the future will complete immediately
         * and the user can use existing results. Normally there should not be a need to set rediscover to true.
         *
         * @param rediscover true to perform a discovery even if it was previously completed
         * @return Command future {@link Boolean} with the success of the discovery
         */
        public Task<bool> DiscoverCommandsGenerated(bool rediscover)
        {
            return Task.Run(() => {
                // Synchronise the request to avoid multiple simultaneous requests to this update the list on this
                // cluster which would cause errors consolidating the responses
                lock(_supportedCommandsGenerated) {
                    // If we don't want to rediscover, and we already have the list of attributes, then return
                    if (!rediscover && !(_supportedCommandsGenerated == null | _supportedCommandsGenerated.Count == 0))
                    {
                        return true;
                    }
                    byte index = 0;
                    bool complete = false;
                    List<byte> commands = new List<byte>();

                    do
                    {
                        DiscoverCommandsGenerated command = new DiscoverCommandsGenerated();
                        command.ClusterId = _clusterId;
                        command.DestinationAddress = _zigbeeEndpoint.GetEndpointAddress();
                        command.StartCommandIdentifier = index;
                        command.MaximumCommandIdentifiers = 20;

                        CommandResult result = Send(command).Result;
                        if (result.IsError())
                        {
                            return false;
                        }

                        DiscoverCommandsGeneratedResponse response = (DiscoverCommandsGeneratedResponse)result.GetResponse();
                        complete = response.DiscoveryComplete;
                        if (response.CommandIdentifiers != null)
                        {
                            commands.AddRange(response.CommandIdentifiers);
                            index = (byte)(commands.Max() + 1);
                        }
                    } while (!complete);

                    _supportedCommandsGenerated.Clear();
                    _supportedCommandsGenerated.AddRange(commands);
                }

                return true;
            });
        }

        /**
         * Adds a {@link ZclAttributeListener} to receive reports when an attribute is updated
         *
         * @param listener the {@link ZclAttributeListener} to add
         */
        public void AddAttributeListener(IZclAttributeListener listener)
        {
            lock (_attributeListeners)
            {
                // Don't add more than once.
                if (_attributeListeners.Contains(listener))
                {
                    return;
                }
                _attributeListeners.Add(listener);
            }
        }

        /**
         * Remove an attribute listener from the cluster.
         *
         * @param listener callback listener implementing {@link ZclAttributeListener} to remove
         */
        public void RemoveAttributeListener(IZclAttributeListener listener)
        {
            lock (_attributeListeners)
            {
                _attributeListeners.Remove(listener);
            }
        }

        /**
         * Notify attribute listeners of an updated {@link ZclAttribute}.
         *
         * @param attribute the {@link ZclAttribute} to notify
         */
        private void NotifyAttributeListener(ZclAttribute attribute)
        {
            foreach (IZclAttributeListener listener in _attributeListeners)
            {
                //    NotificationService.execute(new Runnable() {
                //            @Override
                //            public void run()
                //    {
                //        listener.attributeUpdated(attribute);
                //    }
                //});

                // TODO: async ?
                listener.AttributeUpdated(attribute);
            }
        }

        /**
         * Adds a {@link ZclCommandListener} to receive commands
         *
         * @param listener the {@link ZclCommandListener} to add
         */
        public void AddCommandListener(IZclCommandListener listener)
        {
            // Don't add more than once.
            if (_commandListeners.Contains(listener))
            {
                return;
            }
            _commandListeners.Add(listener);
        }

        /**
         * Remove a {@link ZclCommandListener} from the cluster.
         *
         * @param listener callback listener implementing {@link ZclCommandListener} to remove
         */
        public void RemoveCommandListener(IZclCommandListener listener)
        {
            _commandListeners.Remove(listener);
        }

        /**
         * Notify command listeners of an received {@link ZclCommand}.
         *
         * @param command the {@link ZclCommand} to notify
         */
        private void NotifyCommandListener(ZclCommand command)
        {
            foreach (IZclCommandListener listener in _commandListeners)
            {
                //    NotificationService.execute(new Runnable() {
                //            @Override
                //            public void run()
                //    {
                //        listener.commandReceived(command);
                //    }
                //});

                // TODO: async ?
                listener.CommandReceived(command);
            }
        }

        /**
         * Processes a list of attribute reports for this cluster
         *
         * @param reports {@List} of {@link AttributeReport}
         */
        public void HandleAttributeReport(List<AttributeReport> reports)
        {
            foreach (AttributeReport report in reports)
            {
                ZclAttribute attribute = _attributes[report.AttributeIdentifier];
                if (attribute == null)
                {
                    //logger.debug("{}: Unknown attribute {} in cluster {}", zigbeeEndpoint.getEndpointAddress(),
                    //        report.getAttributeIdentifier(), clusterId);
                }
                else
                {
                    attribute.UpdateValue(_normalizer.NormalizeZclData(attribute.ZclDataType, report.AttributeValue));
                    NotifyAttributeListener(attribute);
                }
            }
        }

        /**
         * Processes a list of attribute status reports for this cluster
         *
         * @param reports {@List} of {@link ReadAttributeStatusRecord}
         */
        public void HandleAttributeStatus(List<ReadAttributeStatusRecord> records)
        {
            foreach (ReadAttributeStatusRecord record in records)
            {
                if (record.Status != ZclStatus.SUCCESS)
                {
                    //logger.debug("{}: Error reading attribute {} in cluster {} - {}", zigbeeEndpoint.getEndpointAddress(),
                    //        record.getAttributeIdentifier(), clusterId, record.getStatus());
                    continue;
                }

                ZclAttribute attribute = null;
                if (_attributes.TryGetValue(record.AttributeIdentifier, out attribute) == true)
                {
                    if (attribute == null)
                    {
                        //logger.debug("{}: Unknown attribute {} in cluster {}", zigbeeEndpoint.getEndpointAddress(),
                        //        record.getAttributeIdentifier(), clusterId);
                    }
                    else
                    {
                        attribute.UpdateValue(_normalizer.NormalizeZclData(attribute.ZclDataType, record.AttributeValue));
                        NotifyAttributeListener(attribute);
                    }
                }
            }
        }

        /**
         * Processes a command received in this cluster. This is called from the node so we already know that the command is
         * addressed to this endpoint and this cluster.
         *
         * @param command the received {@link ZclCommand}
         */
        public void HandleCommand(ZclCommand command)
        {
            NotifyCommandListener(command);
        }

        /**
         * Gets a command from the command ID (ie a command from client to server). If no command with the requested id is
         * found, null is returned.
         *
         * @param commandId the command ID
         * @return the {@link ZclCommand} or null if no command found.
         */
        public virtual ZclCommand GetCommandFromId(int commandId)
        {
            throw new NotImplementedException();
        }

        /**
         * Gets a response from the command ID (ie a command from server to client). If no command with the requested id is
         * found, null is returned.
         *
         * @param commandId the command ID
         * @return the {@link ZclCommand} or null if no command found.
         */
        public ZclCommand GetResponseFromId(int commandId)
        {
            return null;
        }

        public ZclClusterDao GetDao()
        {
            ZclClusterDao dao = new ZclClusterDao
            {
                ClusterId = _clusterId,
                IsClient = _isClient,
                SupportedAttributes = _supportedAttributes,
                SupportedCommandsGenerated = _supportedCommandsGenerated,
                SupportedCommandsReceived = _supportedCommandsReceived,
                Attributes = _attributes
            };

            return dao;
        }

        public void SetDao(ZclClusterDao dao)
        {
            _clusterId = dao.ClusterId;
            _isClient = dao.IsClient;
            _supportedAttributes.AddRange(dao.SupportedAttributes);
            _supportedCommandsGenerated.AddRange(dao.SupportedCommandsGenerated);
            _supportedCommandsReceived.AddRange(dao.SupportedCommandsReceived);
            _attributes = dao.Attributes;
        }
    }
}
