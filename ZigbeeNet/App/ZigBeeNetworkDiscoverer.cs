using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Logging;
using ZigBeeNet.ZCL;

namespace ZigBeeNet.App
{
        /**
     * {@link ZigBeeNetworkDiscoverer} is used to discover devices in the network.
     * <p>
     * Notifications will be sent to the listeners when nodes and endpoints are discovered.
     * Device listeners are always notified first as each endpoint discovery completes.
     * Once a node is fully discovered and all its endpoints are included into the network,
     * we can notify the node listeners.
     *
     * @author Chris Jackson
     */
    public class ZigBeeNetworkDiscoverer : IZigBeeCommandListener, IZigBeeAnnounceListener {
        /**
         * The _logger.
         */
        private readonly ILog _logger = LogProvider.For<ZigBeeNetworkDiscoverer>();


        /**
         * Default maximum number of retries to perform
         */
        private const int DEFAULT_MAX_RETRY_COUNT = 3;

    /**
     * Default period between retries
     */
    private const int DEFAULT_RETRY_PERIOD = 1500;

    /**
     * Default minimum time before information can be queried again for same network address or endpoint.
     */
    private const int DEFAULT_REQUERY_TIME = 300000;

    /**
     * The ZigBee network manager.
     */
    private ZigBeeNetworkManager _networkManager;

    /**
     * Period between retries
     */
    private int _retryPeriod = DEFAULT_RETRY_PERIOD;

    /**
     * Period between retries
     */
    private int _retryCount = DEFAULT_MAX_RETRY_COUNT;

    /**
     * The minimum time before performing a requery
     */
    private int _requeryPeriod = DEFAULT_REQUERY_TIME;

    /**
     * Map of node discovery times.
     */
    private Dictionary<Integer, Long> discoveryStartTime = new Dictionary<Integer, Long>();

    /**
     * Flag used to initialise the discoverer once the network is ONLINE
     */
    private bool _initialized = false;

    /**
     * Discovers ZigBee network state.
     *
     * @param _networkManager the {@link ZigBeeNetworkManager}
     */
    protected ZigBeeNetworkDiscoverer(ZigBeeNetworkManager networkManager)
    {
        _networkManager = networkManager;
    }

    /**
     * Starts up ZigBee network discoverer. This adds a listener to wait for the network to go online.
     */
    protected void Startup()
    {
        _logger.Debug("Network discovery task: starting");

        _initialized = true;

        _networkManager.AddCommandListener(this);
        _networkManager.addAnnounceListener(this);

        // Start discovery from root node.
        StartNodeDiscovery(0);
    }

    /**
     * Shuts down ZigBee network discoverer.
     */
    protected void Shutdown()
    {
        _logger.Debug("Network discovery task: shutdown");
        _networkManager.RemoveCommandListener(this);
        _networkManager.removeAnnounceListener(this);
        _initialized = false;
    }

    /**
     * Sets the retry period in milliseconds. This is the amount of time the service will wait following a failed
     * request before performing a retry.
     *
     * @param retryPeriod the period in milliseconds between retries
     */
    protected void SetRetryPeriod(int retryPeriod)
    {
        this._retryPeriod = retryPeriod;
    }

    /**
     * Sets the maximum number of retries the service will perform at any stage before failing.
     *
     * @param retryCount the maximum number of retries.
     */
    protected void SetRetryCount(int retryCount)
    {
        this._retryCount = retryCount;
    }

    /**
     * Sets the minimum period between requeries on each node
     *
     * @param requeryPeriod the requery period in milliseconds
     */
    protected void SetRequeryPeriod(int requeryPeriod)
    {
        this._requeryPeriod = requeryPeriod;
    }

    public void DeviceStatusUpdate(ZigBeeNodeStatus deviceStatus, ushort networkAddress,
            IeeeAddress ieeeAddress)
    {
        switch (deviceStatus)
        {
            case UNSECURED_JOIN:
            case SECURED_REJOIN:
            case UNSECURED_REJOIN:
                // We only care about devices that have joined or rejoined
                _logger.Debug("{}: Device status updated. NWK={}", ieeeAddress, networkAddress);
                addNode(ieeeAddress, networkAddress);
                break;
            default:
                break;
        }
    }

