using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Util
{
    /// <summary>
  /// Represents a double byte XBeeApi Address.
  /// </summary>
    public class ZToolAddress16 : ZToolAddress
    {

        public static readonly ZToolAddress16 BROADCAST = new ZToolAddress16(0xFF, 0xFF);
        public static readonly ZToolAddress16 ZNET_BROADCAST = new ZToolAddress16(0xFF, 0xFE);
        public static readonly ZToolAddress16 ZCZR_BROADCAST = new ZToolAddress16(0xFF, 0xFC);

        private ushort _doubleByte;// = new DoubleByte();

        /// <summary>
         /// Provide address as msb byte and lsb byte
         ///
         /// <param name="msb"></param>
         /// <param name="lsb"></param>
         /// </summary>
        public ZToolAddress16(byte msb, byte lsb)
        {
            // this._doubleByte.Msb = msb;
            // this._doubleByte.Lsb = lsb;
            _doubleByte = (ushort)(msb<<8|lsb);
        }

        public ZToolAddress16(byte[] arr) : this(arr[0],arr[1])
        {
        }

        public ZToolAddress16()
        {

        }

        public ushort Value
        {
            get
            {
                return _doubleByte; //this._doubleByte.Value;
            }
        }

        public byte Msb => (byte)(_doubleByte>>8);

        public byte Lsb => unchecked((byte)(_doubleByte));
        
        public override int GetHashCode()
        {
            return _doubleByte.GetHashCode();
        }

        public override bool Equals(object o)
        {
            if (this == o)
            {
                return true;
            }
            else
            {
                try
                {
                    ZToolAddress16 addr = (ZToolAddress16)o;

                    return (this.Lsb == addr.Lsb && this.Msb == addr.Msb);
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public override byte[] Address
        {
            get
            {
                return new byte[] { Msb, Lsb };
            }
            protected set
            {
                this._doubleByte = (ushort)(value[0]<<8 | value[1]);
            }
        }
    }
}
