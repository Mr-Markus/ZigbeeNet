using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ZigbeeNet.CC
{
    public class DoubleByte
    {
        /// <summary>
        /// Gets or sets high byte
        /// </summary>
        public byte High { get; set; }

        /// <summary>
        /// Gets or sets low byte
        /// </summary>
        public byte Low { get; set; }


        public DoubleByte()
        {

        }

        /// <summary>
        /// Decomposes a 16bit int into high and low bytes
        /// </summary>
        /// <param name="val"></param>
        public DoubleByte(ushort val)
        {
            if (val > 0xFFFF || val < 0)
                throw new InvalidDataException(nameof(val));

            // split address into high and low bytes
            High = (byte)(val >> 8);
            Low = (byte)(val & 0xFF);
        }

        /// <summary>
        /// Constructs a 16bit value from two bytes (high and low)
        /// </summary>
        /// <param name="high"></param>
        /// <param name="low"></param>
        public DoubleByte(byte low, byte high)
        {

            if (high > 0xFF)
                throw new InvalidDataException(nameof(high));
            if(low > 0xFF)
                throw new InvalidDataException(nameof(low));

            this.High = high;
            this.Low = low;
        }

        public ushort Get16BitValue()
        {
            return BitConverter.ToUInt16(new byte[2] { High, Low }, 0);
        }
    }
}
