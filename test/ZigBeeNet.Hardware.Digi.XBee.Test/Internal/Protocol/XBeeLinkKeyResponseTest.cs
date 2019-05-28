using Xunit;
using Xunit.Abstractions;
using ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol;

namespace ZigBeeNet.Hardware.Digi.XBee.Test.Internal.Protocol
{
    public class XBeeLinkKeyResponseTest : XBeeFrameBaseTest
    {
        private readonly ITestOutputHelper _output;

        public XBeeLinkKeyResponseTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Test()
        {
            XBeeNetworkKeyResponse responseEvent = new XBeeNetworkKeyResponse();
            responseEvent.Deserialize(GetPacketData("00 05 88 08 4B 59 00 CB"));
            _output.WriteLine(responseEvent.ToString());
            Assert.Equal(0x88, responseEvent.GetFrameType());
            Assert.Equal(8, responseEvent.GetFrameId());
        }
    }
}
