using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZDO;
using ZigBeeNet.ZDO.Command;
using ZigBeeNet.App;
using ZigBeeNet.Internal;
using ZigBeeNet.Security;
using ZigBeeNet.Serialization;
using ZigBeeNet.Transport;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.App.Discovery;
using Serilog;

namespace ZigBeeNet
{
    /// <summary>
    /// ZigBeeNetworkManager implements functions for managing the ZigBee interfaces. The network manager is the central
    /// class of the framework. It provides the interface with the dongles to send and receive data, and application
    /// interfaces to provide listeners for system events (eg network status with the <see cref="IZigBeeNetworkStateListener"> or
    /// changes to nodes with the <see cref="IZigBeeNetworkNodeListener"> or to receive incoming commands with the
    /// <see cref="IZigBeeCommandListener">).
    /// 
    /// The ZigBeeNetworkManager maintains a list of all <see cref="ZigBeeNode">s that are known on the network. Depending on the
    /// system configuration, different discovery methods may be utilised to maintain this list. A Coordinator may actively
    /// look for all nodes on the network while a Router implementation may only need to know about specific nodes that it is
    /// communicating with.
    /// 
    /// The ZigBeeNetworkManager also maintains a list of <see cref="ZigBeeNetworkExtension">s which allow the functionality of
    /// the network to be extended. Extensions may provide different levels of functionality - an extension may be as simple
    /// as configuring the framework to work with a specific feature, or could provide a detailed application.
    /// 
    /// Lifecycle
    /// The ZigBeeNetworkManager lifecycle is as follows -:
    /// 
    /// Instantiate a <see cref="IZigBeeTransportTransmit"> class
    /// Instantiate a <see cref="ZigBeeNetworkManager"> class passing the previously created <see cref="IZigBeeTransportTransmit">
    /// class
    /// Optionally set the <see cref="ZigBeeSerializer"> and <see cref="ZigBeeDeserializer"> using the {@link #setSerializer}
    /// method
    /// Call the {@link #initialize} method to perform the initial initialization of the ZigBee network
    /// Set the network configuration (see below).
    /// Call the {@link #startup} method to start using the configured ZigBee network. Configuration methods may not be
    /// used.
    /// Call the <see cref="Shutdown()"> method to close the network
    /// 
    /// Following a call to <see cref="Initialize()"/> configuration calls can be made to configure the transport layer. This
    /// 
    /// Once all transport initialization is complete, {@link #startup} must be called.
    /// </summary>
    public class ZigBeeNetworkManager : IZigBeeNetwork, IZigBeeTransportReceive
    {
        /// <summary>
        /// The nodes in the ZigBee network - maps IeeeAddress to ZigBeeNode
        /// </summary>
        private ConcurrentDictionary<IeeeAddress, ZigBeeNode> _networkNodes = new ConcurrentDictionary<IeeeAddress, ZigBeeNode>();

        /// <summary>
        /// The groups in the ZigBee network
        /// </summary>
        private Dictionary<ushort, ZigBeeGroupAddress> _networkGroups = new Dictionary<ushort, ZigBeeGroupAddress>();

        /// <summary>
        /// The node listeners of the ZigBee network. Registered listeners will be
        /// notified of additions, deletions and changes to ZigBeeNodes.
        /// </summary>
        private List<IZigBeeNetworkNodeListener> _nodeListeners;

        /// <summary>
        /// The announce listeners are notified whenever a new device is discovered.
        /// This can be called from the transport layer, or internally by methods watching
        /// the network state.
        /// </summary>
        private List<IZigBeeAnnounceListener> _announceListeners;

        /// <summary>
        /// AtomicInteger used to generate transaction sequence numbers
        /// </summary>
        private static volatile int _sequenceNumber;

        /// <summary>
        /// AtomicInteger used to generate APS header counters
        /// </summary>
        private static volatile int _apsCounter;

        /// <summary>
        /// The ZigBeeCommandNotifier. This is used for sending notifications asynchronously to listeners.
        /// </summary>
        private ZigBeeCommandNotifier _commandNotifier = new ZigBeeCommandNotifier();

        /// <summary>
        /// The listeners of the ZigBee network state.
        /// </summary>
        private ReadOnlyCollection<IZigBeeNetworkStateListener> _stateListeners;

        /// <summary>
        /// A Set used to remember if node discovery has been completed. This is used to manage the lifecycle notifications.
        /// </summary>
        private List<IeeeAddress> _nodeDiscoveryComplete = new List<IeeeAddress>();

        /// <summary>
        /// List of ZigBeeNetworkExtensions that are available to this network. Extensions are added
        /// with the #addApplication(ZigBeeNetworkExtension extension)} method.
        /// </summary>
        private List<IZigBeeNetworkExtension> _extensions = new List<IZigBeeNetworkExtension>();

        /// <summary>
        /// A ClusterMatcher used to respond to the <see cref="MatchDescriptorRequest"> command.
        /// </summary>
        private ClusterMatcher _clusterMatcher = null;

        /// <summary>
        /// Map of allowable state transitions
        /// </summary>
        private readonly Dictionary<ZigBeeTransportState, List<ZigBeeTransportState>> _validStateTransitions;

        /// <summary>
        /// Our local network address
        /// </summary>
        private ushort _localNwkAddress = 0;

        public ushort LocalNwkAddress => _localNwkAddress;

        private readonly object _networkStateSync = new object();

        /// <summary>
        /// The network state serializer
        /// </summary>
        public IZigBeeNetworkStateSerializer NetworkStateSerializer { get; set; }

