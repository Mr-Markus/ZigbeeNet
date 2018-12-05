﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet;
using ZigBeeNet.App;
using ZigBeeNet.Internal;
using ZigBeeNet.Logging;
using ZigBeeNet.Security;
using ZigBeeNet.Serialization;
using ZigBeeNet.Transport;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZDO;

namespace ZigBeeNet
{
    /**
 * ZigBeeNetworkManager implements functions for managing the ZigBee interfaces. The network manager is the central
 * class of the framework. It provides the interface with the dongles to send and receive data, and application
 * interfaces to provide listeners for system events (eg network status with the {@link IZigBeeNetworkStateListener} or
 * changes to nodes with the {@link IZigBeeNetworkNodeListener} or to receive incoming commands with the
 * {@link IZigBeeCommandListener}).
 * <p>
 * The ZigBeeNetworkManager maintains a list of all {@link ZigBeeNode}s that are known on the network. Depending on the
 * system configuration, different discovery methods may be utilised to maintain this list. A Coordinator may actively
 * look for all nodes on the network while a Router implementation may only need to know about specific nodes that it is
 * communicating with.
 * <p>
 * The ZigBeeNetworkManager also maintains a list of {@link ZigBeeNetworkExtension}s which allow the functionality of
 * the network to be extended. Extensions may provide different levels of functionality - an extension may be as simple
 * as configuring the framework to work with a specific feature, or could provide a detailed application.
 * <p>
 * <h2>Lifecycle</h2>
 * The ZigBeeNetworkManager lifecycle is as follows -:
 * <ul>
 * <li>Instantiate a {@link IZigBeeTransportTransmit} class
 * <li>Instantiate a {@link ZigBeeNetworkManager} class passing the previously created {@link IZigBeeTransportTransmit}
 * class
 * <li>Optionally set the {@link ZigBeeSerializer} and {@link ZigBeeDeserializer} using the {@link #setSerializer}
 * method
 * <li>Call the {@link #initialize} method to perform the initial initialization of the ZigBee network
 * <li>Set the network configuration (see below).
 * <li>Call the {@link #startup} method to start using the configured ZigBee network. Configuration methods may not be
 * used.
 * <li>Call the {@link shutdown} method to close the network
 * </ul>
 * <p>
 * Following a call to {@link #initialize} configuration calls can be made to configure the transport layer. This
 * includes -:
 * <ul>
 * <li>{@link #getZigBeeChannel}
 * <li>{@link #setZigBeeChannel(ZigBeeChannel)}
 * <li>{@link #getZigBeePanId}
 * <li>{@link #setZigBeePanId(int)}
 * <li>{@link #getZigBeeExtendedPanId}
 * <li>{@link #setZigBeeExtendedPanId(ExtendedPanId)}
 * <li>{@link #getZigBeeNetworkKey()}
 * <li>{@link #setZigBeeNetworkKey(ZigBeeKey)}
 * <li>{@link #getZigBeeLinkKey()}
 * <li>{@link #setZigBeeLinkKey(ZigBeeKey)}
 * </ul>
 * <p>
 * Once all transport initialization is complete, {@link #startup} must be called.
 *
 * @author Chris Jackson
 */
    public class ZigBeeNetworkManager : IZigBeeNetwork, IZigBeeTransportReceive
    {
        /**
         * The _logger.
         */
        private readonly ILog _logger = LogProvider.For<ZigBeeNetworkManager>();


        /**
         * The nodes in the ZigBee network - maps {@link IeeeAddress} to {@link ZigBeeNode}
         */
        private ConcurrentDictionary<IeeeAddress, ZigBeeNode> networkNodes = new ConcurrentDictionary<IeeeAddress, ZigBeeNode>();

        /**
         * The groups in the ZigBee network.
         */
        private Dictionary<ushort, ZigBeeGroupAddress> networkGroups = new Dictionary<ushort, ZigBeeGroupAddress>();

        /**
         * The node listeners of the ZigBee network. Registered listeners will be
         * notified of additions, deletions and changes to {@link ZigBeeNode}s.
         */
        private ReadOnlyCollection<IZigBeeNetworkNodeListener> _nodeListeners;

        /**
         * The announce listeners are notified whenever a new device is discovered.
         * This can be called from the transport layer, or internally by methods watching
         * the network state.
         */
        private ReadOnlyCollection<IZigBeeAnnounceListener> announceListeners;

        /**
         * {@link AtomicInteger} used to generate transaction sequence numbers
         */
        private static volatile int _sequenceNumber;

        /**
         * {@link AtomicInteger} used to generate APS header counters
         */
        private static volatile int _apsCounter;

        /**
         * The network state serializer
         */
        public IZigBeeNetworkStateSerializer NetworkStateSerializer { get; set; }

        /**
         * Executor service to execute update threads for discovery or mesh updates etc.
         * We use a {@link Executors.newScheduledThreadPool} to provide a fixed number of threads as otherwise this could
         * result in a large number of simultaneous threads in large networks.
         */
        private ScheduledExecutorService executorService = Executors.newScheduledThreadPool(6);

        /**
         * The {@link IZigBeeTransportTransmit} implementation. This provides the interface
         * for sending data to the network which is an implementation of a ZigBee
         * interface (eg a Dongle).
         */
        public IZigBeeTransportTransmit Transport { get; set; }

        /**
         * The {@link ZigBeeCommandNotifier}. This is used for sending notifications asynchronously to listeners.
         */
        private ZigBeeCommandNotifier commandNotifier = new ZigBeeCommandNotifier();

        /**
         * The listeners of the ZigBee network state.
         */
        private ReadOnlyCollection<IZigBeeNetworkStateListener> _stateListeners;

        /**
         * A Set used to remember if node discovery has been completed. This is used to manage the lifecycle notifications.
         */
        private List<IeeeAddress> nodeDiscoveryComplete = new List<IeeeAddress>();

        /**
         * The serializer class used to serialize commands to data packets
         */
        public IZigBeeSerializer Serializer { get; set; }

