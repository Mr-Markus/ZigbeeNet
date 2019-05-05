using Xunit;
using Xunit.Abstractions;
using ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol;

namespace ZigBeeNet.Hardware.Digi.XBee.Test.Internal.Protocol
{
    public class XBeeHardwareVersionResponseTest : XBeeFrameBaseTest
    {
        private readonly ITestOutputHelper _output;

        public XBeeHardwareVersionResponseTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void test()
        {
            XBeeHardwareVersionResponse responseEvent = new XBeeHardwareVersionResponse();
            responseEvent.Deserialize(GetPacketData("00 07 88 01 48 56 00 19 48 77"));
            _output.WriteLine(responseEvent.ToString());
            Assert.Equal(0x88, responseEvent.GetFrameType());
        }
    }
}
