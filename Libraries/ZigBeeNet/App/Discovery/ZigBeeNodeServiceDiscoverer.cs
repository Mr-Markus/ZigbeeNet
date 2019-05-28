using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet;
using ZigBeeNet.ZDO;
using ZigBeeNet.ZDO.Command;
using ZigBeeNet.ZDO.Field;
using Serilog;

namespace ZigBeeNet.App.Discovery
{
    /// <summary>
    /// This class contains methods for discovering the services and features of a <see cref="ZigBeeNode"/>. All discovery methods
    /// are private and the class is utilised by calling StartDiscovery() with a set of
    /// <see cref="NodeDiscoveryState"/> for the stages wishing to be discovered or updated.
    /// 
    /// A single worker thread is ensured - if the thread is already active when StartDiscovery() is called, the
    /// new tasks will be added to the existing task queue if they are not already in the queue. If the worker thread is not
    /// running, it will be started.
    /// 
    /// This class provides a centralised helper, used for discovering and updating information about the <see cref="ZigBeeNode"/>
    /// 
    /// A random exponential backoff is used for retries to reduce congestion. If the device replies that a command is not
    /// supported, then this will not be issued again on subsequent requests.
    /// 
    /// Once the discovery update is complete the ZigBeeNetworkManager#updateNode() is called to alert
    /// users.
    /// </summary>
    public class ZigBeeNodeServiceDiscoverer
    {
        /// <summary>
        /// The <see cref="ZigBeeNetworkManager"/>.
        /// </summary>
        public ZigBeeNetworkManager NetworkManager { get; private set; }

        /// <summary>
        /// The maximum number of retries to perform before failing the request
        /// </summary>
        public int MaxBackoff { get; set; } = DEFAULT_MAX_BACKOFF;

        /// <summary>
        /// The <see cref="ZigBeeNode"/>.
        /// </summary>
        public ZigBeeNode Node { get; set; }

        /// <summary>
        /// Default maximum number of retries to perform
        /// </summary>
        private const int DEFAULT_MAX_BACKOFF = 8;

        /// <summary>
        /// Default period between retries
        /// </summary>
        private const int DEFAULT_RETRY_PERIOD = 2100;

        /// <summary>
        /// A random jitter will be added to the retry time for each device to avoid any sort of synchronisation
        /// </summary>
        private const int RETRY_RANDOM_TIME = 250;

        /// <summary>
        /// The minimum number of milliseconds to wait before retrying the request
        /// </summary>
        private readonly int _retryPeriod = DEFAULT_RETRY_PERIOD;

        /// <summary>
        /// Flag to indicate if the device supports the <see cref="ManagementLqiRequest"/>
        /// This is updated to false if the device responds with <see cref="ZdoStatus.NOT_SUPPORTED"/>
        /// </summary>
        private bool _supportsManagementLqi = true;

        /// <summary>
        /// Flag to indicate if the device supports the <see cref="ManagementRoutingRequest"/>.
        /// This is updated to false if the device responds with <see cref="ZdoStatus.NOT_SUPPORTED"/>
        /// </summary>
        private bool _supportsManagementRouting = true;

        /// <summary>
        /// The task being run
        /// </summary>
        //private Task _futureTask;

        private CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        /// Record of the last time we started a service discovery or update
        /// </summary>
        public DateTime LastDiscoveryStarted { get; private set; }

        /// <summary>
        /// Record of the last time we completed a service discovery or update
        /// </summary>
        public DateTime LastDiscoveryCompleted { get; private set; }

        /// <summary>
        ///
        ///
        /// </summary>
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

        /// <summary>
        /// The list of tasks we need to complete
        /// </summary>
        public Queue<NodeDiscoveryTask> DiscoveryTasks { get; private set; } = new Queue<NodeDiscoveryTask>();

