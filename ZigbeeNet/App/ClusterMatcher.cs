﻿using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Logging;
using ZigBeeNet.ZCL;

namespace ZigBeeNet.App
{
    public class ClusterMatcher : IZigBeeCommandListener
    {
        private readonly ILog _logger = LogProvider.For<ClusterMatcher>();

        private ZigBeeNetworkManager _networkManager;

        private List<ushort> _clusters = new List<ushort>();

        public ClusterMatcher(ZigBeeNetworkManager networkManager)
        {
            _logger.Debug("ClusterMatcher starting");
            _networkManager = networkManager;

            _networkManager.AddCommandListener(this);
        }

        public void AddCluster(ushort cluster)
        {
            _logger.Debug($"ClusterMatcher adding cluster {cluster}");
            _clusters.Add(cluster);
        }

        public void CommandReceived(ZigBeeCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
