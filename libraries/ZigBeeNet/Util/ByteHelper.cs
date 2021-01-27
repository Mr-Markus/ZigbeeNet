using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
namespace ZigBeeNet
{
    public static class ByteHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort ShortFromBytes(byte[] values, int msb, int lsb)
        {
            return ShortFromBytes(values[msb],values[lsb]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        public static byte[] Slice(this byte[] data, int offset, int len)
        {
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (len < 0)
                throw new ArgumentOutOfRangeException(nameof(len));
            if (data.Length < offset + len)
                throw new ArgumentException("Not enough data for the slice");
            byte[] range = new byte[len];
            Buffer.BlockCopy(data, offset, range, 0, len);
            offset += len;
            return range;
        }

        public static byte ComputeParityChecksum(this byte[] data,int offset, int len)
        {
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (len < 0)
                throw new ArgumentOutOfRangeException(nameof(len));
            if (data.Length < offset + len)
                throw new ArgumentException("data.Length < offset + len");
            byte crc=0;
            for (int i=offset;i<offset+len;i++)
                crc = (byte)(crc ^ data[i]);
            return crc;
        }
    }
}