        /// <summary>
        /// Executor service to execute update threads for discovery or mesh updates etc.
        /// We use a {@link Executors.newScheduledThreadPool} to provide a fixed number of threads as otherwise this could
        /// result in a large number of simultaneous threads in large networks.
        /// </summary>
        //private ScheduledExecutorService executorService = Executors.newScheduledThreadPool(6);

        /// <summary>
        /// The <see cref="IZigBeeTransportTransmit"> implementation. This provides the interface
        /// for sending data to the network which is an implementation of a ZigBee
        /// interface (eg a Dongle).
        /// </summary>
        public IZigBeeTransportTransmit Transport { get; set; }

        /// <summary>
        /// The serializer class used to serialize commands to data packets
        /// </summary>
        public IZigBeeSerializer Serializer { get; set; }

        /// <summary>
        /// The deserializer class used to deserialize commands from data packets
        /// </summary>
        public IZigBeeDeserializer Deserializer { get; set; }

        /// <summary>
        /// The current <see cref="ZigBeeTransportState">
        /// </summary>
        public ZigBeeTransportState NetworkState { get; set; } = ZigBeeTransportState.UNINITIALISED;

        /// <summary>
        /// Our local <see cref="IeeeAddress">
        /// </summary>
        public IeeeAddress LocalIeeeAddress { get; set; }

        public ZigBeeChannel ZigbeeChannel
        {
            get
            {
                return Transport.ZigBeeChannel;
            }
        }

        /// <summary>
        /// Gets the ZigBee PAN ID currently in use by the transport
        ///
        /// <returns>the PAN ID</returns>
        /// </summary>
        public ushort ZigBeePanId
        {
            get
            {
                return (ushort)(Transport.PanID & 0xFFFF);
            }
        }

        /// <summary>
        /// Get the transport layer version string
        ///
        /// <returns><see cref="String"> containing the transport layer version</returns>
        /// </summary>
        public string TransportVersionString
        {
            get
            {
                return Transport.VersionString;
            }
        }

        /// <summary>
        /// Gets the current Trust Centre link key used by the system
        ///
        /// <returns>the current trust centre link <see cref="ZigBeeKey"></returns>
        /// </summary>
        public ZigBeeKey ZigBeeLinkKey
        {
            get
            {
                return Transport.TcLinkKey;
            }
        }

        /// <summary>
        /// Gets a <see cref="Set"> of <see cref="ZigBeeNode">s known by the network
        ///
        /// <returns><see cref="Set"> of <see cref="ZigBeeNode">s</returns>
        /// </summary>
        public List<ZigBeeNode> Nodes
        {
            get
            {
                lock (_networkNodes)
                {
                    return new List<ZigBeeNode>(_networkNodes.Values);
                }
            }
        }

        public enum ZigBeeInitializeResponse
        {
            /// <summary>
            /// Device is initialized successfully and is currently joined to a network
            /// </summary>
            JOINED,
            /// <summary>
            /// Device initialization failed
            /// </summary>
            FAILED,
            /// <summary>
            /// Device is initialized successfully and is currently not joined to a network
            /// </summary>
            NOT_JOINED
        }

        /// <summary>
        /// Constructor which configures serial port and ZigBee network.
        /// </summary>
        /// <param name="transport">Transport the dongle</param>
        public ZigBeeNetworkManager(IZigBeeTransportTransmit transport)
        {
            List<IZigBeeNetworkStateListener> stateListeners = new List<IZigBeeNetworkStateListener>();
            _stateListeners = new List<IZigBeeNetworkStateListener>(stateListeners).AsReadOnly();

            _nodeListeners = new List<IZigBeeNetworkNodeListener>();

            _announceListeners = new List<IZigBeeAnnounceListener>();

            Dictionary<ZigBeeTransportState, List<ZigBeeTransportState>> transitions = new Dictionary<ZigBeeTransportState, List<ZigBeeTransportState>>();

            transitions[ZigBeeTransportState.UNINITIALISED] = new List<ZigBeeTransportState>(new[] { ZigBeeTransportState.INITIALISING, ZigBeeTransportState.OFFLINE });
            transitions[ZigBeeTransportState.INITIALISING] = new List<ZigBeeTransportState>(new[] { ZigBeeTransportState.ONLINE, ZigBeeTransportState.OFFLINE });
            transitions[ZigBeeTransportState.ONLINE] = new List<ZigBeeTransportState>(new[] { ZigBeeTransportState.OFFLINE });
            transitions[ZigBeeTransportState.OFFLINE] = new List<ZigBeeTransportState>(new[] { ZigBeeTransportState.ONLINE });
            transitions[ZigBeeTransportState.SHUTDOWN] = new List<ZigBeeTransportState>(new[] { ZigBeeTransportState.OFFLINE });

            _validStateTransitions = transitions;

            Transport = transport;

            transport.SetZigBeeTransportReceive(this);
        }

