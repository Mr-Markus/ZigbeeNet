using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC
{
    public class DoubleByte
    {

        private byte msb;
        private byte lsb;

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
            {
                throw new ArgumentOutOfRangeException("value is out of range");
            }

            // split address into high and low bytes
            msb = (byte)(val >> 8);
            lsb = (byte)(val & 0xFF);
        }

        /// <summary>
        /// Constructs a 16bit value from two bytes (high and low)
        /// </summary>
        /// <param name="msb"></param>
        /// <param name="lsb"></param>
        public DoubleByte(byte msb, byte lsb)
        {

            if (msb > 0xFF || lsb > 0xFF)
            {
                throw new ArgumentOutOfRangeException("msb or lsb are out of range");
            }

            this.msb = msb;
            this.lsb = lsb;
        }

        public byte GetMsb()
        {
            return msb;
        }

        public byte GetLsb()
        {
            return lsb;
        }

        public int Get16BitValue()
        {
            return (this.msb << 8) + this.lsb;
        }

        public void SetMsb(byte msb)
        {
            this.msb = msb;
        }

        public void SetLsb(byte lsb)
        {
            this.lsb = lsb;
        }
    }
}
