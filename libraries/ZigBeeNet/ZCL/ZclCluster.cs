using Serilog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.Database;
using ZigBeeNet.Extensions;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Clusters.General;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZDO.Command;

namespace ZigBeeNet.ZCL
{
    public abstract class ZclCluster
    {
        /// <summary>
        /// The <see cref="ZigBeeNetworkManager"> to which this device belongs
        /// </summary>
        //private ZigBeeNetworkManager _zigbeeManager;

        /// <summary>
        /// The <see cref="ZigBeeEndpoint"> to which this cluster belongs
        /// </summary>
        private ZigBeeEndpoint _zigbeeEndpoint;

        /// <summary>
        /// The ZCL cluster ID for this cluster
        /// </summary>
        protected ushort _clusterId;

        /// <summary>
        /// The name of this cluster
        /// </summary>
        protected string _clusterName;

        /// <summary>
        /// Defines if the remote is a client (true) or server (false)
        /// The definition of the direction is based on the remote being the server. If it is really
        /// a server, then we need to reverse direction
        /// </summary>
        private bool _isClient = false;

        /// <summary>
        /// The list of supported attributes in the remote device for this cluster.
        /// After initialisation, the list will contain an empty list. Once a successful call to
        /// DiscoverAttributes() has been made, the list will reflect the attributes supported by the remote device.
        /// </summary>
        private HashSet<ushort> _supportedAttributes = new HashSet<ushort>();

        /// <summary>
        /// The list of supported commands that the remote device can generate
        /// </summary>
        private HashSet<byte> _supportedCommandsReceived = new HashSet<byte>();

        /// <summary>
        /// The list of supported commands that the remote device can receive
        /// </summary>
        private HashSet<byte> _supportedCommandsGenerated = new HashSet<byte>();

        /// <summary>
        /// Set of listeners to receive notifications when an attribute updates its value
        /// </summary>
        private List<IZclAttributeListener> _attributeListeners = new List<IZclAttributeListener>();

        /// <summary>
        /// Set of listeners to receive notifications when a command is received
        /// </summary>
        private readonly List<IZclCommandListener> _commandListeners = new List<IZclCommandListener>();

        /// <summary>
        /// Map of client attributes supported by the cluster. This contains all attributes, even if they are not supported by the
        /// remote device. To check what attributes are supported by the remote device, us the DiscoverAttributes()
        /// method followed by the GetSupportedAttributes() method.
        /// </summary>
        protected Dictionary<ushort, ZclAttribute> _clientAttributes;

        /// <summary>
        /// Map of server attributes supported by the cluster. This contains all attributes, even if they are not supported by the
        /// remote device. To check what attributes are supported by the remote device, us the DiscoverAttributes()
        /// method followed by the GetSupportedAttributes() method.
        /// </summary>
        protected Dictionary<ushort, ZclAttribute> _serverAttributes;


        /// <summary>
        /// Map of server side commands supported by the cluster.This contains all server commands, even if they are not
        /// supported by the remote device.
        /// </summary>
        protected Dictionary<ushort, Func<ZclCommand>> _serverCommands;

        /// <summary>
        /// Map of client side commands supported by the cluster.This contains all client commands, even if they are not
        /// supported by the remote device.
        /// </summary>
        protected Dictionary<ushort, Func<ZclCommand>> _clientCommands;

        /// <summary>
        /// Map of the generic commands as implemented by all clusters
        /// </summary>
        protected static Dictionary<ushort, Func<ZclCommand>> _genericCommands = new Dictionary<ushort, Func<ZclCommand>>();

        /// <summary>
        /// The <see cref="ZclAttributeNormalizer"> is used to normalize attribute data types to ensure that data types are
        /// consistent with the ZCL definition. This ensures that the application can rely on consistent and deterministic
        /// data type when listening to attribute updates.
        /// </summary>
        private readonly ZclAttributeNormalizer _normalizer;

        /// <summary>
        /// If this cluster requires all frames to have APS security applied, then this will be true. Any frames not secured
        /// with the link key will be rejected and all frames sent will use APS encryption.
        /// </summary>
        private bool _apsSecurityRequired = false;

        /**
        * A boolean used to record if the list of supported attributes has been recovered from the remote device. This is
        * used to record the validity of supported attributes
        */
        private bool _supportedAttributesKnown;

        /**
        * Abstract method called when the cluster starts to initialise the list of client attributes defined in this
        * cluster by the cluster library
        *
        * @return a {@link Map} of all attributes this cluster is known to support
        */
        protected abstract Dictionary<ushort, ZclAttribute> InitializeClientAttributes();

        /**
         * Abstract method called when the cluster starts to initialise the list of server attributes defined in this
         * cluster by the cluster library
         *
         * @return a {@link Map} of all attributes this cluster is known to support
         */
        protected abstract Dictionary<ushort, ZclAttribute> InitializeServerAttributes();

        /**
         * Abstract method called when the cluster starts to initialise the list of server side commands defined in this
         * cluster by the cluster library
         *
         * @return a {@link Map} of all server side commands this cluster is known to support
         */
        protected virtual Dictionary<ushort, Func<ZclCommand>> InitializeServerCommands()
        {
            return new Dictionary<ushort, Func<ZclCommand>>();
        }

        /**
         * Abstract method called when the cluster starts to initialise the list of client side commands defined in this
         * cluster by the cluster library
         *
         * @return a {@link Map} of all client side commands this cluster is known to support
         */
        protected virtual Dictionary<ushort, Func<ZclCommand>> InitializeClientCommands()
        {
            return new Dictionary<ushort, Func<ZclCommand>>();
        }

