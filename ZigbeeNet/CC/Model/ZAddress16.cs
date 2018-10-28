using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ZigbeeNet.CC
{
    public class ZAddress16 : ZAddress
    {
        public DoubleByte DoubleByte { get; private set; }

        public ushort Value
        {
            get
            {
                return DoubleByte.Get16BitValue();
            }
            set
            {
                DoubleByte = new DoubleByte(value);
            }
        }

        public ZAddress16()
        {
            DoubleByte = new DoubleByte();
        }

        public ZAddress16(byte msb, byte lsb)
        {
            DoubleByte = new DoubleByte(msb, lsb);
        }

        public ZAddress16(byte[] data)
        {
            if (data.Length != 2)
                throw new InvalidDataException(nameof(data));

            DoubleByte = new DoubleByte(data[1], data[0]);
        }

        public override byte[] ToByteArray()
        {
            return BitConverter.GetBytes(Value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
