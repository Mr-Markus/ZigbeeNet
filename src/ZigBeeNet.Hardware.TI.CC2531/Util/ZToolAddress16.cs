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

        private DoubleByte _doubleByte = new DoubleByte();

        /// <summary>
         /// Provide address as msb byte and lsb byte
         ///
         /// <param name="msb"></param>
         /// <param name="lsb"></param>
         /// </summary>
        public ZToolAddress16(byte msb, byte lsb)
        {
            this._doubleByte.Msb = msb;
            this._doubleByte.Lsb = lsb;
        }

        public ZToolAddress16(byte[] arr)
        {
            this._doubleByte.Msb = arr[0];
            this._doubleByte.Lsb = arr[1];
        }

        public ZToolAddress16()
        {

        }

        public ushort Value
        {
            get
            {
                return this._doubleByte.Value;
            }
        }

        public byte Msb
        {
            get
            {
                return this._doubleByte.Msb;
            }
            set
            {
                this._doubleByte.Msb = value;
            }
        }

        public byte Lsb
        {
            get
            {
                return this._doubleByte.Lsb;
            }
            set
            {
                this._doubleByte.Lsb = value;
            }
        }

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
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public override byte[] Address
        {
            get
            {
                return new byte[] { (byte)this._doubleByte.Msb, (byte)this._doubleByte.Lsb };
            }
            protected set
            {
                this._doubleByte = new DoubleByte(value[0], value[1]);
            }
        }
    }
}