        static ZclCluster()
        {
            _genericCommands.Add(0x0000, () => new ReadAttributesCommand());
            _genericCommands.Add(0x0001, () => new ReadAttributesResponse());
            _genericCommands.Add(0x0002, () => new WriteAttributesCommand());
            _genericCommands.Add(0x0003, () => new WriteAttributesUndividedCommand());
            _genericCommands.Add(0x0004, () => new WriteAttributesResponse());
            _genericCommands.Add(0x0005, () => new WriteAttributesNoResponse());
            _genericCommands.Add(0x0006, () => new ConfigureReportingCommand());
            _genericCommands.Add(0x0007, () => new ConfigureReportingResponse());
            _genericCommands.Add(0x0008, () => new ReadReportingConfigurationCommand());
            _genericCommands.Add(0x0009, () => new ReadReportingConfigurationResponse());
            _genericCommands.Add(0x000A, () => new ReportAttributesCommand());
            _genericCommands.Add(0x000B, () => new DefaultResponse());
            _genericCommands.Add(0x000C, () => new DiscoverAttributesCommand());
            _genericCommands.Add(0x000D, () => new DiscoverAttributesResponse());
            _genericCommands.Add(0x000E, () => new ReadAttributesStructuredCommand());
            _genericCommands.Add(0x000F, () => new WriteAttributesStructuredCommand());
            _genericCommands.Add(0x0010, () => new WriteAttributesStructuredResponse());
            _genericCommands.Add(0x0011, () => new DiscoverCommandsReceived());
            _genericCommands.Add(0x0012, () => new DiscoverCommandsReceivedResponse());
            _genericCommands.Add(0x0013, () => new DiscoverCommandsGenerated());
            _genericCommands.Add(0x0014, () => new DiscoverCommandsGeneratedResponse());
            _genericCommands.Add(0x0015, () => new DiscoverAttributesExtended());
            _genericCommands.Add(0x0016, () => new DiscoverAttributesExtendedResponse());
        }

