using Xunit;
using Xunit.Abstractions;
using ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol;

namespace ZigBeeNet.Hardware.Digi.XBee.Test.Internal.Protocol
{
    public class XBeeNetworkKeyResponseTest : XBeeFrameBaseTest
    {
        private readonly ITestOutputHelper _output;

        public XBeeNetworkKeyResponseTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Test()
        {
            XBeeNetworkKeyResponse responseEvent = new XBeeNetworkKeyResponse();
            responseEvent.Deserialize(GetPacketData("00 05 88 05 4E 4B 00 D9"));
            _output.WriteLine(responseEvent.ToString());
            Assert.Equal(0x88, responseEvent.GetFrameType());
            Assert.Equal(5, responseEvent.GetFrameId());
        }
    }
}