        /// <summary>
        /// Initializes ZigBee manager components and initializes the transport layer.
        /// 
        /// If a network state was previously serialized, it will be deserialized here if the serializer is set with the
        /// <see cref="NetworkStateSerializer"/> method.
        /// 
        /// Following a call to <see cref="Initialize()"/> configuration calls can be made to configure the transport layer. This
        /// 
        /// Once all transport initialization is complete, <see cref="Startup(bool)"/> must be called.
        /// </summary>
        /// <returns>Status</returns>
        public ZigBeeStatus Initialize()
        {
            lock (_networkStateSync)
            {
                if (NetworkState != ZigBeeTransportState.UNINITIALISED)
                {
                    return ZigBeeStatus.INVALID_STATE;
                }

                SetNetworkState(ZigBeeTransportState.INITIALISING);

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
            if (ieeeAddress != null)
            {
                ZigBeeNode node = GetNode(ieeeAddress);
                if (node == null)
                {
                    Log.Debug("{IeeeAddress}: Adding local node to network, NWK={NetworkAddress}", ieeeAddress, nwkAddress);

                    node = new ZigBeeNode(this, ieeeAddress)
                    {
                        NetworkAddress = nwkAddress
                    };

                    AddNode(node);
                }
            }
        }

        /// <summary>
        /// Sets the ZigBee RF channel.The allowable channel range is 11 to 26 for 2.4GHz, however the transport
        /// implementation may allow any value it supports.
        /// 
        /// Note that this method may only be called following the 
        /// <see cref="Initialize()"/> call, and before the <see cref="Startup(bool)"/>
        /// call.
        /// </summary>
        /// <param name="channel">Defining the channel to use</param>
        /// <returns>Status</returns>
        public ZigBeeStatus SetZigBeeChannel(ZigBeeChannel channel)
        {
            return Transport.SetZigBeeChannel(channel);
        }

        /// <summary>
        /// Sets the ZigBee PAN ID to the specified value. The range of the PAN ID is 0 to 0x3FFF.
        /// Additionally a value of 0xFFFF is allowed to indicate the user doesn't care and a random value
        /// can be set by the transport.
        /// 
        /// Note that this method may only be called following the {@link #initialize} call, and before the {@link #startup}
        /// call.
        ///
        /// <param name="panId">the new PAN ID</param>
        /// <returns><see cref="ZigBeeStatus"/> with the status of function</returns>
        /// </summary>
        public ZigBeeStatus SetZigBeePanId(ushort panId)
        {
            if (panId < 0 || panId > 0xfffe)
            {
                return ZigBeeStatus.INVALID_ARGUMENTS;
            }
            return Transport.SetZigBeePanId(panId);
        }

        /// <summary>
        /// Gets the ZigBee Extended PAN ID currently in use by the transport
        ///
        /// <returns>the PAN ID</returns>
        /// </summary>
        public ExtendedPanId ZigBeeExtendedPanId
        {
            get
            {
                return Transport.ExtendedPanId;
            }
        }

        /// <summary>
        /// Sets the ZigBee Extended PAN ID to the specified value
        /// 
        /// Note that this method may only be called following the <see cref="Initialize()"/> call, and before the <see cref="Startup(bool)"/>
        /// call.
        ///
        /// <param name="panId">the new <see cref="ExtendedPanId"/></param>
        /// <returns><see cref="ZigBeeStatus"/> with the status of function</returns>
        /// </summary>
        public ZigBeeStatus SetZigBeeExtendedPanId(ExtendedPanId panId)
        {
            return Transport.SetZigBeeExtendedPanId(panId);
        }

        /// <summary>
        /// Set the current network key in use by the system.
        /// 
        /// Note that this method may only be called following the <see cref="Initialize()"/> call, and before the <see cref="Startup(bool)"/>
        /// call.
        ///
        /// <param name="key">the new network key as <see cref="ZigBeeKey"/></param>
        /// <returns><see cref="ZigBeeStatus"> with the status of function</returns>
        /// </summary>
        public ZigBeeStatus SetZigBeeNetworkKey(ZigBeeKey key)
        {
            return Transport.SetZigBeeNetworkKey(key);
        }

        /// <summary>
        /// Gets the current network key used by the system
        ///
        /// <returns>the current network <see cref="ZigBeeKey"/></returns>
        /// </summary>
        public ZigBeeKey ZigBeeNetworkKey
        {
            get
            {
                return Transport.ZigBeeNetworkKey;
            }
        }

        /// <summary>
        /// Set the current link key in use by the system.
        /// 
        /// Note that this method may only be called following the <see cref="Initialize()"/> call, and before the <see cref="Startup(bool)"/>
        /// call.
        ///
        /// <param name="key">the new link key as <see cref="ZigBeeKey"/></param>
        /// <returns><see cref="ZigBeeStatus"> with the status of function</returns>
        /// </summary>
        public ZigBeeStatus SetZigBeeLinkKey(ZigBeeKey key)
        {
            return Transport.SetTcLinkKey(key);
        }

        /// <summary>
        /// Adds an installation key for the specified address. The <see cref="ZigBeeKey"/> should have an address associated with
        /// it.
        ///
        /// <param name="key">the install key as <see cref="ZigBeeKey"/> to be used. The key must contain a partner address.</param>
        /// <returns><see cref="ZigBeeStatus"/> with the status of function</returns>
        /// </summary>
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

        /// <summary>
        /// Starts up ZigBee manager components.
        /// 
        /// <param name="reinitialize">true if the provider is to reinitialise the network with the parameters configured since the <see cref="Initialize()"/> method was called.</param>
        ///            
        /// <returns><see cref="ZigBeeStatus"> with the status of function</returns>
        /// </summary>
        public ZigBeeStatus Startup(bool reinitialize)
        {
            lock (_networkStateSync)
            {
                if(NetworkState == ZigBeeTransportState.UNINITIALISED)
                {
                    return ZigBeeStatus.INVALID_STATE;
                }
            }

            ZigBeeStatus status = Transport.Startup(reinitialize);

            if (status != ZigBeeStatus.SUCCESS)
            {
                SetNetworkState(ZigBeeTransportState.OFFLINE);
                return status;
            }

            SetNetworkState(ZigBeeTransportState.ONLINE);

            return ZigBeeStatus.SUCCESS;
        }

        /// <summary>
        /// Shuts down ZigBee manager components.
        /// </summary>
        public void Shutdown()
        {
            NetworkState = ZigBeeTransportState.SHUTDOWN;

            lock (_networkNodes)
            {
                foreach (ZigBeeNode node in _networkNodes.Values)
                {
                    node.Shutdown();
                }

                if (NetworkStateSerializer != null)
                {
                    NetworkStateSerializer.Serialize(this);
                }

                foreach (IZigBeeNetworkExtension extension in _extensions)
                {
                    extension.ExtensionShutdown();
                }
            }

            Transport.Shutdown();
        }

        /// <summary>
        /// Schedules a runnable task for execution. This uses a fixed size scheduler to limit thread execution.
        ///
        /// <param name="runnableTask">the <see cref="Task"/> to execute</param>
        /// <param name="delay">the delay in milliseconds before the task will be executed</param>
        /// <returns>the <see cref="Task"/> for the scheduled task</returns>
        /// </summary>
        public async Task ScheduleTask(Task runnableTask, int delay, CancellationTokenSource cancellation)
        {
            if (NetworkState != ZigBeeTransportState.ONLINE)
            {
                return;
            }
            await Task.Delay(delay);
            if (cancellation.IsCancellationRequested == false)
            {
                runnableTask.Start();
            }
            return;
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
            command.SourceAddress = new ZigBeeEndpointAddress(_localNwkAddress);

            Log.Debug("TX CMD: {Command}", command);

            apsFrame.Cluster = command.ClusterId;
            apsFrame.ApsCounter = (byte)(((byte)Interlocked.Increment(ref _apsCounter)) & 0xff);
            apsFrame.SecurityEnabled = command.ApsSecurity;

            // TODO: Set the source address correctly?
            apsFrame.SourceAddress = _localNwkAddress;

            apsFrame.Radius = 31;

            if (command.DestinationAddress is ZigBeeEndpointAddress dstAddr)
            {
                apsFrame.AddressMode = ZigBeeNwkAddressMode.Device;
                apsFrame.DestinationAddress = dstAddr.Address;
                apsFrame.DestinationEndpoint = dstAddr.Endpoint;

                ZigBeeNode node = GetNode(command.DestinationAddress.Address);
                if (node != null)
                {
                    apsFrame.DestinationIeeeAddress = node.IeeeAddress;
                }
            }
            else
            {
                apsFrame.AddressMode = ZigBeeNwkAddressMode.Group;
                // TODO: Handle multicast
            }

            ZclFieldSerializer fieldSerializer;

            try
            {
                Serializer = new DefaultSerializer();
                fieldSerializer = new ZclFieldSerializer(Serializer);
            }
            catch (Exception e)
            {
                Log.Debug("Error serializing ZigBee frame {Exception}", e);
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
            // For ZCL commands we pass the NWK and APS headers as classes to the transport layer.
            // The ZCL packet is serialised here.

            if (command is ZclCommand zclCommand)
            {
                apsFrame.SourceEndpoint = 1;

                // TODO set the profile properly
                apsFrame.Profile = 0x104;

                // Create the cluster library header
                ZclHeader zclHeader = new ZclHeader
                {
                    FrameType = zclCommand.GenericCommand ? ZclFrameType.ENTIRE_PROFILE_COMMAND : ZclFrameType.CLUSTER_SPECIFIC_COMMAND,
                    CommandId = zclCommand.CommandId,
                    SequenceNumber = command.TransactionId.Value,
                    Direction = zclCommand.CommandDirection
                };

                command.Serialize(fieldSerializer);

                // Serialise the ZCL header and add the payload
                apsFrame.Payload = zclHeader.Serialize(fieldSerializer, fieldSerializer.Payload);

                Log.Debug("TX ZCL: {ZclHeader}", zclHeader);
            }

            Log.Debug("TX APS: {ApsFrame}", apsFrame);

            Transport.SendCommand(apsFrame);

            return command.TransactionId.Value;
        }

        public void AddCommandListener(IZigBeeCommandListener commandListener)
        {
            _commandNotifier.AddCommandListener(commandListener);
        }

        public void RemoveCommandListener(IZigBeeCommandListener commandListener)
        {
            _commandNotifier.RemoveCommandListener(commandListener);
        }

        public void ReceiveCommand(ZigBeeApsFrame apsFrame)
        {
            lock (_networkStateSync)
            {
                if (NetworkState != ZigBeeTransportState.ONLINE)
                {
                    return;
                }
            }

            Log.Debug("RX APS: {ApsFrame}", apsFrame);

            // Create the deserialiser
            Deserializer = new DefaultDeserializer(apsFrame.Payload);

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
                    Log.Debug("Received message with unknown profile {Profile}", apsFrame.Profile.ToString("X4"));
                    break;
            }

            if (command == null)
            {
                Log.Debug("Incoming message did not translate to command.");
                return;
            }

            // Create an address from the sourceAddress and endpoint
            command.SourceAddress = new ZigBeeEndpointAddress(apsFrame.SourceAddress, apsFrame.SourceEndpoint);
            command.DestinationAddress = new ZigBeeEndpointAddress(apsFrame.DestinationAddress, apsFrame.DestinationEndpoint);
            command.ApsSecurity = apsFrame.SecurityEnabled;

            Log.Debug("RX CMD: {Command}", command);

            // Notify the listeners
            _commandNotifier.NotifyCommandListeners(command);
        }

        private ZigBeeCommand ReceiveZdoCommand(ZclFieldDeserializer fieldDeserializer, ZigBeeApsFrame apsFrame)
        {
            ZdoCommandType commandType = ZdoCommandType.GetValueById(apsFrame.Cluster);

            if (commandType == null)
            {
                return null;
            }

            ZigBeeCommand command;

            try
            {
                command = commandType.GetZdoCommand();
            }
            catch (Exception e)
            {
                Log.Debug("Error instantiating ZDO command", e);
                return null;
            }

            command.Deserialize(fieldDeserializer);

            return command;
        }

        private ZigBeeCommand ReceiveZclCommand(ZclFieldDeserializer fieldDeserializer, ZigBeeApsFrame apsFrame)
        {
            // Process the ZCL header
            ZclHeader zclHeader = new ZclHeader(fieldDeserializer);
            Log.Debug("RX ZCL: {ZclHeader}", zclHeader);

            // Get the command type
            ZclCommandType commandType = null;
            if (zclHeader.FrameType == ZclFrameType.ENTIRE_PROFILE_COMMAND)
            {
                commandType = ZclCommandType.GetGeneric(zclHeader.CommandId);
            }
            else
            {
                commandType = ZclCommandType.GetCommandType(apsFrame.Cluster, zclHeader.CommandId, zclHeader.Direction);
            }

            if (commandType == null)
            {
                Log.Debug("No command type found for {FrameType}, cluster={Cluster}, command={Command}, direction={Direction}", zclHeader.FrameType,
                        apsFrame.Cluster, zclHeader.CommandId, zclHeader.Direction);
                return null;
            }

            ZclCommand command = commandType.GetCommand();
            if (command == null)
            {
                Log.Debug("No command found for {FrameType}, cluster={Cluster}, command={Command}", zclHeader.FrameType,
                        apsFrame.Cluster, zclHeader.CommandId);
                return null;
            }

            command.CommandDirection = zclHeader.Direction;
            command.Deserialize(fieldDeserializer);
            command.ClusterId = apsFrame.Cluster;
            command.TransactionId = zclHeader.SequenceNumber;

            return command;
        }

        /// <summary>
        /// Add a <see cref="IZigBeeAnnounceListener"/> that will be notified whenever a new device is detected
        /// on the network.
        ///
        /// <param name="announceListener">the new <see cref="IZigBeeAnnounceListener"/> to add</param>
        /// </summary>
        public void AddAnnounceListener(IZigBeeAnnounceListener announceListener)
        {
            if (_announceListeners.Contains(announceListener))
            {
                return;
            }

            _announceListeners.Add(announceListener);
        }

        /// <summary>
        /// Remove a <see cref="IZigBeeAnnounceListener"/>
        ///
        /// <param name="statusListener">the new <see cref="IZigBeeAnnounceListener"/> to remove</param>
        /// </summary>
        public void RemoveAnnounceListener(IZigBeeAnnounceListener statusListener)
        {
            _announceListeners.Remove(statusListener);
        }

        public void NodeStatusUpdate(ZigBeeNodeStatus deviceStatus, ushort networkAddress, IeeeAddress ieeeAddress)
        {
            Log.Debug("{IeeeAddress}: nodeStatusUpdate - node status is {DeviceStatus}, network address is {NetworkAddress}.", ieeeAddress, deviceStatus,
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
                        Log.Debug("{NetworkAddress}: Node has left, but wasn't found in the network.", networkAddress);
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
            lock (_announceListeners) // TODO: Consider using a concurrent list/collection
            {
                foreach (IZigBeeAnnounceListener announceListener in _announceListeners)
                {
                    Task.Run(() =>
                    {
                        announceListener.DeviceStatusUpdate(deviceStatus, networkAddress, ieeeAddress);
                    });
                }
            }
        }

        /// <summary>
        /// Adds a <see cref="IZigBeeNetworkStateListener"/> to receive notifications when the network state changes.
        ///
        /// <param name="stateListener">the <see cref="IZigBeeNetworkStateListener"/> to receive the notifications</param>
        /// </summary>
        public void AddNetworkStateListener(IZigBeeNetworkStateListener stateListener)
        {
            List<IZigBeeNetworkStateListener> modifiedStateListeners = new List<IZigBeeNetworkStateListener>(_stateListeners);
            modifiedStateListeners.Add(stateListener);
            _stateListeners = new List<IZigBeeNetworkStateListener>(modifiedStateListeners).AsReadOnly();
        }

        /// <summary>
        /// Removes a <see cref="IZigBeeNetworkStateListener"/>.
        ///
        /// <param name="stateListener">the <see cref="IZigBeeNetworkStateListener"> to stop receiving the notifications</param>
        /// </summary>
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
                Log.Error(t.Exception, "Error");
            }, TaskContinuationOptions.OnlyOnFaulted);
        }

