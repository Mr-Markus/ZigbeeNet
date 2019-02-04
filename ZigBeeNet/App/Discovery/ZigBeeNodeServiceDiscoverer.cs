using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.Logging;
using ZigBeeNet;
using ZigBeeNet.ZDO;
using ZigBeeNet.ZDO.Command;
using ZigBeeNet.ZDO.Field;

namespace ZigBeeNet.App.Discovery
{
    /**
     * This class contains methods for discovering the services and features of a {@link ZigBeeNode}. All discovery methods
     * are private and the class is utilised by calling {@link #startDiscovery(Set)} with a set of
     * {@link #NodeDiscoveryState} for the stages wishing to be discovered or updated.
     * 
     * A single worker thread is ensured - if the thread is already active when {@link #startDiscovery(Set)} is called, the
     * new tasks will be added to the existing task queue if they are not already in the queue. If the worker thread is not
     * running, it will be started.
     * 
     * This class provides a centralised helper, used for discovering and updating information about the {@link ZigBeeNode}
     * 
     * A random exponential backoff is used for retries to reduce congestion. If the device replies that a command is not
     * supported, then this will not be issued again on subsequent requests.
     * 
     * Once the discovery update is complete the {@link ZigBeeNetworkManager#updateNode(ZigBeeNode)} is called to alert
     * users.
     */
    public class ZigBeeNodeServiceDiscoverer
    {
        /**
         * The _logger.
         */
        private readonly ILog _logger = LogProvider.For<ZigBeeNetworkManager>();

        /**
         * The {@link ZigBeeNetworkManager}.
         */
        public ZigBeeNetworkManager NetworkManager { get; private set; }

        /**
       * The maximum number of retries to perform before failing the request
       */
        public int MaxBackoff { get; set; } = DEFAULT_MAX_BACKOFF;

        /**
         * The {@link ZigBeeNode}.
         */
        public ZigBeeNode Node { get; set; }

        /**
         * Default maximum number of retries to perform
         */
        private const int DEFAULT_MAX_BACKOFF = 2;

        /**
         * Default period between retries
         */
        private const int DEFAULT_RETRY_PERIOD = 2100;

        /**
         * A random jitter will be added to the retry time for each device to avoid any sort of synchronisation
         */
        private const int RETRY_RANDOM_TIME = 250;

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

        ///**
        // * The task being run
        // */
        //private Task _futureTask;

        private CancellationTokenSource _cancellationTokenSource;

        /**
         * Record of the last time we started a service discovery or update
         */
        public DateTime LastDiscoveryStarted { get; private set; }

        /**
         * Record of the last time we completed a service discovery or update
         */
        public DateTime LastDiscoveryCompleted { get; private set; }

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
        public Queue<NodeDiscoveryTask> DiscoveryTasks { get; private set; } = new Queue<NodeDiscoveryTask>();

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

            _cancellationTokenSource = new CancellationTokenSource();
        }

        /**
         * Performs node service discovery. This discovers node level attributes such as the endpoints and
         * descriptors, as well as updating routing and neighbor tables - as needed.
         * 
         * If any of the tasks requested are already in the queue, they will not be added again.
         * 
         * If the worker thread is not running, it will be started.
         *
         * @param newTasks a set of {@link NodeDiscoveryTask}s to be performed
         */
        private void StartDiscovery(List<NodeDiscoveryTask> newTasks)
        {
            // Tasks are managed in a queue. The worker thread will only remove the task from the queue once the task is
            // complete. When no tasks are left in the queue, the worker thread will exit.
            lock (DiscoveryTasks)
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
                    _logger.Debug("{IeeeAddress}: Node SVC Discovery: has no tasks to perform", Node.IeeeAddress);
                    return;
                }

                bool startWorker = DiscoveryTasks.Count == 0;

                // Add new tasks, avoiding any duplication
                foreach (NodeDiscoveryTask newTask in newTasks)
                {
                    if (!DiscoveryTasks.Contains(newTask))
                    {
                        DiscoveryTasks.Enqueue(newTask);
                    }
                }

