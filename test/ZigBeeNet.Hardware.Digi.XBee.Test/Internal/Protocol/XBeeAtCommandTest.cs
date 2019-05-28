using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol;

namespace ZigBeeNet.Hardware.Digi.XBee.Test.Internal.Protocol
{
    public class XBeeAtCommandTest
    {
        private readonly ITestOutputHelper _output;

        public XBeeAtCommandTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void TestAtCommandAB()
        {
            XBeeAtCommand command = new XBeeAtCommand();

            command.SetFrameId(0);
            command.SetAtCommand("AB");
            command.SetParameterValue(new int[] { 0, 1, 2, 3, 4, 5 });
            _output.WriteLine(command.ToString());
            int[] arrayToTestAgainst = new int[] { 0, 10, 8, 0, 65, 66, 0, 1, 2, 3, 4, 5, 101 };
            int[] commandSerializeResult = command.Serialize();

            Assert.True(arrayToTestAgainst.SequenceEqual(commandSerializeResult));
        }

        [Fact]
        public void TestAtCommandNJ()
        {
            XBeeAtCommand command = new XBeeAtCommand();

            command.SetFrameId(0x52);
            command.SetAtCommand("NJ");
            _output.WriteLine(command.ToString());
            int[] arrayToTestAgainst = new int[] { 0x00, 0x04, 0x08, 0x52, 0x4E, 0x4A, 0x0D };
            int[] commandSerializeResult = command.Serialize();

            Assert.True(arrayToTestAgainst.SequenceEqual(commandSerializeResult));
        }
    }
}
