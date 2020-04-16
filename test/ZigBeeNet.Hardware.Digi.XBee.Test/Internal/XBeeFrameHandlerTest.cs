using System;
using System.IO;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;
using ZigBeeNet.Hardware.Digi.XBee.Internal;
using ZigBeeNet.Transport;

namespace ZigBeeNet.Hardware.Digi.XBee.Test
{
    public class XBeeFrameHandlerTest
    {
        private readonly ITestOutputHelper _output;

        public XBeeFrameHandlerTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void TestReceivePacket()
        {
            int[] response = GetPacket("7E 00 02 8A 06 6F");
            Assert.NotNull(response);
            Assert.Equal(5, response.Length);
            Assert.Equal(0x8A, response[2]);
        }

        [Fact]
        public void TestReceivePacketBadChecksum()
        {
            int[] response = GetPacket("7E 00 02 8A 06 6E");
            Assert.Null(response);
        }

        [Fact]
        public void TestReceivePacketWithPreamble()
        {
            int[] response = GetPacket("00 11 22 7E 00 02 8A 06 6F");
            Assert.NotNull(response);
            Assert.Equal(5, response.Length);
            Assert.Equal(0x8A, response[2]);
        }

        [Fact]
        public void TestReceivePacketTooLong()
        {
            int[] response = GetPacket("7E FF 00 8A 06 6F");
            Assert.Null(response);
        }

        [Fact]
        public void TestReceivePacketEscaped()
        {
            int[] response = GetPacket("7E 00 02 23 7D 31 CB");
            Assert.NotNull(response);
            Assert.Equal(5, response.Length);
            Assert.Equal(0x23, response[2]);
            Assert.Equal(0x11, response[3]);
        }

        private int[] GetPacket(string packet)
        {

            string[] split = packet.Split(" ");

            byte[] response = new byte[split.Length];
            int cnt = 0;
            foreach (string val in split)
            {
                response[cnt++] = (byte)Convert.ToInt32(val, 16);
            }

            XBeeFrameHandler frameHandler = new XBeeFrameHandler();
            using (MemoryStream memoryStream = new MemoryStream(response))
            {
                IZigBeePort port = new TestPort(memoryStream);

                try
                {
                    Type type = frameHandler.GetType();
                    FieldInfo field = type.GetField("_serialPort", BindingFlags.NonPublic | BindingFlags.Instance);
                    field.SetValue(frameHandler, port);

                    MethodInfo privateMethod = type.GetMethod("GetPacket", BindingFlags.NonPublic | BindingFlags.Instance);

                    return (int[])privateMethod.Invoke(frameHandler, null);
                }
                catch (Exception ex)
                {
                    _output.WriteLine(ex.StackTrace);
                }

                return null;
            }
        }
    }

    class TestPort : IZigBeePort
    {
        readonly MemoryStream _memoryStream;

        public TestPort(MemoryStream memoryStream)
        {
            _memoryStream = memoryStream;
        }

        public void Close()
        {
        }

        public bool Open()
        {
            return true;
        }

        public bool Open(int baudrate)
        {
            return false;
        }

        public bool Open(int baudrate, FlowControl flowControl)
        {
            return false;
        }

        public void PurgeRxBuffer()
        {
        }

        public byte? Read()
        {
            try
            {
                return Convert.ToByte(_memoryStream.ReadByte());
            }
            catch (IOException)
            {
                return null;
            }
        }

        public byte? Read(int timeout)
        {
            return Read();
        }

        public void Write(byte[] value)
        {
        }
    }
}