        public ZclCluster(ZigBeeEndpoint zigbeeEndpoint, ushort clusterId, string clusterName)
        {
            _clientAttributes = InitializeClientAttributes();
            _serverAttributes = InitializeServerAttributes();
            _clientCommands = InitializeClientCommands();
            _serverCommands = InitializeServerCommands();

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

        /// <summary>
        /// Read an attribute given the attribute ID. This method will always send a {@link ReadAttributesCommand} to the
        /// remote device.
        ///
        /// <param name="attribute">the attribute to read</param>
        /// <returns>command Task</returns>
        /// </summary>
        public Task<CommandResult> ReadAttribute(ushort attributeId)
        {
            return ReadAttributes(new List<ushort>(new[] { attributeId }));
        }

        /// <summary>
        /// Read a number of attributes given a list of attribute IDs. Care must be taken not to request too many attributes
        /// so as to exceed the allowable frame length
        ///
        /// <param name="attributeIds">List of attribute identifiers to read</param>
        /// <returns>command Task</returns>
        /// </summary>
        public Task<CommandResult> ReadAttributes(List<ushort> attributeIds)
        {
            ReadAttributesCommand command = new ReadAttributesCommand();

            command.ClusterId = _clusterId;
            command.Identifiers = attributeIds;
            command.DestinationAddress = _zigbeeEndpoint.GetEndpointAddress();

            if (attributeIds.Count > 0 && IsManufacturerSpecific())
            {
                command.ManufacturerCode = GetManufacturerCode();
            }
            else if (AreAttributesManufacturerSpecific(attributeIds))
            {
                command.ManufacturerCode = GetAttribute(attributeIds[0]).ManufacturerCode;
            }

            return Send(command);
        }

        /// <summary>
        /// Read an attribute from the remote cluster
        /// </summary>
        /// <param name="attributeId">attributeId the attribute id to read</param>
        /// <returns>an object containing the value, or null</returns>
        public async Task<object> ReadAttributeValue(ushort attributeId)
        {
            Log.Debug("ReadAttributeValue request attribute {AttributeId}", attributeId);
            CommandResult result;
            try
            {
                result = await ReadAttribute(attributeId);
            }
            catch (Exception e)
            {
                Log.Debug(e, "ReadAttributeValue exception");
                return null;
            }

            if (!result.IsSuccess())
            {
                return null;
            }

            ReadAttributesResponse response = result.GetResponse<ReadAttributesResponse>();
            if (response.Records.Count == 0 || response.Records[0].Status != ZclStatus.SUCCESS)
            {
                return null;
            }

            // If we don't know this attribute, then just return the received data
            if (GetAttribute(attributeId) == null)
            {
                return response.Records[0].AttributeValue;
            }

            return _normalizer.NormalizeZclData(GetAttribute(attributeId).DataType, response.Records[0].AttributeValue);
        }

        /// <summary>
        /// Write an attribute
        ///
        /// <param name="attribute">the attribute ID to write</param>
        /// <param name="dataType">the ZclDataType of the object</param>
        /// <param name="value">the value to set (as Object)</param>
        /// <returns>command Task CommandResult</returns>
        /// </summary>
        public Task<CommandResult> WriteAttribute(ushort attribute, ZclDataType dataType, object value)
        {
            //logger.debug("{}: Writing cluster {}, attribute {}, value {}, as dataType {}", zigbeeEndpoint.getIeeeAddress(),
            //        clusterId, attribute, value, dataType);
            WriteAttributeRecord attributeIdentifier = new WriteAttributeRecord();
            attributeIdentifier.AttributeIdentifier = attribute;
            attributeIdentifier.AttributeDataType = dataType;
            attributeIdentifier.AttributeValue = value;

            return WriteAttributes(new List<WriteAttributeRecord> { attributeIdentifier });
        }

        /// <summary>
        /// Writes a number of attributes in a single command
        ///
        /// <para name="attributes"/>attributes a List of <see cref="WriteAttributeRecord"/>s with the attribute ID, type and value<para>
        /// <returns>command future <see cref="CommandResult"/></returns> 
        /// </summary>
        public Task<CommandResult> WriteAttributes(List<WriteAttributeRecord> attributes)
        {
            WriteAttributesCommand command = new WriteAttributesCommand();
            command.ClusterId = _clusterId;
            command.Records = attributes;
            command.DestinationAddress = _zigbeeEndpoint.GetEndpointAddress();

            ZclAttribute manufacturerSpecificAttribute = null;
            foreach (WriteAttributeRecord attributeRecord in attributes)
            {
                ZclAttribute attribute = GetAttribute(attributeRecord.AttributeIdentifier);
                if (attribute != null)
                {
                    if (attribute.IsManufacturerSpecific())
                    {
                        manufacturerSpecificAttribute = attribute;
                        break;
                    }
                }
            }

            if (IsManufacturerSpecific())
            {
                command.ManufacturerCode = GetManufacturerCode();
            }
            else if (manufacturerSpecificAttribute != null)
            {
                command.ManufacturerCode = manufacturerSpecificAttribute.ManufacturerCode;
            }

            return Send(command);
        }

        /// <summary>
        /// Configures the reporting for the specified attribute ID for analog attributes.
        /// 
        /// minInterval:
        /// The minimum reporting interval field is 16 bits in length and shall contain the
        /// minimum interval, in seconds, between issuing reports of the specified attribute.
        /// If minInterval is set to 0x0000, then there is no minimum limit, unless one is
        /// imposed by the specification of the cluster using this reporting mechanism or by
        /// the applicable profile.
        /// 
        /// maxInterval:
        /// The maximum reporting interval field is 16 bits in length and shall contain the
        /// maximum interval, in seconds, between issuing reports of the specified attribute.
        /// If maxInterval is set to 0xffff, then the device shall not issue reports for the specified
        /// attribute, and the configuration information for that attribute need not be
        /// maintained.
        /// 
        /// reportableChange:
        /// The reportable change field shall contain the minimum change to the attribute that
        /// will result in a report being issued. This field is of variable length. For attributes
        /// with 'analog' data type the field has the same data type as the attribute. The sign (if any) of the reportable
        /// change field is ignored.
        ///
        /// <param name="attribute">the ZclAttribute to configure reporting</param>
        /// <param name="minInterval">the minimum reporting interval</param>
        /// <param name="maxInterval">the maximum reporting interval</param>
        /// <param name="reportableChange">the minimum change required to report an update</param>
        /// <returns>command Task CommandResult</returns>
        /// </summary>
        public Task<CommandResult> SetReporting(ZclAttribute attribute, ushort minInterval, ushort maxInterval, object reportableChange)
        {
            ConfigureReportingCommand command = new ConfigureReportingCommand();
            command.ClusterId = _clusterId;

            AttributeReportingConfigurationRecord record = new AttributeReportingConfigurationRecord();
            record.Direction = 0;
            record.AttributeIdentifier = attribute.Id;
            record.AttributeDataType = attribute.DataType;
            record.MinimumReportingInterval = minInterval;
            record.MaximumReportingInterval = maxInterval;
            record.ReportableChange = reportableChange;
            record.TimeoutPeriod = 0;
            command.Records = new List<AttributeReportingConfigurationRecord>(new[] { record });
            command.DestinationAddress = _zigbeeEndpoint.GetEndpointAddress();

            return Send(command);
        }

        // TODO: comment
        public Task<CommandResult> SetReporting(ushort attributeId, ushort minInterval, ushort maxInterval, object reportableChange)
        {
            return SetReporting(GetAttribute(attributeId), minInterval, maxInterval, reportableChange);
        }

        // TODO: comment
        public Task<CommandResult> SetReporting(ushort attributeId, ushort minInterval, ushort maxInterval)
        {
            return SetReporting(GetAttribute(attributeId), minInterval, maxInterval, null);
        }

        /// <summary>
        /// Configures the reporting for the specified attribute ID for discrete attributes.
        /// 
        /// minInterval:
        /// The minimum reporting interval field is 16 bits in length and shall contain the
        /// minimum interval, in seconds, between issuing reports of the specified attribute.
        /// If minInterval is set to 0x0000, then there is no minimum limit, unless one is
        /// imposed by the specification of the cluster using this reporting mechanism or by
        /// the applicable profile.
        /// 
        /// maxInterval:
        /// The maximum reporting interval field is 16 bits in length and shall contain the
        /// maximum interval, in seconds, between issuing reports of the specified attribute.
        /// If maxInterval is set to 0xffff, then the device shall not issue reports for the specified
        /// attribute, and the configuration information for that attribute need not be
        /// maintained.
        ///
        /// <param name="attribute">the <see cref="ZclAttribute"> to configure reporting</param>
        /// <param name="minInterval">the minimum reporting interval</param>
        /// <param name="maxInterval">the maximum reporting interval</param>
        /// <returns>command Task CommandResult</returns>
        /// </summary>
        public Task<CommandResult> SetReporting(ZclAttribute attribute, ushort minInterval, ushort maxInterval)
        {
            return SetReporting(attribute, minInterval, maxInterval, null);
        }

        /// <summary>
        /// Gets the reporting configuration for an attribute
        /// 
        /// <param name="attributeId"> the attribute on which to get the reporting configuration</param>
        /// <return>command Task <see cref="CommandResult"/></return>
        /// </summary>
        public Task<CommandResult> GetReporting(ushort attributeId)
        {
            ReadReportingConfigurationCommand command = new ReadReportingConfigurationCommand();
            command.ClusterId = _clusterId;
            AttributeRecord record = new AttributeRecord();
            record.AttributeIdentifier = attributeId;
            record.Direction = 0;
            command.Records = new List<AttributeRecord> { record };
            command.DestinationAddress = _zigbeeEndpoint.GetEndpointAddress();

            if (IsManufacturerSpecific())
            {
                command.ManufacturerCode = GetManufacturerCode();
            }
            else
            {
                var attribute = GetAttribute(attributeId);
                if (attribute != null)
                {
                    if (attribute.IsManufacturerSpecific())
                    {
                        command.ManufacturerCode = GetAttribute(attributeId).ManufacturerCode;
                    }
                }
            }

            return Send(command);
        }

        /// <summary>
        /// Gets all the attributes supported by this cluster This will return all
        /// attributes, even if they are not actually supported by the device. The
        /// user should check to see if this is implemented.
        ///
        /// <returns>IEnumerable containing all ZclAttributes available in this cluster</returns>
        /// </summary>
        public IEnumerable<ZclAttribute> GetAttributes()
        {
            if (IsClient())
            {
                return _clientAttributes.Values;
            }
            else
            {
                return _serverAttributes.Values;
            }
        }

        /// <summary>
        /// Gets an attribute from the attribute ID
        ///
        /// <param name="the">attribute ID</param>
        ///            
        /// <returns>the ZclAttribute</returns>
        /// </summary>
        public ZclAttribute GetAttribute(ushort id)
        {
            if (IsClient())
            {
                ZclAttribute clientAttribute = null;

                if (!_clientAttributes.TryGetValue(id, out clientAttribute))
                {
                    Log.Information("Unkown client attribute with id {AttributeId} reported for cluster {Cluster}", id, this._clusterId);
                }
                return clientAttribute;
            }
            else
            {
                ZclAttribute serverAttribute = null;

                // It can be that device reports unknown id
                if (!_serverAttributes.TryGetValue(id, out serverAttribute))
                {
                    Log.Information("Unkown server attribute with id {AttributeId} reported by device for cluster {Cluster}", id, this._clusterId);
                }
                return serverAttribute;
            }
        }

        /// <summary>
        /// Gets the cluster ID for this cluster
        ///
        /// <returns>the cluster ID as Integer</returns>
        /// </summary>
        public ushort GetClusterId()
        {
            return _clusterId;
        }

        /// <summary>
        /// Gets the cluster name for this cluster
        ///
        /// <returns>the cluster name as String</returns>
        /// </summary>
        public string GetClusterName()
        {
            return _clusterName;
        }

        /// <summary>
        /// Returns the ZigBee address of this cluster
        ///
        /// <returns>the ZigBeeEndpointAddress of the cluster</returns>
        /// </summary>
        public ZigBeeEndpointAddress GetZigBeeAddress()
        {
            return _zigbeeEndpoint.GetEndpointAddress();
        }

        /// <summary>
        /// Sets the server flag for this cluster. This means the cluster is listed
        /// in the devices input cluster list
        /// </summary>
        public void SetServer()
        {
            _isClient = false;
        }

        /// <summary>
        /// Gets the state of the server flag. If the cluster is a server this will
        /// return true
        ///
        /// <returns>true if the cluster can act as a server</returns>
        /// </summary>
        public bool IsServer()
        {
            return !_isClient;
        }

        /// <summary>
        /// Sets the client flag for this cluster. This means the cluster is listed
        /// in the devices output cluster list
        ///
        /// </summary>
        public void SetClient()
        {
            _isClient = true;
        }

        /// <summary>
        /// Gets the state of the client flag. If the cluster is a client this will
        /// return true
        ///
        /// <returns>true if the cluster can act as a client</returns>
        /// </summary>
        public bool IsClient()
        {
            return _isClient;
        }

        /// <summary>
        /// Sets APS security requirement on or off for this cluster. If APS security is required, all outgoing frames will
        /// be APS secured, and any incoming frames without APS security will be ignored.
        ///
        /// <param name="requireApsSecurity">true if APS security is required for this cluster</param>
        /// </summary>
        public void GetApsSecurityRequired(bool requireApsSecurity)
        {
            this._apsSecurityRequired = requireApsSecurity;
        }

        /// <summary>
        /// If APS security is required, all outgoing frames will
        /// be APS secured, and any incoming frames without APS security will be ignored.
        ///
        /// <returns>true if APS security is required for this cluster</returns>
        /// </summary>
        public bool GetApsSecurityRequired()
        {
            return _apsSecurityRequired;
        }

        /// <summary>
        /// Adds a binding from the cluster to the destination <see cref="ZigBeeEndpoint">.
        ///
        /// <param name="address">the destination <see cref="IeeeAddress"></param>
        /// <param name="endpointId">the destination endpoint ID</param>
        /// <returns>command Task</returns>
        /// </summary>
        public Task<CommandResult> Bind(IeeeAddress address, byte endpointId)
        {
            BindRequest command = new BindRequest();
            command.DestinationAddress = new ZigBeeEndpointAddress(_zigbeeEndpoint.GetEndpointAddress().Address);
            command.SrcAddress = _zigbeeEndpoint.GetIeeeAddress();
            command.SrcEndpoint = _zigbeeEndpoint.EndpointId;
            command.BindCluster = _clusterId;
            command.DstAddrMode = 3; // 64 bit addressing
            command.DstAddress = address;
            command.DstEndpoint = endpointId;
            // The transaction is not sent to the Endpoint of this cluster, but to the ZDO endpoint 0 directly.
            return _zigbeeEndpoint.Node.SendTransaction(command, command);
        }

        /// <summary>
        /// Removes a binding from the cluster to the destination ZigBeeEndpoint.
        ///
        /// <param name="address">the destination IeeeAddress</param>
        /// <param name="endpointId">the destination endpoint ID</param>
        /// <returns>command Task</returns>
        /// </summary>
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
            // The transaction is not sent to the Endpoint of this cluster, but to the ZDO endpoint 0 directly.
            return _zigbeeEndpoint.Node.SendTransaction(command, command);
        }

