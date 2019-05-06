using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ZigBeeNet.Hardware.TI.CC2531.Frame;
using ZigBeeNet.Hardware.TI.CC2531.Packet;

namespace ZigBeeNet.Hardware.TI.CC2531.Test
{
    public class ZDO_MGMT_LEAVE_RSP_Test : Cc2351TestPacket
    {
        [Fact]
        public void TestReceive()
        {
            ZToolPacket data = GetPacket("FE 03 45 B4 E6 D2 00 C6");

            ZigBeeApsFrame apsFrame = ZdoManagementLeave.Create(data);

            Assert.Equal(53990, apsFrame.SourceAddress);
            Assert.Equal(0, apsFrame.Profile);
            Assert.Equal(0, apsFrame.DestinationEndpoint);
            Assert.Equal(GetPacketData("D2 00"), apsFrame.Payload);
        }
    }
}
