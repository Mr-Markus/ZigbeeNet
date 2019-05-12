using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ZigBeeNet.Hardware.TI.CC2531.Frame;
using ZigBeeNet.Hardware.TI.CC2531.Packet;

namespace ZigBeeNet.Hardware.TI.CC2531.Test
{
    public class ZDO_MGMT_LQI_RSP_Test : Cc2351TestPacket
    {
        [Fact]
        public void TestReceive()
        {
            ZToolPacket data = GetPacket(
                    "FE 32 45 B1 00 00 00 02 00 02 14 D4 F1 02 00 4B 12 00 0B 88 DC 00 01 88 17 00 8F 22 15 02 01 3B 14 D4 F1 02 00 4B 12 00 EC A1 A5 01 00 8D 15 00 35 38 15 02 01 58 B5");

            ZigBeeApsFrame apsFrame = ZdoManagementLqi.Create(data);

            Assert.Equal(0x0000, apsFrame.SourceAddress);
            Assert.Equal(0, apsFrame.Profile);
            Assert.Equal(0, apsFrame.DestinationEndpoint);
            Assert.Equal(GetPacketData(
                            "00 00 02 00 02 14 D4 F1 02 00 4B 12 00 0B 88 DC 00 01 88 17 00 8F 22 15 02 01 3B 14 D4 F1 02 00 4B 12 00 EC A1 A5 01 00 8D 15 00 35 38 15 02 01 58"),
                    apsFrame.Payload);
        }
    }
}
