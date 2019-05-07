using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol;

namespace ZigBeeNet.Hardware.Digi.XBee.Test.Internal.Protocol
{
    public class XBeeGetHardwareVersionCommandTest
    {
        private readonly ITestOutputHelper _output;

        public XBeeGetHardwareVersionCommandTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Test()
        {
            XBeeGetHardwareVersionCommand command = new XBeeGetHardwareVersionCommand();

            command.SetFrameId(0);
            _output.WriteLine(command.ToString());

            int[] arrayToTestAgainst = new int[] { 0, 4, 8, 0, 72, 86, 89 };
            int[] commandSerializationResult = command.Serialize();

            Assert.True(arrayToTestAgainst.SequenceEqual(commandSerializationResult));
        }
    }
}
