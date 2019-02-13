using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ZigBeeNet.DAO;
using ZigBeeNet.ZDO.Command;
using ZigBeeNet.App.Discovery;
using ZigBeeNet.Logging;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZDO.Field;
using static ZigBeeNet.ZDO.Field.NodeDescriptor;
using ZigBeeNet.Transaction;

namespace ZigBeeNet
{
    /**
     * Defines a ZigBee Node. A node is a physical entity on the network and will
     * contain one or more {@link ZigBeeEndpoint}s.
     *
     */
    public class ZigBeeNode : IZigBeeCommandListener
    {
        private readonly ILog _logger = LogProvider.For<ZigBeeNode>();

        /**
         * The {@link Logger}.
         */
        //private Logger logger = LoggerFactory.getLogger(ZigBeeNode.class);

        public DateTime LastUpdateTime { get; set; }

        /**
         * The extended {@link IeeeAddress} for the node
         */
        public IeeeAddress IeeeAddress { get; set; }

        /**
         * The 16 bit network address for the node
         */
        public ushort NetworkAddress { get; set; }

        /**
         * The {@link NodeDescriptor} for the node
         */
        public NodeDescriptor NodeDescriptor { get; set; } = new NodeDescriptor();

        /**
         * The {@link PowerDescriptor} for the node
         */
        public PowerDescriptor PowerDescriptor { get; set; } = new PowerDescriptor();

        /**
         * The time the node information was last updated. This is set from the mesh update class when it the
         * updates neighbor table, routing table etc.
         */

        /**
         * List of associated devices for the node, specified in a {@link List} {@link Integer}
         */
        public List<ushort> AssociatedDevices { get; set; } = new List<ushort>();

        /**
         * List of neighbors for the node, specified in a {@link NeighborTable}
         */
        public List<NeighborTable> Neighbors { get; set; } = new List<NeighborTable>();

        /**
         * List of routes within the node, specified in a {@link RoutingTable}
         */
        public List<RoutingTable> Routes { get; set; } = new List<RoutingTable>();

        /**
         * List of binding records
         */
        public List<BindingTable> BindingTable { get; set; } = new List<BindingTable>();

        /**
         * List of endpoints this node exposes
         */
        public ConcurrentDictionary<int, ZigBeeEndpoint> Endpoints { get; private set; } = new ConcurrentDictionary<int, ZigBeeEndpoint>();

        /**
         * The node service discoverer that is responsible for the discovery of services, and periodic update or routes and
         * Neighbors
         */
        private ZigBeeNodeServiceDiscoverer _serviceDiscoverer;

        /**
         * The endpoint listeners of the ZigBee network. Registered listeners will be
         * notified of additions, deletions and changes to {@link ZigBeeEndpoint}s.
         */
        private ReadOnlyCollection<IZigBeeNetworkEndpointListener> _endpointListeners = new ReadOnlyCollection<IZigBeeNetworkEndpointListener>(new List<IZigBeeNetworkEndpointListener>());

        /**
         * The {@link ZigBeeNetwork} that manages this node
         */
        private IZigBeeNetwork _network;

        /**
         * Broadcast endpoint definition
         */
        private const int BROADCAST_ENDPOINT = 0xFF;

        public ZigBeeNode()
        {

        }

        /**
         * Constructor
         *
         * @param networkManager the {@link ZigBeeNetworkManager}
         * @param ieeeAddress the {@link IeeeAddress} of the node
         * @throws {@link IllegalArgumentException} if ieeeAddress is null
         */
        public ZigBeeNode(IZigBeeNetwork network, IeeeAddress ieeeAddress)
        {
            this._network = network;
            this.IeeeAddress = ieeeAddress ?? throw new ArgumentException("IeeeAddress can't be null when creating ZigBeeNode");
            //this._serviceDiscoverer = new ZigBeeNodeServiceDiscoverer(networkManager, this);

            network.AddCommandListener(this);
        }

        public void Shutdown()
        {

        }

