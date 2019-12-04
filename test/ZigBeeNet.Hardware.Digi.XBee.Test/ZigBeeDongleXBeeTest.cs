using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;
using ZigBeeNet.Hardware.Digi.XBee.Internal;
using ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol;
using ZigBeeNet.ZDO.Command;

namespace ZigBeeNet.Hardware.Digi.XBee.Test
{
    public class ZigBeeDongleXBeeTest
    {
        private readonly ITestOutputHelper _output;

        public ZigBeeDongleXBeeTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void GetVersionString()
        {
            ZigBeeDongleXBee dongle = new ZigBeeDongleXBee(null);

            Assert.Equal("Unknown", dongle.VersionString);
        }

        [Fact]
        public void SendCommand()
        {
            IList<IXBeeCommand> sentCommands = new List<IXBeeCommand>();
            Mock<IXBeeFrameHandler> frameHandlerMock = new Mock<IXBeeFrameHandler>();
            frameHandlerMock.Setup(frameHandler => frameHandler.SendRequestAsync(It.IsAny<IXBeeCommand>())).Callback<IXBeeCommand>(item => sentCommands.Add(item)).Returns(() => null);

            ZigBeeDongleXBee dongle = new ZigBeeDongleXBee(null);

            try
            {
                Type fieldType = dongle.GetType();
                FieldInfo fieldInfo = fieldType.GetField("_frameHandler", BindingFlags.NonPublic | BindingFlags.Instance);
                fieldInfo.SetValue(dongle, frameHandlerMock.Object);
            }
            catch (Exception ex)
            {
                _output.WriteLine(ex.StackTrace);
            }

            MatchDescriptorResponse command = new MatchDescriptorResponse
            {
                DestinationAddress = new ZigBeeEndpointAddress(46946),
                NwkAddrOfInterest = 46946,
                Status = ZDO.ZdoStatus.SUCCESS,
                TransactionId = 0x2A
            };
            List<byte> matchList = new List<byte>
            {
                1
            };
            command.MatchList = matchList;

            ZigBeeApsFrame apsFrame = new ZigBeeApsFrame
            {
                DestinationAddress = 46946,
                DestinationEndpoint = 0,
                DestinationIeeeAddress = new IeeeAddress(BigInteger.Parse("000D6F00057CF7C6", NumberStyles.HexNumber)),
                Cluster = 32774,
                AddressMode = ZigBeeNwkAddressMode.Device,
                Radius = 31,
                ApsCounter = 42,
                Payload = new byte[] { 0x00, 0x00, 0x2E, 0x5B, 0x01, 0x01 }
            };

            _output.WriteLine(command.ToString());
            _output.WriteLine(apsFrame.ToString());

            dongle.SendCommand(apsFrame);
            Assert.Equal(1, sentCommands.Count);
            XBeeTransmitRequestExplicitCommand sentCommand = (XBeeTransmitRequestExplicitCommand)sentCommands[0];
            sentCommand.SetFrameId(32);
            _output.WriteLine(sentCommand.ToString());

            int[] payload = new int[] { 0, 26, 17, 32, 0, 13, 111, 0, 5, 124, 247, 198, 183, 98, 0, 0, 128, 6, 0, 0, 0, 0,
                0, 0, 46, 91, 1, 1, 234 };

            int[] output = sentCommand.Serialize();
            Assert.True(payload.SequenceEqual(output));
        }
    }
}
