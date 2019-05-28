using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet;
using ZigBeeNet.App.Discovery;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZDO;
using ZigBeeNet.ZDO.Command;
using Serilog;

namespace ZigBeeNet.App.Discovery
{
    /// <summary>
    /// <see cref="ZigBeeNetworkDiscoverer"/> is used to discover devices in the network.
    /// 
    /// Notifications will be sent to the listeners when nodes and endpoints are discovered.
    /// Device listeners are always notified first as each endpoint discovery completes.
    /// Once a node is fully discovered and all its endpoints are included into the network,
    /// we can notify the node listeners.
    ///
    /// </summary>
    public class ZigBeeNetworkDiscoverer : IZigBeeCommandListener, IZigBeeAnnounceListener
    {
        /// <summary>
        /// Default maximum number of retries to perform
        /// </summary>
        private const int DEFAULT_MAX_RETRY_COUNT = 3;

        /// <summary>
        /// Default period between retries
        /// </summary>
        private const int DEFAULT_RETRY_PERIOD = 1500;

        /// <summary>
        /// Default minimum time before information can be queried again for same network address or endpoint.
        /// </summary>
        private const int DEFAULT_REQUERY_TIME = 300000;

        /// <summary>
        /// The ZigBee network manager.
        /// </summary>
        private ZigBeeNetworkManager _networkManager;

        /// <summary>
        /// Period between retries
        /// </summary>
        private int _retryPeriod = DEFAULT_RETRY_PERIOD;

        /// <summary>
        /// Period between retries
        /// </summary>
        private int _retryCount = DEFAULT_MAX_RETRY_COUNT;

        /// <summary>
        /// The minimum time before performing a requery
        /// </summary>
        private int _requeryPeriod = DEFAULT_REQUERY_TIME;

        /// <summary>
        /// Map of node discovery times.
        /// </summary>
        private Dictionary<ushort, long> _discoveryStartTime = new Dictionary<ushort, long>();

        /// <summary>
        /// Flag used to initialise the discoverer once the network is ONLINE
        /// </summary>
        private bool _initialized = false;

        /// <summary>
        /// Discovers ZigBee network state.
        ///
        /// <param name="_networkManager">the <see cref="ZigBeeNetworkManager"></param>
        /// </summary>
        public ZigBeeNetworkDiscoverer(ZigBeeNetworkManager networkManager)
        {
            _networkManager = networkManager;
        }

        /// <summary>
        /// Starts up ZigBee network discoverer. This adds a listener to wait for the network to go online.
        /// </summary>
        public void Startup()
        {
            Log.Debug("Network discovery task: starting");

            _initialized = true;

            _networkManager.AddCommandListener(this);
            _networkManager.AddAnnounceListener(this);

            // Start discovery from root node.
            // StartNodeDiscovery(0);
        }

        /// <summary>
        /// Shuts down ZigBee network discoverer.
        /// </summary>
        public void Shutdown()
        {
            Log.Debug("Network discovery task: shutdown");
            _networkManager.RemoveCommandListener(this);
            _networkManager.RemoveAnnounceListener(this);
            _initialized = false;
        }

        /// <summary>
        /// Sets the retry period in milliseconds. This is the amount of time the service will wait following a failed
        /// request before performing a retry.
        ///
        /// <param name="retryPeriod">the period in milliseconds between retries</param>
        /// </summary>
        public void SetRetryPeriod(int retryPeriod)
        {
            _retryPeriod = retryPeriod;
        }

        /// <summary>
        /// Sets the maximum number of retries the service will perform at any stage before failing.
        ///
        /// <param name="retryCount">the maximum number of retries.</param>
        /// </summary>
        public void SetRetryCount(int retryCount)
        {
            _retryCount = retryCount;
        }

        /// <summary>
        /// Sets the minimum period between requeries on each node
        ///
        /// <param name="requeryPeriod">the requery period in milliseconds</param>
        /// </summary>
        public void SetRequeryPeriod(int requeryPeriod)
        {
            _requeryPeriod = requeryPeriod;
        }

        public void DeviceStatusUpdate(ZigBeeNodeStatus deviceStatus, ushort networkAddress, IeeeAddress ieeeAddress)
        {
            switch (deviceStatus)
            {
                case ZigBeeNodeStatus.UNSECURED_JOIN:
                case ZigBeeNodeStatus.SECURED_REJOIN:
                case ZigBeeNodeStatus.UNSECURED_REJOIN:
                    // We only care about devices that have joined or rejoined
                    Log.Debug("{IeeeAddress}: Device status updated. NWK={NetworkAddress}", ieeeAddress, networkAddress);
                    AddNode(ieeeAddress, networkAddress);
                    break;
                default:
                    break;
            }
        }

