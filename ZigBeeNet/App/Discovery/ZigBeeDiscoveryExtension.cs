﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet;
using ZigBeeNet.App;
using ZigBeeNet.App.Discovery;
using ZigBeeNet.Logging;

namespace ZigBeeNet.App.Discovery
{
    public class ZigBeeDiscoveryExtension : IZigBeeNetworkExtension, IZigBeeNetworkNodeListener, IZigBeeCommandListener
    {
        /**
         * The _logger.
         */
        private readonly ILog _logger = LogProvider.For<ZigBeeNetworkManager>();


        /**
         * The ZigBee network {@link ZigBeeNetworkDiscoverer}. The discover is
         * responsible for monitoring the network for new devices and the initial
         * interrogation of their capabilities.
         */
        private ZigBeeNetworkDiscoverer networkDiscoverer;

        /**
         *
         */
        private Dictionary<IeeeAddress, ZigBeeNodeServiceDiscoverer> nodeDiscovery = new Dictionary<IeeeAddress, ZigBeeNodeServiceDiscoverer>();

        /**
         * Refresh period for the mesh update - in seconds
         */
        private int _updatePeriod;

        private ScheduledFuture<?> futureTask = null;

        private ZigBeeNetworkManager _networkManager;

        private bool extensionStarted = false;

        public ZigBeeStatus ExtensionInitialize(ZigBeeNetworkManager networkManager)
        {
            _networkManager = networkManager;
            return ZigBeeStatus.SUCCESS;
        }

        public ZigBeeStatus ExtensionStartup(ZigBeeNetworkManager networkManager)
        {
            _logger.Debug("DISCOVERY Extension: Startup");

            _networkManager = networkManager;

            _networkManager.AddNetworkNodeListener(this);
            _networkManager.AddCommandListener(this);

            networkDiscoverer = new ZigBeeNetworkDiscoverer(_networkManager);
            networkDiscoverer.Startup();

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

            networkDiscoverer.Shutdown();

            extensionStarted = false;

            if (futureTask != null)
            {
                futureTask.cancel(true);
            }
            _logger.Debug("DISCOVERY Extension: Shutdown");
        }

        /**
         * Sets the update period for the mesh update service. This is the number of seconds between
         * subsequent mesh updates. Setting the period to 0 will disable mesh updates.
         *
         * @param updatePeriod number of seconds between mesh updates. Setting to 0 will stop updates.
         */
        public void setUpdatePeriod(int updatePeriod)
        {
            _updatePeriod = updatePeriod;

            if (!extensionStarted)
            {
                return;
            }

            _logger.Debug("DISCOVERY Extension: Set mesh update interval to {} seconds", updatePeriod);

            if (updatePeriod == 0)
            {
                stopScheduler();
                return;
            }

            StartScheduler(updatePeriod);
        }

        /**
         * Gets the current period at which the mesh data is being updated (in seconds). A return value of 0 indicates that
         * automatic updates are currently disabled.
         *
         * @return number of seconds between mesh updates. 0 indicates no automatic updates.
         *
         */
        public int getUpdatePeriod()
        {
            return _updatePeriod;
        }

        /**
         * Performs an immediate refresh of the network. Subsequent updates are performed at the current update rate, and
         * the timer is restarted from the time of calling this method.
         */
        public void refresh()
        {
            _logger.Debug("DISCOVERY Extension: Start mesh update task with interval of {} seconds", _updatePeriod);

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

            _logger.Debug("DISCOVERY Extension: Adding discoverer for {}", node.IeeeAddress);

            ZigBeeNodeServiceDiscoverer nodeDiscoverer = new ZigBeeNodeServiceDiscoverer(_networkManager, node);
            nodeDiscovery[node.IeeeAddress] = nodeDiscoverer;
            nodeDiscoverer.StartDiscovery();
        }

        public void NodeUpdated(ZigBeeNode node)
        {
            // Not used
        }

        public void NodeRemoved(ZigBeeNode node)
        {
            _logger.Debug("DISCOVERY Extension: Removing discoverer for {}", node.IeeeAddress);
            nodeDiscovery.Remove(node.IeeeAddress);
        }

        public void CommandReceived(ZigBeeCommand command)
        {
            // Listen for specific commands that may indicate that the mesh has changed
            if (command is ManagementLeaveResponse || command is DeviceAnnounce)
            {
                _logger.Debug("DISCOVERY Extension: Mesh related command received. Triggering mesh update.");
                refresh();
            }
        }

        /**
         * Starts a discovery on a node.
         *
         * @param nodeAddress the network address of the node to discover
         */
        public void RediscoverNode(ushort nodeAddress)
        {
            networkDiscoverer.RediscoverNode(nodeAddress);
        }

        /**
         * Starts a discovery on a node. This will send a {@link NetworkAddressRequest} as a broadcast and will receive
         * the response to trigger a full discovery.
         *
         * @param ieeeAddress the {@link IeeeAddress} of the node to discover
         */
        public void RediscoverNode(IeeeAddress ieeeAddress)
        {
            networkDiscoverer.RediscoverNode(ieeeAddress);
        }

        private void stopScheduler()
        {
            if (futureTask != null)
            {
                futureTask.cancel(true);
                futureTask = null;
            }
        }

        private void StartScheduler(int initialPeriod)
        {
            stopScheduler();

            futureTask.Run(() =>
            {
                _logger.Debug("DISCOVERY Extension: Starting mesh update");
                foreach (ZigBeeNodeServiceDiscoverer node in nodeDiscovery.Values)
                {
                    _logger.Debug("DISCOVERY Extension: Starting mesh update for {}", node.Node.IeeeAddress);
                    node.UpdateMesh();
                }
            });
        }

        public List<ZigBeeNodeServiceDiscoverer> GetNodeDiscoverers()
        {
            return nodeDiscovery.Values.ToList();
        }
    }
}