        /// <summary>
        /// Creates the discovery class
        ///
        /// <param name="networkManager">the <see cref="ZigBeeNetworkManager"/> for the network</param>
        /// <param name="node">the <see cref="ZigBeeNode"/> whose services we want to discover</param>
        /// </summary>
        public ZigBeeNodeServiceDiscoverer(ZigBeeNetworkManager networkManager, ZigBeeNode node)
        {
            this.NetworkManager = networkManager;
            this.Node = node;

            _retryPeriod = DEFAULT_RETRY_PERIOD + new Random().Next(RETRY_RANDOM_TIME);

            _cancellationTokenSource = new CancellationTokenSource();
        }

        /// <summary>
        /// Performs node service discovery. This discovers node level attributes such as the endpoints and
        /// descriptors, as well as updating routing and neighbor tables - as needed.
        /// 
        /// If any of the tasks requested are already in the queue, they will not be added again.
        /// 
        /// If the worker thread is not running, it will be started.
        ///
        /// <param name="newTasks">a set of <see cref="NodeDiscoveryTask"/>s to be performed</param>
        /// </summary>
        private async Task StartDiscoveryAsync(List<NodeDiscoveryTask> newTasks)
        {
            // Tasks are managed in a queue. The worker thread will only remove the task from the queue once the task is
            // complete. When no tasks are left in the queue, the worker thread will exit.
            lock (DiscoveryTasks)
            {
                // Remove any tasks that we know are not supported by this device
                if ((!_supportsManagementLqi || Node.NodeDescriptor.LogicalNodeType == NodeDescriptor.LogicalType.UNKNOWN) && newTasks.Contains(NodeDiscoveryTask.NEIGHBORS))
                {
                    newTasks.Remove(NodeDiscoveryTask.NEIGHBORS);
                }

                if ((!_supportsManagementRouting ||
                    Node.NodeDescriptor.LogicalNodeType == NodeDescriptor.LogicalType.UNKNOWN ||
                    Node.NodeDescriptor.LogicalNodeType == NodeDescriptor.LogicalType.END_DEVICE)
                    && newTasks.Contains(NodeDiscoveryTask.ROUTES))
                {
                    newTasks.Remove(NodeDiscoveryTask.ROUTES);
                }

                // Make sure there are still tasks to perform
                if (newTasks.Count == 0)
                {
                    Log.Debug("{IeeeAddress}: Node SVC Discovery: has no tasks to perform", Node.IeeeAddress);
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
                    Log.Debug("{IeeeAddress}: Node SVC Discovery: already scheduled or running", Node.IeeeAddress);
                }
                else
                {
                    LastDiscoveryStarted = DateTime.UtcNow;
                }
            }

            Log.Debug("{IeeeAddress}: Node SVC Discovery: scheduled {Task}", Node.IeeeAddress, DiscoveryTasks);

            await GetNodeServiceDiscoveryTask();
        }

        private async Task Discovery(CancellationToken ct, int cnt)
        {
            int retryCnt = cnt;//0;
            int retryMin = 0;

            try
            {
                Log.Debug("{IeeeAddress}: Node SVC Discovery: running", Node.IeeeAddress);
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
                    Log.Debug("{IeeeAddress}: Node SVC Discovery: complete", Node.IeeeAddress);
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
                        Log.Debug("{IeeeAddress}: Node SVC Discovery: unknown task: {Task}", Node.IeeeAddress, discoveryTask);
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

                    Log.Debug("{IeeeAddress}: Node SVC Discovery: request {Task} successful.", Node.IeeeAddress, discoveryTask);

                    retryCnt = 0;
                }
                else if (retryCnt > MaxBackoff)
                {
                    Log.Debug("{IeeeAddress}: Node SVC Discovery: request {Task} failed after {Count} attempts.", Node.IeeeAddress, discoveryTask, retryCnt);

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
                    retryDelay = (new Random().Next(retryCnt) + 1 + retryMin); /// _retryPeriod;
                    Log.Debug("{IeeeAddress}: Node SVC Discovery: request {Task} failed. Retry {Retry}, wait {Delay} ms before retry.", Node.IeeeAddress, discoveryTask, retryCnt, retryDelay);
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
                Log.Error("{IeeeAddress}: Node SVC Discovery: exception: {Exception}", Node.IeeeAddress, e);
            }
        }

