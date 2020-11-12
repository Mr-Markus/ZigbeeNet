﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ZigBeeNet.Database;
using ZigBeeNet.ZDO.Command;
using ZigBeeNet.App.Discovery;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZDO.Field;
using static ZigBeeNet.ZDO.Field.NodeDescriptor;
using ZigBeeNet.Transaction;
using System.Linq;
using Serilog;

namespace ZigBeeNet
{
    /// <summary>
    /// Defines a ZigBee Node. A node is a physical entity on the network and will
    /// contain one or more <see cref="ZigBeeEndpoint">s.
    ///
    /// </summary>
    public class ZigBeeNode : IZigBeeCommandListener
    {
        /// <summary>
        /// Gets the current state for the node
        /// </summary>
        public ZigBeeNodeState NodeState { get; private set; } = ZigBeeNodeState.UNKNOWN;

        /// <summary>
        /// The <see cref="Logger">.
        /// </summary>
        //private Logger logger = LoggerFactory.getLogger(ZigBeeNode.class);

        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// The extended <see cref="IeeeAddress"> for the node
        /// </summary>
        public IeeeAddress IeeeAddress { get; set; }

        /// <summary>
        /// The 16 bit network address for the node
        /// </summary>
        public ushort NetworkAddress { get; set; }

        /// <summary>
        /// The <see cref="NodeDescriptor"> for the node
        /// </summary>
        public NodeDescriptor NodeDescriptor { get; set; }

        /// <summary>
        /// The <see cref="PowerDescriptor"> for the node
        /// </summary>
        public PowerDescriptor PowerDescriptor { get; set; }

        private HashSet<ushort> _associatedDevices = new HashSet<ushort>();
        /// <summary>
        /// List of associated devices for the node, specified in a <see cref="List"> <see cref="Integer">
        /// </summary>
        public ReadOnlyCollection<ushort> AssociatedDevices
        {
            get
            {
                return new ReadOnlyCollection<ushort>(_associatedDevices.ToList());
            }
        }

        private HashSet<NeighborTable> _neighbors = new HashSet<NeighborTable>();
        /// <summary>
        /// List of neighbors for the node, specified in a <see cref="NeighborTable">
        /// </summary>
        public ReadOnlyCollection<NeighborTable> Neighbors
        {
            get
            {
                return new ReadOnlyCollection<NeighborTable>(_neighbors.ToList());
            }
        }

        private HashSet<RoutingTable> _routes = new HashSet<RoutingTable>();
        /// <summary>
        /// List of routes within the node, specified in a <see cref="RoutingTable">
        /// </summary>
        public ReadOnlyCollection<RoutingTable> Routes
        {
            get
            {
                return new ReadOnlyCollection<RoutingTable>(_routes.ToList());
            }
        }

        private readonly HashSet<BindingTable> _bindingTable = new HashSet<BindingTable>();
        /// <summary>
        /// List of binding records
        /// </summary>
        public ReadOnlyCollection<BindingTable> BidndingTable
        {
            get
            {
                return new ReadOnlyCollection<BindingTable>(_bindingTable.ToList());
            }
        }

        private readonly ConcurrentDictionary<int, ZigBeeEndpoint> _endpoints = new ConcurrentDictionary<int, ZigBeeEndpoint>();
        /// <summary>
        /// List of endpoints this node exposes
        /// </summary>
        public ReadOnlyDictionary<int, ZigBeeEndpoint> Endpoints
        {
            get
            {
                return new ReadOnlyDictionary<int, ZigBeeEndpoint>(_endpoints);
            }
        }

        /// <summary>
        /// The endpoint listeners of the ZigBee network. Registered listeners will be
        /// notified of additions, deletions and changes to <see cref="ZigBeeEndpoint">s.
        /// </summary>
        private ReadOnlyCollection<IZigBeeNetworkEndpointListener> _endpointListeners = new ReadOnlyCollection<IZigBeeNetworkEndpointListener>(new List<IZigBeeNetworkEndpointListener>());

        /// <summary>
        /// The <see cref="ZigBeeNetwork"> that manages this node
        /// </summary>
        private IZigBeeNetwork _network;

