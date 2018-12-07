using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CC.Packet
{
    public class ZToolParseException : Exception
    {
        private static long serialVersionUID = 6752060371295132748L;

        public ZToolParseException(string s)
            : base(s)
        {
            
        }
    }
}
