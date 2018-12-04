using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.Logging;
using ZigbeeNet.ZCL;

namespace ZigbeeNet.App
{
    public class ClusterMatcher : IZigBeeCommandListener
    {
        private readonly ILog _logger = LogProvider.For<ClusterMatcher>();

        private ZigbeeNetworkManager _networkManager;

        private List<ushort> _clusters = new List<ushort>();

        public ClusterMatcher(ZigbeeNetworkManager networkManager)
        {
            _logger.Debug("ClusterMatcher starting");
            _networkManager = networkManager;

            _networkManager.AddCommandListener(this);
        }

        public void AddCluster(ushort cluster)
        {
            _logger.Debug($"ClusterMatcher adding cluster {(ZclClusterId)cluster}");
            _clusters.Add(cluster);
        }

        public void CommandReceived(ZigBeeCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