        /**
         * Enables or disables nodes to join to this node.
         * <p>
         * Nodes can only join the network when joining is enabled. It is not advised to leave joining enabled permanently
         * since it allows nodes to join the network without the installer knowing.
         *
         * @param duration sets the duration of the join enable. Setting this to 0 disables joining. Setting to a value
         *            greater than 255 seconds will permanently enable joining.
         */
        public void PermitJoin(byte duration)
        {
            ManagementPermitJoiningRequest command = new ManagementPermitJoiningRequest();

            if (duration > 255)
            {
                command.PermitDuration = 255;
            }
            else
            {
                command.PermitDuration = duration;
            }

            command.TcSignificance = true;
            command.DestinationAddress = new ZigBeeEndpointAddress(0);
            command.SourceAddress = new ZigBeeEndpointAddress(0);

            _network.SendTransaction(command);
        }

        /**
         * Enables or disables nodes to join to this node.
         * <p>
         * Nodes can only join the network when joining is enabled. It is not advised to leave joining enabled permanently
         * since it allows nodes to join the network without the installer knowing.
         *
         * @param enable if true joining is enabled, otherwise it is disabled
         */
        public void PermitJoin(bool enable)
        {
            if (enable)
            {
                PermitJoin(0xFF);
            }
            else
            {
                PermitJoin(0);
            }
        }

        /**
         * Returns true if the node is a Full Function Device. Returns false if not an FFD or logical type is unknown.
         * <p>
         * A FFD (Full Function Device) is a device that has full levels of functionality.
         * It can be used for sending and receiving data, but it can also route data from other nodes.
         * FFDs are Coordinators and Routers
         *
         * @return true if the device is a Full Function Device. Returns false if not an FFD or logical type is unknown.
         */
        public bool IsFullFuntionDevice()
        {
            if (NodeDescriptor == null)
            {
                return false;
            }
            return NodeDescriptor.MacCapabilities.Contains(MacCapabilitiesType.FULL_FUNCTION_DEVICE);
        }

        /**
         * Returns true if the node is a Reduced Function Device. Returns false if not an RFD or logical type is unknown.
         * <p>
         * An RFD (Reduced Function Device) is a device that has a reduced level of functionality.
         * Typically it is an end node which may be typically a sensor or switch. RFDs can only talk to FFDs
         * as they contain no routing functionality. These devices can be very low power devices because they
         * do not need to route other traffic and they can be put into a sleep mode when they are not in use.
         *
         * @return true if the device is a Reduced Function Device
         */
        public bool IsReducedFuntionDevice {
            get {
                if (NodeDescriptor == null)
                {
                    return false;
                }
                return NodeDescriptor.MacCapabilities.Contains(MacCapabilitiesType.REDUCED_FUNCTION_DEVICE);
            }
        }

        public bool IsSecurityCapable {
            get {
                if (NodeDescriptor == null)
                {
                    return false;
                }
                return NodeDescriptor.MacCapabilities.Contains(MacCapabilitiesType.SECURITY_CAPABLE);
            }
        }

        public bool IsPrimaryTrustCenter {
            get {
                if (NodeDescriptor == null)
                {
                    return false;
                }
                return NodeDescriptor.ServerCapabilities.Contains(ServerCapabilitiesType.PRIMARY_TRUST_CENTER);
            }
        }

        /**
         * Gets the {@link LogicalType} of the node.
         * 
         * Possible types are -:
         * 
         * {@link LogicalType#COORDINATOR}
         * {@link LogicalType#ROUTER}
         * {@link LogicalType#END_DEVICE}
         * 
         *
         * @return the {@link LogicalType} of the node
         */
        public LogicalType LogicalType {
            get {
                return NodeDescriptor.LogicalNodeType;
            }
        }

        private void SetBindingTable(List<BindingTable> bindingTable)
        {
            lock (BindingTable)
            {
                BindingTable.Clear();
                BindingTable.AddRange(bindingTable);
                _logger.Debug("{Address}: Binding table updated: {BindingTable}", IeeeAddress, bindingTable);
            }
        }

