using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ZigBeeNet.Hardware.TI.CC2531.Frame;
using ZigBeeNet.Hardware.TI.CC2531.Packet;

namespace ZigBeeNet.Hardware.TI.CC2531.Test
{
    public class ZDO_ACTIVE_EP_RSP_Test : Cc2351TestPacket
    {
        [Fact]
        public void TestReceive()
        {
            string packetString = "FE 08 45 85 00 00 00 00 00 02 02 01 C9";
            ZToolPacket data = GetPacket(packetString);
            Assert.Equal(packetString, data.PacketString);

            ZigBeeApsFrame apsFrame = ZdoActiveEndpoint.Create(data);

            Assert.Equal(0x0000, apsFrame.SourceAddress);
            Assert.Equal(0, apsFrame.Profile);
            Assert.Equal(0, apsFrame.DestinationEndpoint);
            Assert.Equal(GetPacketData("00 00 00 00 02 02 01"), apsFrame.Payload);
        }
    }
}
