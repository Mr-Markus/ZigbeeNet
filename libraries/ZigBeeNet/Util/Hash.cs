using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Util
{
    public static class Hash
    {
        public static int CalcHashCode(byte[] array)
        {
            int hc = array.Length;
            for (int i = 0; i < array.Length; ++i)
            {
                hc = unchecked(hc * 17 + array[i]);
            }
            return hc;
        }
    }
}