        /**
         * The deserializer class used to deserialize commands from data packets
         */
        public IZigBeeDeserializer Deserializer { get; set; }

        /**
         * List of {@link ZigBeeNetworkExtension}s that are available to this network. Extensions are added
         * with the {@link #addApplication(ZigBeeNetworkExtension extension)} method.
         */
        private List<IZigBeeNetworkExtension> extensions = new List<IZigBeeNetworkExtension>();

        /**
         * A ClusterMatcher used to respond to the {@link MatchDescriptorRequest} command.
         */
        private ClusterMatcher clusterMatcher = null;

        /**
         * The current {@link ZigBeeTransportState}
         */
        public ZigBeeTransportState NetworkState { get; set; }

        /**
         * Map of allowable state transitions
         */
        private Dictionary<ZigBeeTransportState, List<ZigBeeTransportState>> validStateTransitions;

        /**
         * Our local {@link IeeeAddress}
         */
        public IeeeAddress LocalIeeeAddress { get; set; }

        /**
         * Our local network address
         */
        private ushort LocalNwkAddress = 0;

        public enum ZigBeeInitializeResponse
        {
            /**
             * Device is initialized successfully and is currently joined to a network
             */
            JOINED,
            /**
             * Device initialization failed
             */
            FAILED,
            /**
             * Device is initialized successfully and is currently not joined to a network
             */
            NOT_JOINED
        }

        /**
         * Constructor which configures serial port and ZigBee network.
         *
         * @param transport the dongle
         * @param resetNetwork whether network is to be reset
         */
        public ZigBeeNetworkManager(IZigBeeTransportTransmit transport)
        {
            Dictionary<ZigBeeTransportState, List<ZigBeeTransportState>> transitions = new Dictionary<ZigBeeTransportState, List<ZigBeeTransportState>>();

            //transitions.put(null, new HashSet<>(Arrays.asList(ZigBeeTransportState.UNINITIALISED)));
            transitions[ZigBeeTransportState.UNINITIALISED] = new List<ZigBeeTransportState>(new[] { ZigBeeTransportState.INITIALISING, ZigBeeTransportState.OFFLINE });
            transitions[ZigBeeTransportState.INITIALISING] = new List<ZigBeeTransportState>(new[] { ZigBeeTransportState.ONLINE, ZigBeeTransportState.OFFLINE });
            transitions[ZigBeeTransportState.ONLINE] = new List<ZigBeeTransportState>(new[] { ZigBeeTransportState.OFFLINE });
            transitions[ZigBeeTransportState.OFFLINE] = new List<ZigBeeTransportState>(new[] { ZigBeeTransportState.ONLINE });

            validStateTransitions = transitions;

            Transport = transport;

            transport.SetZigBeeTransportReceive(this);
        }

        /**
         * Initializes ZigBee manager components and initializes the transport layer.
         * <p>
         * If a network state was previously serialized, it will be deserialized here if the serializer is set with the
         * {@link #setNetworkStateSerializer} method.
         * <p>
         * Following a call to {@link #initialize} configuration calls can be made to configure the transport layer. This
         * includes -:
         * <ul>
         * <li>{@link #getZigBeeChannel}
         * <li>{@link #setZigBeeChannel}
         * <li>{@link #getZigBeePanId}
         * <li>{@link #setZigBeePanId}
         * <li>{@link #getZigBeeExtendedPanId}
         * <li>{@link #setZigBeeExtendedPanId}
         * </ul>
         * <p>
         * Once all transport initialization is complete, {@link #startup} must be called.
         *
         * @return {@link ZigBeeStatus}
         */
        public ZigBeeStatus Initialize()
        {
            SetNetworkState(ZigBeeTransportState.UNINITIALISED);

            lock(typeof(ZigBeeNetworkManager)) {
                if (NetworkStateSerializer != null)
                {
                    NetworkStateSerializer.Deserialize(this);
                }
            }

            ZigBeeStatus transportResponse = Transport.Initialize();
            if (transportResponse != ZigBeeStatus.SUCCESS)
            {
                SetNetworkState(ZigBeeTransportState.OFFLINE);
                return transportResponse;
            }
            SetNetworkState(ZigBeeTransportState.INITIALISING);

            AddLocalNode();

            return ZigBeeStatus.SUCCESS;
        }

        private void AddLocalNode()
        {
            ushort nwkAddress = Transport.NwkAddress;
            IeeeAddress ieeeAddress = Transport.IeeeAddress;
            if (nwkAddress != null && ieeeAddress != null)
            {
                ZigBeeNode node = GetNode(ieeeAddress);
                if (node == null)
                {
                    _logger.Debug("{}: Adding local node to network, NWK={}", ieeeAddress, nwkAddress);
                    node = new ZigBeeNode(this, ieeeAddress);
                    node.NetworkAddress = nwkAddress;

                    AddNode(node);
                }
            }
        }

        public ZigBeeChannel ZigbeeChannel
        {
            get
            {
                return Transport.ZigBeeChannel;
            }
        }

        /**
         * Sets the ZigBee RF channel. The allowable channel range is 11 to 26 for 2.4GHz, however the transport
         * implementation may allow any value it supports.
         * <p>
         * Note that this method may only be called following the {@link #initialize} call, and before the {@link #startup}
         * call.
         *
         * @param channel {@link int} defining the channel to use
         * @return {@link ZigBeeStatus} with the status of function
         */
        public ZigBeeStatus SetZigBeeChannel(ZigBeeChannel channel)
        {
            return Transport.SetZigBeeChannel(channel);
        }

        /**
         * Gets the ZigBee PAN ID currently in use by the transport
         *
         * @return the PAN ID
         */
        public ushort ZigBeePanId
        {
            get
            {
                return Transport.PanID.Value;
            }
        }
        