        /// <summary>
        /// Sends a default response to the client
        ///
        /// <param name="transactionId">the transaction ID to use in the response</param>
        /// <param name="commandIdentifier">the command identifier to which this is a response</param>
        /// <param name="status">the ZclStatus to send in the response</param>
        /// <param name="manufacturerCode">
        /// manufacturerCode the manufacturer code to set in the response (or null, if the command is not
        /// manufacturer-specific, or if the cluster is itself manufacturer-specific)
        /// </param>
        /// </summary>
        public void SendDefaultResponse(byte transactionId, byte commandIdentifier, ZclStatus status, int? manufacturerCode)
        {
            DefaultResponse defaultResponse = new DefaultResponse
            {
                TransactionId = transactionId,
                CommandIdentifier = commandIdentifier,
                DestinationAddress = _zigbeeEndpoint.GetEndpointAddress(),
                ClusterId = _clusterId,
                StatusCode = status
            };

            if (IsManufacturerSpecific())
            {
                defaultResponse.ManufacturerCode = GetManufacturerCode();
            }
            else if (manufacturerCode != null)
            {
                defaultResponse.ManufacturerCode = manufacturerCode;
            }

            _zigbeeEndpoint.SendTransaction(defaultResponse);
        }

        public void SendDefaultResponse(byte transactionId, byte commandIdentifier, ZclStatus status)
        {
            SendDefaultResponse(transactionId, commandIdentifier, status, null);
        }

