using Xunit;
using Xunit.Abstractions;
using ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol;

namespace ZigBeeNet.Hardware.Digi.XBee.Test.Internal.Protocol
{
    public class XBeeEncryptionOptionsResponseTest : XBeeFrameBaseTest
    {
        private readonly ITestOutputHelper _output;

        public XBeeEncryptionOptionsResponseTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Test()
        {
            XBeeEncryptionOptionsResponse responseEvent = new XBeeEncryptionOptionsResponse();
            responseEvent.Deserialize(GetPacketData("00 05 88 0A 45 4F 00 D9"));
            _output.WriteLine(responseEvent.ToString());
            Assert.Equal(0x88, responseEvent.GetFrameType());
            Assert.Equal(CommandStatus.OK, responseEvent.GetCommandStatus());
        }
    }
}
