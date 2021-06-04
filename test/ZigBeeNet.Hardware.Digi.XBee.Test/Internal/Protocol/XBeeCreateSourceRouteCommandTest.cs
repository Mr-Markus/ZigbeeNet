using System.Linq;
using System.Numerics;
using Xunit;
using Xunit.Abstractions;
using ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol;

namespace ZigBeeNet.Hardware.Digi.XBee.Test.Internal.Protocol
{
    public class XBeeCreateSourceRouteCommandTest
    {
        private readonly ITestOutputHelper _output;

        public XBeeCreateSourceRouteCommandTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Test()
        {
            XBeeCreateSourceRouteCommand command = new XBeeCreateSourceRouteCommand();

            command.SetFrameId(0);
            command.SetIeeeAddress(new IeeeAddress(0x0013A20040401122ul));
            
            command.SetNetworkAddress(0x3344);
            command.SetAddressList(new int[] { 0xEEFF, 0xCCDD, 0xAABB });
            _output.WriteLine(command.ToString());

            int[] arrayToTestAgainst = new int[] { 0x00, 0x14, 0x21, 0x00, 0x00, 0x13, 0xA2, 0x00, 0x40, 0x40, 0x11, 0x22,
                0x33, 0x44, 0x00, 0x03, 0xEE, 0xFF, 0xCC, 0xDD, 0xAA, 0xBB, 0x01 };
            int[] commandSerializeResult = command.Serialize();

            Assert.True(arrayToTestAgainst.SequenceEqual(commandSerializeResult));
        }
    }
}