        /// <summary>
        /// Gets the list of attributes supported by this device.
        /// After initialisation, the list will contain all known standard attributes, so is not customised to the specific
        /// device. Once a successful call to {@link #discoverAttributes()} has been made, the list will reflect the
        /// attributes supported by the remote device.
        ///
        /// <returns>IEnumerable of ushort containing the list of supported attributes</returns>
        /// </summary>
        public IEnumerable<ushort> GetSupportedAttributes()
        {
            lock (_supportedAttributes)
            {
                if (!_supportedAttributesKnown)
                {
                    if (_isClient)
                    {
                        return _clientAttributes.Keys;
                    }
                    else
                    {
                        return _serverAttributes.Keys;
                    }
                }

                return _supportedAttributes;
            }
        }

        /// <summary>
        /// Checks if the cluster supports a specified attribute ID.
        /// Note that if DiscoverAttributes(bool) has not been called, this method will return false.
        ///
        /// <param name="attributeId">the attribute to check</param>
        /// <returns>true if the attribute is known to be supported, otherwise false</returns>
        /// </summary>
        public bool IsAttributeSupported(ushort attributeId)
        {
            lock (_supportedAttributes)
            {
                return _supportedAttributes.Contains(attributeId);
            }
        }

        /// <summary>
        /// Discovers the list of attributes supported by the cluster on the remote device.
        /// 
        /// If the discovery has already been completed, and rediscover is false, then the future will complete immediately
        /// and the user can use existing results. Normally there should not be a need to set rediscover to true.
        /// 
        /// This method returns a future to a boolean. Upon success the caller should call GetSupportedAttributes()
        /// to get the list of supported attributes.
        ///
        /// <param name="rediscover">true to perform a discovery even if it was previously completed</param>
        /// <returns>Task returning a bool</returns>
        /// </summary>
        public async Task<bool> DiscoverAttributes(bool rediscover, int? manufacturerCode)
        {
            //return Task.Run(() => {
            // Synchronise the request to avoid multiple simultaneous requests to this update the list on this
            // cluster which would cause errors consolidating the responses

            // If we don't want to rediscover, and we already have the list of attributes, then return
            if (!rediscover && _supportedAttributesKnown)
            {
                return true;
            }

            ushort index = 0;
            bool complete = false;
            HashSet<AttributeInformation> attributes = new HashSet<AttributeInformation>();

            do
            {
                DiscoverAttributesCommand command = new DiscoverAttributesCommand
                {
                    ClusterId = _clusterId,
                    DestinationAddress = _zigbeeEndpoint.GetEndpointAddress(),
                    StartAttributeIdentifier = index,
                    MaximumAttributeIdentifiers = 10
                };

                if (IsManufacturerSpecific())
                {
                    command.ManufacturerCode = GetManufacturerCode();
                }
                else if (manufacturerCode != null)
                {
                    command.ManufacturerCode = manufacturerCode;
                }

                CommandResult result = await Send(command);
                if (result.IsError())
                {
                    return false;
                }

                DiscoverAttributesResponse response = (DiscoverAttributesResponse)result.GetResponse();
                complete = response.DiscoveryComplete;
                if (response.AttributeInformation != null && response.AttributeInformation.Count > 0)
                {
                    attributes.UnionWith(response.AttributeInformation);
                    index = (ushort)(attributes.Max().Identifier + 1);
                }
            } while (!complete);

            lock (_supportedAttributes)
            {
                _supportedAttributes.Clear();

                foreach (AttributeInformation attribute in attributes)
                {
                    _supportedAttributes.Add(attribute.Identifier);
                }
                _supportedAttributesKnown = true;
            }
            return true;
        }
        public Task<bool> DiscoverAttributes(bool rediscover)
        {
            return DiscoverAttributes(rediscover, null);
        }

