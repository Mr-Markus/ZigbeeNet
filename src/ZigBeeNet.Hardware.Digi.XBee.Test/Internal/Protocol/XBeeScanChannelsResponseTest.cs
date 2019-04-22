using Xunit;
using Xunit.Abstractions;
using ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol;

namespace ZigBeeNet.Hardware.Digi.XBee.Test.Internal.Protocol
{
    public class XBeeScanChannelsResponseTest : XBeeFrameBaseTest
    {
        private readonly ITestOutputHelper _output;

        public XBeeScanChannelsResponseTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Test()
        {
            XBeeScanChannelsResponse responseEvent = new XBeeScanChannelsResponse();
            responseEvent.Deserialize(GetPacketData("00 05 88 13 53 43 00 CE"));
            _output.WriteLine(responseEvent.ToString());
            Assert.Equal(0x88, responseEvent.GetFrameType());
            Assert.Equal(CommandStatus.OK, responseEvent.GetCommandStatus());
            Assert.Equal(19, responseEvent.GetFrameId());
        }
    }
}