        /**
         * Sets the ZigBee PAN ID to the specified value. The range of the PAN ID is 0 to 0x3FFF.
         * Additionally a value of 0xFFFF is allowed to indicate the user doesn't care and a random value
         * can be set by the transport.
         * <p>
         * Note that this method may only be called following the {@link #initialize} call, and before the {@link #startup}
         * call.
         *
         * @param panId the new PAN ID
         * @return {@link ZigBeeStatus} with the status of function
         */
        public ZigBeeStatus SetZigBeePanId(ushort panId)
        {
            if (panId < 0 || panId > 0xfffe)
            {
                return ZigBeeStatus.INVALID_ARGUMENTS;
            }
            return Transport.SetZigBeePanId(panId);
        }

        /**
         * Gets the ZigBee Extended PAN ID currently in use by the transport
         *
         * @return the PAN ID
         */
        public ExtendedPanId ZigBeeExtendedPanId
        {
            get
            {
                return Transport.ExtendedPanId;
            }
        }

        /**
         * Sets the ZigBee Extended PAN ID to the specified value
         * <p>
         * Note that this method may only be called following the {@link #initialize} call, and before the {@link #startup}
         * call.
         *
         * @param panId the new {@link ExtendedPanId}
         * @return {@link ZigBeeStatus} with the status of function
         */
        public ZigBeeStatus SetZigBeeExtendedPanId(ExtendedPanId panId)
        {
            return Transport.ExtendedPanId = panId;
        }

        /**
         * Set the current network key in use by the system.
         * <p>
         * Note that this method may only be called following the {@link #initialize} call, and before the {@link #startup}
         * call.
         *
         * @param key the new network key as {@link ZigBeeKey}
         * @return {@link ZigBeeStatus} with the status of function
         */
        public ZigBeeStatus SetZigBeeNetworkKey(ZigBeeKey key)
        {
            return Transport.SetZigBeeNetworkKey(key);
        }

        /**
         * Gets the current network key used by the system
         *
         * @return the current network {@link ZigBeeKey}
         */
        public ZigBeeKey ZigBeeNetworkKey
        {
            get
            {
                return Transport.ZigBeeNetworkKey;
            }
        }

        /**
         * Set the current link key in use by the system.
         * <p>
         * Note that this method may only be called following the {@link #initialize} call, and before the {@link #startup}
         * call.
         *
         * @param key the new link key as {@link ZigBeeKey}
         * @return {@link ZigBeeStatus} with the status of function
         */
        public ZigBeeStatus SetZigBeeLinkKey(ZigBeeKey key)
        {
            return Transport.SetTcLinkKey(key);
        }

        /**
         * Gets the current Trust Centre link key used by the system
         *
         * @return the current trust centre link {@link ZigBeeKey}
         */
        public ZigBeeKey ZigBeeLinkKey
        {
            get
            {
                return Transport.TcLinkKey;
            }
        }

        /**
         * Adds an installation key for the specified address. The {@link ZigBeeKey} should have an address associated with
         * it.
         *
         * @param key the install key as {@link ZigBeeKey} to be used. The key must contain a partner address.
         * @return {@link ZigBeeStatus} with the status of function
         */
        public ZigBeeStatus SetZigBeeInstallKey(ZigBeeKey key)
        {
            if (!key.HasAddress())
            {
                return ZigBeeStatus.INVALID_ARGUMENTS;
            }
            TransportConfig config = new TransportConfig(TransportConfigOption.INSTALL_KEY, key);
            Transport.UpdateTransportConfig(config);

            return config.GetResult(TransportConfigOption.INSTALL_KEY);
        }

        /**
         * Starts up ZigBee manager components.
         * <p>
         *
         * @param reinitialize true if the provider is to reinitialise the network with the parameters configured since the
         *            {@link #initialize} method was called.
         * @return {@link ZigBeeStatus} with the status of function
         */
        public ZigBeeStatus Startup(bool reinitialize)
        {
            ZigBeeStatus status = Transport.Startup(reinitialize);
            if (status != ZigBeeStatus.SUCCESS)
            {
                SetNetworkState(ZigBeeTransportState.OFFLINE);
                return status;
            }
            SetNetworkState(ZigBeeTransportState.ONLINE);
            return ZigBeeStatus.SUCCESS;
        }

        /**
         * Shuts down ZigBee manager components.
         */
        public void Shutdown()
        {
            executorService.shutdownNow();

            lock(typeof(ZigBeeNetworkManager)) {
                foreach (ZigBeeNode node in networkNodes.Values)
                {
                    node.Shutdown();
                }

                if (NetworkStateSerializer != null)
                {
                    NetworkStateSerializer.Serialize(this);
                }

                foreach (IZigBeeNetworkExtension extension in extensions)
                {
                    extension.ExtensionShutdown();
                }
            }

            Transport.Shutdown();
        }

        /**
         * Schedules a runnable task for execution. This uses a fixed size scheduler to limit thread execution.
         *
         * @param runnableTask the {@link Runnable} to execute
         * @param delay the delay in milliseconds before the task will be executed
         * @return the {@link ScheduledFuture} for the scheduled task
         */
        public Task ScheduleTask(Task runnableTask, long delay)
        {
            if (NetworkState != ZigBeeTransportState.ONLINE)
            {
                return null;
            }

            return executorService.schedule(runnableTask, delay, TimeUnit.MILLISECONDS);
        }

        /**
         * Stops the current execution of a task and schedules a runnable task for execution again.
         * This uses a fixed size scheduler to limit thread execution.
         *
         * @param futureTask the {@link ScheduledFuture} for the current scheduled task
         * @param runnableTask the {@link Runnable} to execute
         * @param delay the delay in milliseconds before the task will be executed
         * @return the {@link ScheduledFuture} for the scheduled task
         */
        public Task RescheduleTask(Task runnableTask, long delay)
        {
            if (NetworkState != ZigBeeTransportState.ONLINE)
            {
                return null;
            }

            return executorService.schedule(runnableTask, delay, TimeUnit.MILLISECONDS);
        }