        public ZigBeeNode()
        {

        }

        /// <summary>
        /// Constructor
        /// <param name="network">the <see cref="IZigBeeNetwork"></param>
        /// <param name="ieeeAddress">the <see cref="IeeeAddress"> of the node</param>
        /// </summary>
        public ZigBeeNode(IZigBeeNetwork network, IeeeAddress ieeeAddress)
        {
            this._network = network;
            this.IeeeAddress = ieeeAddress ?? throw new ArgumentException("IeeeAddress can't be null when creating ZigBeeNode");
            //this._serviceDiscoverer = new ZigBeeNodeServiceDiscoverer(networkManager, this);
        }

        /// <summary>
        /// Constructor
        ///
        /// <param name="networkr">the <see cref="IZigBeeNetwork"></param>
        /// <param name="ieeeAddress">the <see cref="IeeeAddress"> of the node</param>
        /// <param name="networkAddress">the network address of the node</param>
        /// </summary>
        public ZigBeeNode(IZigBeeNetwork network, IeeeAddress ieeeAddress, ushort networkAddress)
            : this(network, ieeeAddress)
        {
            NetworkAddress = networkAddress;
        }

        public void Shutdown()
        {

        }

        /// <summary>
        /// Enables or disables nodes to join to this node.
        /// <p>
        /// Nodes can only join the network when joining is enabled. It is not advised to leave joining enabled permanently
        /// since it allows nodes to join the network without the installer knowing.
        ///
        /// <param name="duration">sets the duration of the join enable. Setting this to 0 disables joining. Setting to a value</param>
        ///            greater than 255 seconds will permanently enable joining.
        /// </summary>
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

        /// <summary>
        /// Enables or disables nodes to join to this node.
        /// <p>
        /// Nodes can only join the network when joining is enabled. It is not advised to leave joining enabled permanently
        /// since it allows nodes to join the network without the installer knowing.
        ///
        /// <param name="enable">if true joining is enabled, otherwise it is disabled</param>
        /// </summary>
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

        /// <summary>
        /// Returns true if the node is a Full Function Device. Returns false if not an FFD or logical type is unknown.
        /// <p>
        /// A FFD (Full Function Device) is a device that has full levels of functionality.
        /// It can be used for sending and receiving data, but it can also route data from other nodes.
        /// FFDs are Coordinators and Routers
        ///
        /// <returns>true if the device is a Full Function Device. Returns false if not an FFD or logical type is unknown.</returns>
        /// </summary>
        public bool IsFullFunctionDevice()
        {
            if (NodeDescriptor == null)
            {
                return false;
            }
            return NodeDescriptor.MacCapabilities.Contains(MacCapabilitiesType.FULL_FUNCTION_DEVICE);
        }

        /// <summary>
        /// Returns true if the node is a Reduced Function Device. Returns false if not an RFD or logical type is unknown.
        /// <p>
        /// An RFD (Reduced Function Device) is a device that has a reduced level of functionality.
        /// Typically it is an end node which may be typically a sensor or switch. RFDs can only talk to FFDs
        /// as they contain no routing functionality. These devices can be very low power devices because they
        /// do not need to route other traffic and they can be put into a sleep mode when they are not in use.
        ///
        /// <returns>true if the device is a Reduced Function Device</returns>
        /// </summary>
        public bool IsReducedFuntionDevice
        {
            get
            {
                if (NodeDescriptor == null)
                {
                    return false;
                }
                return NodeDescriptor.MacCapabilities.Contains(MacCapabilitiesType.REDUCED_FUNCTION_DEVICE);
            }
        }

        public bool IsSecurityCapable
        {
            get
            {
                if (NodeDescriptor == null)
                {
                    return false;
                }
                return NodeDescriptor.MacCapabilities.Contains(MacCapabilitiesType.SECURITY_CAPABLE);
            }
        }

        public bool IsPrimaryTrustCenter
        {
            get
            {
                if (NodeDescriptor == null)
                {
                    return false;
                }
                return NodeDescriptor.ServerCapabilities.Contains(ServerCapabilitiesType.PRIMARY_TRUST_CENTER);
            }
        }

