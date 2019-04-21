using System;
using System.Numerics;
using Xunit;
using Xunit.Abstractions;
using ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol;

namespace ZigBeeNet.Hardware.Digi.XBee.Test
{
    public class XBeeEventFactoryTest
    {
        private readonly ITestOutputHelper _output;

        public XBeeEventFactoryTest(ITestOutputHelper output)
        {
            _output = output;
        }


        [Fact]
        public void TestBootloaderEvent()
        {
            int[] data = GetPacketData("00 16 A0 00 13 A2 00 41 62 F6 1A 00 00 01 40 00 00 00 00 00 00 00 FF FF B8");
            IXBeeEvent frame = XBeeEventFactory.GetXBeeFrame(data);
            Assert.True(frame is XBeeOtaFirmwareUpdateStatusEvent);

            _output.WriteLine(frame.ToString());

            XBeeOtaFirmwareUpdateStatusEvent xbeeEvent = (XBeeOtaFirmwareUpdateStatusEvent)frame;
            Assert.Equal(0, xbeeEvent.GetBlockNumber());
            Assert.Equal(new IeeeAddress(BigInteger.Parse("0013A2004162F61A", System.Globalization.NumberStyles.HexNumber)), xbeeEvent.GetIeeeAddress());
            Assert.Equal(0, xbeeEvent.GetNetworkAddress());
        }

        [Fact]
        public void TestGetEvent()
        {
            int[] data = GetPacketData("00 1A 91 00 17 88 01 02 13 65 36 F7 7B 02 01 00 01 01 04 41 18 7C 01 21 00 00 20 C8 C4");
            IXBeeEvent frame = XBeeEventFactory.GetXBeeFrame(data);
            Assert.True(frame is XBeeReceivePacketExplicitEvent);
            _output.WriteLine(frame.ToString());

            XBeeReceivePacketExplicitEvent xbeeEvent = (XBeeReceivePacketExplicitEvent)frame;
            Assert.Equal(1, xbeeEvent.GetClusterId());
            Assert.Equal(1, xbeeEvent.GetDestinationEndpoint());
            Assert.Equal(63355, xbeeEvent.GetNetworkAddress());
            Assert.Equal(0x104, xbeeEvent.GetProfileId());
        }

        private static int[] GetPacketData(string stringData)
        {
            string[] split = stringData.Split(" ");

            int[] response = new int[split.Length];
            int cnt = 0;
            foreach (string val in split)
            {
                response[cnt++] = Convert.ToInt32(val, 16);
            }

            return response;
        }
    }
}
