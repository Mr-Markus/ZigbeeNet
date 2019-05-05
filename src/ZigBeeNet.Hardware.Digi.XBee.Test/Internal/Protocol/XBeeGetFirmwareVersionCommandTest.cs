using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Abstractions;
using ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol;

namespace ZigBeeNet.Hardware.Digi.XBee.Test.Internal.Protocol
{
    public class XBeeGetFirmwareVersionCommandTest
    {
        private readonly ITestOutputHelper _output;

        public XBeeGetFirmwareVersionCommandTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Test()
        {
            XBeeGetFirmwareVersionCommand command = new XBeeGetFirmwareVersionCommand();

            command.SetFrameId(0);
            _output.WriteLine(command.ToString());

            int[] arrayToTestAgainst = new int[] { 0, 4, 8, 0, 86, 82, 79 };
            int[] commandSerializationResult = command.Serialize();

            Assert.True(arrayToTestAgainst.SequenceEqual(commandSerializationResult));
        }
    }
}