        /// <summary>
        /// Returns a list of all the commands the remote device can receive. This will be an empty list if the
        /// DiscoverCommandsReceived() method has not completed.
        ///
        /// <returns>a IEnumerable of command IDs the device supports</returns>
        /// </summary>
        public IEnumerable<byte> GetSupportedCommandsReceived()
        {
            lock (_supportedCommandsReceived)
            {
                return new List<byte>(_supportedCommandsReceived);
            }
        }

        /// <summary>
        /// Checks if the cluster supports a specified received command ID.
        /// Note that if DiscoverCommandsReceived(boolean) has not been called, this method will return false.
        ///
        /// <param name="commandId">the attribute to check</param>
        /// <returns>true if the command is known to be supported, otherwise false</returns>
        /// </summary>
        public bool IsReceivedCommandSupported(byte commandId)
        {
            lock (_supportedCommandsReceived)
            {
                return _supportedCommandsReceived.Contains(commandId);
            }
        }

        /// <summary>
        /// Discovers the list of commands received by the cluster on the remote device. If the discovery is successful,
        /// users should call ZclCluster GetSupportedCommandsReceived() to get the list of supported commands.
        /// 
        /// If the discovery has already been completed, and rediscover is false, then the future will complete immediately
        /// and the user can use existing results. Normally there should not be a need to set rediscover to true.
        ///
        /// <param name="rediscover">true to perform a discovery even if it was previously completed</param>
        /// <returns>Command future bool with the success of the discovery</returns>
        /// </summary>
        public async Task<bool> DiscoverCommandsReceived(bool rediscover, int? manufacturerCode)
        {

            // Synchronise the request to avoid multiple simultaneous requests to this update the list on this
            // cluster which would cause errors consolidating the responses
            // If we don't want to rediscover, and we already have the list of attributes, then return
            if (!rediscover && _supportedCommandsReceived?.Count > 0)
            {
                return true;
            }

            byte index = 0;
            bool complete = false;
            HashSet<byte> commands = new HashSet<byte>();

            do
            {
                DiscoverCommandsReceived command = new DiscoverCommandsReceived
                {
                    ClusterId = _clusterId,
                    DestinationAddress = _zigbeeEndpoint.GetEndpointAddress(),
                    StartCommandIdentifier = index,
                    MaximumCommandIdentifiers = 20
                };

                if (IsManufacturerSpecific())
                {
                    command.ManufacturerCode = GetManufacturerCode();
                }
                else if (manufacturerCode != null)
                {
                    command.ManufacturerCode = manufacturerCode;
                }

                CommandResult result = await Send(command);
                if (result.IsError())
                {
                    return false;
                }

                DiscoverCommandsReceivedResponse response = (DiscoverCommandsReceivedResponse)result.GetResponse();
                complete = response.DiscoveryComplete;
                if (response.CommandIdentifiers != null)
                {
                    commands.UnionWith(response.CommandIdentifiers);
                    index = (byte)(commands.Max() + 1);
                }
            } while (!complete);

            lock (_supportedCommandsReceived)
            {
                _supportedCommandsReceived.Clear();
                _supportedCommandsReceived.UnionWith(commands);
            }

            return true;
        }
        public Task<bool> DiscoverCommandsReceived(bool rediscover)
        {
            return DiscoverCommandsReceived(rediscover, null);
        }

        /// <summary>
        /// Returns a list of all the commands the remote device can generate. This will be an empty list if the
        /// DiscoverCommandsGenerated() method has not completed.
        ///
        /// <returns>a IEnumerable of command IDs the device supports</returns>
        /// </summary>
        public IEnumerable<byte> GetSupportedCommandsGenerated()
        {
            lock (_supportedCommandsGenerated)
            {
                return new List<byte>(_supportedCommandsGenerated);
            }
        }

        /// <summary>
        /// Checks if the cluster supports a specified generated command ID.
        /// Note that if DiscoverCommandsGenerated(bool) has not been called, this method will return false.
        ///
        /// <param name="commandId">the attribute to check</param>
        /// <returns>true if the command is known to be supported, otherwise false</returns>
        /// </summary>
        public bool IsGeneratedCommandSupported(byte commandId)
        {
            lock (_supportedCommandsGenerated)
            {
                return _supportedCommandsGenerated.Contains(commandId);
            }
        }

