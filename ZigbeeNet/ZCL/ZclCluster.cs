﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigbeeNet.DAO;
using ZigbeeNet.ZCL.Fileld;
using ZigbeeNet.ZCL.Protocol;

namespace ZigbeeNet.ZCL
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
        protected int _clusterId;

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
        private readonly List<int> _supportedAttributes = new List<int>();

        /**
         * The list of supported commands that the remote device can generate
         */
        private readonly List<int> _supportedCommandsReceived = new List<int>();

        /**
         * The list of supported commands that the remote device can receive
         */
        private readonly List<int> _supportedCommandsGenerated = new List<int>();

        /**
         * Set of listeners to receive notifications when an attribute updates its value
         */
        private readonly ConcurrentBag<IZclAttributeListener> _attributeListeners = new ConcurrentBag<IZclAttributeListener>();

        /**
         * Set of listeners to receive notifications when a command is received
         */
        private readonly ConcurrentBag<IZclCommandListener> _commandListeners = new ConcurrentBag<IZclCommandListener>();

        /**
         * Map of attributes supported by the cluster. This contains all attributes, even if they are not supported by the
         * remote device. To check what attributes are supported by the remove device, us the {@link #discoverAttributes()}
         * method followed by the {@link #getSupportedAttributes()} method.
         */
        protected Dictionary<int, ZclAttribute> _attributes;

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
        protected abstract Dictionary<int, ZclAttribute> InitializeAttributes();

        public ZclCluster(ZigBeeNetworkManager zigbeeManager, ZigBeeEndpoint zigbeeEndpoint, int clusterId, string clusterName)
        {
            _attributes = InitializeAttributes();

            this._zigbeeEndpoint = zigbeeEndpoint;
            this._clusterId = clusterId;
            this._clusterName = clusterName;
            this._normalizer = new ZclAttributeNormalizer();
        }

        protected Task<CommandResult> Send(ZclCommand command)
        {
            command.DestinationAddress = _zigbeeEndpoint.GetEndpointAddress();
            if (IsClient())
            {
                command.Direction = ZclCommandDirection.ServerToClient;
            }

            return _zigbeeManager.unicast(command, new ZclTransactionMatcher());
        }

        /**
         * Read an attribute
         *
         * @param attribute the attribute to read
         * @return command future
         */
        public Task<CommandResult> Read(int attribute)
        {
            ReadAttributesCommand command = new ReadAttributesCommand();

            command.setClusterId(_clusterId);
            command.setIdentifiers(Collections.singletonList(attribute));
            command.setDestinationAddress(_zigbeeEndpoint.GetEndpointAddress());

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
        public Task<CommandResult> Write(int attribute, ZclDataType dataType, object value)
        {
            //logger.debug("{}: Writing cluster {}, attribute {}, value {}, as dataType {}", zigbeeEndpoint.getIeeeAddress(),
            //        clusterId, attribute, value, dataType);

            WriteAttributesCommand command = new WriteAttributesCommand();

            command.setClusterId(clusterId);
            WriteAttributeRecord attributeIdentifier = new WriteAttributeRecord();
            attributeIdentifier.setAttributeIdentifier(attribute);
            attributeIdentifier.setAttributeDataType(dataType);
            attributeIdentifier.setAttributeValue(value);
            command.setRecords(Collections.singletonList(attributeIdentifier));
            command.setDestinationAddress(_zigbeeEndpoint.GetEndpointAddress());

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
                result = Read(attribute).get();
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

            if (!result.isSuccess())
            {
                return null;
            }

            ReadAttributesResponse response = result.getResponse();
            if (response.getRecords().get(0).getStatus() == ZclStatus.SUCCESS)
            {
                ReadAttributeStatusRecord attributeRecord = response.getRecords().get(0);
                return _normalizer.normalizeZclData(attribute.ZclDataType, attributeRecord.getAttributeValue());
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
        public Task<CommandResult> setReporting(ZclAttribute attribute, int minInterval, int maxInterval, object reportableChange)
        {

            ConfigureReportingCommand command = new ConfigureReportingCommand();
            command.setClusterId(_clusterId);

            AttributeReportingConfigurationRecord record = new AttributeReportingConfigurationRecord();
            record.setDirection(0);
            record.setAttributeIdentifier(attribute.Id);
            record.setAttributeDataType(attribute.ZclDataType);
            record.setMinimumReportingInterval(minInterval);
            record.setMaximumReportingInterval(maxInterval);
            record.setReportableChange(reportableChange);
            record.setTimeoutPeriod(0);
            command.setRecords(Collections.singletonList(record));
            command.setDestinationAddress(_zigbeeEndpoint.GetEndpointAddress());

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
        public Task<CommandResult> setReporting(ZclAttribute attribute, int minInterval, int maxInterval)
        {
            return setReporting(attribute, minInterval, maxInterval, null);
        }

        /**
         * Gets the reporting configuration for an attribute
         *
         * @param attribute the {@link ZclAttribute} on which to enable reporting
         * @return command future {@link CommandResult}
         */
        public Task<CommandResult> getReporting(ZclAttribute attribute)
        {
            ReadReportingConfigurationCommand command = new ReadReportingConfigurationCommand();
            command.setClusterId(clusterId);
            AttributeRecord record = new AttributeRecord();
            record.setAttributeIdentifier(attribute.Id);
            record.setDirection(0);
            command.setRecords(Collections.singletonList(record));
            command.setDestinationAddress(_zigbeeEndpoint.GetEndpointAddress());

            return Send(command);
        }

        /**
         * Gets all the attributes supported by this cluster This will return all
         * attributes, even if they are not actually supported by the device. The
         * user should check to see if this is implemented.
         *
         * @return {@link Set} containing all {@link ZclAttributes} available in this cluster
         */
        public IEnumerable<ZclAttribute> getAttributes()
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
        public ZclAttribute GetAttribute(int id)
        {
            return _attributes[id];
        }

        /**
         * Gets the cluster ID for this cluster
         *
         * @return the cluster ID as {@link Integer}
         */
        public int GetClusterId()
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
        public Task<CommandResult> Bind(IeeeAddress address, int endpointId)
        {
            BindRequest command = new BindRequest();
            command.setDestinationAddress(new ZigBeeEndpointAddress(_zigbeeEndpoint.GetEndpointAddress().GetAddress()));
            command.setSrcAddress(_zigbeeEndpoint.GetIeeeAddress());
            command.setSrcEndpoint(_zigbeeEndpoint.getEndpointId());
            command.setBindCluster(_clusterId);
            command.setDstAddrMode(3); // 64 bit addressing
            command.setDstAddress(address);
            command.setDstEndpoint(endpointId);
            return _zigbeeEndpoint.sendTransaction(command, new BindRequest());
        }

        /**
         * Removes a binding from the cluster to the destination {@link ZigBeeEndpoint}.
         *
         * @param address the destination {@link IeeeAddress}
         * @param endpointId the destination endpoint ID
         * @return Command future
         */
        public Task<CommandResult> Unbind(IeeeAddress address, int endpointId)
        {
            UnbindRequest command = new UnbindRequest();
            command.setDestinationAddress(new ZigBeeEndpointAddress(_zigbeeEndpoint.GetEndpointAddress().GetAddress()));
            command.setSrcAddress(_zigbeeEndpoint.GetIeeeAddress());
            command.setSrcEndpoint(_zigbeeEndpoint.getEndpointId());
            command.setBindCluster(_clusterId);
            command.setDstAddrMode(3); // 64 bit addressing
            command.setDstAddress(address);
            command.setDstEndpoint(endpointId);
            return _zigbeeEndpoint.sendTransaction(command, new UnbindRequest());
        }

        /**
         * Sends a default response to the client
         *
         * @param transactionId the transaction ID to use in the response
         * @param commandIdentifier the command identifier to which this is a response
         * @param status the {@link ZclStatus} to send in the response
         */
        public void SendDefaultResponse(int transactionId, int commandIdentifier, ZclStatus status)
        {
            DefaultResponse defaultResponse = new DefaultResponse();
            defaultResponse.setTransactionId(transactionId);
            defaultResponse.setCommandIdentifier(commandIdentifier);
            defaultResponse.setDestinationAddress(_zigbeeEndpoint.GetEndpointAddress());
            defaultResponse.setClusterId(_clusterId);
            defaultResponse.setStatusCode(status);

            _zigbeeEndpoint.sendTransaction(defaultResponse);
        }

        /**
         * Gets the list of attributes supported by this device.
         * After initialisation, the list will contain all known standard attributes, so is not customised to the specific
         * device. Once a successful call to {@link #discoverAttributes()} has been made, the list will reflect the
         * attributes supported by the remote device.
         *
         * @return {@link Set} of {@link Integer} containing the list of supported attributes
         */
        public IEnumerable<int> GetSupportedAttributes()
        {
            lock (_supportedAttributes)
            {
                if (_supportedAttributes.Count == 0)
                {
                    return new List<int>(_attributes.Keys);
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
        public bool IsAttributeSupported(int attributeId)
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
        public Task<Boolean> discoverAttributes(bool rediscover)
        {
            //    RunnableFuture<Boolean> future = new FutureTask<Boolean>(new Callable<Boolean>() {
            //    @Override
            //    public Boolean call() throws Exception {
            //        // Synchronise the request to avoid multiple simultaneous requests to this update the list on this
            //        // cluster which would cause errors consolidating the responses
            //        lock(_supportedAttributes) {
            //            // If we don't want to rediscover, and we already have the list of attributes, then return
            //            if (!rediscover && !supportedAttributes.isEmpty())
            //            {
            //                return true;
            //            }

            //            int index = 0;
            //            boolean complete = false;
            //            Set<AttributeInformation> attributes = new HashSet<AttributeInformation>();

            //            do
            //            {
            //                final DiscoverAttributesCommand command = new DiscoverAttributesCommand();
            //                command.setClusterId(clusterId);
            //                command.setDestinationAddress(zigbeeEndpoint.getEndpointAddress());
            //                command.setStartAttributeIdentifier(index);
            //                command.setMaximumAttributeIdentifiers(10);

            //                CommandResult result = send(command).get();
            //                if (result.isError())
            //                {
            //                    return false;
            //                }

            //                DiscoverAttributesResponse response = (DiscoverAttributesResponse)result.getResponse();
            //                complete = response.getDiscoveryComplete();
            //                if (response.getAttributeInformation() != null)
            //                {
            //                    attributes.addAll(response.getAttributeInformation());
            //                    index = Collections.max(attributes).getIdentifier() + 1;
            //                }
            //            } while (!complete);

            //            supportedAttributes.clear();
            //            for (AttributeInformation attribute : attributes)
            //            {
            //                supportedAttributes.add(attribute.getIdentifier());
            //            }
            //        }
            //        return true;
            //    }
            //});

            //// start the thread to execute it
            //new Thread(future).start();
            //return future;
        }

        /**
         * Returns a list of all the commands the remote device can receive. This will be an empty list if the
         * {@link #discoverCommandsReceived()} method has not completed.
         *
         * @return a {@link Set} of command IDs the device supports
         */
        public IEnumerable<int> GetSupportedCommandsReceived()
        {
            lock (_supportedCommandsReceived)
            {
                return new List<int>(_supportedCommandsReceived);
            }
        }

        /**
         * Checks if the cluster supports a specified received command ID.
         * Note that if {@link #discoverCommandsReceived(boolean)} has not been called, this method will return false.
         *
         * @param commandId the attribute to check
         * @return true if the command is known to be supported, otherwise false
         */
        public bool IsReceivedCommandSupported(int commandId)
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
            //    RunnableFuture<Boolean> future = new FutureTask<Boolean>(new Callable<Boolean>() {
            //        @Override
            //        public Boolean call() throws Exception {
            //        // Synchronise the request to avoid multiple simultaneous requests to this update the list on this
            //        // cluster which would cause errors consolidating the responses
            //        synchronized(supportedCommandsReceived) {
            //            // If we don't want to rediscover, and we already have the list of attributes, then return
            //            if (!rediscover && !supportedCommandsReceived.isEmpty())
            //            {
            //                return true;
            //            }

            //            int index = 0;
            //            boolean complete = false;
            //            Set<Integer> commands = new HashSet<Integer>();

            //            do
            //            {
            //                final DiscoverCommandsReceived command = new DiscoverCommandsReceived();
            //                command.setClusterId(clusterId);
            //                command.setDestinationAddress(zigbeeEndpoint.getEndpointAddress());
            //                command.setStartCommandIdentifier(index);
            //                command.setMaximumCommandIdentifiers(20);

            //                CommandResult result = send(command).get();
            //                if (result.isError())
            //                {
            //                    return false;
            //                }

            //                DiscoverCommandsReceivedResponse response = (DiscoverCommandsReceivedResponse)result
            //                        .getResponse();
            //                complete = response.getDiscoveryComplete();
            //                if (response.getCommandIdentifiers() != null)
            //                {
            //                    commands.addAll(response.getCommandIdentifiers());
            //                    index = Collections.max(commands) + 1;
            //                }
            //            } while (!complete);

            //            supportedCommandsReceived.clear();
            //            supportedCommandsReceived.addAll(commands);
            //        }
            //        return true;
            //    }
            //});

            //    // start the thread to execute it
            //    new Thread(future).start();
            //    return future;
        }

        /**
         * Returns a list of all the commands the remote device can generate. This will be an empty list if the
         * {@link #discoverCommandsGenerated()} method has not completed.
         *
         * @return a {@link Set} of command IDs the device supports
         */
        public IEnumerable<int> GetSupportedCommandsGenerated()
        {
            lock (_supportedCommandsGenerated)
            {
                return new List<int>(_supportedCommandsGenerated);
            }
        }

        /**
         * Checks if the cluster supports a specified generated command ID.
         * Note that if {@link #discoverCommandsGenerated(boolean)} has not been called, this method will return false.
         *
         * @param commandId the attribute to check
         * @return true if the command is known to be supported, otherwise false
         */
        public bool IsGeneratedCommandSupported(int commandId)
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
            //    RunnableFuture<Boolean> future = new FutureTask<Boolean>(new Callable<Boolean>() {
            //            @Override
            //            public Boolean call() throws Exception {
            //        // Synchronise the request to avoid multiple simultaneous requests to this update the list on this
            //        // cluster which would cause errors consolidating the responses
            //        synchronized(supportedCommandsGenerated) {
            //            // If we don't want to rediscover, and we already have the list of attributes, then return
            //            if (!rediscover && !supportedCommandsGenerated.isEmpty())
            //            {
            //                return true;
            //            }
            //            int index = 0;
            //            boolean complete = false;
            //            Set<Integer> commands = new HashSet<Integer>();

            //            do
            //            {
            //                final DiscoverCommandsGenerated command = new DiscoverCommandsGenerated();
            //                command.setClusterId(clusterId);
            //                command.setDestinationAddress(zigbeeEndpoint.getEndpointAddress());
            //                command.setStartCommandIdentifier(index);
            //                command.setMaximumCommandIdentifiers(20);

            //                CommandResult result = send(command).get();
            //                if (result.isError())
            //                {
            //                    return false;
            //                }

            //                DiscoverCommandsGeneratedResponse response = (DiscoverCommandsGeneratedResponse)result
            //                        .getResponse();
            //                complete = response.getDiscoveryComplete();
            //                if (response.getCommandIdentifiers() != null)
            //                {
            //                    commands.addAll(response.getCommandIdentifiers());
            //                    index = Collections.max(commands) + 1;
            //                }
            //            } while (!complete);

            //            supportedCommandsGenerated.clear();
            //            supportedCommandsGenerated.addAll(commands);
            //        }

            //        return true;
            //    }
            //});

            //        // start the thread to execute it
            //        new Thread(future).start();
            //        return future;
        }

        /**
         * Adds a {@link ZclAttributeListener} to receive reports when an attribute is updated
         *
         * @param listener the {@link ZclAttributeListener} to add
         */
        public void AddAttributeListener(ZclAttributeListener listener)
        {
            // Don't add more than once.
            if (_attributeListeners.Contains(listener))
            {
                return;
            }
            _attributeListeners.Add(listener);
        }

        /**
         * Remove an attribute listener from the cluster.
         *
         * @param listener callback listener implementing {@link ZclAttributeListener} to remove
         */
        public void removeAttributeListener(ZclAttributeListener listener)
        {
            _attributeListeners.Remove(listener);
        }

        /**
         * Notify attribute listeners of an updated {@link ZclAttribute}.
         *
         * @param attribute the {@link ZclAttribute} to notify
         */
        private void notifyAttributeListener(ZclAttribute attribute)
        {
            foreach (ZclAttributeListener listener in attributeListeners)
            {
                //    NotificationService.execute(new Runnable() {
                //            @Override
                //            public void run()
                //    {
                //        listener.attributeUpdated(attribute);
                //    }
                //});
            }
        }

        /**
         * Adds a {@link ZclCommandListener} to receive commands
         *
         * @param listener the {@link ZclCommandListener} to add
         */
        public void AddCommandListener(ZclCommandListener listener)
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
        public void RemoveCommandListener(ZclCommandListener listener)
        {
            _commandListeners.Remove(listener);
        }

        /**
         * Notify command listeners of an received {@link ZclCommand}.
         *
         * @param command the {@link ZclCommand} to notify
         */
        private void notifyCommandListener(ZclCommand command)
        {
            foreach (ZclCommandListener listener in commandListeners)
            {
                //    NotificationService.execute(new Runnable() {
                //            @Override
                //            public void run()
                //    {
                //        listener.commandReceived(command);
                //    }
                //});
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
                ZclAttribute attribute = _attributes.get(report.getAttributeIdentifier());
                if (attribute == null)
                {
                    //logger.debug("{}: Unknown attribute {} in cluster {}", zigbeeEndpoint.getEndpointAddress(),
                    //        report.getAttributeIdentifier(), clusterId);
                }
                else
                {
                    attribute.UpdateValue(_normalizer.normalizeZclData(attribute.ZclDataType, report.getAttributeValue()));
                    notifyAttributeListener(attribute);
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
                if (record.getStatus() != ZclStatus.SUCCESS)
                {
                    //logger.debug("{}: Error reading attribute {} in cluster {} - {}", zigbeeEndpoint.getEndpointAddress(),
                    //        record.getAttributeIdentifier(), clusterId, record.getStatus());
                    continue;
                }

                ZclAttribute attribute = _attributes.FirstOrDefault(record.getAttributeIdentifier());
                if (attribute == null)
                {
                    //logger.debug("{}: Unknown attribute {} in cluster {}", zigbeeEndpoint.getEndpointAddress(),
                    //        record.getAttributeIdentifier(), clusterId);
                }
                else
                {
                    attribute.UpdateValue(_normalizer.normalizeZclData(attribute.ZclDataType, record.getAttributeValue()));
                    notifyAttributeListener(attribute);
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
            notifyCommandListener(command);
        }

        /**
         * Gets a command from the command ID (ie a command from client to server). If no command with the requested id is
         * found, null is returned.
         *
         * @param commandId the command ID
         * @return the {@link ZclCommand} or null if no command found.
         */
        public ZclCommand GetCommandFromId(int commandId)
        {
            return null;
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

        public ZclClusterDao getDao()
        {
            ZclClusterDao dao = new ZclClusterDao();

            dao.ClusterId = _clusterId;
            dao.IsClient = _isClient;
            dao.SupportedAttributes = _supportedAttributes;
            dao.SupportedCommandsGenerated = _supportedCommandsGenerated;
            dao.SupportedCommandsReceived =_supportedCommandsReceived;
            dao.Attributes = _attributes;

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
