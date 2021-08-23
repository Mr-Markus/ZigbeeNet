using System.Numerics;
using Xunit;
using Xunit.Abstractions;
using ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol;

namespace ZigBeeNet.Hardware.Digi.XBee.Test.Internal.Protocol
{
    public class XBeeRouteRecordEventTest : XBeeFrameBaseTest
    {
        private readonly ITestOutputHelper _output;

        public XBeeRouteRecordEventTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Test()
        {
            XBeeRouteRecordEvent responseEvent = new XBeeRouteRecordEvent();
            responseEvent.Deserialize(GetPacketData("00 13 A1 00 13 A2 00 40 40 11 22 33 44 01 03 EE FF CC DD AA BB 80"));
            _output.WriteLine(responseEvent.ToString());
            Assert.Equal(0xA1, responseEvent.GetFrameType());
            Assert.Equal(0x3344, responseEvent.GetNetworkAddress());
            Assert.Equal(ReceiveOptions.PACKET_ACKNOWLEDGED, responseEvent.GetReceiveOptions());
            Assert.Equal(new IeeeAddress(0x0013A20040401122ul), responseEvent.GetIeeeAddress());
            Assert.Equal(3, responseEvent.GetAddressList().Length);
        }
    }
}