        /// <summary>
        /// Discovers the list of commands generated by the cluster on the remote device If the discovery is successful,
        /// users should call ZclCluster GetSupportedCommandsGenerated() to get the list of supported commands.
        /// 
        /// If the discovery has already been completed, and rediscover is false, then the future will complete immediately
        /// and the user can use existing results. Normally there should not be a need to set rediscover to true.
        ///
        /// <param name="rediscover">true to perform a discovery even if it was previously completed</param>
        /// <returns>Command future bool with the success of the discovery</returns>
        /// </summary>
        public async Task<bool> DiscoverCommandsGenerated(bool rediscover, int? manufacturerCode)
        {
            // Synchronise the request to avoid multiple simultaneous requests to this update the list on this
            // cluster which would cause errors consolidating the responses
            // If we don't want to rediscover, and we already have the list of attributes, then return
            if (!rediscover && _supportedCommandsGenerated?.Count > 0)
            {
                return true;
            }
            byte index = 0;
            bool complete = false;
            HashSet<byte> commands = new HashSet<byte>();

            do
            {
                DiscoverCommandsGenerated command = new DiscoverCommandsGenerated
                {
                    ClusterId = _clusterId,
                    DestinationAddress = _zigbeeEndpoint.GetEndpointAddress(),
                    StartCommandIdentifier = index,
                    MaximumCommandIdentifiers = 20
                };

                if (IsManufacturerSpecific())
                {
                    command.ManufacturerCode = GetManufacturerCode();
                }
                else if (manufacturerCode != null)
                {
                    command.ManufacturerCode = manufacturerCode;
                }

                CommandResult result = await Send(command);

                if (result.IsError())
                {
                    return false;
                }

                DiscoverCommandsGeneratedResponse response = (DiscoverCommandsGeneratedResponse)result.GetResponse();
                complete = response.DiscoveryComplete;
                if (response.CommandIdentifiers != null)
                {
                    commands.UnionWith(response.CommandIdentifiers);
                    index = (byte)(commands.Max() + 1);
                }
            } while (!complete);

            lock (_supportedCommandsGenerated)
            {
                _supportedCommandsGenerated.Clear();
                _supportedCommandsGenerated.UnionWith(commands);
            }

            return true;
        }

        public Task<bool> DiscoverCommandsGenerated(bool rediscover)
        {
            return DiscoverCommandsGenerated(rediscover, null);
        }

        /// <summary>
        /// Adds a ZclAttributeListener to receive reports when an attribute is updated
        ///
        /// <param name="listener">the ZclAttributeListener to add</param>
        /// </summary>
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

        /// <summary>
        /// Remove an attribute listener from the cluster.
        ///
        /// <param name="listener">callback listener implementing ZclAttributeListener to remove</param>
        /// </summary>
        public void RemoveAttributeListener(IZclAttributeListener listener)
        {
            lock (_attributeListeners)
            {
                _attributeListeners.Remove(listener);
            }
        }

