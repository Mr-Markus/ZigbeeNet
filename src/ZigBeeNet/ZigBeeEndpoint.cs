using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZigBeeNet.App;
using ZigBeeNet.DAO;
using ZigBeeNet.Logging;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Clusters;
using ZigBeeNet.ZCL.Clusters.General;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet
{
    public class ZigBeeEndpoint
    {
        private ILog _logger = LogProvider.For<ZigBeeEndpoint>();

        ///**
        // * The {@link ZigBeeNetworkManager} that manages this endpoint
        // */
        //private readonly ZigBeeNetworkManager _networkManager;

        /**
         * Link to the parent {@link ZigBeeNode} to which this endpoint belongs
         */
        public ZigBeeNode Node { get; private set; }

        /**
         * The endpoint number for this endpoint. Applications shall only use endpoints 1-254. Endpoints 241-254 shall be
         * used only with the approval of the ZigBee Alliance.
         */
        public byte EndpointId { get; private set; }

        /**
         * The profile ID.
         */
        public ushort ProfileId { get; set; }

        /**
         * The device ID. Specifies the device description supported on this endpoint. Device description identifiers shall
         * be obtained from the ZigBee Alliance.
         */
        public ushort DeviceId { get; set; }

        /**
         * The device version.
         */
        public int DeviceVersion { get; set; }

        /**
         * List of input clusters supported by the endpoint
         */
        private readonly ConcurrentDictionary<int, ZclCluster> _inputClusters = new ConcurrentDictionary<int, ZclCluster>();

        /**
         * List of output clusters supported by the endpoint
         */
        private readonly ConcurrentDictionary<int, ZclCluster> _outputClusters = new ConcurrentDictionary<int, ZclCluster>();

        /**
         * Map of {@link ZigBeeApplication}s that are available to this endpoint. Applications are added
         * with the {@link #addApplication(ZigBeeApplication application)} method and can be retrieved with the
         * {@link #getApplication(int clusterId)} method.
         */
        private readonly ConcurrentDictionary<int, IZigBeeApplication> _applications = new ConcurrentDictionary<int, IZigBeeApplication>();

        /**
         * Constructor
         *
         * @param networkManager the {@link ZigBeeNetworkManager} to which the endpoint belongs
         * @param node the parent {@link ZigBeeNode}
         * @param endpoint the endpoint number within the {@link ZigBeeNode}
         */
        public ZigBeeEndpoint(ZigBeeNode node, byte endpoint)
        {
            this.Node = node;
            this.EndpointId = endpoint;
        }


        /**
         * Gets input cluster IDs. This lists the IDs of all clusters the device
         * supports as a server.
         *
         * @return the {@link Collection} of input cluster IDs
         */
        public IEnumerable<int> GetInputClusterIds()
        {
            return _inputClusters.Keys;
        }

        /**
         * Gets an input cluster
         *
         * @deprecated Use {@link #getInputCluster}
         * @param clusterId
         *            the cluster number
         * @return the cluster or null if cluster is not found
         */

        /**
         * Gets an input cluster
         *
         * @param clusterId the cluster number
         * @return the {@link ZclCluster} or null if cluster is not found
         */
        public ZclCluster GetInputCluster(int clusterId)
        {
            _inputClusters.TryGetValue(clusterId, out ZclCluster cluster);
            return cluster;
        }

        /**
         * Gets an output cluster
         *
         * @param clusterId the cluster number
         * @return the {@link ZclCluster} or null if cluster is not found
         */
        public ZclCluster GetOutputCluster(int clusterId)
        {
            _outputClusters.TryGetValue(clusterId, out ZclCluster cluster);
            return cluster;
        }

        /**
         * Sets input cluster IDs.
         *
         * @param inputClusterIds
         *            the input cluster IDs
         */
        public void SetInputClusterIds(List<ushort> inputClusterIds)
        {
            this._inputClusters.Clear();

            _logger.Debug("{Endpoint}: Setting input clusters {Clusters}", GetEndpointAddress(), inputClusterIds);

            UpdateClusters(_inputClusters, inputClusterIds, true);
        }

        /**
         * Gets the {@link IeeeAddress} for this endpoint from it's parent {@link ZigBeeNode}
         *
         * @return the node {@link IeeeAddress}
         */
        public IeeeAddress GetIeeeAddress()
        {
            return Node.IeeeAddress;
        }

        /**
         * Gets the endpoint address
         *
         * @return the {@link ZigBeeEndpointAddress}
         */
        public ZigBeeEndpointAddress GetEndpointAddress()
        {
            return new ZigBeeEndpointAddress(Node.NetworkAddress, (byte)EndpointId);
        }

        /**
         * Gets output cluster IDs. This provides the IDs of all clusters the endpoint
         * supports as a client.
         *
         * @return the {@link Collection} of output cluster IDs
         */
        public IEnumerable<int> GetOutputClusterIds()
        {
            return _outputClusters.Keys;
        }

        /**
         * Sets output cluster IDs.
         *
         * @param outputClusterIds
         *            the output cluster IDs
         */
        public void SetOutputClusterIds(List<ushort> outputClusterIds)
        {
            this._outputClusters.Clear();

            _logger.Debug("{}: Setting output clusters {}", GetEndpointAddress(), outputClusterIds);

            UpdateClusters(_outputClusters, outputClusterIds, false);
        }

        /**
         * Gets a cluster from the input or output cluster list depending on the command {@link ZclCommandDirection} for a
         * received command.
         * 
         * If commandDirection is {@link ZclCommandDirection#CLIENT_TO_SERVER} then the cluster comes from the output
         * cluster list.
         * If commandDirection is {@link ZclCommandDirection#SERVER_TO_CLIENT} then the cluster comes from the input
         * cluster list.
         *
         * @param clusterId the cluster ID to get
         * @param direction the {@link ZclCommandDirection}
         * @return the {@link ZclCluster} or null if the cluster is not known
         */
        private ZclCluster GetReceiveCluster(int clusterId, ZclCommandDirection direction)
        {
            if (direction == ZclCommandDirection.CLIENT_TO_SERVER)
            {
                return GetOutputCluster(clusterId);
            }
            else
            {
                return GetInputCluster(clusterId);
            }
        }

        public ZclCluster GetClusterClass(ushort clusterId)
        {
            ZclClusterType clusterType = ZclClusterType.GetValueById(clusterId);
            if (clusterType == null)
            {
                // Unsupported cluster
                _logger.Debug("{Endpoint}: Unsupported cluster {Cluster}", GetEndpointAddress(), clusterId);
                return null;
            }

            // Create a cluster class
            if (clusterType.ClusterClass != null)
                return (ZclCluster)Activator.CreateInstance(clusterType.ClusterClass, this);
            else
                return null;
        }

        private void UpdateClusters(ConcurrentDictionary<int, ZclCluster> clusters, List<ushort> newList, bool isInput)
        {
            // Get a list any clusters that are no longer in the list
            List<int> removeIds = new List<int>();

            foreach (ZclCluster cluster in clusters.Values)
            {
                if (newList.Contains(cluster.GetClusterId()))
                {
                    // The existing cluster is in the new list, so no need to remove it
                    continue;
                }

                removeIds.Add(cluster.GetClusterId());
            }

            // Remove clusters no longer in use
            foreach (int id in removeIds)
            {
                _logger.Debug("{Endpoint}: Removing cluster {Cluster}", GetEndpointAddress(), id);
                clusters.TryRemove(id, out ZclCluster not_used);
            }

            // Add any missing clusters into the list
            foreach (ushort id in newList)
            {
                if (!clusters.ContainsKey(id))
                {
                    // Get the cluster type
                    ZclCluster clusterClass = GetClusterClass(id);
                    if (clusterClass == null)
                    {
                        continue;
                    }

                    if (isInput)
                    {
                        _logger.Debug("{EndpointAddress}: Setting cluster {Cluster} as server", GetEndpointAddress(), ZclClusterType.GetValueById(id));
                        clusterClass.SetServer();
                    }
                    else
                    {
                        _logger.Debug("{EndpointAddress}: Setting cluster {Cluster} as client", GetEndpointAddress(), ZclClusterType.GetValueById(id));
                        clusterClass.SetClient();
                    }

                    // Add to our list of clusters
                    clusters.TryAdd(id, clusterClass);
                }
            }
        }


        /**
         * Adds an application and makes it available to this endpoint.
         * The cluster used by the server must be in the output clusters list and this will be passed to the
         * {@link ZclApplication#serverStartup()) method to start the application.
         *
         * @param application the new {@link ZigBeeApplication}
         */
        public void AddApplication(IZigBeeApplication application)
        {
            _applications.TryAdd(application.GetClusterId(), application);
            _outputClusters.TryGetValue(application.GetClusterId(), out ZclCluster cluster);

            if (cluster == null)
            {
                _inputClusters.TryGetValue(application.GetClusterId(), out cluster);
            }

            application.AppStartup(cluster);
        }

        /**
         * Gets the application associated with the clusterId. Returns null if there is no server linked to the requested
         * cluster
         *
         * @param clusterId
         * @return the {@link ZigBeeApplication}
         */
        public IZigBeeApplication GetApplication(int clusterId)
        {
            _applications.TryGetValue(clusterId, out IZigBeeApplication app);

            return app;
        }

        /**
         * Incoming command handler. The endpoint will process any commands addressed to this endpoint ID and pass o
         * clusters and applications
         *
         * @param command the {@link ZclCommand} received
         */
        public void CommandReceived(ZclCommand command)
        {
            if (!command.SourceAddress.Equals(GetEndpointAddress()))
            {
                return;
            }

            // Pass all commands received from this endpoint to any registered applications
            lock (_applications)
            {
                foreach (IZigBeeApplication application in _applications.Values)
                {
                    application.CommandReceived(command);
                }
            }

            // Get the cluster
            ZclCluster cluster = GetReceiveCluster(command.ClusterId, command.CommandDirection);
            if (cluster == null)
            {
                _logger.Debug("{EndpointAdress}: Cluster {Cluster} not found for attribute response", GetEndpointAddress(), command.ClusterId);
                return;
            }

            if (command is ReportAttributesCommand reportAttributesCommand)
            {
                // Pass the reports to the cluster
                cluster.HandleAttributeReport(reportAttributesCommand.Reports);
                return;
            }

            if (command is ReadAttributesResponse readAttributesResponse)
            {
                // Pass the reports to the cluster
                cluster.HandleAttributeStatus(readAttributesResponse.Records);
                return;
            }

            // If this is a specific cluster command, pass the command to the cluster command handler
            if (!command.GenericCommand)
            {
                cluster.HandleCommand(command);
            }
        }

        /**
         * Gets a {@link ZigBeeEndpointDao} used for serialisation of the {@link ZigBeeEndpoint}
         *
         * @return the {@link ZigBeeEndpointDao}
         */
        public ZigBeeEndpointDao GetDao()
        {
            ZigBeeEndpointDao dao = new ZigBeeEndpointDao
            {
                EndpointId = EndpointId,
                ProfileId = ProfileId
            };

            List<ZclClusterDao> clusters;

            clusters = new List<ZclClusterDao>();
            foreach (ZclCluster cluster in _inputClusters.Values)
            {
                clusters.Add(cluster.GetDao());
            }

            dao.SetInputClusters(clusters);

            clusters = new List<ZclClusterDao>();
            foreach (ZclCluster cluster in _outputClusters.Values)
            {
                clusters.Add(cluster.GetDao());
            }
            dao.SetOutputClusters(clusters);

            return dao;
        }

        public void SetDao(ZigBeeEndpointDao dao)
        {
            EndpointId = dao.EndpointId;

            //if (dao.ProfileId != null)
            //{
            ProfileId = dao.ProfileId;
            //}

            if (dao.InputClusterIds != null)
            {
                foreach (ZclClusterDao clusterDao in dao.InputClusters)
                {
                    ZclCluster cluster = GetClusterClass(clusterDao.ClusterId);
                    cluster.SetDao(clusterDao);
                    _inputClusters.TryAdd(clusterDao.ClusterId, cluster);
                }
            }

            if (dao.OutputClusterIds != null)
            {
                foreach (ZclClusterDao clusterDao in dao.OutputClusters)
                {
                    ZclCluster cluster = GetClusterClass(clusterDao.ClusterId);
                    cluster.SetDao(clusterDao);
                    _outputClusters.TryAdd(clusterDao.ClusterId, cluster);
                }
            }
        }

        /**
         * Sends ZigBee command without waiting for response.
         *
         * @param command the {@link ZigBeeCommand} to send
        */
        public void SendTransaction(ZigBeeCommand command)
        {
            command.DestinationAddress = GetEndpointAddress();
        }

        /**
         * Sends {@link ZigBeeCommand} command and uses the {@link ZigBeeTransactionMatcher} to match the response.
         *
         * @param command the {@link ZigBeeCommand} to send
         * @param responseMatcher the {@link ZigBeeTransactionMatcher} used to match the response to the request
         * @return the {@link CommandResult} future.
         */
        public async Task<CommandResult> SendTransaction(ZigBeeCommand command, IZigBeeTransactionMatcher responseMatcher)
        {
            //command.DestinationAddress = GetEndpointAddress();
            return await Node.SendTransaction(command, responseMatcher);
        }

        public override string ToString()
        {
            return "ZigBeeEndpoint [networkAddress=" + GetEndpointAddress().ToString() + ", profileId="
                    + string.Format("{0}4X", ProfileId) + ", deviceId=" + DeviceId + ", deviceVersion=" + DeviceVersion
                    + ", inputClusterIds=" + string.Join(",", GetInputClusterIds()) + ", outputClusterIds="
                    + string.Join(",", GetOutputClusterIds())+ "]";
        }
    }
}
