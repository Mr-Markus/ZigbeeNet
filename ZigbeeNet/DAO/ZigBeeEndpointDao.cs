using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.DAO
{
    public class ZigBeeEndpointDao
    {
        public int ProfileId { get; set; }

        public int EndpointId { get; set; }

        /**
         * Input cluster IDs
         */
        public List<int> InputClusterIds { get; } = new List<int>();

        /**
         * Output cluster IDs
         */
        public List<int> OutputClusterIds { get; } = new List<int>();

        public List<ZclClusterDao> InputClusters { get; } = new List<ZclClusterDao>();

        public List<ZclClusterDao> OutputClusters { get; } = new List<ZclClusterDao>();


        public void SetInputClusterIds(IEnumerable<int> ids)
        {
            this.InputClusterIds.AddRange(ids);
        }

        public void SetOutputClusterIds(IEnumerable<int> ids)
        {
            this.OutputClusterIds.AddRange(ids);
        }

        public void SetInputClusters(List<ZclClusterDao> clusters)
        {
            InputClusters.AddRange(clusters);
        }

        public void SetOutputClusters(List<ZclClusterDao> clusters)
        {
            OutputClusters.AddRange(clusters);
        }

        /**
         * public static ZigBeeEndpointDao createFromZigBeeDevice(ZigBeeEndpoint endpoint) {
         * ZigBeeEndpointDao endpointDao = new ZigBeeEndpointDao();
         * endpointDao.setEndpointId(endpoint.getEndpointId());
         * endpointDao.setProfileId(endpoint.getProfileId());
         * endpointDao.setInputClusterIds(endpoint.getInputClusterIds());
         * endpointDao.setOutputClusterIds(endpoint.getOutputClusterIds());
         * // endpointDao.setInputClusters(endpoint.getInputClusters());
         * // endpointDao.setOutputClusters();
         * return endpointDao;
         * }
         * 
         * public static ZigBeeEndpoint createFromZigBeeDao(ZigBeeNetworkManager networkManager, ZigBeeNode node,
         * ZigBeeEndpointDao endpointDao) {
         * ZigBeeEndpoint endpoint = new ZigBeeEndpoint(networkManager, node, endpointDao.endpointId);
         * endpoint.setProfileId(endpointDao.getProfileId());
         * endpoint.setInputClusterIds(endpointDao.getInputClusterIds());
         * endpoint.setOutputClusterIds(endpointDao.getOutputClusterIds());
         * return endpoint;
         * }
         */
    }
}
