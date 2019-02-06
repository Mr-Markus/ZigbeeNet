using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Extensions
{
    public static class DataTypeExtension
    {
        public static byte GetByte(this ushort value, int index)
        {
            if (index < 0 || index > 1)
                throw new ArgumentOutOfRangeException(nameof(index));

            return BitConverter.GetBytes(value)[index];
        }

        public static byte GetByte(this int value, int index)
        {
            if (index < 0 || index > 1)
                throw new ArgumentOutOfRangeException(nameof(index));

            return BitConverter.GetBytes(value)[index];
        }

        public static byte[] GetBytes(this ushort value)
        {
            return BitConverter.GetBytes(value);
        }
    }
}
