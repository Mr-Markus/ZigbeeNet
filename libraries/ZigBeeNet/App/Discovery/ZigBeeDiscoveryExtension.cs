using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet;
using ZigBeeNet.App;
using ZigBeeNet.App.Discovery;
using ZigBeeNet.ZDO.Command;
using Serilog;

namespace ZigBeeNet.App.Discovery
{

    /// <summary>
    /// This class implements a <see cref="ZigBeeNetworkExtension"/> to perform network discovery and monitoring.
    /// <p>
    /// The periodic mesh update will periodically request information from the node so as to keep information updated. The
    /// tasks may be specified from the list of <see cref="ZigBeeNodeServiceDiscoverer.NodeDiscoveryTask"/>s, and in general it may be expected to update the
    /// routing information (with <see cref="ZigBeeNodeServiceDiscoverer.NodeDiscoveryTask.ROUTES"/>) and the neighbour information (with
    /// <see cref="ZigBeeNodeServiceDiscoverer.NodeDiscoveryTask.NEIGHBORS"/>). In some networks, it may be advantageous not to update all information as it
    /// may place a high load on the network.
    ///
    /// </summary>
    public class ZigBeeDiscoveryExtension : IZigBeeNetworkExtension, IZigBeeNetworkNodeListener, IZigBeeCommandListener
    {
        /// <summary>
        /// The ZigBee network <see cref="ZigBeeNetworkDiscoverer"/>. The discover is responsible for monitoring the network for 
        /// new devices and the initial interrogation of their capabilities.
        /// </summary>
        private ZigBeeNetworkDiscoverer _networkDiscoverer;

        /// <summary>
        /// Map of <see cref="ZigBeeNetworkDiscoverer"/> for each node
        /// </summary>
        private ConcurrentDictionary<IeeeAddress, ZigBeeNodeServiceDiscoverer> _nodeDiscovery = new ConcurrentDictionary<IeeeAddress, ZigBeeNodeServiceDiscoverer>();

        /// <summary>
        /// Refresh period for the mesh update - in seconds
        /// </summary>
        private int _updatePeriod;

        private Task _futureTask = null;

        private CancellationTokenSource _cancellationTokenSource;

        private ZigBeeNetworkManager _networkManager;

        private bool extensionStarted = false;

        /// <summary>
        /// List of tasks to be completed during a mesh update
        /// </summary>
        public List<ZigBeeNodeServiceDiscoverer.NodeDiscoveryTask> MeshUpdateTasks { get; set; } = new List<ZigBeeNodeServiceDiscoverer.NodeDiscoveryTask>() {
            ZigBeeNodeServiceDiscoverer.NodeDiscoveryTask.NEIGHBORS, ZigBeeNodeServiceDiscoverer.NodeDiscoveryTask.ROUTES };


        public ZigBeeStatus ExtensionInitialize(ZigBeeNetworkManager networkManager)
        {
            _networkManager = networkManager;
            return ZigBeeStatus.SUCCESS;
        }

        public ZigBeeStatus ExtensionStartup()
        {
            if (extensionStarted)
            {
                Log.Debug("DISCOVERY Extension: Already started");
                return ZigBeeStatus.INVALID_STATE;
            }

            Log.Debug("DISCOVERY Extension: Startup");

            _networkManager.AddNetworkNodeListener(this);
            _networkManager.AddCommandListener(this);

            _networkDiscoverer = new ZigBeeNetworkDiscoverer(_networkManager);
            _networkDiscoverer.Startup();

            if (_updatePeriod != 0)
            {
                StartScheduler(10);
            }

            extensionStarted = true;

            return ZigBeeStatus.SUCCESS;
        }

        public void ExtensionShutdown()
        {
            _networkManager.RemoveNetworkNodeListener(this);
            _networkManager.RemoveCommandListener(this);

            StopScheduler();
            _networkDiscoverer?.Shutdown();

            lock (_nodeDiscovery)
            {
                foreach (var nodeDiscoverer in _nodeDiscovery.Values)
                {
                    nodeDiscoverer.StopDiscovery();
                }
            }
            extensionStarted = false;

            Log.Debug("DISCOVERY Extension: Shutdown");
        }

        /// <summary>
        /// Sets the update period for the mesh update service. This is the number of seconds between
        /// subsequent mesh updates. Setting the period to 0 will disable mesh updates.
        ///
        /// <param name="updatePeriod">number of seconds between mesh updates. Setting to 0 will stop updates</param>
        /// </summary>
        public void SetUpdatePeriod(int updatePeriod)
        {
            _updatePeriod = updatePeriod;

            if (!extensionStarted)
            {
                return;
            }

            Log.Debug("DISCOVERY Extension: Set mesh update interval to {UpdatePeriod} seconds", updatePeriod);

            if (updatePeriod == 0)
            {
                StopScheduler();
                return;
            }

            StartScheduler(updatePeriod);
        }