        /**
         * Schedules a runnable task for periodic execution. This uses a fixed size scheduler to limit thread execution
         * resources.
         *
         * @param runnableTask the {@link Runnable} to execute
         * @param initialDelay the delay in milliseconds before the task will be executed
         * @param period the period in milliseconds between each subsequent execution
         * @return the {@link ScheduledFuture} for the scheduled task
         */
        public ScheduledFuture<?> scheduleTask(Runnable runnableTask, long initialDelay, long period)
        {
            return executorService.scheduleAtFixedRate(runnableTask, initialDelay, period, TimeUnit.MILLISECONDS);
        }

        /**
         * Get the transport layer version string
         *
         * @return {@link String} containing the transport layer version
         */
        public string TransportVersionString
        {
            get
            {
                return Transport.VersionString;
            }
        }

        public int SendCommand(ZigBeeCommand command)
        {
            // Create the application frame
            ZigBeeApsFrame apsFrame = new ZigBeeApsFrame();

            if (command.TransactionId == null)
            {
                command.TransactionId = (byte)(((byte)Interlocked.Increment(ref _sequenceNumber)) & 0xff);
            }

            // Set the source address - should probably be improved!
            // Note that the endpoint is set (currently!) in the transport layer
            // TODO: Use only a single endpoint for HA and fix this here
            command.SourceAddress = new ZigBeeEndpointAddress(LocalNwkAddress);

            _logger.Debug("TX CMD: {}", command);

            apsFrame.Cluster = command.ClusterId;
            apsFrame.ApsCounter = (byte)(((byte)Interlocked.Increment(ref _apsCounter)) & 0xff);
            apsFrame.SecurityEnabled = command.ApsSecurity;

            // TODO: Set the source address correctly?
            apsFrame.SourceAddress = LocalNwkAddress;

            apsFrame.Radius = 31;

            if (command.DestinationAddress is ZigBeeEndpointAddress dstAddr) {
                apsFrame.AddressMode = ZigBeeNwkAddressMode.Device;
                apsFrame.DestinationAddress = dstAddr.Address;
                apsFrame.DestinationEndpoint = dstAddr.Endpoint;

                ZigBeeNode node = GetNode(command.DestinationAddress.Address);
                if (node != null)
                {
                    apsFrame.DestinationIeeeAddress = node.IeeeAddress;
                }
            } else {
                apsFrame.AddressMode = ZigBeeNwkAddressMode.Group;
                // TODO: Handle multicast
            }

            ZclFieldSerializer fieldSerializer;
            try
            {
                fieldSerializer = new ZclFieldSerializer(Serializer);
            }
            catch (Exception e)
            {
                _logger.Debug("Error serializing ZigBee frame {}", e);
                return 0;
            }

            if (command is ZdoCommand)
            {
                // Source endpoint is (currently) set by the dongle since it registers the clusters into an endpoint
                // apsHeader.setSourceEndpoint(sourceEndpoint);

                apsFrame.Profile = 0;
                apsFrame.SourceEndpoint = 0;
                apsFrame.DestinationEndpoint = 0;
                command.Serialize(fieldSerializer);

                // Serialise the ZCL header and add the payload
                apsFrame.Payload = fieldSerializer.Payload;
            }

            if (command is ZclCommand)
            {
                // For ZCL commands we pass the NWK and APS headers as classes to the transport layer.
                // The ZCL packet is serialised here.
                ZclCommand zclCommand = (ZclCommand)command;

                apsFrame.SourceEndpoint = 1;

                // TODO set the profile properly
                apsFrame.Profile = 0x104;

                // Create the cluster library header
                ZclHeader zclHeader = new ZclHeader();
                zclHeader.FrameType = zclCommand.IsGenericCommand ? ZclFrameType.ENTIRE_PROFILE_COMMAND : ZclFrameType.CLUSTER_SPECIFIC_COMMAND;
                zclHeader.CommandId = zclCommand.CommandId;
                zclHeader.SequenceNumber = command.TransactionId;
                zclHeader.Direction = zclCommand.Direction;

                command.Serialize(fieldSerializer);

                // Serialise the ZCL header and add the payload
                apsFrame.Payload = zclHeader.Serialize(fieldSerializer, fieldSerializer.Payload);

                _logger.Debug("TX ZCL: {}", zclHeader);
            }
            _logger.Debug("TX APS: {}", apsFrame);

            Transport.SendCommand(apsFrame);

            return command.TransactionId;
        }


        public void AddCommandListener(IZigBeeCommandListener commandListener)
        {
            commandNotifier.AddCommandListener(commandListener);
        }


        public void RemoveCommandListener(IZigBeeCommandListener commandListener)
        {
            commandNotifier.RemoveCommandListener(commandListener);
        }


        public void ReceiveCommand(ZigBeeApsFrame apsFrame)
        {
            _logger.Debug("RX APS: {}", apsFrame);

            // Create the deserialiser
            try
            {
                //    constructor = deserializerClass.getConstructor(int[].class);
                //deserializer = constructor.newInstance(new Object[] { apsFrame.getPayload()});
            }
            catch (Exception e)
            {
                _logger.Debug("Error creating deserializer", e);
                return;
            }
            ZclFieldDeserializer fieldDeserializer = new ZclFieldDeserializer(Deserializer);

            ZigBeeCommand command = null;
            switch (apsFrame.Profile)
            { // TODO: Use ZigBeeProfileType
                case 0x0000:
                    command = ReceiveZdoCommand(fieldDeserializer, apsFrame);
                    break;
                case 0x0104:
                case 0xC05E:
                    command = ReceiveZclCommand(fieldDeserializer, apsFrame);
                    break;
                default:
                    _logger.Debug("Received message with unknown profile {}", string.Format("%04X", apsFrame.Profile));
                    break;
            }

            if (command == null)
            {
                _logger.Debug("Incoming message did not translate to command.");
                return;
            }

            // Create an address from the sourceAddress and endpoint
            command.SourceAddress = new ZigBeeEndpointAddress(apsFrame.SourceAddress, apsFrame.SourceEndpoint);
            command.DestinationAddress = new ZigBeeEndpointAddress(apsFrame.DestinationAddress, apsFrame.DestinationEndpoint);
            command.ApsSecurity = apsFrame.SecurityEnabled;

            _logger.Debug("RX CMD: {}", command);

            // Notify the listeners
            commandNotifier.NotifyCommandListeners(command);
        }

