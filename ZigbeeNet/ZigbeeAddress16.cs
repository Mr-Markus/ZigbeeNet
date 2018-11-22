using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ZigbeeNet
{
    public class ZigbeeAddress16 : ZigbeeAddress
    {
        public DoubleByte DoubleByte { get; private set; }

        public ushort Value
        {
            get
            {
                return DoubleByte.Value;
            }
            set
            {
                DoubleByte = new DoubleByte(value);
            }
        }

        public ZigbeeAddress16()
        {
            DoubleByte = new DoubleByte();
        }

        public ZigbeeAddress16(ushort value)
        {
            Value = value;
        }

        public ZigbeeAddress16(byte msb, byte lsb)
        {
            DoubleByte = new DoubleByte(msb, lsb);
        }

        public ZigbeeAddress16(byte[] data)
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

        public override bool Equals(object obj)
        {
            if(obj is ZigbeeAddress16 z16)
            {
                return Value == z16.Value;
            }
            return false;
        }
    }
}
