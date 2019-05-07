using System;
using Xunit;
using Xunit.Abstractions;
using ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol;

namespace ZigBeeNet.Hardware.Digi.XBee.Test
{
    public class XBeeResponseFactoryTest
    {
        private readonly ITestOutputHelper _output;

        public XBeeResponseFactoryTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void TestGetResponse1()
        {
            int[] data = GetPacketData("00 07 8B 8F F7 7B 00 00 40 33");
            IXBeeResponse frame = XBeeResponseFactory.GetXBeeFrame(data);
            Assert.True(frame is XBeeTransmitStatusResponse);
            _output.WriteLine(frame.ToString());

            XBeeTransmitStatusResponse responseEvent = (XBeeTransmitStatusResponse)frame;
            Assert.Equal(143, responseEvent.GetFrameId());
            Assert.Equal(0, responseEvent.GetTransmitRetryCount());
            Assert.Equal(DeliveryStatus.SUCCESS, responseEvent.GetDeliveryStatus());
            Assert.Equal(DiscoveryStatus.EXTENDED_TIMEOUT_DISCOVERY, responseEvent.GetDiscoveryStatus());
        }

        [Fact]
        public void TestGetResponse2()
        {
            int[] data = GetPacketData("00 05 88 10 4F 49 02 CD");
            IXBeeResponse frame = XBeeResponseFactory.GetXBeeFrame(data);
            Assert.True(frame is XBeePanIdResponse);
            _output.WriteLine(frame.ToString());

            XBeePanIdResponse responseEvent = (XBeePanIdResponse)frame;
            Assert.Equal(CommandStatus.INVALID_COMMAND, responseEvent.GetCommandStatus());
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