        private Task GetNodeServiceDiscoveryTask()
        {
            return Discovery(_cancellationTokenSource.Token, 0);
        }

        /// <summary>
        /// Stops service discovery and removes any scheduled tasks
        /// </summary>
        public void StopDiscovery()
        {
            _cancellationTokenSource.Cancel();

            Log.Debug("{IeeeAddress}: Node SVC Discovery: stopped", Node.IeeeAddress);
        }

        /// <summary>
        /// Get node descriptor
        ///
        /// <returns>true if the message was processed ok</returns>
        /// </summary>
        private async Task<bool> RequestNetworkAddress()
        {
            NetworkAddressRequest networkAddressRequest = new NetworkAddressRequest();
            networkAddressRequest.IeeeAddr = Node.IeeeAddress;
            networkAddressRequest.RequestType = 0;
            networkAddressRequest.StartIndex = 0;
            networkAddressRequest.DestinationAddress = new ZigBeeEndpointAddress(ZigBeeBroadcastDestination.GetBroadcastDestination(BroadcastDestination.BROADCAST_ALL_DEVICES).Key);

            CommandResult response = await NetworkManager.SendTransaction(networkAddressRequest, networkAddressRequest);
            NetworkAddressResponse networkAddressResponse = response.GetResponse<NetworkAddressResponse>();

            Log.Debug("{NetworkAddress}: Node SVC Discovery: NetworkAddressRequest returned {Response}", Node.NetworkAddress, networkAddressResponse);

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

        /// <summary>
        /// Get Node Network address and the list of associated devices
        ///
        /// <returns>true if the message was processed ok</returns>
        /// </summary>
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

                Log.Debug("{IeeeAddress}: Node SVC Discovery: IeeeAddressResponse returned {Response}", Node.IeeeAddress, ieeeAddressResponse);

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

        /// <summary>
        /// Get node descriptor
        ///
        /// <returns>true if the message was processed ok</returns>
        /// </summary>
        private async Task<bool> RequestNodeDescriptor()
        {
            NodeDescriptorRequest nodeDescriptorRequest = new NodeDescriptorRequest();
            nodeDescriptorRequest.DestinationAddress = new ZigBeeEndpointAddress(Node.NetworkAddress);
            nodeDescriptorRequest.NwkAddrOfInterest = Node.NetworkAddress;

            CommandResult response = await NetworkManager.SendTransaction(nodeDescriptorRequest, nodeDescriptorRequest);
            NodeDescriptorResponse nodeDescriptorResponse = (NodeDescriptorResponse)response.Response;

            Log.Debug("{IeeeAddress}: Node SVC Discovery: NodeDescriptorResponse returned {Response}", Node.IeeeAddress, nodeDescriptorResponse);

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

        /// <summary>
        /// Get node power descriptor
        ///
        /// <returns>true if the message was processed ok, or if the end device does not support the power descriptor</returns>
        /// </summary>
        private async Task<bool> RequestPowerDescriptor()
        {
            PowerDescriptorRequest powerDescriptorRequest = new PowerDescriptorRequest();
            powerDescriptorRequest.DestinationAddress = new ZigBeeEndpointAddress((ushort)Node.NetworkAddress);
            powerDescriptorRequest.NwkAddrOfInterest = (ushort)Node.NetworkAddress;

            CommandResult response = await NetworkManager.SendTransaction(powerDescriptorRequest, powerDescriptorRequest);
            PowerDescriptorResponse powerDescriptorResponse = (PowerDescriptorResponse)response.Response;

            Log.Debug("IeeeAddress{IeeeAddress}: Node SVC Discovery: PowerDescriptorResponse returned {Response}", Node.IeeeAddress, powerDescriptorResponse);

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

        /// <summary>
        /// Get the active endpoints for a node
        ///
        /// <returns>true if the message was processed ok</returns>
        /// </summary>
        private async Task<bool> RequestActiveEndpoints()
        {
            ActiveEndpointsRequest activeEndpointsRequest = new ActiveEndpointsRequest();
            activeEndpointsRequest.DestinationAddress = new ZigBeeEndpointAddress((ushort)Node.NetworkAddress);
            activeEndpointsRequest.NwkAddrOfInterest = Node.NetworkAddress;

            CommandResult response = await NetworkManager.SendTransaction(activeEndpointsRequest, activeEndpointsRequest);
            ActiveEndpointsResponse activeEndpointsResponse = (ActiveEndpointsResponse)response.Response;

            Log.Debug("{IeeeAddress}: Node SVC Discovery: ActiveEndpointsResponse returned {Response}", Node.IeeeAddress, response);

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

        /// <summary>
        /// Get node neighbor table by making a <see cref="ManagementLqiRequest"> call.
        ///
        /// <returns>list of <see cref="NeighborTable"> if the request was processed ok, null otherwise</returns>
        /// </summary>
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

                Log.Debug("{IeeeAddress}: Node SVC Discovery: ManagementLqiRequest response {Response}", Node.IeeeAddress, response);

                if (neighborResponse == null)
                {
                    return false;
                }

                if (neighborResponse.Status == ZdoStatus.NOT_SUPPORTED)
                {
                    Log.Debug("{IeeeAddress}: Node SVC Discovery: ManagementLqiRequest not supported", Node.IeeeAddress);
                    _supportsManagementLqi = false;
                    return true;
                }
                else if (neighborResponse.Status != ZdoStatus.SUCCESS)
                {
                    Log.Debug("{IeeeAddress}: Node SVC Discovery: ManagementLqiRequest failed", Node.IeeeAddress);
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

            Log.Debug("{IeeeAddress}: Node SVC Discovery: ManagementLqiRequest complete [{Count} neighbors]", Node.IeeeAddress, neighbors.Count);

            Node.Neighbors = neighbors;

            return true;
        }

        /// <summary>
        /// Get node routing table by making a <see cref="ManagementRoutingRequest"> request
        ///
        /// <returns>list of <see cref="RoutingTable"> if the request was processed ok, null otherwise</returns>
        /// </summary>
        private async Task<bool> RequestRoutingTable()
        {
            // Start index for the list is 0
            byte startIndex = 0;
            int totalRoutes = 0;
            List<RoutingTable> routes = new List<RoutingTable>();

            do
            {
                ManagementRoutingRequest routeRequest = new ManagementRoutingRequest();
                routeRequest.DestinationAddress = new ZigBeeEndpointAddress((ushort)Node.NetworkAddress);
                routeRequest.StartIndex = startIndex;

                CommandResult response = await NetworkManager.SendTransaction(routeRequest, routeRequest);
                ManagementRoutingResponse routingResponse = (ManagementRoutingResponse)response.Response;

                Log.Debug("{IeeeAddress}: Node SVC Discovery: ManagementRoutingRequest returned {Response}", Node.IeeeAddress, response);

                if (routingResponse == null)
                {
                    return false;
                }

                if (routingResponse.Status == ZdoStatus.NOT_SUPPORTED)
                {
                    Log.Debug("{IeeeAddress}: Node SVC Discovery ManagementLqiRequest not supported", Node.IeeeAddress);

                    _supportsManagementRouting = false;

                    return true;
                }
                else if (routingResponse.Status != ZdoStatus.SUCCESS)
                {
                    Log.Debug("{IeeeAddress}: Node SVC Discovery: ManagementLqiRequest failed", Node.IeeeAddress);

                    return false;
                }

                // Save the routes
                routes.AddRange(routingResponse.RoutingTableList);

                // Continue with next request
                startIndex += (byte)routingResponse.RoutingTableList.Count;
                totalRoutes = routingResponse.RoutingTableEntries;

            } while (startIndex < totalRoutes);

            Log.Debug("{IeeeAddress}: Node SVC Discovery: ManagementLqiRequest complete [{Count} routes]", Node.IeeeAddress, routes.Count);

            Node.Routes = routes;

            return true;
        }

        /// <summary>
        /// Get the simple descriptor for an endpoint and create the <see cref="ZigBeeEndpoint">
        ///
        /// <param name="endpointId">the endpoint id to request</param>
        /// <returns>the newly created <see cref="ZigBeeEndpoint"> for the endpoint, or null on error</returns>
        /// </summary>
        private async Task<ZigBeeEndpoint> GetSimpleDescriptor(byte endpointId)
        {
            SimpleDescriptorRequest simpleDescriptorRequest = new SimpleDescriptorRequest();
            simpleDescriptorRequest.DestinationAddress = new ZigBeeEndpointAddress((ushort)Node.NetworkAddress);
            simpleDescriptorRequest.NwkAddrOfInterest = Node.NetworkAddress;
            simpleDescriptorRequest.Endpoint = endpointId;

            CommandResult response = await NetworkManager.SendTransaction(simpleDescriptorRequest, simpleDescriptorRequest);
            SimpleDescriptorResponse simpleDescriptorResponse = (SimpleDescriptorResponse)response.Response;

            Log.Debug("{IeeeAddress}: Node SVC Discovery: SimpleDescriptorResponse returned {Response}", Node.IeeeAddress, simpleDescriptorResponse);

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

        /// <summary>
        /// Starts service discovery for the node.
        /// </summary>
        public async Task StartDiscovery()
        {
            Log.Debug("{IeeeAddress}: Node SVC Discovery: start discovery", Node.IeeeAddress);

            List<NodeDiscoveryTask> tasks = new List<NodeDiscoveryTask>();

            // Always request the network address unless this is our local node - in case it's changed
            if (!NetworkManager.LocalNwkAddress.Equals(Node.NetworkAddress))
            {
                tasks.Add(NodeDiscoveryTask.NWK_ADDRESS);
            }

            if (Node.NodeDescriptor.LogicalNodeType == NodeDescriptor.LogicalType.UNKNOWN)
            {
                tasks.Add(NodeDiscoveryTask.NODE_DESCRIPTOR);
            }

            if (Node.PowerDescriptor.CurrentPowerMode == PowerDescriptor.CurrentPowerModeType.UNKNOWN)
            {
                tasks.Add(NodeDiscoveryTask.POWER_DESCRIPTOR);
            }

            if (Node.Endpoints.Count == 0 && !NetworkManager.LocalNwkAddress.Equals(Node.NetworkAddress))
            {
                tasks.Add(NodeDiscoveryTask.ACTIVE_ENDPOINTS);
            }

            await StartDiscoveryAsync(tasks);
        }

        /// <summary>
        /// Starts service discovery for the node in order to update the mesh. This adds the
        /// <see cref="NodeDiscoveryTask"></see> and <see cref="NodeDiscoveryTask></see> tasks to the task list. Note that
        /// <see cref="NodeDiscoveryTask</see> is not added for end devices.
        /// </summary>
        public void UpdateMesh()
        {
            Log.Debug("{IeeeAddress}: Node SVC Discovery: Update mesh", Node.IeeeAddress);

            List<NodeDiscoveryTask> tasks = new List<NodeDiscoveryTask>();

            tasks.Add(NodeDiscoveryTask.NEIGHBORS);

            if (Node.NodeDescriptor.LogicalNodeType != NodeDescriptor.LogicalType.END_DEVICE)
            {
                tasks.Add(NodeDiscoveryTask.ROUTES);
            }

            _ = StartDiscoveryAsync(tasks);
        }

    }
}
