using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.DAO
{
    public class ZigBeeEndpointDao
    {
        public ushort ProfileId { get; set; }

        public byte EndpointId { get; set; }

        /// <summary>
         /// Input cluster IDs
         /// </summary>
        public List<ushort> InputClusterIds { get; } = new List<ushort>();

        /// <summary>
         /// Output cluster IDs
         /// </summary>
        public List<ushort> OutputClusterIds { get; } = new List<ushort>();

        public List<ZclClusterDao> InputClusters { get; } = new List<ZclClusterDao>();

        public List<ZclClusterDao> OutputClusters { get; } = new List<ZclClusterDao>();


        public void SetInputClusterIds(IEnumerable<ushort> ids)
        {
            this.InputClusterIds.AddRange(ids);
        }

        public void SetOutputClusterIds(IEnumerable<ushort> ids)
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

        /// <summary>
         /// public static ZigBeeEndpointDao createFromZigBeeDevice(ZigBeeEndpoint endpoint) {
         /// ZigBeeEndpointDao endpointDao = new ZigBeeEndpointDao();
         /// endpointDao.setEndpointId(endpoint.getEndpointId());
         /// endpointDao.setProfileId(endpoint.getProfileId());
         /// endpointDao.setInputClusterIds(endpoint.getInputClusterIds());
         /// endpointDao.setOutputClusterIds(endpoint.getOutputClusterIds());
         /// // endpointDao.setInputClusters(endpoint.getInputClusters());
         /// // endpointDao.setOutputClusters();
         /// return endpointDao;
         /// }
         /// 
         /// public static ZigBeeEndpoint createFromZigBeeDao(ZigBeeNetworkManager networkManager, ZigBeeNode node,
         /// ZigBeeEndpointDao endpointDao) {
         /// ZigBeeEndpoint endpoint = new ZigBeeEndpoint(networkManager, node, endpointDao.endpointId);
         /// endpoint.setProfileId(endpointDao.getProfileId());
         /// endpoint.setInputClusterIds(endpointDao.getInputClusterIds());
         /// endpoint.setOutputClusterIds(endpointDao.getOutputClusterIds());
         /// return endpoint;
         /// }
         /// </summary>
    }
}
