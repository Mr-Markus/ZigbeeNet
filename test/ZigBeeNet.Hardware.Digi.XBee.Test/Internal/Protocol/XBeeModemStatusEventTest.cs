using Xunit;
using Xunit.Abstractions;
using ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol;

namespace ZigBeeNet.Hardware.Digi.XBee.Test.Internal.Protocol
{
    public class XBeeModemStatusEventTest : XBeeFrameBaseTest
    {
        private readonly ITestOutputHelper _output;

        public XBeeModemStatusEventTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Test()
        {
            XBeeModemStatusEvent responseEvent = new XBeeModemStatusEvent();
            responseEvent.Deserialize(GetPacketData("00 02 8A 06 6F"));
            _output.WriteLine(responseEvent.ToString());
            Assert.Equal(0x8A, responseEvent.GetFrameType());
            Assert.Equal(ModemStatus.COORDINATOR_STARTED, responseEvent.GetStatus());
        }
    }
}
