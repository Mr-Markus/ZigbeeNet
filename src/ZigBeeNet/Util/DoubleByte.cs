using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ZigBeeNet
{
    public class DoubleByte
    {
        /// <summary>
        /// Gets or sets high byte
        /// </summary>
        public byte Msb { get; set; }

        /// <summary>
        /// Gets or sets low byte
        /// </summary>
        public byte Lsb { get; set; }


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
            Msb = (byte)(val >> 8);
            Lsb = (byte)(val & 0xFF);
        }

        /// <summary>
        /// Constructs a 16bit value from two bytes (high and low)
        /// </summary>
        /// <param name="msb"></param>
        /// <param name="lsb"></param>
        public DoubleByte(byte msb, byte lsb)
        {

            if (msb > 0xFF)
                throw new InvalidDataException(nameof(msb));
            if(lsb > 0xFF)
                throw new InvalidDataException(nameof(lsb));

            this.Msb = msb;
            this.Lsb = lsb;
        }

        public ushort Value => BitConverter.ToUInt16(new byte[2] { Lsb, Msb }, 0);

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
