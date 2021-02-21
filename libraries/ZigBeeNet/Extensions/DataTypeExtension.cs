using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Extensions
{
    public static class DataTypeExtension
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte GetByte(this ushort value, int index)
        {
            if (index < 0 || index > 1)
                throw new ArgumentOutOfRangeException(nameof(index));
            
            if (index==0) // Little endian encoding
                return value.GetLSB();
            else
                return value.GetMSB();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte GetMSB(this ushort value) => unchecked((byte)(value>>8));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte GetLSB(this ushort value) => unchecked((byte)(value));

        public static byte GetByte(this int value, int index)
        {
            if (index < 0 || index > 3)
                throw new ArgumentOutOfRangeException(nameof(index));

            return unchecked((byte)(value>>(8*index)));
        }

        public static byte[] GetBytes(this ushort value)
        {
            return new byte[] { unchecked((byte)value), unchecked((byte)(value>>8)) };
        }
     }    
}
