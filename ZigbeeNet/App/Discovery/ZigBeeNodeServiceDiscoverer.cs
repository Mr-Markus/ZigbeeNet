using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZigbeeNet.App.Discovery
{
    /**
     * This class contains methods for discovering the services and features of a {@link ZigBeeNode}. All discovery methods
     * are private and the class is utilised by calling {@link #startDiscovery(Set)} with a set of
     * {@link #NodeDiscoveryState} for the stages wishing to be discovered or updated.
     * <p>
     * A single worker thread is ensured - if the thread is already active when {@link #startDiscovery(Set)} is called, the
     * new tasks will be added to the existing task queue if they are not already in the queue. If the worker thread is not
     * running, it will be started.
     * <p>
     * This class provides a centralised helper, used for discovering and updating information about the {@link ZigBeeNode}
     * <p>
     * A random exponential backoff is used for retries to reduce congestion. If the device replies that a command is not
     * supported, then this will not be issued again on subsequent requests.
     * <p>
     * Once the discovery update is complete the {@link ZigBeeNetworkManager#updateNode(ZigBeeNode)} is called to alert
     * users.
     *
     */
    public class ZigBeeNodeServiceDiscoverer
    {
        /**
         * The logger.
         */
        //private  Logger logger = LoggerFactory.getLogger(ZigBeeNodeServiceDiscoverer.class);

        /**
         * The {@link ZigBeeNetworkManager}.
         */
        public ZigBeeNetworkManager NetworkManager { get; private set; }

        /**
         * The {@link ZigBeeNode}.
         */
        public ZigBeeNode Node { get; set; }

        /**
         * Default maximum number of retries to perform
         */
        private const int DEFAULT_MAX_BACKOFF = 12;

        /**
         * Default period between retries
         */
        private const int DEFAULT_RETRY_PERIOD = 2100;

        /**
         * A random jitter will be added to the retry time for each device to avoid any sort of synchronisation
         */
        private const int RETRY_RANDOM_TIME = 250;

        /**
         * The maximum number of retries to perform before failing the request
         */
        private readonly int _maxBackoff = DEFAULT_MAX_BACKOFF;

        /**
         * The minimum number of milliseconds to wait before retrying the request
         */
        private readonly int _retryPeriod = DEFAULT_RETRY_PERIOD;

        /**
         * Flag to indicate if the device supports the {@link ManagementLqiRequest}
         * This is updated to false if the device responds with {@link ZdoStatus#NOT_SUPPORTED}
         */
        private bool _supportsManagementLqi = true;

        /**
         * Flag to indicate if the device supports the {@link ManagementRoutingRequest}.
         * This is updated to false if the device responds with {@link ZdoStatus#NOT_SUPPORTED}
         */
        private bool _supportsManagementRouting = true;

        /**
         * The task being run
         */
        private Task _futureTask;

        /**
         * Record of the last time we started a service discovery or update
         */
        private DateTime _lastDiscoveryStarted;

        /**
         * Record of the last time we completed a service discovery or update
         */
        private DateTime _lastDiscoveryCompleted;

        /**
         *
         *
         */
        public enum NodeDiscoveryTask
        {
            NWK_ADDRESS,
            ASSOCIATED_NODES,
            NODE_DESCRIPTOR,
            POWER_DESCRIPTOR,
            ACTIVE_ENDPOINTS,
            NEIGHBORS,
            ROUTES
        }

        /**
         * The list of tasks we need to complete
         */
        private Queue<NodeDiscoveryTask> _discoveryTasks = new Queue<NodeDiscoveryTask>();

        /**
         * Creates the discovery class
         *
         * @param networkManager the {@link ZigBeeNetworkManager} for the network
         * @param node the {@link ZigBeeNode} whose services we want to discover
         */
        public ZigBeeNodeServiceDiscoverer(ZigBeeNetworkManager networkManager, ZigBeeNode node)
        {
            this.NetworkManager = networkManager;
            this.Node = node;

            _retryPeriod = DEFAULT_RETRY_PERIOD + new Random().Next(RETRY_RANDOM_TIME);
        }

        /**
         * Performs node service discovery. This discovers node level attributes such as the endpoints and
         * descriptors, as well as updating routing and neighbor tables - as needed.
         * <p>
         * If any of the tasks requested are already in the queue, they will not be added again.
         * <p>
         * If the worker thread is not running, it will be started.
         *
         * @param newTasks a set of {@link NodeDiscoveryTask}s to be performed
         */
        private void StartDiscovery(List<NodeDiscoveryTask> newTasks)
        {
            // Tasks are managed in a queue. The worker thread will only remove the task from the queue once the task is
            // complete. When no tasks are left in the queue, the worker thread will exit.
            lock(_discoveryTasks)
            {
                // Remove any tasks that we know are not supported by this device
                if (!_supportsManagementLqi && newTasks.Contains(NodeDiscoveryTask.NEIGHBORS))
                {
                    newTasks.Remove(NodeDiscoveryTask.NEIGHBORS);
                }
                if (!_supportsManagementRouting && newTasks.Contains(NodeDiscoveryTask.ROUTES))
                {
                    newTasks.Remove(NodeDiscoveryTask.ROUTES);
                }

                // Make sure there are still tasks to perform
                if (newTasks.Count == 0)
                {
                    //logger.debug("{}: Node SVC Discovery: has no tasks to perform", Node.getIeeeAddress());
                    return;
                }

                bool startWorker = _discoveryTasks.Count == 0;

                // Add new tasks, avoiding any duplication
                foreach (NodeDiscoveryTask newTask in newTasks)
                {
                    if (!_discoveryTasks.Contains(newTask))
                    {
                        _discoveryTasks.Enqueue(newTask);
                    }
                }

                if (!startWorker)
                {
                    //logger.debug("{}: Node SVC Discovery: already scheduled or running", Node.getIeeeAddress());
                }
                else
                {
                    _lastDiscoveryStarted = DateTime.UtcNow;
                }
            }

            //logger.debug("{}: Node SVC Discovery: scheduled {}", Node.getIeeeAddress(), discoveryTasks);
            Runnable runnable = new NodeServiceDiscoveryTask();

            _futureTask = NetworkManager.ScheduleTask(runnable, new Random().Next(_retryPeriod));
        }

        /**
         * Stops service discovery and removes any scheduled tasks
         */
        public void stopDiscovery()
        {
            if (_futureTask != null)
            {
                _futureTask.cancel(true);
            }
            //logger.debug("{}: Node SVC Discovery: stopped", Node.getIeeeAddress());
        }

        /**
         * @return the maxBackoff
         */
        public int getMaxBackoff()
        {
            return _maxBackoff;
        }

        /**
         * @param maxBackoff the maxBackoff to set
         */
        public void setMaxBackoff(int maxBackoff)
        {
            this.maxBackoff = maxBackoff;
        }

        /**
         * Get node descriptor
         *
         * @return true if the message was processed ok
         * @throws ExecutionException
         * @throws InterruptedException
         */
        private boolean requestNetworkAddress() throws InterruptedException, ExecutionException {
        NetworkAddressRequest networkAddressRequest = new NetworkAddressRequest();
        networkAddressRequest.setIeeeAddr(node.getIeeeAddress());
        networkAddressRequest.setRequestType(0);
        networkAddressRequest.setStartIndex(0);
        networkAddressRequest.setDestinationAddress(


                    new ZigBeeEndpointAddress(ZigBeeBroadcastDestination.BROADCAST_ALL_DEVICES.getKey()));

        CommandResult response = NetworkManager.sendTransaction(networkAddressRequest, networkAddressRequest).get();
        NetworkAddressResponse networkAddressResponse = (NetworkAddressResponse)response.getResponse();
        logger.debug("{}: Node SVC Discovery: NetworkAddressRequest returned {}", node.getNetworkAddress(),
                networkAddressResponse);
        if (networkAddressResponse == null) {
            return false;
        }

        if (networkAddressResponse.getStatus() == ZdoStatus.SUCCESS) {
            node.setNetworkAddress(networkAddressResponse.getNwkAddrRemoteDev());

            return true;
        }

        return false;
    }

/**
 * Get Node Network address and the list of associated devices
 *
 * @return true if the message was processed ok
 * @throws ExecutionException
 * @throws InterruptedException
 */
private boolean requestAssociatedNodes() throws InterruptedException, ExecutionException {
        Integer startIndex = 0;
int totalAssociatedDevices = 0;
Set<Integer> associatedDevices = new HashSet<Integer>();

        do {
            // Request extended response, to get associated list
             IeeeAddressRequest ieeeAddressRequest = new IeeeAddressRequest();
ieeeAddressRequest.setDestinationAddress(new ZigBeeEndpointAddress(node.getNetworkAddress()));
            ieeeAddressRequest.setRequestType(1);
            ieeeAddressRequest.setStartIndex(startIndex);
            ieeeAddressRequest.setNwkAddrOfInterest(node.getNetworkAddress());
            CommandResult response = networkManager.sendTransaction(ieeeAddressRequest, ieeeAddressRequest).get();

IeeeAddressResponse ieeeAddressResponse = response.getResponse();
logger.debug("{}: Node SVC Discovery: IeeeAddressResponse returned {}", node.getIeeeAddress(),
                    ieeeAddressResponse);
            if (ieeeAddressResponse != null && ieeeAddressResponse.getStatus() == ZdoStatus.SUCCESS) {
                associatedDevices.addAll(ieeeAddressResponse.getNwkAddrAssocDevList());

                startIndex += ieeeAddressResponse.getNwkAddrAssocDevList().size();
totalAssociatedDevices = ieeeAddressResponse.getNwkAddrAssocDevList().size();
            }
        } while (startIndex<totalAssociatedDevices);

        node.setAssociatedDevices(associatedDevices);

        return true;
    }

    /**
     * Get node descriptor
     *
     * @return true if the message was processed ok
     * @throws ExecutionException
     * @throws InterruptedException
     */
    private boolean requestNodeDescriptor() throws InterruptedException, ExecutionException {
         NodeDescriptorRequest nodeDescriptorRequest = new NodeDescriptorRequest();
nodeDescriptorRequest.setDestinationAddress(new ZigBeeEndpointAddress(node.getNetworkAddress()));
        nodeDescriptorRequest.setNwkAddrOfInterest(node.getNetworkAddress());

        CommandResult response = networkManager.sendTransaction(nodeDescriptorRequest, nodeDescriptorRequest).get();
NodeDescriptorResponse nodeDescriptorResponse = (NodeDescriptorResponse)response.getResponse();
logger.debug("{}: Node SVC Discovery: NodeDescriptorResponse returned {}", node.getIeeeAddress(),
                nodeDescriptorResponse);
        if (nodeDescriptorResponse == null) {
            return false;
        }

        if (nodeDescriptorResponse.getStatus() == ZdoStatus.SUCCESS) {
            node.setNodeDescriptor(nodeDescriptorResponse.getNodeDescriptor());

            return true;
        }

        return false;
    }

    /**
     * Get node power descriptor
     *
     * @return true if the message was processed ok, or if the end device does not support the power descriptor
     * @throws ExecutionException
     * @throws InterruptedException
     */
    private boolean requestPowerDescriptor() throws InterruptedException, ExecutionException {
         PowerDescriptorRequest powerDescriptorRequest = new PowerDescriptorRequest();
powerDescriptorRequest.setDestinationAddress(new ZigBeeEndpointAddress(node.getNetworkAddress()));
        powerDescriptorRequest.setNwkAddrOfInterest(node.getNetworkAddress());

        CommandResult response = networkManager.sendTransaction(powerDescriptorRequest, powerDescriptorRequest).get();
PowerDescriptorResponse powerDescriptorResponse = (PowerDescriptorResponse)response.getResponse();
logger.debug("{}: Node SVC Discovery: PowerDescriptorResponse returned {}", node.getIeeeAddress(),
                powerDescriptorResponse);
        if (powerDescriptorResponse == null) {
            return false;
        }

        if (powerDescriptorResponse.getStatus() == ZdoStatus.SUCCESS) {
            node.setPowerDescriptor(powerDescriptorResponse.getPowerDescriptor());

            return true;
        } else if (powerDescriptorResponse.getStatus() == ZdoStatus.NOT_SUPPORTED) {
            return true;
        }

        return false;
    }

    /**
     * Get the active endpoints for a node
     *
     * @return true if the message was processed ok
     * @throws ExecutionException
     * @throws InterruptedException
     */
    private boolean requestActiveEndpoints() throws InterruptedException, ExecutionException {
         ActiveEndpointsRequest activeEndpointsRequest = new ActiveEndpointsRequest();
activeEndpointsRequest.setDestinationAddress(new ZigBeeEndpointAddress(node.getNetworkAddress()));
        activeEndpointsRequest.setNwkAddrOfInterest(node.getNetworkAddress());

        CommandResult response = networkManager.sendTransaction(activeEndpointsRequest, activeEndpointsRequest).get();
ActiveEndpointsResponse activeEndpointsResponse = (ActiveEndpointsResponse)response.getResponse();
logger.debug("{}: Node SVC Discovery: ActiveEndpointsResponse returned {}", node.getIeeeAddress(), response);
        if (activeEndpointsResponse == null) {
            return false;
        }

        // Get the simple descriptors for all endpoints
        List<ZigBeeEndpoint> endpoints = new ArrayList<ZigBeeEndpoint>();
        for (int endpointId : activeEndpointsResponse.getActiveEpList()) {
            ZigBeeEndpoint endpoint = getSimpleDescriptor(endpointId);
            if (endpoint == null) {
                return false;
            }

            endpoints.add(endpoint);
        }

        // All endpoints have been received, so add them to the node
        for (ZigBeeEndpoint endpoint : endpoints) {
            node.addEndpoint(endpoint);
        }

        return true;
    }

    /**
     * Get node neighbor table by making a {@link ManagementLqiRequest} call.
     *
     * @return list of {@link NeighborTable} if the request was processed ok, null otherwise
     * @throws ExecutionException
     * @throws InterruptedException
     */
    private boolean requestNeighborTable() throws InterruptedException, ExecutionException {
        // Start index for the list is 0
        int startIndex = 0;
int totalNeighbors = 0;
Set<NeighborTable> neighbors = new HashSet<NeighborTable>();
        do {
             ManagementLqiRequest neighborRequest = new ManagementLqiRequest();
neighborRequest.setDestinationAddress(new ZigBeeEndpointAddress(node.getNetworkAddress()));
            neighborRequest.setStartIndex(startIndex);

            CommandResult response = networkManager.sendTransaction(neighborRequest, neighborRequest).get();
ManagementLqiResponse neighborResponse = response.getResponse();
logger.debug("{}: Node SVC Discovery: ManagementLqiRequest response {}", node.getIeeeAddress(), response);
            if (neighborResponse == null) {
                return false;
            }

            if (neighborResponse.getStatus() == ZdoStatus.NOT_SUPPORTED) {
                logger.debug("{}: Node SVC Discovery: ManagementLqiRequest not supported", node.getIeeeAddress());
                supportsManagementLqi = false;
                return true;
            } else if (neighborResponse.getStatus() != ZdoStatus.SUCCESS) {
                logger.debug("{}: Node SVC Discovery: ManagementLqiRequest failed", node.getIeeeAddress());
                return false;
            }

            // Some devices may report the number of entries as the total number they can maintain.
            // To avoid a loop, we need to check if there's any response.
            if (neighborResponse.getNeighborTableList().size() == 0) {
                break;
            }

            // Save the neighbors
            neighbors.addAll(neighborResponse.getNeighborTableList());

            // Continue with next request
            startIndex += neighborResponse.getNeighborTableList().size();
totalNeighbors = neighborResponse.getNeighborTableEntries();
        } while (startIndex<totalNeighbors);

        logger.debug("{}: Node SVC Discovery: ManagementLqiRequest complete [{} neighbors]", node.getIeeeAddress(),
                neighbors.size());
        node.setNeighbors(neighbors);

        return true;
    }

    /**
     * Get node routing table by making a {@link ManagementRoutingRequest} request
     *
     * @return list of {@link RoutingTable} if the request was processed ok, null otherwise
     * @throws ExecutionException
     * @throws InterruptedException
     */
    private boolean requestRoutingTable() throws InterruptedException, ExecutionException {
        // Start index for the list is 0
        int startIndex = 0;
int totalRoutes = 0;
Set<RoutingTable> routes = new HashSet<RoutingTable>();
        do {
             ManagementRoutingRequest routeRequest = new ManagementRoutingRequest();
routeRequest.setDestinationAddress(new ZigBeeEndpointAddress(node.getNetworkAddress()));
            routeRequest.setStartIndex(startIndex);

            CommandResult response = networkManager.sendTransaction(routeRequest, routeRequest).get();
ManagementRoutingResponse routingResponse = response.getResponse();
logger.debug("{}: Node SVC Discovery: ManagementRoutingRequest returned {}", node.getIeeeAddress(),
                    response);
            if (routingResponse == null) {
                return false;
            }

            if (routingResponse.getStatus() == ZdoStatus.NOT_SUPPORTED) {
                logger.debug("{}: Node SVC Discovery ManagementLqiRequest not supported", node.getIeeeAddress());
                supportsManagementRouting = false;
                return true;
            } else if (routingResponse.getStatus() != ZdoStatus.SUCCESS) {
                logger.debug("{}: Node SVC Discovery: ManagementLqiRequest failed", node.getIeeeAddress());
                return false;
            }

            // Save the routes
            routes.addAll(routingResponse.getRoutingTableList());

            // Continue with next request
            startIndex += routingResponse.getRoutingTableList().size();
totalRoutes = routingResponse.getRoutingTableEntries();
        } while (startIndex<totalRoutes);

        logger.debug("{}: Node SVC Discovery: ManagementLqiRequest complete [{} routes]", node.getIeeeAddress(),
                routes.size());
        node.setRoutes(routes);

        return true;
    }

    /**
     * Get the simple descriptor for an endpoint and create the {@link ZigBeeEndpoint}
     *
     * @param endpointId the endpoint id to request
     * @return the newly created {@link ZigBeeEndpoint} for the endpoint, or null on error
     * @throws ExecutionException
     * @throws InterruptedException
     */
    private ZigBeeEndpoint getSimpleDescriptor(int endpointId) throws InterruptedException, ExecutionException {
         SimpleDescriptorRequest simpleDescriptorRequest = new SimpleDescriptorRequest();
simpleDescriptorRequest.setDestinationAddress(new ZigBeeEndpointAddress(node.getNetworkAddress()));
        simpleDescriptorRequest.setNwkAddrOfInterest(node.getNetworkAddress());
        simpleDescriptorRequest.setEndpoint(endpointId);

        CommandResult response = networkManager.sendTransaction(simpleDescriptorRequest, simpleDescriptorRequest).get();

SimpleDescriptorResponse simpleDescriptorResponse = (SimpleDescriptorResponse)response.getResponse();
logger.debug("{}: Node SVC Discovery: SimpleDescriptorResponse returned {}", node.getIeeeAddress(),
                simpleDescriptorResponse);
        if (simpleDescriptorResponse == null) {
            return null;
        }

        if (simpleDescriptorResponse.getStatus() == ZdoStatus.SUCCESS) {
            ZigBeeEndpoint endpoint = new ZigBeeEndpoint(node, endpointId);
SimpleDescriptor simpleDescriptor = simpleDescriptorResponse.getSimpleDescriptor();
endpoint.setProfileId(simpleDescriptor.getProfileId());
            endpoint.setDeviceId(simpleDescriptor.getDeviceId());
            endpoint.setDeviceVersion(simpleDescriptor.getDeviceVersion());
            endpoint.setInputClusterIds(simpleDescriptor.getInputClusterList());
            endpoint.setOutputClusterIds(simpleDescriptor.getOutputClusterList());

            return endpoint;
        }

        return null;
    }

    private class NodeServiceDiscoveryTask implements Runnable
{
        private int retryCnt = 0;
private int retryMin = 0;

@Override
        public void run()
{
    try
    {
        logger.debug("{}: Node SVC Discovery: running", node.getIeeeAddress());
        NodeDiscoveryTask discoveryTask;

        synchronized(discoveryTasks) {
            discoveryTask = discoveryTasks.peek();
        }
        if (discoveryTask == null)
        {
            lastDiscoveryCompleted = Instant.now();
            logger.debug("{}: Node SVC Discovery: complete", node.getIeeeAddress());
            networkManager.updateNode(node);
            return;
        }

        boolean success = false;
        switch (discoveryTask)
        {
            case NWK_ADDRESS:
                success = requestNetworkAddress();
                break;
            case NODE_DESCRIPTOR:
                success = requestNodeDescriptor();
                break;
            case POWER_DESCRIPTOR:
                success = requestPowerDescriptor();
                break;
            case ACTIVE_ENDPOINTS:
                success = requestActiveEndpoints();
                break;
            case ASSOCIATED_NODES:
                success = requestAssociatedNodes();
                break;
            case NEIGHBORS:
                success = requestNeighborTable();
                break;
            case ROUTES:
                success = requestRoutingTable();
                break;
            default:
                logger.debug("{}: Node SVC Discovery: unknown task: {}", node.getIeeeAddress(), discoveryTask);
                break;
        }

        retryCnt++;
        int retryDelay = 0;
        if (success)
        {
            synchronized(discoveryTasks) {
                discoveryTasks.remove(discoveryTask);
            }
            logger.debug("{}: Node SVC Discovery: request {} successful. Advanced to {}.",
                    node.getIeeeAddress(), discoveryTask, discoveryTasks.peek());

            retryCnt = 0;
        }
        else if (retryCnt > maxBackoff)
        {
            logger.debug("{}: Node SVC Discovery: request {} failed after {} attempts.", node.getIeeeAddress(),
                    discoveryTask, retryCnt);
            synchronized(discoveryTasks) {
                discoveryTasks.remove(discoveryTask);
            }

            retryCnt = 0;
        }
        else
        {
            retryMin = retryCnt / 4;

            // We failed with the last request. Wait a bit then retry.
            retryDelay = (new Random().nextInt(retryCnt) + 1 + retryMin) * retryPeriod;
            logger.debug("{}: Node SVC Discovery: request {} failed. Retry {}, wait {}ms before retry.",
                    node.getIeeeAddress(), discoveryTask, retryCnt, retryDelay);
        }

        // Reschedule the task
        futureTask = networkManager.rescheduleTask(futureTask, this, retryDelay);
    }
    catch (InterruptedException e)
    {
        // Eat me
    }
    catch (Exception e)
    {
        logger.error("{}: Node SVC Discovery: exception: ", node.getIeeeAddress(), e);
    }
}
    }

    /**
     * Starts service discovery for the node.
     */
    public void startDiscovery()
{
    logger.debug("{}: Node SVC Discovery: start discovery", node.getIeeeAddress());
    Set<NodeDiscoveryTask> tasks = new HashSet<NodeDiscoveryTask>();

    // Always request the network address - in case it's changed
    tasks.add(NodeDiscoveryTask.NWK_ADDRESS);

    if (node.getNodeDescriptor().getLogicalType() == LogicalType.UNKNOWN)
    {
        tasks.add(NodeDiscoveryTask.NODE_DESCRIPTOR);
    }

    if (node.getPowerDescriptor().getCurrentPowerMode() == CurrentPowerModeType.UNKNOWN)
    {
        tasks.add(NodeDiscoveryTask.POWER_DESCRIPTOR);
    }

    if (node.getEndpoints().size() == 0 && node.getNetworkAddress() != 0)
    {
        tasks.add(NodeDiscoveryTask.ACTIVE_ENDPOINTS);
    }

    tasks.add(NodeDiscoveryTask.NEIGHBORS);

    startDiscovery(tasks);
}

/**
 * Starts service discovery for the node in order to update the mesh. This adds the
 * {@link NodeDiscoveryTask#NEIGHBORS} and {@link NodeDiscoveryTask#ROUTES} tasks to the task list. Note that
 * {@link NodeDiscoveryTask#ROUTES} is not added for end devices.
 */
public void updateMesh()
{
    logger.debug("{}: Node SVC Discovery: Update mesh", node.getIeeeAddress());
    Set<NodeDiscoveryTask> tasks = new HashSet<NodeDiscoveryTask>();

    tasks.add(NodeDiscoveryTask.NEIGHBORS);

    if (node.getNodeDescriptor().getLogicalType() != LogicalType.END_DEVICE)
    {
        tasks.add(NodeDiscoveryTask.ROUTES);
    }

    startDiscovery(tasks);
}

/**
 * Gets the collection of {@link NodeDiscoveryTask}s that are currently outstanding for this discoverer
 *
 * @return collection of {@link NodeDiscoveryTask}s
 */
public Collection<NodeDiscoveryTask> getTasks()
{
    return discoveryTasks;
}

/**
 * Gets the {@link ZigBeeNode} to which this service discoverer is associated
 *
 * @return the {@link ZigBeeNode}
 */
public ZigBeeNode getNode()
{
    return node;
}

/**
 * Gets the time the last discovery was started.
 *
 * @return the {@link Instant} that the last discovery was started
 */
public Instant getLastDiscoveryStarted()
{
    return lastDiscoveryStarted;
}

/**
 * Gets the time the last discovery was completed.
 *
 * @return the {@link Instant} that the last discovery was completed
 */
public Instant getLastDiscoveryCompleted()
{
    return lastDiscoveryCompleted;
}
}
}
