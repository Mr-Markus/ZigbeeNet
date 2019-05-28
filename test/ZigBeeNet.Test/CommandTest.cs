using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZigBeeNet.Test
{
    public class CommandTest
    {
        public byte[] GetPacketData(string stringData)
        {
            string hex = stringData.Replace(" ", "");

            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