        /// <summary>
        /// Gets the current period at which the mesh data is being updated (in seconds). A return value of 0 indicates that
        /// automatic updates are currently disabled.
        ///
        /// <returns>number of seconds between mesh updates. 0 indicates no automatic updates.</returns>
        ///
        /// </summary>
        public int GetUpdatePeriod()
        {
            return _updatePeriod;
        }

        /// <summary>
        /// Performs an immediate refresh of the network mesh information. Subsequent updates are performed at the current 
        /// update rate, and the timer is restarted from the time of calling this method.
        /// </summary>
        public void Refresh()
        {
            Log.Debug("DISCOVERY Extension: Start mesh update task with interval of {UpdatePeriod} seconds", _updatePeriod);

            // Delay the start slightly to allow any further processing to complete.
            // Also allows successive responses to filter through without retriggering an update.
            StartScheduler(10);
        }

        public void NodeAdded(ZigBeeNode node)
        {
            lock (_nodeDiscovery)
            {
                if (_nodeDiscovery.ContainsKey(node.IeeeAddress))
                {
                    return;
                }

                Log.Debug("{IeeeAddress}: DISCOVERY Extension: Adding discoverer for added node", node.IeeeAddress);

                StartDiscovery(node);
            }
        }

        public void NodeUpdated(ZigBeeNode node)
        {
            lock (_nodeDiscovery)
            {
                if (node.NodeState == ZigBeeNodeState.ONLINE && !_nodeDiscovery.ContainsKey(node.IeeeAddress))
                {
                    Log.Debug("{IeeeAddress}: DISCOVERY Extension: Adding discoverer for updated node", node.IeeeAddress);
                    // If the state is ONLINE, then ensure discovery is running
                    StartDiscovery(node);
                }
                else if (node.NodeState != ZigBeeNodeState.ONLINE && _nodeDiscovery.ContainsKey(node.IeeeAddress))
                {
                    // If state is not ONLINE, then stop discovery
                    StopDiscovery(node);
                }
            }
        }

        public void NodeRemoved(ZigBeeNode node)
        {
            Log.Debug("{IeeeAddress}: DISCOVERY Extension: Removing discoverer", node.IeeeAddress);
            StopDiscovery(node);
        }

        public void CommandReceived(ZigBeeCommand command)
        {
            // Listen for specific commands that may indicate that the mesh has changed
            if (command is ManagementLeaveResponse || command is DeviceAnnounce)
            {
                Log.Debug("DISCOVERY Extension: Mesh related command received. Triggering mesh update.");
                Refresh();
            }
        }

        /// <summary>
        /// Starts a discovery on a node.
        ///
        /// <param name="nodeAddress">the network address of the node to discove</param>
        /// </summary>
        public void RediscoverNode(ushort nodeAddress)
        {
            _networkDiscoverer.RediscoverNode(nodeAddress);
        }

        /// <summary>
        /// Starts a discovery on a node. This will send a <see cref="NetworkAddressRequest"/> as a broadcast and will receive
        /// the response to trigger a full discovery.
        ///
        /// <param name="ieeeAddress">the <see cref="IeeeAddress"/> of the node to discove</param>
        /// </summary>
        public void RediscoverNode(IeeeAddress ieeeAddress)
        {
            _networkDiscoverer.RediscoverNode(ieeeAddress);
        }

        protected void StartDiscovery(ZigBeeNode node)
        {
            lock (_nodeDiscovery)
            {
                ZigBeeNodeServiceDiscoverer nodeDiscoverer = new ZigBeeNodeServiceDiscoverer(_networkManager, node);
                nodeDiscoverer.MeshUpdateTasks = MeshUpdateTasks;
                _nodeDiscovery[node.IeeeAddress] = nodeDiscoverer;
                _ = nodeDiscoverer.StartDiscovery();
            }
        }

        protected void StopDiscovery(ZigBeeNode node)
        {
            if (_nodeDiscovery.TryRemove(node.IeeeAddress, out ZigBeeNodeServiceDiscoverer discoverer))
            {
                discoverer.StopDiscovery();
            }
        }
        void StopScheduler()
        {
            if (_futureTask != null)
            {
                _cancellationTokenSource.Cancel();
                _futureTask = null;
            }
        }

        private void StartScheduler(int initialPeriod)
        {
            StopScheduler();

            _cancellationTokenSource = new CancellationTokenSource();

            _futureTask = Task.Run(() =>
            {
                lock (_nodeDiscovery)
                {
                    Log.Debug("DISCOVERY Extension: Starting mesh update");
                    foreach (ZigBeeNodeServiceDiscoverer node in _nodeDiscovery.Values)
                    {
                        Log.Debug("{IeeeAddress}: DISCOVERY Extension: Starting mesh update", node.Node.IeeeAddress);
                        node.MeshUpdateTasks = MeshUpdateTasks;
                        node.UpdateMesh();
                    }
                }
            }, _cancellationTokenSource.Token);
        }

        public List<ZigBeeNodeServiceDiscoverer> GetNodeDiscoverers()
        {
            lock (_nodeDiscovery)
            {
                return _nodeDiscovery.Values.ToList();
            }
        }
    }
}