        public void CommandReceived(ZigBeeCommand command)
        {
            // ZCL command received from remote node. Perform discovery if it is not yet known.
            if (command is ZclCommand zclCommand)
            {
                if (_networkManager.GetNode(zclCommand.SourceAddress.Address) == null)
                {
                    // TODO: Protect against group address?
                    ZigBeeEndpointAddress address = (ZigBeeEndpointAddress)zclCommand.SourceAddress;
                    StartNodeDiscovery(address.Address);
                }

                return;
            }

            // Node has been announced.
            if (command is DeviceAnnounce)
            {
                DeviceAnnounce announce = (DeviceAnnounce)command;

                Log.Debug("{IeeeAddress}: Device announce received. NWK={NetworkAddress}", announce.IeeeAddr,
                        announce.NwkAddrOfInterest);
                AddNode(announce.IeeeAddr, announce.NwkAddrOfInterest);
            }
        }

        /// <summary>
        /// Starts a discovery on a node given the network address.
        ///
        /// <param name="requeryPeriod">networkAddress the network address of the node to discover</param>
        /// </summary>
        public void RediscoverNode(ushort networkAddress)
        {
            if (!_initialized)
            {
                Log.Debug("Network discovery task: can't perform rediscovery on {NetworkAddress} until initialization complete.", networkAddress);
                return;
            }

            Log.Debug("{NetworkAddress}: NWK Discovery starting node rediscovery", networkAddress);
            int retries = 0;

            Task.Run(async () =>
            {
                try
                {
                    do
                    {
                        // Request basic response, start index for associated list is 0
                        IeeeAddressRequest ieeeAddressRequest = new IeeeAddressRequest();
                        ieeeAddressRequest.DestinationAddress = new ZigBeeEndpointAddress(networkAddress);
                        ieeeAddressRequest.RequestType = 0;
                        ieeeAddressRequest.StartIndex = 0;
                        ieeeAddressRequest.NwkAddrOfInterest = networkAddress;
                        CommandResult response = await _networkManager.SendTransaction(ieeeAddressRequest, ieeeAddressRequest);

                        if (response.IsError())
                        {
                            return;
                        }

                        IeeeAddressResponse ieeeAddressResponse = (IeeeAddressResponse)response.GetResponse();

                        Log.Debug("{NetworkAddress}: NWK Discovery IeeeAddressRequest returned {IeeeAddressResponse}", networkAddress, ieeeAddressResponse);

                        if (ieeeAddressResponse != null && ieeeAddressResponse.Status == ZdoStatus.SUCCESS)
                        {
                            AddNode(ieeeAddressResponse.IeeeAddrRemoteDev, ieeeAddressResponse.NwkAddrRemoteDev);
                            StartNodeDiscovery(ieeeAddressResponse.NwkAddrRemoteDev);
                            break;
                        }

                        // We failed with the last request. Wait a bit then retry
                        try
                        {
                            Log.Debug("{NetworkAddress}: NWK Discovery node rediscovery request failed. Wait before retry.", networkAddress);

                            await Task.Delay(_retryPeriod);
                        }
                        catch (ThreadAbortException)
                        {
                            break;
                        }

                    } while (retries++ < _retryCount);
                }
                catch (Exception e) // TODO: Handle more secific Exception here (ThreadAbortException etc.)
                {
                    Log.Debug("NWK Discovery Error in checkIeeeAddressResponse ", e);
                }

                Log.Debug("{NetworkAddress}: NWK Discovery finishing node rediscovery after {Retries} attempts", networkAddress, retries);
            });

            // StartNodeDiscovery(nodeAddress);
        }