        /**
         * Request an update of the binding table for this node.
         * 
         * This method returns a future to a bool. Upon success the caller should call {@link #getBindingTable()}
         *
         * @return {@link Future} returning a {@link Boolean}
         */
        public async Task<bool> UpdateBindingTable()
        {
            byte index = 0;
            int tableSize = 0;
            List<BindingTable> bindingTable = new List<BindingTable>();

            do
            {
                ManagementBindRequest bindingRequest = new ManagementBindRequest();
                bindingRequest.DestinationAddress = new ZigBeeEndpointAddress(NetworkAddress);
                bindingRequest.StartIndex = index;

                CommandResult result = await _network.SendTransaction(bindingRequest, new ManagementBindRequest());

                if (result.IsError())
                {
                    return false;
                }

                ManagementBindResponse response = (ManagementBindResponse)result.GetResponse();
                if (response.StartIndex == index)
                {
                    tableSize = response.BindingTableEntries;
                    index += (byte)response.BindingTableList.Count;
                    bindingTable.AddRange(response.BindingTableList);
                }
            } while (index < tableSize);

            SetBindingTable(bindingTable);
            return true;
        }

        /**
         * Gets an endpoint given the {@link ZigBeeAddress} address.
         *
         * @param endpointId the endpoint ID to get
         * @return the {@link ZigBeeEndpoint}
         */
        public ZigBeeEndpoint GetEndpoint(byte endpointId)
        {
            lock (Endpoints)
            {
                return Endpoints[endpointId];
            }
        }

        /**
         * Adds an endpoint to the node
         *
         * @param endpoint the {@link ZigBeeEndpoint} to add
         */
        public void AddEndpoint(ZigBeeEndpoint endpoint)
        {
            //lock (Endpoints)
            //{
            Endpoints.AddOrUpdate(endpoint.EndpointId, endpoint, (_, __) => endpoint);
            //}

            lock (_endpointListeners)
            {
                foreach (IZigBeeNetworkEndpointListener listener in _endpointListeners)
                {
                    Task.Run(() =>
                    {
                        listener.DeviceAdded(endpoint);
                    }).ContinueWith((t) =>
                    {
                        _logger.Error(t.Exception, "Error");
                    }, TaskContinuationOptions.OnlyOnFaulted);
                }
            }
        }

        /**
         * Updates an endpoint information in the node
         *
         * @param endpoint the {@link ZigBeeEndpoint} to update
         */
        public void UpdateEndpoint(ZigBeeEndpoint endpoint)
        {
            lock (Endpoints)
            {
                Endpoints[endpoint.EndpointId] = endpoint;
            }
            lock (_endpointListeners)
            {
                foreach (IZigBeeNetworkEndpointListener listener in _endpointListeners)
                {
                    Task.Run(() =>
                    {
                        listener.DeviceUpdated(endpoint);
                    }).ContinueWith((t) =>
                    {
                        _logger.Error(t.Exception, "Error");
                    }, TaskContinuationOptions.OnlyOnFaulted);
                }
            }
        }

        /**
         * Removes endpoint by network address.
         *
         * @param endpointId the network address
         */
        public void RemoveEndpoint(byte endpointId)
        {
            ZigBeeEndpoint endpoint;
            lock (Endpoints)
            {
                Endpoints.TryRemove(endpointId, out endpoint);
            }
            lock (_endpointListeners)
            {
                if (endpoint != null)
                {
                    foreach (IZigBeeNetworkEndpointListener listener in _endpointListeners)
                    {
                        Task.Run(() =>
                        {
                            listener.DeviceRemoved(endpoint);
                        }).ContinueWith((t) =>
                        {
                            _logger.Error(t.Exception, "Error");
                        }, TaskContinuationOptions.OnlyOnFaulted);
                    }
                }
            }
        }

        public void AddNetworkEndpointListener(IZigBeeNetworkEndpointListener networkDeviceListener)
        {
            lock (_endpointListeners)
            {
                List<IZigBeeNetworkEndpointListener> modifiedListeners = new List<IZigBeeNetworkEndpointListener>(_endpointListeners);
                modifiedListeners.Add(networkDeviceListener);
                _endpointListeners = new ReadOnlyCollection<IZigBeeNetworkEndpointListener>(modifiedListeners);
            }
        }

