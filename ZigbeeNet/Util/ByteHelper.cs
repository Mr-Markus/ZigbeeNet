using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet
{
    public static class ByteHelper
    {
        public static ushort ShortFromBytes(byte[] values, int msb, int lsb)
        {
            int value = (values[msb] << 8) + values[lsb];

            return (ushort)value;
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