        /// <summary>
        /// Starts a discovery on a node. This will send a <see cref="NetworkAddressRequest"/> as a broadcast and will receive
        /// the response to trigger a full discovery.
        ///
        /// <param name="ieeeAddress">the <see cref="IeeeAddress"/> of the node to discover</param>
        /// </summary>
        public void RediscoverNode(IeeeAddress ieeeAddress)
        {
            if (!_initialized)
            {
                Log.Debug("Network discovery task: can't perform rediscovery on {IeeeAddress} until initialization complete.",
                        ieeeAddress);
                return;
            }

            Task.Run(async () =>
            {
                Log.Debug("{IeeeAddress}: NWK Discovery starting node rediscovery", ieeeAddress);
                int retries = 0;
                try
                {
                    do
                    {
                        if (Thread.CurrentThread.ThreadState == ThreadState.WaitSleepJoin)
                        {
                            break;
                        }

                        NetworkAddressRequest request = new NetworkAddressRequest();
                        request.IeeeAddr = ieeeAddress;
                        request.RequestType = 0;
                        request.StartIndex = 0;
                        request.DestinationAddress = new ZigBeeEndpointAddress(ZigBeeBroadcastDestination.GetBroadcastDestination(BroadcastDestination.BROADCAST_RX_ON).Key);
                        CommandResult response = await _networkManager.SendTransaction(request, request);

                        NetworkAddressResponse nwkAddressResponse = response.GetResponse<NetworkAddressResponse>();
                        if (nwkAddressResponse != null && nwkAddressResponse.Status == ZdoStatus.SUCCESS)
                        {
                            StartNodeDiscovery(nwkAddressResponse.NwkAddrRemoteDev);
                            break;
                        }

                        // We failed with the last request. Wait a bit then retry
                        try
                        {
                            Log.Debug("{IeeeAddress}: NWK Discovery node rediscovery request failed. Wait before retry.", ieeeAddress);

                            await Task.Delay(_retryPeriod);
                        }
                        catch (Exception)
                        {
                            break;
                        }

                    } while (retries++ < _retryCount);
                }
                catch (Exception e)
                {
                    Log.Debug("NWK Discovery error in rediscoverNode ", e);
                }

                Log.Debug("{IeeeAddress}: NWK Discovery finishing node rediscovery", ieeeAddress);
            });
        }

        /// <summary>
        /// Performs the top level node discovery. This discovers node level attributes such as the endpoints and
        /// descriptors.
        ///
        /// <param name="nodeNetworkAddress">the network address to start a discovery on</param>
        /// </summary>
        private void StartNodeDiscovery(ushort nodeNetworkAddress)
        {
            // Check if we need to do a rediscovery on this node first...
            lock (_discoveryStartTime)
            {
                if (_discoveryStartTime.ContainsKey(nodeNetworkAddress) && DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - _discoveryStartTime[nodeNetworkAddress] < _requeryPeriod)
                {
                    Log.Information("{NetworkAddress}: NWK Discovery node discovery already in progress", nodeNetworkAddress);
                    return;
                }
                _discoveryStartTime[nodeNetworkAddress] = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            }

            Log.Debug("{NetworkAddress}: NWK Discovery scheduling node discovery", nodeNetworkAddress);

            // TODO: Return Task ?
            Task.Run(async () =>
            {
                try
                {
                    Log.Debug("{NetworkAddress}: NWK Discovery starting node discovery", nodeNetworkAddress);
                    int retries = 0;
                    bool success;

                    do
                    {
                        if (Thread.CurrentThread.ThreadState == ThreadState.WaitSleepJoin)
                        {
                            break;
                        }

                        success = await GetIeeeAddress(nodeNetworkAddress);

                        if (success)
                        {
                            break;
                        }

                        try
                        {
                            // We failed with the last request. Wait a bit then retry
                            await Task.Delay(_retryPeriod);
                        }
                        catch (Exception)
                        {
                            // If we don't know the node yet, then try to find the IEEE address
                            // before requesting the associated nodes.
                            if (_networkManager.GetNode(nodeNetworkAddress) == null)
                            {
                                success = await GetIeeeAddress(nodeNetworkAddress);
                                continue;
                            }

                            success = await GetAssociatedNodes(nodeNetworkAddress);

                            if (success)
                            {
                                break;
                            }
                        }

                    } while (retries++ < _retryCount);

                    Log.Debug("{NetworkAddress}: NWK Discovery ending node discovery", nodeNetworkAddress);
                }
                catch (Exception e)
                {
                    Log.Error("{NetworkAddress}: NWK Discovery error during node discovery: {Error}", nodeNetworkAddress, e);
                }
            });
        }