        public void RemoveNetworkEndpointListener(IZigBeeNetworkEndpointListener networkDeviceListener)
        {
            lock (_endpointListeners)
            {
                List<IZigBeeNetworkEndpointListener> modifiedListeners = new List<IZigBeeNetworkEndpointListener>(_endpointListeners);
                modifiedListeners.Remove(networkDeviceListener);
                _endpointListeners = new ReadOnlyCollection<IZigBeeNetworkEndpointListener>(modifiedListeners);
            }
        }


        public void CommandReceived(ZigBeeCommand command)
        {
            // This gets called for all received commands
            // Check if it's our address
            if (command.SourceAddress.Address != NetworkAddress)
            {
                return;
            }

            if (!(command is ZclCommand))
            {
                return;
            }

            ZclCommand zclCommand = (ZclCommand)command;
            ZigBeeEndpointAddress endpointAddress = (ZigBeeEndpointAddress)zclCommand.SourceAddress;

            if (endpointAddress.Endpoint == BROADCAST_ENDPOINT)
            {
                foreach (ZigBeeEndpoint endpoint in Endpoints.Values)
                {
                    endpoint.CommandReceived(zclCommand);
                }
            }
            else if (Endpoints.TryGetValue(endpointAddress.Endpoint, out ZigBeeEndpoint endpoint))
            {
                endpoint.CommandReceived(zclCommand);
            }
        }

        /**
         * Starts service discovery for the node.
         */
        //public void StartDiscovery()
        //{
        //    List<NodeDiscoveryTask> tasks = new List<NodeDiscoveryTask>();

        //    // Always request the network address - in case it's changed
        //    tasks.Add(NodeDiscoveryTask.NWK_ADDRESS);

        //    if (NodeDescriptor.LogicalNodeType == LogicalType.UNKNOWN)
        //    {
        //        tasks.Add(NodeDiscoveryTask.NODE_DESCRIPTOR);
        //    }

        //    if (PowerDescriptor.CurrentPowerMode == CurrentPowerModeType.UNKNOWN)
        //    {
        //        tasks.Add(NodeDiscoveryTask.POWER_DESCRIPTOR);
        //    }

        //    if (Endpoints.Count == 0 && NetworkAddress != 0)
        //    {
        //        tasks.Add(NodeDiscoveryTask.ACTIVE_ENDPOINTS);
        //    }

        //    tasks.Add(NodeDiscoveryTask.NEIGHBORS);

        //    _serviceDiscoverer.StartDiscovery(tasks);
        //}

        /**
         * Starts service discovery for the node in order to update the mesh
         */
        //public void UpdateMesh()
        //{
        //    List<NodeDiscoveryTask> tasks = new List<NodeDiscoveryTask>();

        //    tasks.Add(NodeDiscoveryTask.NEIGHBORS);

        //    if (NodeDescriptor.LogicalNodeType != LogicalType.END_DEVICE)
        //    {
        //        tasks.Add(NodeDiscoveryTask.ROUTES);
        //    }

        //    _serviceDiscoverer.StartDiscovery(tasks);
        //}

        /**
         * Checks if basic device discovery is complete.
         *
         * @return true if basic device information is known
         */
        public bool IsDiscovered()
        {
            return NodeDescriptor.LogicalNodeType != LogicalType.UNKNOWN && Endpoints.Count != 0;
        }

        public void UpdateNetworkManager(ZigBeeNetworkManager networkManager)
        {
            _network = networkManager;
        }

