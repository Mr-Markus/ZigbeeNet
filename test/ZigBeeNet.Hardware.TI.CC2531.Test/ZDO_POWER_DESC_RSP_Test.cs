using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ZigBeeNet.Hardware.TI.CC2531.Frame;
using ZigBeeNet.Hardware.TI.CC2531.Packet;

namespace ZigBeeNet.Hardware.TI.CC2531.Test
{
    public class ZDO_POWER_DESC_RSP_Test : Cc2351TestPacket
    {
        [Fact]
        public void testReceive()
        {
            ZToolPacket data = GetPacket("FE 07 45 83 00 00 00 00 00 10 C1 10");

            ZigBeeApsFrame apsFrame = ZdoPowerDescriptor.Create(data);

            Assert.Equal(0x0000, apsFrame.SourceAddress);
            Assert.Equal(0, apsFrame.Profile);
            Assert.Equal(0, apsFrame.DestinationEndpoint);
            Assert.Equal(GetPacketData("00 00 00 00 10 C1"), apsFrame.Payload);
        }
    }
}