        /**
        * Get the associated nodes for this address, and start a discovery of the associated nodes.
        *
        * <param name="networkAddress">networkAddress the network address of the node</param>
        * <returns>true if the message was processed ok</returns>
        */
        private async Task<bool> GetAssociatedNodes(ushort networkAddress)
        {
            int startIndex = 0;
            int totalAssociatedDevices = 0;
            List<ushort> associatedDevices = new List<ushort>();

            do
            {
                // Request extended response, start index for associated list is 0
                IeeeAddressRequest ieeeAddressRequest = new IeeeAddressRequest();
                ieeeAddressRequest.DestinationAddress = new ZigBeeEndpointAddress(networkAddress);
                ieeeAddressRequest.RequestType = 1;
                ieeeAddressRequest.StartIndex = (byte)startIndex;
                ieeeAddressRequest.NwkAddrOfInterest = networkAddress;
                CommandResult response = await _networkManager.SendTransaction(ieeeAddressRequest, ieeeAddressRequest);
                if (response.IsError())
                {
                    return false;
                }

                IeeeAddressResponse ieeeAddressResponse = (IeeeAddressResponse)response.GetResponse();

                Log.Debug("{NetworkAddress}: NWK Discovery IeeeAddressRequest returned {IeeeAddressResponse}", networkAddress, ieeeAddressResponse);

                if (ieeeAddressResponse != null && ieeeAddressResponse.Status == ZdoStatus.SUCCESS && startIndex.Equals(ieeeAddressResponse.StartIndex))
                {
                    associatedDevices.AddRange(ieeeAddressResponse.NwkAddrAssocDevList);

                    startIndex += ieeeAddressResponse.NwkAddrAssocDevList.Count;
                    totalAssociatedDevices = ieeeAddressResponse.NwkAddrAssocDevList.Count;
                }
            } while (startIndex < totalAssociatedDevices);

            // Start discovery for any associated nodes
            foreach (var deviceNetworkAddress in associatedDevices)
            {
                StartNodeDiscovery(deviceNetworkAddress);
            }

            return true;
        }

        /// <summary>
        /// Discovers the IeeeAddress of a remote device. This uses a broadcast request to try to discover the
        /// device.
        ///
        /// <param name="networkAddress">the network address of the node</param>
        /// <returns>true if the message was processed ok</returns>
        /// </summary>
        private async Task<bool> GetIeeeAddress(ushort networkAddress)
        {
            // Request basic response, start index for associated list is 0
            IeeeAddressRequest request = new IeeeAddressRequest();
            request.RequestType = 0;
            request.StartIndex = 0;
            request.NwkAddrOfInterest = networkAddress;
            request.DestinationAddress = new ZigBeeEndpointAddress(ZigBeeBroadcastDestination.GetBroadcastDestination(BroadcastDestination.BROADCAST_RX_ON).Key);
            CommandResult response = await _networkManager.SendTransaction(request, request);

            if (response.IsError())
            {
                return false;
            }

            IeeeAddressResponse ieeeAddressResponse = (IeeeAddressResponse)response.GetResponse();

            Log.Debug("{NetworkAddress}: NWK Discovery IeeeAddressRequest returned {IeeeAddressResponse}", networkAddress, ieeeAddressResponse);

            if (ieeeAddressResponse != null && ieeeAddressResponse.Status == ZdoStatus.SUCCESS)
            {
                AddNode(ieeeAddressResponse.IeeeAddrRemoteDev, ieeeAddressResponse.NwkAddrRemoteDev);
                StartNodeDiscovery(ieeeAddressResponse.NwkAddrRemoteDev);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Updates ZigBeeNode and adds it to the ZigBeeNetworkManager
        ///
        /// <param name="ieeeAddress">the <see cref="IeeeAddress"> of the newly announced node</param>
        /// <param name="networkAddress">the network address of the newly announced node</param>
        /// </summary>
        private void AddNode(IeeeAddress ieeeAddress, ushort networkAddress)
        {
            ZigBeeNode node = _networkManager.GetNode(ieeeAddress);
            if (node != null)
            {
                if (node.NetworkAddress != networkAddress)
                {
                    Log.Debug("{IeeeAddress}: Network address updated to {NetworkAddress}", ieeeAddress, networkAddress);
                }
                node.NetworkAddress = networkAddress;
                _networkManager.UpdateNode(node);
                return;
            }

            node = new ZigBeeNode(_networkManager, ieeeAddress);
            node.NetworkAddress = networkAddress;

            // Add the node to the network...
            _networkManager.AddNode(node);
        }
    }
}