        private ZigBeeCommand ReceiveZdoCommand(ZclFieldDeserializer fieldDeserializer, ZigBeeApsFrame apsFrame)
        {
            ZdoCommandType commandType = ZdoCommandType.getValueById(apsFrame.Cluster);
            if (commandType == null)
            {
                return null;
            }

            ZigBeeCommand command;
            try
            {
                Class <? extends ZdoCommand > commandClass = commandType.getCommandClass();
                Constructor <? extends ZdoCommand > constructor;
                constructor = commandClass.getConstructor();
                command = constructor.newInstance();
            }
            catch (Exception e)
            {
                _logger.Debug("Error instantiating ZDO command", e);
                return null;
            }

            command.Deserialize(fieldDeserializer);

            return command;
        }

        private ZigBeeCommand ReceiveZclCommand(ZclFieldDeserializer fieldDeserializer, ZigBeeApsFrame apsFrame)
        {
            // Process the ZCL header
            ZclHeader zclHeader = new ZclHeader(fieldDeserializer);
            _logger.Debug("RX ZCL: {}", zclHeader);

            // Get the command type
            ZclCommandType commandType = null;
            if (zclHeader.FrameType == ZclFrameType.ENTIRE_PROFILE_COMMAND)
            {
                commandType = ZclCommandType.getGeneric(zclHeader.CommandId);
            }
            else
            {
                commandType = ZclCommandType.getCommandType(apsFrame.Cluster, zclHeader.CommandId,
                        zclHeader.Direction);
            }

            if (commandType == null)
            {
                _logger.Debug("No command type found for {}, cluster={}, command={}, direction={}", zclHeader.FrameType,
                        apsFrame.Cluster, zclHeader.CommandId, zclHeader.Direction);
                return null;
            }

            ZclCommand command = commandType.instantiateCommand();
            if (command == null)
            {
                _logger.Debug("No command found for {}, cluster={}, command={}", zclHeader.FrameType,
                        apsFrame.Cluster, zclHeader.CommandId);
                return null;
            }

            command.Direction = zclHeader.Direction;
            command.Deserialize(fieldDeserializer);
            command.ClusterId = apsFrame.Cluster;
            command.TransactionId = zclHeader.SequenceNumber;

            return command;
        }

        /**
         * Add a {@link IZigBeeAnnounceListener} that will be notified whenever a new device is detected
         * on the network.
         *
         * @param statusListener the new {@link IZigBeeAnnounceListener} to add
         */
        public void AddAnnounceListener(IZigBeeAnnounceListener statusListener)
        {
            List<IZigBeeAnnounceListener> modifiedStateListeners = new List<IZigBeeAnnounceListener>(announceListeners);
            modifiedStateListeners.Add(statusListener);
            announceListeners = new ReadOnlyCollection<IZigBeeAnnounceListener>(modifiedStateListeners);
        }

        /**
         * Remove a {@link IZigBeeAnnounceListener}
         *
         * @param statusListener the new {@link IZigBeeAnnounceListener} to remove
         */
        public void RemoveAnnounceListener(IZigBeeAnnounceListener statusListener)
        {
            List<IZigBeeAnnounceListener> modifiedStateListeners = new List<IZigBeeAnnounceListener>(
                    announceListeners);
            modifiedStateListeners.Remove(statusListener);
            announceListeners = new ReadOnlyCollection<IZigBeeAnnounceListener>(modifiedStateListeners);
        }


        public void NodeStatusUpdate(ZigBeeNodeStatus deviceStatus, ushort networkAddress, ulong ieeeAddress)
        {
            _logger.Debug("{}: nodeStatusUpdate - node status is {}, network address is {}.", ieeeAddress, deviceStatus,
                    networkAddress);

            // This method should only be called when the transport layer has authoritative information about
            // a devices status. Therefore, we should update the network manager view of a device as appropriate.
            switch (deviceStatus)
            {
                // Device has gone - lets remove it
                case ZigBeeNodeStatus.DEVICE_LEFT:
                    // Find the node
                    ZigBeeNode node = GetNode(networkAddress);
                    if (node == null)
                    {
                        _logger.Debug("{}: Node has left, but wasn't found in the network.", networkAddress);
                    }
                    else
                    {
                        // Remove the node from the network
                        RemoveNode(node);
                    }
                    break;

                // Leave the join/rejoin notifications for the discovery handler
                case ZigBeeNodeStatus.UNSECURED_JOIN:
                    break;
                case ZigBeeNodeStatus.SECURED_REJOIN:
                case ZigBeeNodeStatus.UNSECURED_REJOIN:
                    break;
                default:
                    break;
            }

            // Notify the listeners
            lock(announceListeners) {
                foreach (IZigBeeAnnounceListener announceListener in announceListeners)
                {
                    Task.Run(() =>
                    {
                        announceListener.DeviceStatusUpdate(deviceStatus, networkAddress, ieeeAddress);

                    });
                }
            }
        }

        /**
         * Adds a {@link IZigBeeNetworkStateListener} to receive notifications when the network state changes.
         *
         * @param stateListener the {@link IZigBeeNetworkStateListener} to receive the notifications
         */
        public void AddNetworkStateListener(IZigBeeNetworkStateListener stateListener)
        {
            List<IZigBeeNetworkStateListener> modifiedStateListeners = new List<IZigBeeNetworkStateListener>(_stateListeners);
            modifiedStateListeners.Add(stateListener);
            _stateListeners = new List<IZigBeeNetworkStateListener>(modifiedStateListeners).AsReadOnly();
        }

        /**
         * Removes a {@link IZigBeeNetworkStateListener}.
         *
         * @param stateListener the {@link IZigBeeNetworkStateListener} to stop receiving the notifications
         */
        public void RemoveNetworkStateListener(IZigBeeNetworkStateListener stateListener)
        {
            List<IZigBeeNetworkStateListener> modifiedStateListeners = new List<IZigBeeNetworkStateListener>(_stateListeners);
            modifiedStateListeners.Remove(stateListener);
            _stateListeners = new List<IZigBeeNetworkStateListener>(modifiedStateListeners).AsReadOnly();
        }


