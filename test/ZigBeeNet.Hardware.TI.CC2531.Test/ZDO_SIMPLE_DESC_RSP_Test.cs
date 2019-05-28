using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ZigBeeNet.Hardware.TI.CC2531.Frame;
using ZigBeeNet.Hardware.TI.CC2531.Packet;

namespace ZigBeeNet.Hardware.TI.CC2531.Test
{
    public class ZDO_SIMPLE_DESC_RSP_Test : Cc2351TestPacket
    {
        [Fact]
        public void TestReceive1()
        {
            ZToolPacket data = GetPacket(
                    "FE 2E 45 84 00 00 00 00 00 28 01 04 01 00 00 00 00 10 00 00 01 00 02 00 03 00 04 00 05 00 06 00 07 00 08 00 09 00 0F 00 0A 00 0C 00 15 00 00 01 01 01 CF");

            ZigBeeApsFrame apsFrame = ZdoSimpleDescriptor.Create(data);

            Assert.Equal(0x0000, apsFrame.SourceAddress);
            Assert.Equal(0, apsFrame.Profile);
            Assert.Equal(0, apsFrame.DestinationEndpoint);
            Assert.Equal(GetPacketData(
                            "00 00 00 00 28 01 04 01 00 00 00 00 10 00 00 01 00 02 00 03 00 04 00 05 00 06 00 07 00 08 00 09 00 0F 00 0A 00 0C 00 15 00 00 01 01 01"),
                    apsFrame.Payload);
        }

        [Fact]
    public void TestReceive2()
        {
            ZToolPacket data = GetPacket(
                    "FE 18 45 84 21 A4 00 21 A4 12 02 04 01 01 00 00 02 00 00 03 00 03 05 00 06 00 08 00 C4");

            ZigBeeApsFrame apsFrame = ZdoSimpleDescriptor.Create(data);

            Assert.Equal(42017, apsFrame.SourceAddress);
            Assert.Equal(0, apsFrame.Profile);
            Assert.Equal(0, apsFrame.DestinationEndpoint);
            Assert.Equal(GetPacketData("A4 00 21 A4 12 02 04 01 01 00 00 02 00 00 03 00 03 05 00 06 00 08 00"),
                    apsFrame.Payload);
        }
    }
}
