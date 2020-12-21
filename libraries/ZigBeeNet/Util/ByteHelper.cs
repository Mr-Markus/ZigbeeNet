using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet
{
    public static class ByteHelper
    {
        public static ushort ShortFromBytes(byte[] values, int msb, int lsb)
        {
            return ShortFromBytes(values[msb],values[lsb]);
        }

        public static ushort ShortFromBytes(byte msb, byte lsb)
        {
            return (ushort)(msb<<8 | lsb);
        }

        public static long LongFromBytes(byte[] values, int msb, int lsb)
        {
            long result = (values[msb] & 0xFF);

            if (msb < lsb)
            {
                for (int i = msb + 1; i <= lsb; i++)
                {
                    result = (result << 8) + (values[i] & 0xFF);
                }
            }
            else
            {
                for (int i = msb - 1; i >= lsb; i--)
                {
                    result = (result << 8) + (values[i] & 0xFF);
                }
            }

            return result;
        }
    }
}
