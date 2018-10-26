using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ZigbeeNet.CC
{
    public class ZAddress16 : ZAddress
    {
        public DoubleByte doubleByte { get; set; }

        public ushort Value
        {
            get
            {
                return doubleByte.Get16BitValue();
            }
            set
            {
                doubleByte = new DoubleByte(value);
            }
        }

        public ZAddress16()
        {
            doubleByte = new DoubleByte();
        }

        public ZAddress16(byte high, byte low)
        {
            doubleByte = new DoubleByte(high, low);
        }

        public ZAddress16(byte[] data)
        {
            if (data.Length != 2)
                throw new InvalidDataException(nameof(data));

            doubleByte = new DoubleByte(data[0], data[1]);
        }

        public override byte[] ToByteArray()
        {
            return BitConverter.GetBytes(Value);
        }
    }
}
