using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ZigBeeNet.Hardware.TI.CC2531.Frame;
using ZigBeeNet.Hardware.TI.CC2531.Packet;

namespace ZigBeeNet.Hardware.TI.CC2531.Test
{
    public class ZDO_MSG_CB_INCOMING_Test : Cc2351TestPacket
    {
        [Fact]
        public void TestReceive()
        {
            ZToolPacket data = GetPacket("FE 15 45 FF 00 00 00 01 80 00 00 00 1B 00 5B 23 EB 09 00 4B 12 00 00 00 00 F6");

            ZigBeeApsFrame apsFrame = ZdoCallbackIncoming.Create(data);

            Assert.Equal(0x8001, apsFrame.Cluster);
            Assert.Equal(0x0000, apsFrame.SourceAddress);
            Assert.Equal(0, apsFrame.Profile);
            Assert.Equal(0, apsFrame.DestinationEndpoint);
            Assert.Equal(GetPacketData("1B 00 5B 23 EB 09 00 4B 12 00 00 00 00"), apsFrame.Payload);
        }
    }
}
