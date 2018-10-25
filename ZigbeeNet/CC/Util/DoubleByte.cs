using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC
{
    public class DoubleByte
    {

        private byte _high;
        private byte _low;

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
            _high = (byte)(val >> 8);
            _low = (byte)(val & 0xFF);
        }

        /// <summary>
        /// Constructs a 16bit value from two bytes (high and low)
        /// </summary>
        /// <param name="high"></param>
        /// <param name="low"></param>
        public DoubleByte(byte low, byte high)
        {

            if (high > 0xFF || low > 0xFF)
            {
                throw new ArgumentOutOfRangeException("msb or lsb are out of range");
            }

            this._high = high;
            this._low = low;
        }

        /// <summary>
        /// Get high byte
        /// </summary>
        /// <returns></returns>
        public byte GetHighByte()
        {
            return _high;
        }

        /// <summary>
        /// Get low byte
        /// </summary>
        /// <returns></returns>
        public byte GetLowByte()
        {
            return _low;
        }

        public ushort Get16BitValue()
        {
            return BitConverter.ToUInt16(new byte[2] { _high, _low }, 0);
        }

        public void SetHigh(byte high)
        {
            this._high = high;
        }

        public void SetLow(byte low)
        {
            this._low = low;
        }
    }
}
