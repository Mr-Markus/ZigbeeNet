using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet
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