        private void SetNetworkStateRunnable(ZigBeeTransportState state)
        {
            lock (_networkNodes)
            {
                // Only notify users of state changes
                if (state == NetworkState)
                {
                    return;
                }

                if (!_validStateTransitions[NetworkState].Contains(state))
                {
                    Log.Debug("Ignoring invalid network state transition from {NetworkState} to {State}", NetworkState, state);
                    return;
                }
                NetworkState = state;

                Log.Debug("Network state is updated to {NetworkState}", state);

                // If the state has changed to online, then we need to add any pending nodes,
                // and ensure that the local node is added
                if (state == ZigBeeTransportState.ONLINE)
                {
                    _localNwkAddress = Transport.NwkAddress;
                    LocalIeeeAddress = Transport.IeeeAddress;

                    // Make sure that we know the local node, and that the network address is correct.
                    AddLocalNode();

                    // Globally update the state
                    NetworkState = state;

                    // Disable JOIN mode.
                    // This should be disabled by default (at least in ZigBee 3.0) but some older stacks may
                    // have join enabled permanently by default.
                    PermitJoin(0);

                    // Start the extensions
                    foreach (IZigBeeNetworkExtension extension in _extensions)
                    {
                        extension.ExtensionStartup();
                    }

                    foreach (ZigBeeNode node in _networkNodes.Values)
                    {
                        foreach (IZigBeeNetworkNodeListener listener in _nodeListeners)
                        {
                            Task.Run(() =>
                            {

                                listener.NodeAdded(node);

                            }).ContinueWith((t) =>
                            {
                                Log.Error(t.Exception, "Error");
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
                        Log.Error(t.Exception, "Error");
                    }, TaskContinuationOptions.OnlyOnFaulted);
                }
            }
        }

        /// <summary>
        /// Sends <see cref="ZclCommand"/> command to <see cref="ZigBeeAddress"/>.
        ///
        /// <param name="destination">the destination</param>
        /// <param name="command">the <see cref="ZclCommand"/></param>
        /// <returns>the command result future</returns>
        /// </summary>
        public async Task<CommandResult> Send(IZigBeeAddress destination, ZclCommand command)
        {
            command.DestinationAddress = destination;
            if (destination.IsGroup)
            {
                return await Broadcast(command);
            }
            else
            {
                IZigBeeTransactionMatcher responseMatcher = new ZclTransactionMatcher();
                return await SendTransaction(command, responseMatcher);
            }
        }

        /// <summary>
        /// Broadcasts command i.e. does not wait for response.
        ///
        /// <param name="command">the <see cref="ZigBeeCommand"/></param>
        /// <returns>the <see cref="CommandResult"/> <see cref="Task"/>.</returns>
        /// </summary>
        private Task<CommandResult> Broadcast(ZigBeeCommand command)
        {
            return Task.Run(() =>
            {
                lock (command)
                {
                    //ZigBeeTransactionFuture transactionFuture = new ZigBeeTransactionFuture();

                    SendCommand(command);
                    //transactionFuture.set(new CommandResult(new BroadcastResponse()));

                    return new CommandResult(new BroadcastResponse());
                }
            });
        }

        /// <summary>
        /// Enables or disables devices to join the whole network.
        /// 
        /// Devices can only join the network when joining is enabled. It is not advised to leave joining enabled permanently
        /// since it allows devices to join the network without the installer knowing.
        ///
        /// <param name="duration">sets the duration of the join enable. Setting this to 0 disables joining. As per ZigBee 3, a value of 255 is not permitted and will be ignored.</param>
        ///            
        /// <returns><see cref="ZigBeeStatus"/> with the status of function</returns>
        /// </summary>
        public ZigBeeStatus PermitJoin(byte duration)
        {
            return PermitJoin(new ZigBeeEndpointAddress(ZigBeeBroadcastDestination.GetBroadcastDestination(BroadcastDestination.BROADCAST_ROUTERS_AND_COORD).Key), duration);
        }

        /// <summary>
        /// Enables or disables devices to join the network.
        /// 
        /// Devices can only join the network when joining is enabled. It is not advised to leave joining enabled permanently
        /// since it allows devices to join the network without the installer knowing.
        ///
        /// <param name="destination">the <see cref="ZigBeeEndpointAddress"/> to send the join request to</param>
        /// <param name="duration">sets the duration of the join enable. Setting this to 0 disables joining. As per ZigBee 3, a value of 255 is not permitted and will be ignored.</param>
        ///            
        /// <returns><see cref="ZigBeeStatus"/> with the status of function</returns>
        /// </summary>
        public ZigBeeStatus PermitJoin(ZigBeeEndpointAddress destination, byte duration)
        {
            if (duration < 0 || duration >= 255)
            {
                Log.Debug("Permit join to {Destination} invalid period of {Duration} seconds.", destination, duration);
                return ZigBeeStatus.INVALID_ARGUMENTS;
            }
            Log.Debug("Permit join to {Destination} for {Duration} seconds.", destination, duration);

            ManagementPermitJoiningRequest command = new ManagementPermitJoiningRequest
            {
                PermitDuration = duration,
                TcSignificance = true,
                DestinationAddress = destination,
                SourceAddress = new ZigBeeEndpointAddress(0)
            };

            SendCommand(command);

            // If this is a broadcast, then we send it to our own address as well
            // This seems to be required for some stacks (eg ZNP)
            if (ZigBeeBroadcastDestination.GetBroadcastDestination(destination.Address) != null)
            {
                command = new ManagementPermitJoiningRequest
                {
                    PermitDuration = duration,
                    TcSignificance = true,
                    DestinationAddress = new ZigBeeEndpointAddress(0),
                    SourceAddress = new ZigBeeEndpointAddress(0)
                };

                SendCommand(command);
            }

            return ZigBeeStatus.SUCCESS;
        }

        /// <summary>
        /// Sends a ZDO Leave Request to a device requesting that an end device leave the network.
        ///
        /// <param name="destinationAddress">the network address to send the request to - this is the device parent or the the device we want to leave.</param>
        ///            
        /// <param name="leaveAddress">the <see cref="IeeeAddress"/> of the end device we want to leave the network</param>
        /// </summary>
        public void Leave(ushort destinationAddress, IeeeAddress leaveAddress)
        {
            ManagementLeaveRequest command = new ManagementLeaveRequest();

            command.setDeviceAddress(leaveAddress);
            command.DestinationAddress = new ZigBeeEndpointAddress(destinationAddress);
            command.SourceAddress = new ZigBeeEndpointAddress(0);
            command.RemoveChildrenRejoin = false;

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
            //                        Log.Debug("{}: No node found after successful leave command", leaveAddress);
            //                    }
            //                }
            //                else
            //                {
            //                    Log.Debug("{}: No successful response received to leave command (status code {})",
            //                            leaveAddress, response.getStatusCode());
            //                }
            //            }
            //            catch (InterruptedException | ExecutionException e) {
            //            Log.Debug("Error sending leave command.", e);
            //        }
            //    }
            //}.start();
            //}
        }

        public void AddGroup(ZigBeeGroupAddress group)
        {
            lock (_networkGroups)
            {
                _networkGroups[group.GroupId] = group;
            }
        }

        public void UpdateGroup(ZigBeeGroupAddress group)
        {
            lock (_networkGroups)
            {
                _networkGroups[group.GroupId] = group;
            }
        }

        public ZigBeeGroupAddress GetGroup(ushort groupId)
        {
            lock (_networkGroups)
            {
                return _networkGroups[groupId];
            }
        }

        public void RemoveGroup(ushort groupId)
        {
            lock (_networkGroups)
            {
                _networkGroups.Remove(groupId);
            }
        }

        public List<ZigBeeGroupAddress> GetGroups()
        {
            lock (_networkGroups)
            {
                return _networkGroups.Values.ToList();
            }
        }

        /// <summary>
        /// Adds a <see cref="IZigBeeNetworkNodeListener"/> that will be notified when node information changes
        ///
        /// <param name="networkNodeListener">the <see cref="IZigBeeNetworkNodeListener"/> to add</param>
        /// </summary>
        public void AddNetworkNodeListener(IZigBeeNetworkNodeListener networkNodeListener)
        {
            if (networkNodeListener == null)
            {
                return;
            }

            lock (_nodeListeners)
            {
                _nodeListeners.Add(networkNodeListener);
            }
        }

        /// <summary>
        /// Removes a <see cref="IZigBeeNetworkNodeListener"> that will be notified when node information changes
        ///
        /// <param name="networkNodeListener">the <see cref="IZigBeeNetworkNodeListener"/> to remove</param>
        /// </summary>
        public void RemoveNetworkNodeListener(IZigBeeNetworkNodeListener networkNodeListener)
        {
            lock (_nodeListeners)
            {
                _nodeListeners.Remove(networkNodeListener);
            }
        }

        /// <summary>
        /// Starts a rediscovery on a node. This will send a <see cref="NetworkAddressRequest"/> as a broadcast and will receive
        /// the response to trigger a full discovery.
        ///
        /// <param name="ieeeAddress">the <see cref="IeeeAddress"/> of the node to rediscover</param>
        /// </summary>
        public void RediscoverNode(IeeeAddress address)
        {
            ZigBeeDiscoveryExtension networkDiscoverer = (ZigBeeDiscoveryExtension)GetExtension(typeof(ZigBeeDiscoveryExtension));
            if (networkDiscoverer == null)
            {
                return;
            }
            networkDiscoverer.RediscoverNode(address);
        }

        /// <summary>
        /// Gets a node given the 16 bit network address
        ///
        /// <param name="networkAddress">the 16 bit network address as <see cref="ushort"/></param>
        /// <returns>the <see cref="ZigBeeNode"/> or null if the node with the requested network address was not found</returns>
        /// </summary>
        public ZigBeeNode GetNode(ushort networkAddress)
        {
            lock (_networkNodes)
            {
                foreach (ZigBeeNode node in _networkNodes.Values)
                {
                    if (node.NetworkAddress.Equals(networkAddress))
                    {
                        return node;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Gets a node given the <see cref="IeeeAddress"/>
        ///
        /// <param name="ieeeAddress">the <see cref="IeeeAddress"/></param>
        /// <returns>the <see cref="ZigBeeNode"/> or null if the node was not found</returns>
        /// </summary>
        public ZigBeeNode GetNode(IeeeAddress ieeeAddress)
        {
            bool result = _networkNodes.TryGetValue(ieeeAddress, out ZigBeeNode node);

            return node;
        }

        /// <summary>
        /// Removes a <see cref="ZigBeeNode"/> from the network
        ///
        /// <param name="node">the <see cref="ZigBeeNode"/> to remove - must not be null</param>
        /// </summary>
        public void RemoveNode(ZigBeeNode node)
        {
            if (node == null)
            {
                return;
            }

            Log.Debug("{IeeeAddress}: Node {NetworkAddress} is removed from the network", node.IeeeAddress, node.NetworkAddress);

            _nodeDiscoveryComplete.Remove(node.IeeeAddress);

            lock (_networkNodes)
            {
                // Don't update if the node is not known
                // We especially don't want to notify listeners of a device we removed, that didn't exist!
                if (!_networkNodes.ContainsKey(node.IeeeAddress))
                {
                    return;
                }
                ZigBeeNode removedNode = null;
                _networkNodes.TryRemove(node.IeeeAddress, out removedNode);
            }

            lock (_nodeListeners)
            {
                foreach (IZigBeeNetworkNodeListener listener in _nodeListeners)
                {
                    Task.Run(() =>
                    {
                        listener.NodeRemoved(node);
                    }).ContinueWith((t) =>
                    {
                        Log.Error(t.Exception, "Error");
                    }, TaskContinuationOptions.OnlyOnFaulted);
                }
            }

            node.Shutdown();

            if (NetworkStateSerializer != null)
            {
                NetworkStateSerializer.Serialize(this);
            }
        }

        /// <summary>
        /// Adds a <see cref="ZigBeeNode"/> to the network
        ///
        /// <param name="node">the <see cref="ZigBeeNode"/> to add</param>
        /// </summary>
        public void AddNode(ZigBeeNode node)
        {
            if (node == null)
            {
                return;
            }

            Log.Debug("{IeeeAddress}: Node {NetworkAddress} added to the network", node.IeeeAddress, node.NetworkAddress);

            lock (_networkNodes)
            {
                // Don't add if the node is already known
                // We especially don't want to notify listeners
                if (_networkNodes.ContainsKey(node.IeeeAddress))
                {
                    UpdateNode(node);
                    return;
                }
                _networkNodes[node.IeeeAddress] = node;
            }

            lock (_nodeListeners)
            {
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
                        Log.Error(t.Exception, "Error");
                    }, TaskContinuationOptions.OnlyOnFaulted);
                }
            }

            if (NetworkStateSerializer != null)
            {
                NetworkStateSerializer.Serialize(this);
            }
        }

        /// <summary>
        /// Update a <see cref="ZigBeeNode"/> within the network
        ///
        /// <param name="node">the <see cref="ZigBeeNode"/> to update</param>
        /// </summary>
        public void UpdateNode(ZigBeeNode node)
        {
            if (node == null)
            {
                return;
            }
            Log.Debug("{IeeeAddress}: Node {NetworkAddress} update", node.IeeeAddress, node.NetworkAddress);

            ZigBeeNode currentNode;
            lock (_networkNodes)
            {
                currentNode = _networkNodes[node.IeeeAddress];

                // Return if we don't know this node
                if (currentNode == null)
                {
                    Log.Debug("{IeeeAddress}: Node {NetworkAddress} is not known - can't be updated", node.IeeeAddress, node.NetworkAddress);
                    return;
                }

                // Return if there were no updates
                if (!currentNode.UpdateNode(node))
                {
                    Log.Debug("{IeeeAddress}: Node {NwkAddress} is not updated", node.IeeeAddress, node.NetworkAddress);
                    return;
                }
            }

            bool updated = _nodeDiscoveryComplete.Contains(node.IeeeAddress);
            if (!updated && node.IsDiscovered() || node.IeeeAddress.Equals(LocalIeeeAddress))
            {
                _nodeDiscoveryComplete.Add(node.IeeeAddress);
            }

            lock (_nodeListeners)
            {
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
                        Log.Error(t.Exception, "Here is the error additional text");
                    }, TaskContinuationOptions.OnlyOnFaulted);
                }
            }

            if (NetworkStateSerializer != null)
            {
                NetworkStateSerializer.Serialize(this);
            }
        }

