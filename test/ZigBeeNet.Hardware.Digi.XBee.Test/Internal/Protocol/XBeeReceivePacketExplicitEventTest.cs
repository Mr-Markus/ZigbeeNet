using System.Linq;
using System.Numerics;
using Xunit;
using Xunit.Abstractions;
using ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol;

namespace ZigBeeNet.Hardware.Digi.XBee.Test.Internal.Protocol
{
    public class XBeeReceivePacketExplicitEventTest : XBeeFrameBaseTest
    {
        private readonly ITestOutputHelper _output;

        public XBeeReceivePacketExplicitEventTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Test()
        {
            XBeeReceivePacketExplicitEvent responseEvent = new XBeeReceivePacketExplicitEvent();
            responseEvent.Deserialize(
                GetPacketData("00 18 91 00 13 A2 00 40 52 2B AA 7D 84 E0 E0 22 11 C1 05 02 52 78 44 61 74 61 52"));
            _output.WriteLine(responseEvent.ToString());
            Assert.Equal(0x91, responseEvent.GetFrameType());
            Assert.Equal(0x7D84, responseEvent.GetNetworkAddress());
            Assert.Equal(224, responseEvent.GetSourceEndpoint());
            Assert.Equal(224, responseEvent.GetDestinationEndpoint());
            Assert.Equal(0x2211, responseEvent.GetClusterId());
            Assert.Equal(0xC105, responseEvent.GetProfileId());
            Assert.Equal(ReceiveOptions.PACKET_BROADCAST, responseEvent.GetReceiveOptions());
            Assert.Equal(new IeeeAddress(0x0013A20040522BAAul), responseEvent.GetIeeeAddress());
            
            int[] arrayToTestAgainst = GetPacketData("52 78 44 61 74 61");
            int[] getDataResult = responseEvent.GetData();

            Assert.True(arrayToTestAgainst.SequenceEqual(getDataResult));
        }
    }
}