        /// <summary>
        /// Gets the <see cref="LogicalType"> of the node.
        /// 
        /// Possible types are -:
        /// 
        /// {@link LogicalType#COORDINATOR}
        /// {@link LogicalType#ROUTER}
        /// {@link LogicalType#END_DEVICE}
        /// {@link LogicalType#UNKNOWN}
        ///
        /// <returns>the <see cref="LogicalType"> of the node</returns>
        /// </summary>
        public LogicalType LogicalType
        {
            get
            {
                if (NodeDescriptor == null)
                    return LogicalType.UNKNOWN;
                return NodeDescriptor.LogicalNodeType;
            }
        }

        private void SetBindingTable(HashSet<BindingTable> bindingTable)
        {
            lock (_bindingTable)
            {
                _bindingTable.Clear();
                _bindingTable.UnionWith(bindingTable);
                Log.Debug("{Address}: Binding table updated: {BindingTable}", IeeeAddress, bindingTable);
            }
        }

        /// <summary>
        /// Request an update of the binding table for this node.
        /// 
        /// This method returns a future to a bool. Upon success the caller should call {@link #getBindingTable()}
        ///
        /// <returns><see cref="Future"> returning a <see cref="Boolean"></returns>
        /// </summary>
        public async Task<ZigBeeStatus> UpdateBindingTable()
        {
            byte index = 0;
            int tableSize = 0;
            HashSet<BindingTable> bindingTable = new HashSet<BindingTable>();

            do
            {
                ManagementBindRequest bindingRequest = new ManagementBindRequest();
                bindingRequest.DestinationAddress = new ZigBeeEndpointAddress(NetworkAddress);
                bindingRequest.StartIndex = index;

                CommandResult result = await _network.SendTransaction(bindingRequest, new ManagementBindRequest());

                if (result.IsTimeout())
                {
                    return ZigBeeStatus.FAILURE;
                }

                ManagementBindResponse response = (ManagementBindResponse)result.GetResponse();
                if (response.StartIndex == index)
                {
                    tableSize = response.BindingTableEntries;
                    index += (byte)response.BindingTableList.Count;
                    bindingTable.UnionWith(response.BindingTableList);
                }
            } while (index < tableSize);

            SetBindingTable(bindingTable);
            return ZigBeeStatus.SUCCESS;
        }

        /// <summary>
        /// Gets an endpoint given the <see cref="ZigBeeAddress"> address.
        ///
        /// <param name="endpointId">the endpoint ID to get</param>
        /// <returns>the <see cref="ZigBeeEndpoint"></returns>
        /// </summary>
        public ZigBeeEndpoint GetEndpoint(byte endpointId)
        {
            if (_endpoints.TryGetValue(endpointId, out ZigBeeEndpoint endpoint))
            {
                return endpoint;
            }

            return null;
        }

        /// <summary>
        /// Gets all endpoint.
        /// <returns>A <see cref="IReadOnlyCollection{ZigBeeEndpoint}"/> of <see cref="ZigBeeEndpoint">s</returns>
        /// </summary>
        public IReadOnlyCollection<ZigBeeEndpoint> GetEndpoints()
        {
            return new ReadOnlyCollection<ZigBeeEndpoint>(_endpoints.Values.ToList());
        }

