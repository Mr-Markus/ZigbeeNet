using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.Util
{
    public static class Hash
    {
        public static int CalcHashCode(int[] array)
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
