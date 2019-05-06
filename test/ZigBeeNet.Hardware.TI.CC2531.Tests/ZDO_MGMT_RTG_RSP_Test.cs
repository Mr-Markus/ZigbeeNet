using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ZigBeeNet.Hardware.TI.CC2531.Frame;
using ZigBeeNet.Hardware.TI.CC2531.Packet;

namespace ZigBeeNet.Hardware.TI.CC2531.Test
{
    public class ZDO_MGMT_RTG_RSP_Test : Cc2351TestPacket
    {
        [Fact]
        public void TestReceive()
        {
            ZToolPacket data = GetPacket("FE 0B 45 B2 00 00 00 01 00 01 2A 2F 00 35 38 F4");

            ZigBeeApsFrame apsFrame = ZdoManagementRouting.Create(data);

            Assert.Equal(0x0000, apsFrame.SourceAddress);
            Assert.Equal(0, apsFrame.Profile);
            Assert.Equal(0, apsFrame.DestinationEndpoint);
            Assert.Equal(GetPacketData("00 00 01 00 01 2A 2F 00 35 38"), apsFrame.Payload);
        }
    }
}
