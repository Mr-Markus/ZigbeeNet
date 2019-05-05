using System.Linq;
using Xunit;
using Xunit.Abstractions;
using ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol;

namespace ZigBeeNet.Hardware.Digi.XBee.Test.Internal.Protocol
{
    public class XBeeFirmwareVersionResponseTest : XBeeFrameBaseTest
    {
        private readonly ITestOutputHelper _output;

        public XBeeFirmwareVersionResponseTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Test()
        {
            XBeeFirmwareVersionResponse responseEvent = new XBeeFirmwareVersionResponse();
            responseEvent.Deserialize(GetPacketData("00 07 88 02 56 52 00 21 A7 05"));
            _output.WriteLine(responseEvent.ToString());
            Assert.Equal(0x88, responseEvent.GetFrameType());
            Assert.Equal(CommandStatus.OK, responseEvent.GetCommandStatus());

            int[] arrayToTestAgainst = new int[] { 0x21, 0xA7 };
            int[] firmwareVersionResult = responseEvent.GetFirmwareVersion();

            Assert.True(arrayToTestAgainst.SequenceEqual(firmwareVersionResult));
        }
    }
}
