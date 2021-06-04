using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ZigBeeNet.Test
{
    public class ZigBeeBroadcastDestinationTest
    {
        [Fact]
    public void GetDestination()
        {
            Assert.Equal(0xFFFC, (ushort)ZigBeeBroadcastDestination.BROADCAST_ROUTERS_AND_COORD);

            Assert.Equal(0xFFFB,(ushort)ZigBeeBroadcastDestination.BROADCAST_LOW_POWER_ROUTERS);
            Assert.Equal(0xFFFF, (ushort)ZigBeeBroadcastDestination.BROADCAST_ALL_DEVICES);
        }
    }
}