        public void SetNetworkState(ZigBeeTransportState state)
        {

            Task.Run(() =>
            {
                SetNetworkStateRunnable(state);
            }).ContinueWith((t) =>
            {
                _logger.Error(t.Exception, "Error");
            }, TaskContinuationOptions.OnlyOnFaulted);
        }

        private void SetNetworkStateRunnable(ZigBeeTransportState state)
        {
            lock(typeof(ZigBeeNetworkManager)) {
                // Only notify users of state changes
                if (state == NetworkState)
                {
                    return;
                }

                if (!validStateTransitions[NetworkState].Contains(state))
                {
                    _logger.Debug("Ignoring invalid network state transition from {} to {}", NetworkState, state);
                    return;
                }
                NetworkState = state;

                _logger.Debug("Network state is updated to {}", state);

                // If the state has changed to online, then we need to add any pending nodes,
                // and ensure that the local node is added
                if (state == ZigBeeTransportState.ONLINE)
                {
                    LocalNwkAddress = Transport.NwkAddress;
                    LocalIeeeAddress = Transport.IeeeAddress;

                    // Make sure that we know the local node, and that the network address is correct.
                    AddLocalNode();

                    // Globally update the state
                    NetworkState = state;

                    // Start the extensions
                    foreach (IZigBeeNetworkExtension extension in extensions)
                    {
                        extension.ExtensionStartup(this);
                    }

                    foreach (ZigBeeNode node in networkNodes.Values)
                    {
                        foreach (IZigBeeNetworkNodeListener listener in _nodeListeners)
                        {
                            Task.Run(() =>
                            {
                                listener.NodeAdded(node);
                            }).ContinueWith((t) =>
                            {
                                _logger.Error(t.Exception, "Error");
                            }, TaskContinuationOptions.OnlyOnFaulted);
                        }
                    }
                }

                // Now that everything is added and started, notify the listeners that the state has updated
                foreach (IZigBeeNetworkStateListener stateListener in _stateListeners)
                {
                    Task.Run(() =>
                    {
                        stateListener.NetworkStateUpdated(state);                        
                    }).ContinueWith((t) =>
                    {
                        _logger.Error(t.Exception, "Error");
                    }, TaskContinuationOptions.OnlyOnFaulted);
                }
            }
        }

        /**
         * Sends {@link ZclCommand} command to {@link ZigBeeAddress}.
         *
         * @param destination the destination
         * @param command the {@link ZclCommand}
         * @return the command result future
         */
        public async Task<CommandResult> Send(IZigBeeAddress destination, ZclCommand command)
        {
            command.DestinationAddress = destination;
            if (destination.IsGroup())
            {
                return Broadcast(command);
            }
            else
            {
                IZigBeeTransactionMatcher responseMatcher = new ZclTransactionMatcher();
                return SendTransaction(command, responseMatcher);
            }
        }

        /**
         * Broadcasts command i.e. does not wait for response.
         *
         * @param command the {@link ZigBeeCommand}
         * @return the {@link CommandResult} future.
         */
        private async Task<CommandResult> Broadcast(ZigBeeCommand command)
        {
            lock(command) {
                ZigBeeTransactionFuture transactionFuture = new ZigBeeTransactionFuture();

                SendCommand(command);
                transactionFuture.set(new CommandResult(new BroadcastResponse()));

                return transactionFuture;
            }
        }

        /**
         * Enables or disables devices to join the whole network.
         * <p>
         * Devices can only join the network when joining is enabled. It is not advised to leave joining enabled permanently
         * since it allows devices to join the network without the installer knowing.
         *
         * @param duration sets the duration of the join enable. Setting this to 0 disables joining. As per ZigBee 3, a
         *            value of 255 is not permitted and will be ignored.
         * @return {@link ZigBeeStatus} with the status of function
         */
        public ZigBeeStatus PermitJoin(int duration)
        {
            return PermitJoin(new ZigBeeEndpointAddress(ZigBeeBroadcastDestination.BROADCAST_ROUTERS_AND_COORD.getKey()),
                    duration);
        }

        /**
         * Enables or disables devices to join the network.
         * <p>
         * Devices can only join the network when joining is enabled. It is not advised to leave joining enabled permanently
         * since it allows devices to join the network without the installer knowing.
         *
         * @param destination the {@link ZigBeeEndpointAddress} to send the join request to
         * @param duration sets the duration of the join enable. Setting this to 0 disables joining. As per ZigBee 3, a
         *            value of 255 is not permitted and will be ignored.
         * @return {@link ZigBeeStatus} with the status of function
         */
        public ZigBeeStatus PermitJoin(ZigBeeEndpointAddress destination, int duration)
        {
            if (duration < 0 || duration >= 255)
            {
                _logger.Debug("Permit join to {} invalid period of {} seconds.", destination, duration);
                return ZigBeeStatus.INVALID_ARGUMENTS;
            }
            _logger.Debug("Permit join to {} for {} seconds.", destination, duration);

            ManagementPermitJoiningRequest command = new ManagementPermitJoiningRequest();
            command.setPermitDuration(duration);
            command.setTcSignificance(true);
            command.setDestinationAddress(destination);
            command.SourceAddress(new ZigBeeEndpointAddress(0));

            SendCommand(command);

            // If this is a broadcast, then we send it to our own address as well
            // This seems to be required for some stacks (eg ZNP)
            if (ZigBeeBroadcastDestination.GetBroadcastDestination(destination.Address) != null)
            {
                command = new ManagementPermitJoiningRequest();
                command.setPermitDuration(duration);
                command.setTcSignificance(true);
                command.setDestinationAddress(new ZigBeeEndpointAddress(0));
                command.SourceAddress(new ZigBeeEndpointAddress(0));

                SendCommand(command);
            }

            return ZigBeeStatus.SUCCESS;
        }

