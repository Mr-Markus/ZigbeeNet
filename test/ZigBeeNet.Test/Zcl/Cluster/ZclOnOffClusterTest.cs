using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Clusters;
using ZigBeeNet.ZCL.Clusters.General;
using ZigBeeNet.ZCL.Clusters.OnOff;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.Test.Zcl.Cluster
{
    public class ZclOnOffClusterTest
    {
        public ZclOnOffCluster _cluster;

        public ZclOnOffClusterTest()
        {
            var node = new ZigBeeNode();
            var endpoint = new ZigBeeEndpoint(node, 0);

            _cluster = new ZclOnOffCluster(endpoint);
        }

        [Fact]
        public void GetClusterId()
        {
            Assert.Equal(6, _cluster.GetClusterId());
        }

        [Fact]
        public void GetOffCommandFromId()
        {
            Assert.IsType<OffCommand>(_cluster.GetCommandFromId(new OffCommand().CommandId));
        }

        [Fact]
        public void GetOnCommandFromId()
        {
            Assert.IsType<OnCommand>(_cluster.GetCommandFromId(new OnCommand().CommandId));
        }

        [Fact]
        public void GetToggleCommandFromId()
        {
            Assert.IsType<ToggleCommand>(_cluster.GetCommandFromId(new ToggleCommand().CommandId));
        }
    }
}