        /// <summary>
        /// Notify attribute listeners of an updated ZclAttribute.
        ///
        /// <param name="attribute">the ZclAttribute to notify</param>
        /// <param name="value">the current value of the attribute</param> 
        /// </summary>
        private void NotifyAttributeListener(ZclAttribute attribute, object value)
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
                listener.AttributeUpdated(attribute, value);
            }
        }

        /// <summary>
        /// Adds a ZclCommandListener to receive commands
        ///
        /// <param name="listener">the ZclCommandListener to add</param>
        /// </summary>
        public void AddCommandListener(IZclCommandListener listener)
        {
            // Don't add more than once.
            if (_commandListeners.Contains(listener))
            {
                return;
            }
            _commandListeners.Add(listener);
        }

        /// <summary>
        /// Remove a ZclCommandListener from the cluster.
        ///
        /// <param name="listener">callback listener implementing ZclCommandListener to remove</param>
        /// </summary>
        public void RemoveCommandListener(IZclCommandListener listener)
        {
            _commandListeners.Remove(listener);
        }

        /// <summary>
        /// Notify command listeners of an received ZclCommand.
        ///
        /// <param name="command">the ZclCommand to notify</param>
        /// </summary>
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

        /// <summary>
        /// Processes a list of attribute reports for this cluster
        ///
        /// <param name="reports">List of AttributeReport</param>
        /// </summary>
        public void HandleAttributeReport(List<AttributeReport> reports)
        {
            foreach (AttributeReport report in reports)
            {
                UpdateAttribute(report.AttributeIdentifier, report.AttributeValue);
            }
        }

        private void UpdateAttribute(ushort attributeId, object attributeValue)
        {
            ZclAttribute attribute = GetAttribute(attributeId);
            if (attribute == null)
            {
                Log.Debug("{EndPoint}: Unknown {IsClient} attribute in {AttributeId} cluster {ClusterId}", _zigbeeEndpoint.GetEndpointAddress(), (_isClient ? "Client" : "Server"), attributeId, _clusterId);
            }
            else
            {
                object value = _normalizer.NormalizeZclData(attribute.DataType, attributeValue);
                attribute.UpdateValue(value);
                NotifyAttributeListener(attribute, value);
            }
        }

        /// <summary>
        /// Processes a list of attribute status reports for this cluster
        ///
        /// <param name="reports">List of ReadAttributeStatusRecord</param>
        /// </summary>
        public void HandleAttributeStatus(List<ReadAttributeStatusRecord> records)
        {
            foreach (ReadAttributeStatusRecord record in records)
            {
                if (record.Status != ZclStatus.SUCCESS)
                {
                    Log.Debug("{EndpointAddress}: Error reading attribute {AttributeIdentifier} in cluster {clusterId} - {Status}", _zigbeeEndpoint.GetEndpointAddress(), record.AttributeIdentifier, _clusterId, record.Status);
                    continue;
                }

                UpdateAttribute(record.AttributeIdentifier, record.AttributeValue);
            }
        }

        /// <summary>
        /// Processes a command received in this cluster. This is called from the node so we already know that the command is
        /// addressed to this endpoint and this cluster.
        ///
        /// <param name="command">the received ZclCommand</param>
        /// </summary>
        public void HandleCommand(ZclCommand command)
        {
            NotifyCommandListener(command);
        }

        /// <summary>
        /// Gets a command from the command ID (ie a command from client to server). If no command with the requested id is
        /// found, null is returned.
        ///
        /// <param name="zclFrametype">the ZclFrametype of the command</param>
        /// <param name="commandId">the command ID</param>
        /// <returns>the ZclCommand or null if no command found.</returns>
        /// </summary>
        public ZclCommand GetCommandFromId(ZclFrameType zclFrameType, ushort commandId)
        {
            if (zclFrameType == ZclFrameType.CLUSTER_SPECIFIC_COMMAND)
            {
                return GetCommand(commandId, _clientCommands);
            }
            else
            {
                return GetCommand(commandId, _genericCommands);
            }
        }

        /// <summary>
        /// Gets a response from the command ID (ie a command from server to client). If no command with the requested id is
        /// found, null is returned.
        ///
        /// <param name="zclFrametype">the ZclFrametype of the command</param>
        /// <param name="commandId">the command ID</param>
        /// <returns>the ZclCommand or null if no command found.</returns>
        /// </summary>
        public ZclCommand GetResponseFromId(ZclFrameType zclFrameType, ushort commandId)
        {
            if (zclFrameType == ZclFrameType.CLUSTER_SPECIFIC_COMMAND)
            {
                return GetCommand(commandId, _serverCommands);
            }
            else
            {
                return GetCommand(commandId, _genericCommands);
            }
        }

        private ZclCommand GetCommand(ushort commandId, Dictionary<ushort, Func<ZclCommand>> commands)
        {
            if (!commands.ContainsKey(commandId))
            {
                return null;
            }

            try
            {
                return commands[commandId]();
            }
            catch (Exception e)
            {
                Log.Debug(e, "Error instantiating cluster command {ClusterName}, id={CommandId}", _clusterName, commandId);
            }
            return null;
        }

        /// <summary>
        /// Indicates whether this is a manufacturer-specific attribute. Default is not manufacturer-specific.
        /// </summary>
        public bool IsManufacturerSpecific()
        {
            return GetManufacturerCode() != null;
        }

        /// <summary>
        /// Returns the manufacturer code; must be non-null for manufacturer-specific clusters.
        /// </summary>
        public virtual int? GetManufacturerCode()
        {
            // TODO: see https://github.com/zsmartsystems/com.zsmartsystems.zigbee/blob/master/com.zsmartsystems.zigbee/src/main/java/com/zsmartsystems/zigbee/zcl/ZclCluster.java#L1469
            return null;
        }

        private bool AreAttributesManufacturerSpecific(List<ushort> attributeIds)
        {
            return attributeIds.Select(attributeId => GetAttribute(attributeId))
                    .All(attribute => attribute != null && attribute.IsManufacturerSpecific());
        }

        public ZclClusterDao GetDao()
        {
            ZclClusterDao dao = new ZclClusterDao
            {
                ClusterId = _clusterId,
                ClusterName = _clusterName,
                IsClient = _isClient,
            };

            if (_supportedAttributesKnown)
            {
                dao.SupportedAttributes = _supportedAttributes.ToList();
            }

            dao.SupportedCommandsGenerated = _supportedCommandsGenerated.ToList();
            dao.SupportedCommandsReceived = _supportedCommandsReceived.ToList();

            List<ZclAttribute> daoZclAttributes;
            if (_isClient)
            {
                daoZclAttributes = _clientAttributes.Values.ToList();
            }
            else
            {
                daoZclAttributes = _serverAttributes.Values.ToList();
            }

            List<ZclAttributeDao> daoAttributes = new List<ZclAttributeDao>();

            foreach (ZclAttribute attribute in daoZclAttributes)
            {
                daoAttributes.Add(attribute.GetDao());
            }

            dao.Attributes = daoAttributes;

            return dao;
        }

        public void SetDao(ZclClusterDao dao)
        {
            _clusterId = dao.ClusterId;
            _clusterName = dao.ClusterName;
            _isClient = dao.IsClient;
            _supportedAttributesKnown = dao.SupportedAttributes != null;

            if (_supportedAttributesKnown)
            {
                _supportedAttributes.UnionWith(dao.SupportedAttributes);
            }

            _supportedCommandsGenerated.UnionWith(dao.SupportedCommandsGenerated);
            _supportedCommandsReceived.UnionWith(dao.SupportedCommandsReceived);

            Dictionary<ushort, ZclAttribute> daoZclAttributes = new Dictionary<ushort, ZclAttribute>();
            foreach (ZclAttributeDao daoAttribute in dao.Attributes)
            {
                // Normalize the data to protect against the users serialisation system restoring incorrect data classes
                daoAttribute.LastValue = _normalizer.NormalizeZclData(daoAttribute.DataType, daoAttribute.LastValue);
                ZclAttribute attribute = new ZclAttribute();
                attribute.SetDao(this, daoAttribute);
                daoZclAttributes.Add(daoAttribute.Id, attribute);
            }

            if (_isClient)
            {
                _clientAttributes = daoZclAttributes;
            }
            else
            {
                _serverAttributes = daoZclAttributes;
            }
        }
    }
}
