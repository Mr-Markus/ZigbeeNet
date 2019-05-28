using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Util
{
    /// <summary>
     /// Big Endian container for 64-bit XBee Address
     /// <p/>
     /// See device addressing in manual p.32
     /// </summary>
    public class ZToolAddress64 : ZToolAddress
    {

        // private final static Logger log = Logger.getLogger(ZToolAddress64.class);

        // broadcast address 0x000000ff
        public static ZToolAddress64 BROADCAST = new ZToolAddress64(new byte[] { 0, 0, 0, 0, 0, 0, (byte)0xff, (byte)0xff });
        public static ZToolAddress64 ZNET_COORDINATOR = new ZToolAddress64(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });

        public long Long
        {
            get
            {
                return ByteHelper.LongFromBytes(Address, 0, 7);
            }
        }

        public override byte[] Address { get; protected set; }

        public ZToolAddress64(long ieee)
        {
            Address = new byte[8];
            for (int i = Address.Length - 1; i >= 0; i--)
            {
                Address[i] = (byte)ieee;
                ieee = ieee >> 8;
            }
        }

        public ZToolAddress64(byte[] address)
        {
            Address = new byte[8];

            Array.Copy(address, Address, address.Length);
        }

        public ZToolAddress64()
        {
            Address = new byte[8];
        }

        public override int GetHashCode()
        {
            return Address.GetHashCode();
        }

        public override bool Equals(object o)
        {
            if (o == this)
            {
                return true;
            }
            else if (o is ZToolAddress64)
            {
                ZToolAddress64 ieee = (ZToolAddress64)o;
                if (ieee.Address == this.Address)
                {
                    return true;
                }
                for (int i = 0; i < this.Address.Length; i++)
                {
                    if (ieee.Address[i] != this.Address[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
