using Xunit;
using Xunit.Abstractions;
using ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol;

namespace ZigBeeNet.Hardware.Digi.XBee.Test.Internal.Protocol
{
    public class XBeeAtResponseTest : XBeeFrameBaseTest
    {
        private readonly ITestOutputHelper _output;

        public XBeeAtResponseTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void TestNoData()
        {
            XBeeAtResponse responseEvent = new XBeeAtResponse();
            responseEvent.Deserialize(GetPacketData("00 05 88 01 42 44 00 F0"));
            _output.WriteLine(responseEvent.ToString());
            Assert.Equal(0x01, responseEvent.GetFrameId());
            Assert.Equal(0x88, responseEvent.GetFrameType());
            Assert.Equal(CommandStatus.OK, responseEvent.GetCommandStatus());
            Assert.Null(responseEvent.GetCommandData());
        }

        [Fact]
        public void TestData()
        {
            XBeeAtResponse responseEvent = new XBeeAtResponse();
            responseEvent.Deserialize(GetPacketData("00 05 88 01 42 44 00 01 02 03 F0"));
            _output.WriteLine(responseEvent.ToString());
            Assert.Equal(0x01, responseEvent.GetFrameId());
            Assert.Equal(0x88, responseEvent.GetFrameType());
            Assert.Equal(CommandStatus.OK, responseEvent.GetCommandStatus());
            Assert.NotNull(responseEvent.GetCommandData());
            Assert.Equal(3, responseEvent.GetCommandData().Length);
        }
    }
}
