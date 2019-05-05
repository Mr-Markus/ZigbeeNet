using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ZigBeeNet.Hardware.TI.CC2531.Frame;
using ZigBeeNet.Hardware.TI.CC2531.Packet;

namespace ZigBeeNet.Hardware.TI.CC2531.Tests
{
    public class ZDO_END_DEVICE_ANNCE_IND_Test : Cc2351TestPacket
    {
        [Fact]
        public void TestReceive()
        {
            ZToolPacket data = GetPacket("FE 0D 45 C1 2A 2F 2A 2F F9 41 F6 02 00 4B 12 00 00 9C");

            ZigBeeApsFrame apsFrame = ZdoEndDeviceAnnounce.Create(data);

            Assert.Equal(0x2f2a, apsFrame.SourceAddress);
            Assert.Equal(0, apsFrame.Profile);
            Assert.Equal(0, apsFrame.DestinationEndpoint);
            Assert.Equal(GetPacketData("2F 2A 2F F9 41 F6 02 00 4B 12 00 00"), apsFrame.Payload);
        }
    }
}
