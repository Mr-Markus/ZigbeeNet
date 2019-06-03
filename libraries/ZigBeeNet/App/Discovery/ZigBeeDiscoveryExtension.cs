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
    public class ZigBeeDiscoveryExtension : IZigBeeNetworkExtension, IZigBeeNetworkNodeListener, IZigBeeCommandListener
    {
        /// <summary>
        /// The ZigBee network <see cref="ZigBeeNetworkDiscoverer"/>. The discover is
        /// responsible for monitoring the network for new devices and the initial
        /// interrogation of their capabilities.
        /// </summary>
        private ZigBeeNetworkDiscoverer _networkDiscoverer;

        /// <summary>
        ///
        /// </summary>
        private ConcurrentDictionary<IeeeAddress, ZigBeeNodeServiceDiscoverer> nodeDiscovery = new ConcurrentDictionary<IeeeAddress, ZigBeeNodeServiceDiscoverer>();

        /// <summary>
        /// Refresh period for the mesh update - in seconds
        /// </summary>
        private int _updatePeriod;

        private Task _futureTask = null;

        private CancellationTokenSource _cancellationTokenSource;

        private ZigBeeNetworkManager _networkManager;

        private bool extensionStarted = false;

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

            _networkDiscoverer?.Shutdown();

            extensionStarted = false;

            if (_futureTask != null)
            {
                _cancellationTokenSource.Cancel();
            }
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
        /// Performs an immediate refresh of the network. Subsequent updates are performed at the current update rate, and
        /// the timer is restarted from the time of calling this method.
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
            if (nodeDiscovery.ContainsKey(node.IeeeAddress))
            {
                return;
            }

            Log.Debug("DISCOVERY Extension: Adding discoverer for {IeeeAddress}", node.IeeeAddress);

            ZigBeeNodeServiceDiscoverer nodeDiscoverer = new ZigBeeNodeServiceDiscoverer(_networkManager, node);
            nodeDiscovery[node.IeeeAddress] = nodeDiscoverer;
            _ = nodeDiscoverer.StartDiscovery();
        }

        public void NodeUpdated(ZigBeeNode node)
        {
            // Not used
        }

        public void NodeRemoved(ZigBeeNode node)
        {
            Log.Debug("DISCOVERY Extension: Removing discoverer for {IeeeAddress}", node.IeeeAddress);

            nodeDiscovery.TryRemove(node.IeeeAddress, out ZigBeeNodeServiceDiscoverer ignored);
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

        private void StopScheduler()
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
                Log.Debug("DISCOVERY Extension: Starting mesh update");
                foreach (ZigBeeNodeServiceDiscoverer node in nodeDiscovery.Values)
                {
                    Log.Debug("DISCOVERY Extension: Starting mesh update for {IeeeAddress}", node.Node.IeeeAddress);
                    node.UpdateMesh();
                }
            }, _cancellationTokenSource.Token);
        }

        public List<ZigBeeNodeServiceDiscoverer> GetNodeDiscoverers()
        {
            return nodeDiscovery.Values.ToList();
        }
    }
}
