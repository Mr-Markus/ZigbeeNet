using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ZigBeeNet.Hardware.TI.CC2531.Frame;
using ZigBeeNet.Hardware.TI.CC2531.Packet;

namespace ZigBeeNet.Hardware.TI.CC2531.Test
{
    public class ZDO_NODE_DESC_RSP_Test : Cc2351TestPacket
    {
        [Fact]
        public void TestReceive()
        {
            ZToolPacket data = GetPacket("FE 12 45 82 00 00 00 00 00 00 40 0F 00 00 50 A0 00 01 00 A0 00 00 CB");

            ZigBeeApsFrame apsFrame = ZdoNodeDescriptor.Create(data);

            Assert.Equal(0x0000, apsFrame.SourceAddress);
            Assert.Equal(0, apsFrame.Profile);
            Assert.Equal(0, apsFrame.DestinationEndpoint);
            Assert.Equal(GetPacketData("00 00 00 00 00 40 0F 00 00 50 A0 00 01 00 A0 00 00"), apsFrame.Payload);
        }

    }
}