        /**
         * Sends a ZDO Leave Request to a device requesting that an end device leave the network.
         *
         * @param destinationAddress the network address to send the request to - this is the device parent or the the
         *            device we want to leave.
         * @param leaveAddress the {@link IeeeAddress} of the end device we want to leave the network
         */
        public void Leave(ushort destinationAddress, IeeeAddress leaveAddress)
        {
            ManagementLeaveRequest command = new ManagementLeaveRequest();

            command.setDeviceAddress(leaveAddress);
            command.setDestinationAddress(new ZigBeeEndpointAddress(destinationAddress));
            command.SourceAddress(new ZigBeeEndpointAddress(0));
            command.setRemoveChildrenRejoin(false);

            // Start a thread to wait for the response
            // When we receive the response, if it's successful, we assume the device left.
            //new Thread()
            //{


            //        public void run()
            //        {
            //            try
            //            {
            //                CommandResult response = sendTransaction(command, command).get();
            //                if (response.getStatusCode() == 0)
            //                {
            //                    ZigBeeNode node = getNode(leaveAddress);
            //                    if (node != null)
            //                    {
            //                        removeNode(node);
            //                    }
            //                    else
            //                    {
            //                        _logger.Debug("{}: No node found after successful leave command", leaveAddress);
            //                    }
            //                }
            //                else
            //                {
            //                    _logger.Debug("{}: No successful response received to leave command (status code {})",
            //                            leaveAddress, response.getStatusCode());
            //                }
            //            }
            //            catch (InterruptedException | ExecutionException e) {
            //            _logger.Debug("Error sending leave command.", e);
            //        }
            //    }
            //}.start();
            //}
        }

        public void AddGroup(ZigBeeGroupAddress group)
        {
            lock(networkGroups) {
                networkGroups[group.GroupId] = group;
            }
        }

        public void UpdateGroup(ZigBeeGroupAddress group)
        {
            lock(networkGroups) {
                networkGroups [group.GroupId] = group;
            }
        }

        public ZigBeeGroupAddress GetGroup(ushort groupId)
        {
            lock(networkGroups) {
                return networkGroups[groupId];
            }
        }

        public void RemoveGroup(ushort groupId)
        {
            lock(networkGroups) {
                networkGroups.Remove(groupId);
            }
        }

        public List<ZigBeeGroupAddress> GetGroups()
        {
            lock(networkGroups) {
                return networkGroups.Values.ToList(); 
            }
        }

        /**
         * Adds a {@link IZigBeeNetworkNodeListener} that will be notified when node information changes
         *
         * @param networkNodeListener the {@link IZigBeeNetworkNodeListener} to add
         */
        public void AddNetworkNodeListener(IZigBeeNetworkNodeListener networkNodeListener)
        {
            if (networkNodeListener == null)
            {
                return;
            }
            lock(_nodeListeners) {
                List<IZigBeeNetworkNodeListener> modifiedListeners = new List<IZigBeeNetworkNodeListener>(_nodeListeners);
                modifiedListeners.Add(networkNodeListener);
                _nodeListeners = new ReadOnlyCollection<IZigBeeNetworkNodeListener>(modifiedListeners);
            }
        }

        /**
         * Removes a {@link IZigBeeNetworkNodeListener} that will be notified when node information changes
         *
         * @param networkNodeListener the {@link IZigBeeNetworkNodeListener} to remove
         */
        public void RemoveNetworkNodeListener(IZigBeeNetworkNodeListener networkNodeListener)
        {
            lock(_nodeListeners) {
                List<IZigBeeNetworkNodeListener> modifiedListeners = new List<IZigBeeNetworkNodeListener>(_nodeListeners);
                modifiedListeners.Remove(networkNodeListener);
                _nodeListeners = new ReadOnlyCollection<IZigBeeNetworkNodeListener>(modifiedListeners);
            }
        }

        /**
         * Starts a rediscovery on a node. This will send a {@link NetworkAddressRequest} as a broadcast and will receive
         * the response to trigger a full discovery.
         *
         * @param ieeeAddress the {@link IeeeAddress} of the node to rediscover
         */
        public void RediscoverNode(IeeeAddress address)
        {

            asdfsdf
            //ZigBeeDiscoveryExtension networkDiscoverer = (ZigBeeDiscoveryExtension)getExtension(ZigBeeDiscoveryExtension.class);
            //if (NetworkDiscoverer == null) {
            //    return;
            //}
            //networkDiscoverer.rediscoverNode(address);
        }

        /**
         * Gets a {@link Set} of {@link ZigBeeNode}s known by the network
         *
         * @return {@link Set} of {@link ZigBeeNode}s
         */
        public List<ZigBeeNode> GetNodes()
        {
            lock(networkNodes) {
                return new List<ZigBeeNode>(networkNodes.Values);
            }
        }

        /**
         * Gets a node given the 16 bit network address
         *
         * @param networkAddress the 16 bit network address as {@link Integer}
         * @return the {@link ZigBeeNode} or null if the node with the requested network address was not found
         */
        public ZigBeeNode GetNode(ushort networkAddress)
        {
            lock(networkNodes) {
                foreach (ZigBeeNode node in networkNodes.Values)
                {
                    if (node.NetworkAddress.Equals(networkAddress))
                    {
                        return node;
                    }
                }
            }
            return null;
        }

        /**
         * Gets a node given the {@link IeeeAddress}
         *
         * @param ieeeAddress the {@link IeeeAddress}
         * @return the {@link ZigBeeNode} or null if the node was not found
         */
        public ZigBeeNode GetNode(IeeeAddress ieeeAddress)
        {
            return networkNodes[ieeeAddress];
        }