        /// <summary>
        /// Adds a cluster to the list of clusters we will respond to with the <see cref="MatchDescriptorRequest"/>. Adding a
        /// cluster here is only required in order to respond to this request. Typically the application should provide
        /// further support for such clusters.
        ///
        /// <param name="cluster">the supported cluster ID</param>
        /// </summary>
        public void AddSupportedCluster(ushort cluster)
        {
            Log.Debug("Adding supported cluster {Cluster}", cluster);
            if (_clusterMatcher == null)
            {
                _clusterMatcher = new ClusterMatcher(this);
            }

            _clusterMatcher.AddCluster(cluster);
        }

        /// <summary>
        /// Adds a functional extension to the network.
        ///
        /// <param name="extension">the new <see cref="ZigBeeNetworkExtension"/></param>
        /// </summary>
        public void AddExtension(IZigBeeNetworkExtension extension)
        {
            lock (_extensions)
            {
                _extensions.Add(extension);
                extension.ExtensionInitialize(this);

                // If the network is online, start the extension
                if (NetworkState == ZigBeeTransportState.ONLINE)
                {
                    extension.ExtensionStartup();
                }
            }
        }

        /// <summary>
        /// Gets a functional extension that has been registered with the network.
        ///
        /// <param name="<T>"><see cref="ZigBeeNetworkExtension"/></param>
        /// <param name="requestedExtension">the <see cref="ZigBeeNetworkExtension"/> to get</param>
        /// <returns>the requested <see cref="ZigBeeNetworkExtension"/> if it exists, or null</returns>
        /// </summary>
        public IZigBeeNetworkExtension GetExtension(Type requestedExtension)
        {
            foreach (IZigBeeNetworkExtension extensionCheck in _extensions)
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

        public Task<CommandResult> SendTransaction(ZigBeeCommand command, IZigBeeTransactionMatcher responseMatcher)
        {
            ZigBeeTransaction transaction = new ZigBeeTransaction(this);
            return transaction.SendTransaction(command, responseMatcher);
        }
    }
}
