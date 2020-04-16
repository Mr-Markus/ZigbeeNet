using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ZigBeeNet.Transport;

namespace ZigBeeNet.Hardware.TI.CC2531.Test
{
    public class TestPort : IZigBeePort
    {
        Stream input;
        Stream output;

        public TestPort(Stream input, Stream output)
        {
            this.input = input;
            this.output = output;
        }

        public bool Open()
        {
            return true;
        }

        public void Close()
        {
        }

        public void Write(byte[] value)
        {
        }

        public byte? Read(int timeout)
        {
            return Read();
        }

        public byte? Read()
        {
            try
            {
                return (byte)input.ReadByte();
            }
            catch (IOException)
            {
                return null;
            }
        }

        public bool Open(int baudRate)
        {
            return false;
        }

        public bool Open(int baudRate, FlowControl flowControl)
        {
            return false;
        }

        public void PurgeRxBuffer()
        {
        }
    }
}