    public void commandReceived(ZigBeeCommand command)
    {
        // ZCL command received from remote node. Perform discovery if it is not yet known.
        if (command is ZclCommand zclCommand)
        {
            if (_networkManager.getNode(zclCommand.SourceAddress.getAddress()) == null)
            {
                // TODO: Protect against group address?
                ZigBeeEndpointAddress address = (ZigBeeEndpointAddress)zclCommand.SourceAddress;
                startNodeDiscovery(address.getAddress());
            }

            return;
        }

        // Node has been announced.
        if (command instanceof DeviceAnnounce) {
            final DeviceAnnounce announce = (DeviceAnnounce)command;

            _logger.Debug("{}: Device announce received. NWK={}", announce.getIeeeAddr(),
                    announce.getNwkAddrOfInterest());
            addNode(announce.getIeeeAddr(), announce.getNwkAddrOfInterest());
        }
    }

    /**
     * Starts a discovery on a node.
     *
     * @param nodeAddress the network address of the node to discover
     */
    protected void rediscoverNode(final int nodeAddress)
    {
        if (!_initialized)
        {
            _logger.Debug("Network discovery task: can't perform rediscovery on {} until initialization complete.",
                    nodeAddress);
            return;
        }
        startNodeDiscovery(nodeAddress);
    }

    /**
     * Starts a discovery on a node. This will send a {@link NetworkAddressRequest} as a broadcast and will receive
     * the response to trigger a full discovery.
     *
     * @param ieeeAddress the {@link IeeeAddress} of the node to discover
     */
    protected void rediscoverNode(final IeeeAddress ieeeAddress)
    {
        if (!_initialized)
        {
            _logger.Debug("Network discovery task: can't perform rediscovery on {} until initialization complete.",
                    ieeeAddress);
            return;
        }

        Runnable runnable = new Runnable() {
            @Override
            public void run()
        {
            _logger.Debug("{}: NWK Discovery starting node rediscovery", ieeeAddress);
            int retries = 0;
            try
            {
                do
                {
                    if (Thread.currentThread().isInterrupted())
                    {
                        break;
                    }

                    NetworkAddressRequest request = new NetworkAddressRequest();
                    request.setIeeeAddr(ieeeAddress);
                    request.setRequestType(0);
                    request.setStartIndex(0);
                    request.setDestinationAddress(
                            new ZigBeeEndpointAddress(ZigBeeBroadcastDestination.BROADCAST_RX_ON.getKey()));
                    CommandResult response;
                    response = _networkManager.sendTransaction(request, request).get();

                    final NetworkAddressResponse nwkAddressResponse = response.getResponse();
                    if (nwkAddressResponse != null && nwkAddressResponse.getStatus() == ZdoStatus.SUCCESS)
                    {
                        startNodeDiscovery(nwkAddressResponse.getNwkAddrRemoteDev());
                        break;
                    }

                    // We failed with the last request. Wait a bit then retry
                    try
                    {
                        _logger.Debug("{}: NWK Discovery node rediscovery request failed. Wait before retry.",
                                ieeeAddress);
                        Thread.sleep(_retryPeriod);
                    }
                    catch (InterruptedException e)
                    {
                        break;
                    }
                } while (retries++ < _retryCount);
            }
            catch (InterruptedException | ExecutionException e) {
            _logger.Debug("NWK Discovery error in rediscoverNode ", e);
        }
        _logger.Debug("{}: NWK Discovery finishing node rediscovery", ieeeAddress);
    }
};

_networkManager.executeTask(runnable);
    }

