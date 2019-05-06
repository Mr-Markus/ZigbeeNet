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
            ZigBeeBroadcastDestination destination = ZigBeeBroadcastDestination.GetBroadcastDestination(0xFFFC);
            Assert.Equal(BroadcastDestination.BROADCAST_ROUTERS_AND_COORD, destination.Destination);

            destination = ZigBeeBroadcastDestination.GetBroadcastDestination(0xFFFB);
            Assert.Equal(BroadcastDestination.BROADCAST_LOW_POWER_ROUTERS, destination.Destination);

            Assert.Equal(0xFFFF, (ushort)BroadcastDestination.BROADCAST_ALL_DEVICES);
        }
    }
}
