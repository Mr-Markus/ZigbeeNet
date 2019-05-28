using System;
using System.Linq;
using System.Numerics;
using Xunit;
using Xunit.Abstractions;
using ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol;

namespace ZigBeeNet.Hardware.Digi.XBee.Test.Internal.Protocol
{
    public class XBeeReceivePacketEventTest : XBeeFrameBaseTest
    {
        private readonly ITestOutputHelper _output;

        public XBeeReceivePacketEventTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Test()
        {
            XBeeReceivePacketEvent responseEvent = new XBeeReceivePacketEvent();
            responseEvent.Deserialize(GetPacketData("00 12 90 00 13 A2 00 40 52 2B AA 7D 84 01 52 78 44 61 74 61 0D"));
            _output.WriteLine(responseEvent.ToString());
            Assert.Equal(0x90, responseEvent.GetFrameType());
            Assert.Equal(0x7D84, responseEvent.GetNetworkAddress());
            Assert.Equal(ReceiveOptions.PACKET_ACKNOWLEDGED, responseEvent.GetReceiveOptions());
            Assert.Equal(new IeeeAddress(BigInteger.Parse("0013A20040522BAA", System.Globalization.NumberStyles.HexNumber)), responseEvent.GetIeeeAddress());

            int[] arrayToTestAgainst = GetPacketData("52 78 44 61 74 61");
            int[] getDataResult = responseEvent.GetData();

            Assert.True(arrayToTestAgainst.SequenceEqual(getDataResult));
        }
    }
}