    /**
     * Performs the top level node discovery. This discovers node level attributes such as the endpoints and
     * descriptors.
     *
     * @param networkAddress the network address to start a discovery on
     */
    private void startNodeDiscovery(final int nodeNetworkAddress)
{
    // Check if we need to do a rediscovery on this node first...
    synchronized(discoveryStartTime) {
        if (discoveryStartTime.get(nodeNetworkAddress) != null
                && System.currentTimeMillis() - discoveryStartTime.get(nodeNetworkAddress) < requeryPeriod)
        {
            _logger.trace("{}: NWK Discovery node discovery already in progress", nodeNetworkAddress);
            return;
        }
        discoveryStartTime.put(nodeNetworkAddress, System.currentTimeMillis());
    }

    _logger.Debug("{}: NWK Discovery scheduling node discovery", nodeNetworkAddress);
    Runnable runnable = new Runnable() {
            @Override
            public void run()
    {
        try
        {
            _logger.Debug("{}: NWK Discovery starting node discovery", nodeNetworkAddress);
            int retries = 0;
            boolean success;
            do
            {
                if (Thread.currentThread().isInterrupted())
                {
                    break;
                }

                success = getIeeeAddress(nodeNetworkAddress);

                if (success)
                {
                    break;
                }

                // We failed with the last request. Wait a bit then retry
                try
                {
                    Thread.sleep(retryPeriod);
                }
                catch (InterruptedException e)
                {
                    break;
                }
            } while (retries++ < retryCount);

            _logger.Debug("{}: NWK Discovery ending node discovery", nodeNetworkAddress);
        }
        catch (Exception e)
        {
            _logger.error("{}: NWK Discovery error during node discovery: ", nodeNetworkAddress, e);
        }
    }
};

_networkManager.executeTask(runnable);
    }

    /**
     * Get Node IEEE address
     *
     * @param networkAddress the network address of the node
     * @return true if the message was processed ok
     */
    private boolean getIeeeAddress(final int networkAddress)
{
    try
    {
        Integer startIndex = 0;
        int totalAssociatedDevices = 0;
        Set<Integer> associatedDevices = new HashSet<Integer>();
        IeeeAddress ieeeAddress = null;

        do
        {
            // Request extended response, start index for associated list is 0
            final IeeeAddressRequest ieeeAddressRequest = new IeeeAddressRequest();
            ieeeAddressRequest.setDestinationAddress(new ZigBeeEndpointAddress(networkAddress));
            ieeeAddressRequest.setRequestType(1);
            ieeeAddressRequest.setStartIndex(startIndex);
            ieeeAddressRequest.setNwkAddrOfInterest(networkAddress);
            CommandResult response = _networkManager.sendTransaction(ieeeAddressRequest, ieeeAddressRequest).get();
            if (response.isError())
            {
                return false;
            }

            final IeeeAddressResponse ieeeAddressResponse = response.getResponse();
            _logger.Debug("{}: NWK Discovery IeeeAddressRequest returned {}", networkAddress, ieeeAddressResponse);
            if (ieeeAddressResponse != null && ieeeAddressResponse.getStatus() == ZdoStatus.SUCCESS)
            {
                ieeeAddress = ieeeAddressResponse.getIeeeAddrRemoteDev();
                if (startIndex.equals(ieeeAddressResponse.getStartIndex()))
                {
                    associatedDevices.addAll(ieeeAddressResponse.getNwkAddrAssocDevList());

                    startIndex += ieeeAddressResponse.getNwkAddrAssocDevList().size();
                    totalAssociatedDevices = ieeeAddressResponse.getNwkAddrAssocDevList().size();
                }
            }

        } while (startIndex < totalAssociatedDevices);

        addNode(ieeeAddress, networkAddress);

        // Start discovery for any associated nodes
        for (final int deviceNetworkAddress : associatedDevices)
        {
            startNodeDiscovery(deviceNetworkAddress);
        }
    }
    catch (InterruptedException | ExecutionException e) {
        _logger.Debug("NWK Discovery Error in checkIeeeAddressResponse ", e);
    }

    return true;
    }

    /**
     * Updates {@link ZigBeeNode} and adds it to the {@link ZigBeeNetworkManager}
     *
     * @param ieeeAddress the {@link IeeeAddress} of the newly announced node
     * @param networkAddress the network address of the newly announced node
     */
    private void addNode(final IeeeAddress ieeeAddress, int networkAddress)
    {
        ZigBeeNode node = _networkManager.getNode(ieeeAddress);
        if (node != null)
        {
            if (node.getNetworkAddress() != networkAddress)
            {
                _logger.Debug("{}: Network address updated to {}", ieeeAddress, networkAddress);
            }
            node.setNetworkAddress(networkAddress);
            _networkManager.updateNode(node);
            return;
        }

        node = new ZigBeeNode(_networkManager, ieeeAddress);
        node.setNetworkAddress(networkAddress);

        // Add the node to the network...
        _networkManager.addNode(node);
    }
}
}