        /**
         * Removes a {@link ZigBeeNode} from the network
         *
         * @param node the {@link ZigBeeNode} to remove - must not be null
         */
        public void RemoveNode(ZigBeeNode node)
        {
            if (node == null)
            {
                return;
            }

            _logger.Debug("{}: Node {} is removed from the network", node.IeeeAddress, node.NetworkAddress);

            nodeDiscoveryComplete.Remove(node.IeeeAddress);

            lock(networkNodes) {
                // Don't update if the node is not known
                // We especially don't want to notify listeners of a device we removed, that didn't exist!
                if (!networkNodes.ContainsKey(node.IeeeAddress))
                {
                    return;
                }
                ZigBeeNode removedNode = null;
                networkNodes.TryRemove(node.IeeeAddress, out removedNode);
            }

            lock(_nodeListeners) {
                foreach (IZigBeeNetworkNodeListener listener in _nodeListeners)
                {
                    Task.Run(() =>
                    {
                        listener.NodeRemoved(node);
                    }).ContinueWith((t) =>
                    {
                        _logger.Error(t.Exception, "Error");
                    }, TaskContinuationOptions.OnlyOnFaulted);
                }
            }

            node.Shutdown();

            if (NetworkStateSerializer != null)
            {
                NetworkStateSerializer.Serialize(this);
            }
        }

        /**
         * Adds a {@link ZigBeeNode} to the network
         *
         * @param node the {@link ZigBeeNode} to add
         */
        public void AddNode(ZigBeeNode node)
        {
            if (node == null)
            {
                return;
            }

            _logger.Debug("{}: Node {} added to the network", node.IeeeAddress, node.NetworkAddress);

            lock(networkNodes) {
                // Don't add if the node is already known
                // We especially don't want to notify listeners
                if (networkNodes.ContainsKey(node.IeeeAddress))
                {
                    UpdateNode(node);
                    return;
                }
                networkNodes[node.IeeeAddress] = node;
            }

            lock(_nodeListeners) {
                if (NetworkState != ZigBeeTransportState.ONLINE)
                {
                    return;
                }

                foreach (IZigBeeNetworkNodeListener listener in _nodeListeners)
                {
                    Task.Run(() =>
                    {
                        listener.NodeAdded(node);
                    }).ContinueWith((t) =>
                    {
                        _logger.Error(t.Exception, "Error");
                    }, TaskContinuationOptions.OnlyOnFaulted);
                }
            }

            if (NetworkStateSerializer != null)
            {
                NetworkStateSerializer.Serialize(this);
            }
        }

        /**
         * Update a {@link ZigBeeNode} within the network
         *
         * @param node the {@link ZigBeeNode} to update
         */
        public void UpdateNode(ZigBeeNode node)
        {
            if (node == null)
            {
                return;
            }
            _logger.Debug("{}: Node {} update", node.IeeeAddress, node.NetworkAddress);

            ZigBeeNode currentNode;
            lock(networkNodes) {
                currentNode = networkNodes[node.IeeeAddress];

                // Return if we don't know this node
                if (currentNode == null)
                {
                    _logger.Debug("{}: Node {} is not known - can't be updated", node.IeeeAddress,
                            node.NetworkAddress);
                    return;
                }

                // Return if there were no updates
                if (!currentNode.UpdateNode(node))
                {
                    // _logger.Debug("{}: Node {} is not updated", node.getIeeeAddress(), node.getNetworkAddress());
                    // return;
                }
            }

            bool updated = nodeDiscoveryComplete.Contains(node.IeeeAddress);
            if (!updated && node.IsDiscovered() || node.IeeeAddress.Equals(LocalIeeeAddress))
            {
                nodeDiscoveryComplete.Add(node.IeeeAddress);
            }

            lock(_nodeListeners) {
                foreach (IZigBeeNetworkNodeListener listener in _nodeListeners)
                {
                    Task.Run(() =>
                    {
                        if (updated)
                        {
                            listener.NodeUpdated(currentNode);
                        }
                        else
                        {
                            listener.NodeAdded(currentNode);
                        }
                    }).ContinueWith((t) => 
                    {
                        _logger.Error(t.Exception, "Here is the error additional text");
                    }, TaskContinuationOptions.OnlyOnFaulted);
                }
            }

            if (NetworkStateSerializer != null)
            {
                NetworkStateSerializer.Serialize(this);
            }
        }

        /**
         * Adds a cluster to the list of clusters we will respond to with the {@link MatchDescriptorRequest}. Adding a
         * cluster here is only required in order to respond to this request. Typically the application should provide
         * further support for such clusters.
         *
         * @param cluster the supported cluster ID
         */
        public void AddSupportedCluster(byte cluster)
        {
            _logger.Debug("Adding supported cluster {}", cluster);
            if (clusterMatcher == null)
            {
                clusterMatcher = new ClusterMatcher(this);
            }

            clusterMatcher.AddCluster(cluster);
        }

        /**
         * Adds a functional extension to the network.
         *
         * @param extension the new {@link ZigBeeNetworkExtension}
         */
        public void AddExtension(IZigBeeNetworkExtension extension)
        {
            lock (extensions)
            {
                extensions.Add(extension);
                extension.ExtensionInitialize(this);

                // If the network is online, start the extension
                if (NetworkState == ZigBeeTransportState.ONLINE)
                {
                    extension.ExtensionStartup(this);
                }
            }
        }

        /**
         * Gets a functional extension that has been registered with the network.
         *
         * @param <T> {@link ZigBeeNetworkExtension}
         * @param requestedExtension the {@link ZigBeeNetworkExtension} to get
         * @return the requested {@link ZigBeeNetworkExtension} if it exists, or null
         */
        public IZigBeeNetworkExtension GetExtension(Type requestedExtension)
        {
            foreach (IZigBeeNetworkExtension extensionCheck in extensions)
            {
                if (extensionCheck.GetType().IsAssignableFrom(requestedExtension))
                {
                    return extensionCheck;
                }
            }

            return null;
        }

        public void SendTransaction(ZigBeeCommand command)
        {
            SendCommand(command);
        }


        public async Task<CommandResult> SendTransaction(ZigBeeCommand command, IZigBeeTransactionMatcher responseMatcher)
        {
            ZigBeeTransaction transaction = new ZigBeeTransaction(this);
            return transaction.sendTransaction(command, responseMatcher);
        }
    }
}