        /// <summary>
        /// Adds an endpoint to the node
        ///
        /// <param name="endpoint">the <see cref="ZigBeeEndpoint"> to add</param>
        /// </summary>
        public void AddEndpoint(ZigBeeEndpoint endpoint)
        {
            //lock (Endpoints)
            //{
            _endpoints.AddOrUpdate(endpoint.EndpointId, endpoint, (_, __) => endpoint);
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
                        Log.Error(t.Exception, "Error: {Exception}");
                    }, TaskContinuationOptions.OnlyOnFaulted);
                }
            }
        }

        /// <summary>
        /// Updates an endpoint information in the node
        ///
        /// <param name="endpoint">the <see cref="ZigBeeEndpoint"> to update</param>
        /// </summary>
        public void UpdateEndpoint(ZigBeeEndpoint endpoint)
        {

            _endpoints[endpoint.EndpointId] = endpoint;

            lock (_endpointListeners)
            {
                foreach (IZigBeeNetworkEndpointListener listener in _endpointListeners)
                {
                    Task.Run(() =>
                    {
                        listener.DeviceUpdated(endpoint);
                    }).ContinueWith((t) =>
                    {
                        Log.Error(t.Exception, "Error: {Exception}");
                    }, TaskContinuationOptions.OnlyOnFaulted);
                }
            }
        }

        /// <summary>
        /// Removes endpoint by network address.
        ///
        /// <param name="endpointId">the network address</param>
        /// </summary>
        public void RemoveEndpoint(byte endpointId)
        {
            _endpoints.TryRemove(endpointId, out ZigBeeEndpoint endpoint);

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
                            Log.Error(t.Exception, "Error: {Exception}");
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

            if (_endpoints.TryGetValue(endpointAddress.Endpoint, out ZigBeeEndpoint endpoint))
            {
                endpoint.CommandReceived(zclCommand);
            }
        }

       

        /// <summary>
        /// Checks if basic device discovery is complete.
        ///
        /// <returns>true if basic device information is known</returns>
        /// </summary>
        public bool IsDiscovered()
        {
            return NodeDescriptor != null && NodeDescriptor.LogicalNodeType != LogicalType.UNKNOWN && _endpoints.Count != 0;
        }

        public void UpdateNetworkManager(ZigBeeNetworkManager networkManager)
        {
            _network = networkManager;
        }

        /// <summary>
        /// Updates the node. This will copy data from another node into this node. Updated elements are checked for equality
        /// and the method will only return true if the node data has been changed.
        ///
        /// <param name="node">the <see cref="ZigBeeNode"> that contains the newer node data.</param>
        /// <returns>true if there were changes made as a result of the update</returns>
        /// </summary>
        public bool UpdateNode(ZigBeeNode node)
        {
            if (!node.IeeeAddress.Equals(IeeeAddress))
            {
                Log.Debug("{IeeeAddress}: Ieee address inconsistent during update <>{NodeIeeeAddress}", IeeeAddress, node.IeeeAddress);
                return false;
            }

            bool updated = false;

            if (node.NodeState != ZigBeeNodeState.UNKNOWN && NodeState != node.NodeState)
            {
                Log.Debug("{IeeeAddress}: Node state updated from {NodeState} to {NewNodeState}", IeeeAddress, NodeState, node.NodeState);
                NodeState = node.NodeState;
                updated = true;
            }

            if (NetworkAddress != 0 && !NetworkAddress.Equals(node.NetworkAddress))
            {
                Log.Debug("{IeeeAddress}: Network address updated from {NetworkAddress} to {NodeNetworkAddress}", IeeeAddress, NetworkAddress, node.NetworkAddress);
                updated = true;
                NetworkAddress = node.NetworkAddress;
            }

            if (node.NodeDescriptor != null && (NodeDescriptor == null || !NodeDescriptor.Equals(node.NodeDescriptor)))
            {
                Log.Debug("{IeeeAddress}: Node descriptor updated", IeeeAddress);
                updated = true;
                NodeDescriptor = node.NodeDescriptor;
            }

            if (node.PowerDescriptor != null && (PowerDescriptor == null || !PowerDescriptor.Equals(node.PowerDescriptor)))
            {
                Log.Debug("{IeeeAddress}: Power descriptor updated", IeeeAddress);
                updated = true;
                PowerDescriptor = node.PowerDescriptor;
            }

            lock (_associatedDevices)
            {
                if (!_associatedDevices.SetEquals(node._associatedDevices))
                {
                    Log.Debug("{IeeeAddress}: Associated devices updated", IeeeAddress);
                    updated = true;
                    _associatedDevices.Clear();
                    _associatedDevices.UnionWith(node._associatedDevices);
                }
            }

            lock (_bindingTable)
            {
                if (!_bindingTable.SetEquals(node._bindingTable))
                {
                    Log.Debug("{IeeeAddress}: Binding table updated", IeeeAddress);
                    updated = true;
                    _bindingTable.Clear();
                    _bindingTable.UnionWith(node._bindingTable);
                }
            }

            lock (_neighbors)
            {
                if (!_neighbors.SetEquals(node._neighbors))
                {
                    Log.Debug("{IeeeAddress}: Neighbors updated", IeeeAddress);
                    updated = true;
                    _neighbors.Clear();
                    _neighbors.UnionWith(node._neighbors);
                }
            }

            lock (_routes)
            {
                if (!_routes.SetEquals(node._routes))
                {
                    Log.Debug("{IeeeAddress}: Routes updated", IeeeAddress);
                    updated = true;
                    _routes.Clear();
                    _routes.UnionWith(node._routes);
                }
            }

            // Endpoints are only copied over if they don't exist in the node
            // The assumption here is that endpoints are only set once, and not changed.
            // This should be valid as they are set through the SimpleDescriptor.
            foreach (var endpoint in node._endpoints)
            {
                if (_endpoints.ContainsKey(endpoint.Key))
                {
                    continue;
                }
                Log.Debug("{IeeeAddress}: Endpoint {EndpointId} added", IeeeAddress, endpoint.Key);
                updated = true;
                _endpoints[endpoint.Key] = endpoint.Value;
            }

            return updated;
        }

        /// <summary>
        /// Gets a <see cref="ZigBeeNodeDao"> representing the node
        ///
        /// <returns>the <see cref="ZigBeeNodeDao"></returns>
        /// </summary>
        public ZigBeeNodeDao GetDao()
        {
            ZigBeeNodeDao dao = new ZigBeeNodeDao();

            dao.IeeeAddress = IeeeAddress;
            dao.NetworkAddress = NetworkAddress;
            dao.NodeDescriptor = NodeDescriptor;
            dao.PowerDescriptor = PowerDescriptor;
            dao.BindingTable = _bindingTable;

            List<ZigBeeEndpointDao> endpointDaoList = new List<ZigBeeEndpointDao>();
            foreach (ZigBeeEndpoint endpoint in _endpoints.Values)
            {
                endpointDaoList.Add(endpoint.GetDao());
            }
            dao.Endpoints = endpointDaoList;

            return dao;
        }

        public void SetDao(ZigBeeNodeDao dao)
        {
            IeeeAddress = dao.IeeeAddress;
            NetworkAddress = dao.NetworkAddress;
            NodeDescriptor = dao.NodeDescriptor;
            PowerDescriptor = dao.PowerDescriptor;
            if (dao.BindingTable != null)
            {
                _bindingTable.UnionWith(dao.BindingTable);
            }

            foreach (ZigBeeEndpointDao endpointDao in dao.Endpoints)
            {
                ZigBeeEndpoint endpoint = new ZigBeeEndpoint(this, endpointDao.EndpointId);
                endpoint.SetDao(endpointDao);
                _endpoints[endpoint.EndpointId] = endpoint;
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

        public Task<CommandResult> SendTransaction(ZigBeeCommand command, IZigBeeTransactionMatcher responseMatcher)
        {
            return _network.SendTransaction(command, responseMatcher);
        }

        public override string ToString()
        {
            if (NodeDescriptor == null)
            {
                return "ZigBeeNode [state=" + NodeState + ", IEEE=" + IeeeAddress + ", NWK=0x" + NetworkAddress.ToString("X4") + "]";
            }

            return "ZigBeeNode [IEEE=" + IeeeAddress + ", NWK=0x" + NetworkAddress.ToString("X4") + ", Type=" + NodeDescriptor.LogicalNodeType + "]";
        }

        public bool SetNodeState(ZigBeeNodeState state)
        {
            if (NodeState.Equals(state))
            {
                return false;
            }
            Log.Debug("{IeeeAddress}: Node state updated from {oldState} to {newState}", IeeeAddress, NodeState, state);

            NodeState = state;
            return true;
        }

        internal void SetAssociatedDevices(HashSet<ushort> associatedDevices)
        {
            _associatedDevices = associatedDevices;
        }

        internal void SetNeighbors(HashSet<NeighborTable> neighbors)
        {
            _neighbors = neighbors;
        }

        internal void SetRoutes(HashSet<RoutingTable> routes)
        {
            _routes = routes;
        }

    }
}