        /**
         * Updates the node. This will copy data from another node into this node. Updated elements are checked for equality
         * and the method will only return true if the node data has been changed.
         *
         * @param node the {@link ZigBeeNode} that contains the newer node data.
         * @return true if there were changes made as a result of the update
         */
        public bool UpdateNode(ZigBeeNode node)
        {
            if (!node.IeeeAddress.Equals(IeeeAddress))
            {
                return false;
            }

            bool updated = false;

            if (!NetworkAddress.Equals(node.NetworkAddress))
            {
                updated = true;
                NetworkAddress = node.NetworkAddress;
            }

            if (!NodeDescriptor.Equals(node.NodeDescriptor))
            {
                updated = true;
                NodeDescriptor = node.NodeDescriptor;
            }

            if (!PowerDescriptor.Equals(node.PowerDescriptor))
            {
                updated = true;
                PowerDescriptor = node.PowerDescriptor;
            }

            lock (AssociatedDevices)
            {
                if (!AssociatedDevices.Equals(node.AssociatedDevices))
                {
                    updated = true;
                    AssociatedDevices.Clear();
                    AssociatedDevices.AddRange(node.AssociatedDevices);
                }
            }

            lock (BindingTable)
            {
                if (!BindingTable.Equals(node.BindingTable))
                {
                    updated = true;
                    BindingTable.Clear();
                    BindingTable.AddRange(node.BindingTable);
                }
            }

            lock (Neighbors)
            {
                if (!Neighbors.Equals(node.Neighbors))
                {
                    updated = true;
                    Neighbors.Clear();
                    Neighbors.AddRange(node.Neighbors);
                }
            }

            lock (Routes)
            {
                if (!Routes.Equals(node.Routes))
                {
                    updated = true;
                    Routes.Clear();
                    Routes.AddRange(node.Routes);
                }
            }

            // TODO: How to deal with endpoints

            return updated;
        }

        /**
         * Gets a {@link ZigBeeNodeDao} representing the node
         *
         * @return the {@link ZigBeeNodeDao}
         */
        public ZigBeeNodeDao GetDao()
        {
            ZigBeeNodeDao dao = new ZigBeeNodeDao();

            dao.IeeeAddress = IeeeAddress.ToString();
            dao.NetworkAddress = NetworkAddress;
            dao.NodeDescriptor = NodeDescriptor;
            dao.PowerDescriptor = PowerDescriptor;
            dao.BindingTable = BindingTable;

            List<ZigBeeEndpointDao> endpointDaoList = new List<ZigBeeEndpointDao>();
            foreach (ZigBeeEndpoint endpoint in Endpoints.Values)
            {
                endpointDaoList.Add(endpoint.GetDao());
            }
            dao.Endpoints = endpointDaoList;

            return dao;
        }

        public void SetDao(ZigBeeNodeDao dao)
        {
            IeeeAddress = new IeeeAddress(dao.IeeeAddress);
            NetworkAddress = dao.NetworkAddress;
            NodeDescriptor = dao.NodeDescriptor;
            PowerDescriptor = dao.PowerDescriptor;
            if (dao.BindingTable != null)
            {
                BindingTable.AddRange(dao.BindingTable);
            }

            foreach (ZigBeeEndpointDao endpointDao in dao.Endpoints)
            {
                ZigBeeEndpoint endpoint = new ZigBeeEndpoint(this, endpointDao.EndpointId);
                endpoint.SetDao(endpointDao);
                Endpoints[endpoint.EndpointId] = endpoint;
            }
        }

        /// <summary>
        /// Sends ZigBee command without waiting for response.
        /// </summary>
        /// <param name="command"></param>
        public void SendTransaction(ZigBeeCommand command)
        {
            _network.SendTransaction(command);
        }

        public async Task<CommandResult> SendTransaction(ZigBeeCommand command, IZigBeeTransactionMatcher responseMatcher)
        {
            return await _network.SendTransaction(command, responseMatcher);
        }

        public override string ToString()
        {
            if (NodeDescriptor == null)
            {
                return "ZigBeeNode [IEEE=" + IeeeAddress + ", NWK=0x" + NetworkAddress.ToString("X4") + "]";
            }

            return "ZigBeeNode [IEEE=" + IeeeAddress + ", NWK=0x" + NetworkAddress.ToString("X4") + ", Type=" + NodeDescriptor.LogicalNodeType + "]";
        }
    }
}