                if (!startWorker)
                {
                    _logger.Debug("{IeeeAddress}: Node SVC Discovery: already scheduled or running", Node.IeeeAddress);
                }
                else
                {
                    LastDiscoveryStarted = DateTime.UtcNow;
                }
            }

            _logger.Debug("{IeeeAddress}: Node SVC Discovery: scheduled {Task}", Node.IeeeAddress, DiscoveryTasks);

            var nodeDiscoveryTask = GetNodeServiceDiscoveryTask();

            //NetworkManager.ScheduleTask(nodeDiscoveryTask, new Random().Next(_retryPeriod));
        }

        private async Task Discovery(CancellationToken ct, int cnt)
        {
            int retryCnt = cnt;//0;
            int retryMin = 0;

            try
            {
                _logger.Debug("{IeeeAddress}: Node SVC Discovery: running", Node.IeeeAddress);
                NodeDiscoveryTask? discoveryTask = null;

                lock (DiscoveryTasks)
                {
                    if (DiscoveryTasks.Count != 0)
                    {
                        discoveryTask = DiscoveryTasks.Peek();
                    }
                }

                if (discoveryTask == null)
                {
                    LastDiscoveryCompleted = DateTime.UtcNow;
                    _logger.Debug("{IeeeAddress}: Node SVC Discovery: complete", Node.IeeeAddress);
                    NetworkManager.UpdateNode(Node);
                    return;
                }

                ct.ThrowIfCancellationRequested();

                bool success = false;
                switch (discoveryTask.Value)
                {
                    case NodeDiscoveryTask.NWK_ADDRESS:
                        success = await RequestNetworkAddress();
                        break;
                    case NodeDiscoveryTask.NODE_DESCRIPTOR:
                        success = await RequestNodeDescriptor();
                        break;
                    case NodeDiscoveryTask.POWER_DESCRIPTOR:
                        success = await RequestPowerDescriptor();
                        break;
                    case NodeDiscoveryTask.ACTIVE_ENDPOINTS:
                        success = await RequestActiveEndpoints();
                        break;
                    case NodeDiscoveryTask.ASSOCIATED_NODES:
                        success = await RequestAssociatedNodes();
                        break;
                    case NodeDiscoveryTask.NEIGHBORS:
                        success = await RequestNeighborTable();
                        break;
                    case NodeDiscoveryTask.ROUTES:
                        success = await RequestRoutingTable();
                        break;
                    default:
                        _logger.Debug("{IeeeAddress}: Node SVC Discovery: unknown task: {Task}", Node.IeeeAddress, discoveryTask);
                        break;
                }

                ct.ThrowIfCancellationRequested();

                retryCnt++;
                int retryDelay = 0;
                if (success)
                {
                    lock (DiscoveryTasks)
                    {
                        DiscoveryTasks = new Queue<NodeDiscoveryTask>(DiscoveryTasks.Where(t => t != discoveryTask));
                    }

                    _logger.Debug("{IeeeAddress}: Node SVC Discovery: request {Task} successful.", Node.IeeeAddress, discoveryTask);

                    retryCnt = 0;
                }
                else if (retryCnt > MaxBackoff)
                {
                    _logger.Debug("{IeeeAddress}: Node SVC Discovery: request {Task} failed after {Count} attempts.", Node.IeeeAddress, discoveryTask, retryCnt);

                    lock (DiscoveryTasks)
                    {
                        DiscoveryTasks = new Queue<NodeDiscoveryTask>(DiscoveryTasks.Where(t => t != discoveryTask));
                    }

                    retryCnt = 0;
                }
                else
                {
                    retryMin = retryCnt / 4;

                    // We failed with the last request. Wait a bit then retry.
                    retryDelay = (new Random().Next(retryCnt) + 1 + retryMin) * _retryPeriod;
                    _logger.Debug("{IeeeAddress}: Node SVC Discovery: request {Task} failed. Retry {Retry}, wait {Delay} ms before retry.", Node.IeeeAddress, discoveryTask, retryCnt, retryDelay);
                }

                // Reschedule the task
                var tmp = _cancellationTokenSource;
                _cancellationTokenSource = new CancellationTokenSource();
                //await NetworkManager.ScheduleTask(Discovery(_cancellationTokenSource.Token), retryDelay);

                await Discovery(_cancellationTokenSource.Token, retryCnt);
                tmp.Cancel();
            }
            catch (TaskCanceledException)
            {
                // Eat me
            }
            catch (OperationCanceledException)
            {
                // Eat me
            }
            catch (Exception e)
            {
                _logger.Error("{IeeeAddress}: Node SVC Discovery: exception: {Exception}", Node.IeeeAddress, e);
            }
        }

        private Task GetNodeServiceDiscoveryTask()
        {
            return Discovery(_cancellationTokenSource.Token, 0);
        }

        /**
         * Stops service discovery and removes any scheduled tasks
         */
        public void StopDiscovery()
        {
            _cancellationTokenSource.Cancel();

            _logger.Debug("{IeeeAddress}: Node SVC Discovery: stopped", Node.IeeeAddress);
        }

        /**
         * Get node descriptor
         *
         * @return true if the message was processed ok
         */
        private async Task<bool> RequestNetworkAddress()
        {
            NetworkAddressRequest networkAddressRequest = new NetworkAddressRequest();
            networkAddressRequest.IeeeAddr = Node.IeeeAddress;
            networkAddressRequest.RequestType = 0;
            networkAddressRequest.StartIndex = 0;
            networkAddressRequest.DestinationAddress = new ZigBeeEndpointAddress(ZigBeeBroadcastDestination.GetBroadcastDestination(BroadcastDestination.BROADCAST_ALL_DEVICES).Key);

            CommandResult response = await NetworkManager.SendTransaction(networkAddressRequest, networkAddressRequest);
            NetworkAddressResponse networkAddressResponse = response.GetResponse<NetworkAddressResponse>();

            _logger.Debug("{NetworkAddress}: Node SVC Discovery: NetworkAddressRequest returned {Response}", Node.NetworkAddress, networkAddressResponse);

            if (networkAddressResponse == null)
            {
                return false;
            }

            if (networkAddressResponse.Status == ZdoStatus.SUCCESS)
            {
                Node.NetworkAddress = networkAddressResponse.NwkAddrRemoteDev;

                return true;
            }

            return false;
        }

        /**
         * Get Node Network address and the list of associated devices
         *
         * @return true if the message was processed ok
         */
        private async Task<bool> RequestAssociatedNodes()
        {
            byte startIndex = 0;
            int totalAssociatedDevices = 0;
            List<ushort> associatedDevices = new List<ushort>();

            do
            {
                // Request extended response, to get associated list
                IeeeAddressRequest ieeeAddressRequest = new IeeeAddressRequest();
                ieeeAddressRequest.DestinationAddress = new ZigBeeEndpointAddress((ushort)Node.NetworkAddress);
                ieeeAddressRequest.RequestType = 1;
                ieeeAddressRequest.StartIndex = startIndex;
                ieeeAddressRequest.NwkAddrOfInterest = Node.NetworkAddress;
                CommandResult response = await NetworkManager.SendTransaction(ieeeAddressRequest, ieeeAddressRequest);

                IeeeAddressResponse ieeeAddressResponse = (IeeeAddressResponse)response.Response;

                _logger.Debug("{IeeeAddress}: Node SVC Discovery: IeeeAddressResponse returned {Response}", Node.IeeeAddress, ieeeAddressResponse);

                if (ieeeAddressResponse != null && ieeeAddressResponse.Status == ZdoStatus.SUCCESS)
                {
                    associatedDevices.AddRange(ieeeAddressResponse.NwkAddrAssocDevList);

                    startIndex += (byte)ieeeAddressResponse.NwkAddrAssocDevList.Count;
                    totalAssociatedDevices = ieeeAddressResponse.NwkAddrAssocDevList.Count;
                }
            } while (startIndex < totalAssociatedDevices);

            Node.AssociatedDevices = associatedDevices;

            return true;
        }

        /**
         * Get node descriptor
         *
         * @return true if the message was processed ok
         */
        private async Task<bool> RequestNodeDescriptor()
        {
            NodeDescriptorRequest nodeDescriptorRequest = new NodeDescriptorRequest();
            nodeDescriptorRequest.DestinationAddress = new ZigBeeEndpointAddress(Node.NetworkAddress);
            nodeDescriptorRequest.NwkAddrOfInterest = Node.NetworkAddress;

            CommandResult response = await NetworkManager.SendTransaction(nodeDescriptorRequest, nodeDescriptorRequest);
            NodeDescriptorResponse nodeDescriptorResponse = (NodeDescriptorResponse)response.Response;

            _logger.Debug("{IeeeAddress}: Node SVC Discovery: NodeDescriptorResponse returned {Response}", Node.IeeeAddress, nodeDescriptorResponse);

            if (nodeDescriptorResponse == null)
            {
                return false;
            }

            if (nodeDescriptorResponse.Status == ZdoStatus.SUCCESS)
            {
                Node.NodeDescriptor = nodeDescriptorResponse.NodeDescriptor;

                return true;
            }

            return false;
        }

        /**
         * Get node power descriptor
         *
         * @return true if the message was processed ok, or if the end device does not support the power descriptor
         */
        private async Task<bool> RequestPowerDescriptor()
        {
            PowerDescriptorRequest powerDescriptorRequest = new PowerDescriptorRequest();
            powerDescriptorRequest.DestinationAddress = new ZigBeeEndpointAddress((ushort)Node.NetworkAddress);
            powerDescriptorRequest.NwkAddrOfInterest = (ushort)Node.NetworkAddress;

            CommandResult response = await NetworkManager.SendTransaction(powerDescriptorRequest, powerDescriptorRequest);
            PowerDescriptorResponse powerDescriptorResponse = (PowerDescriptorResponse)response.Response;

            _logger.Debug("IeeeAddress{IeeeAddress}: Node SVC Discovery: PowerDescriptorResponse returned {Response}", Node.IeeeAddress, powerDescriptorResponse);

            if (powerDescriptorResponse == null)
            {
                return false;
            }

            if (powerDescriptorResponse.Status == ZdoStatus.SUCCESS)
            {
                Node.PowerDescriptor = powerDescriptorResponse.PowerDescriptor;

                return true;
            }
            else if (powerDescriptorResponse.Status == ZdoStatus.NOT_SUPPORTED)
            {
                return true;
            }

            return false;
        }

        /**
         * Get the active endpoints for a node
         *
         * @return true if the message was processed ok
         */
        private async Task<bool> RequestActiveEndpoints()
        {
            ActiveEndpointsRequest activeEndpointsRequest = new ActiveEndpointsRequest();
            activeEndpointsRequest.DestinationAddress = new ZigBeeEndpointAddress((ushort)Node.NetworkAddress);
            activeEndpointsRequest.NwkAddrOfInterest = Node.NetworkAddress;

            CommandResult response = await NetworkManager.SendTransaction(activeEndpointsRequest, activeEndpointsRequest);
            ActiveEndpointsResponse activeEndpointsResponse = (ActiveEndpointsResponse)response.Response;

            _logger.Debug("{IeeeAddress}: Node SVC Discovery: ActiveEndpointsResponse returned {Response}", Node.IeeeAddress, response);

            if (activeEndpointsResponse == null)
            {
                return false;
            }

            // Get the simple descriptors for all endpoints
            List<ZigBeeEndpoint> endpoints = new List<ZigBeeEndpoint>();
            foreach (byte endpointId in activeEndpointsResponse.ActiveEpList)
            {
                ZigBeeEndpoint endpoint = await GetSimpleDescriptor(endpointId);
                if (endpoint == null)
                {
                    return false;
                }

                endpoints.Add(endpoint);
            }

            // All endpoints have been received, so add them to the node
            foreach (ZigBeeEndpoint endpoint in endpoints)
            {
                Node.AddEndpoint(endpoint);
            }

            return true;
        }

        /**
         * Get node neighbor table by making a {@link ManagementLqiRequest} call.
         *
         * @return list of {@link NeighborTable} if the request was processed ok, null otherwise
         */
        private async Task<bool> RequestNeighborTable()
        {
            // Start index for the list is 0
            byte startIndex = 0;
            int totalNeighbors = 0;
            List<NeighborTable> neighbors = new List<NeighborTable>();
            do
            {
                ManagementLqiRequest neighborRequest = new ManagementLqiRequest();
                neighborRequest.DestinationAddress = new ZigBeeEndpointAddress((ushort)Node.NetworkAddress);
                neighborRequest.StartIndex = startIndex;

                CommandResult response = await NetworkManager.SendTransaction(neighborRequest, neighborRequest);
                ManagementLqiResponse neighborResponse = (ManagementLqiResponse)response.Response;

                _logger.Debug("{IeeeAddress}: Node SVC Discovery: ManagementLqiRequest response {Response}", Node.IeeeAddress, response);

                if (neighborResponse == null)
                {
                    return false;
                }

                if (neighborResponse.Status == ZdoStatus.NOT_SUPPORTED)
                {
                    _logger.Debug("{IeeeAddress}: Node SVC Discovery: ManagementLqiRequest not supported", Node.IeeeAddress);
                    _supportsManagementLqi = false;
                    return true;
                }
                else if (neighborResponse.Status != ZdoStatus.SUCCESS)
                {
                    _logger.Debug("{IeeeAddress}: Node SVC Discovery: ManagementLqiRequest failed", Node.IeeeAddress);
                    return false;
                }

                // Some devices may report the number of entries as the total number they can maintain.
                // To avoid a loop, we need to check if there's any response.
                if (neighborResponse.NeighborTableList.Count == 0)
                {
                    break;
                }

                // Save the neighbors
                neighbors.AddRange(neighborResponse.NeighborTableList);

                // Continue with next request
                startIndex += (byte)neighborResponse.NeighborTableList.Count;
                totalNeighbors = neighborResponse.NeighborTableEntries;

            } while (startIndex < totalNeighbors);

            _logger.Debug("{IeeeAddress}: Node SVC Discovery: ManagementLqiRequest complete [{Count} neighbors]", Node.IeeeAddress, neighbors.Count);

            Node.Neighbors = neighbors;

            return true;
        }

        /**
         * Get node routing table by making a {@link ManagementRoutingRequest} request
         *
         * @return list of {@link RoutingTable} if the request was processed ok, null otherwise
         */
        private async Task<bool> RequestRoutingTable()
        {
            // Start index for the list is 0
            byte startIndex = 0;
            int totalRoutes = 0;
            List<RoutingTable> routes = new List<RoutingTable>();

            do
            {
                ManagementRoutingRequest routeRequest = new ManagementRoutingRequest();
                routeRequest.DestinationAddress  = new ZigBeeEndpointAddress((ushort)Node.NetworkAddress);
                routeRequest.StartIndex = startIndex;

                CommandResult response = await NetworkManager.SendTransaction(routeRequest, routeRequest);
                ManagementRoutingResponse routingResponse = (ManagementRoutingResponse)response.Response;

                _logger.Debug("{IeeeAddress}: Node SVC Discovery: ManagementRoutingRequest returned {Response}", Node.IeeeAddress, response);

                if (routingResponse == null)
                {
                    return false;
                }

                if (routingResponse.Status == ZdoStatus.NOT_SUPPORTED)
                {
                    _logger.Debug("{IeeeAddress}: Node SVC Discovery ManagementLqiRequest not supported", Node.IeeeAddress);

                    _supportsManagementRouting = false;

                    return true;
                }
                else if (routingResponse.Status != ZdoStatus.SUCCESS)
                {
                    _logger.Debug("{IeeeAddress}: Node SVC Discovery: ManagementLqiRequest failed", Node.IeeeAddress);

                    return false;
                }

                // Save the routes
                routes.AddRange(routingResponse.RoutingTableList);

                // Continue with next request
                startIndex += (byte)routingResponse.RoutingTableList.Count;
                totalRoutes = routingResponse.RoutingTableEntries;

            } while (startIndex < totalRoutes);

            _logger.Debug("{IeeeAddress}: Node SVC Discovery: ManagementLqiRequest complete [{Count} routes]", Node.IeeeAddress, routes.Count);

            Node.Routes = routes;

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
        private async Task<ZigBeeEndpoint> GetSimpleDescriptor(byte endpointId)
        {
            SimpleDescriptorRequest simpleDescriptorRequest = new SimpleDescriptorRequest();
            simpleDescriptorRequest.DestinationAddress = new ZigBeeEndpointAddress((ushort)Node.NetworkAddress);
            simpleDescriptorRequest.NwkAddrOfInterest = Node.NetworkAddress;
            simpleDescriptorRequest.Endpoint = endpointId;

            CommandResult response = await NetworkManager.SendTransaction(simpleDescriptorRequest, simpleDescriptorRequest);
            SimpleDescriptorResponse simpleDescriptorResponse = (SimpleDescriptorResponse)response.Response;

            _logger.Debug("{IeeeAddress}: Node SVC Discovery: SimpleDescriptorResponse returned {Response}", Node.IeeeAddress, simpleDescriptorResponse);

            if (simpleDescriptorResponse == null)
            {
                return null;
            }

            if (simpleDescriptorResponse.Status == ZdoStatus.SUCCESS)
            {
                ZigBeeEndpoint endpoint = new ZigBeeEndpoint(Node, endpointId);
                SimpleDescriptor simpleDescriptor = simpleDescriptorResponse.SimpleDescriptor;

                endpoint.ProfileId = simpleDescriptor.ProfileId;
                endpoint.DeviceId = simpleDescriptor.DeviceId;
                endpoint.DeviceVersion = simpleDescriptor.DeviceVersion;
                endpoint.SetInputClusterIds(simpleDescriptor.InputClusterList.Select(id => id).ToList());
                endpoint.SetOutputClusterIds(simpleDescriptor.OutputClusterList.Select(id => id).ToList());

                return endpoint;
            }

            return null;
        }

        /**
         * Starts service discovery for the node.
         */
        public void StartDiscovery()
        {
            _logger.Debug("{IeeeAddress}: Node SVC Discovery: start discovery", Node.IeeeAddress);

            List<NodeDiscoveryTask> tasks = new List<NodeDiscoveryTask>();

            // Always request the network address - in case it's changed
            tasks.Add(NodeDiscoveryTask.NWK_ADDRESS);

            if (Node.NodeDescriptor.LogicalNodeType == NodeDescriptor.LogicalType.UNKNOWN)
            {
                tasks.Add(NodeDiscoveryTask.NODE_DESCRIPTOR);
            }

            if (Node.PowerDescriptor.CurrentPowerMode == PowerDescriptor.CurrentPowerModeType.UNKNOWN)
            {
                tasks.Add(NodeDiscoveryTask.POWER_DESCRIPTOR);
            }

            if (Node.Endpoints.Count == 0 && Node.NetworkAddress != 0)
            {
                tasks.Add(NodeDiscoveryTask.ACTIVE_ENDPOINTS);
            }

            tasks.Add(NodeDiscoveryTask.NEIGHBORS);

            StartDiscovery(tasks);
        }

        /**
         * Starts service discovery for the node in order to update the mesh. This adds the
         * {@link NodeDiscoveryTask#NEIGHBORS} and {@link NodeDiscoveryTask#ROUTES} tasks to the task list. Note that
         * {@link NodeDiscoveryTask#ROUTES} is not added for end devices.
         */
        public void UpdateMesh()
        {
            _logger.Debug("{IeeeAddress}: Node SVC Discovery: Update mesh", Node.IeeeAddress);

            List<NodeDiscoveryTask> tasks = new List<NodeDiscoveryTask>();

            tasks.Add(NodeDiscoveryTask.NEIGHBORS);

            if (Node.NodeDescriptor.LogicalNodeType != NodeDescriptor.LogicalType.END_DEVICE)
            {
                tasks.Add(NodeDiscoveryTask.ROUTES);
            }

            StartDiscovery(tasks);
        }

    }
}
