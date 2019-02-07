using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Util
{
    public class CieColor { 
        public double RawX { get; set; }

        public double RawY { get; set; }
    
        public ushort X
        {
            get
            {
                return (ushort)(RawX * 65535 + 0.5);
            }
        }

        public ushort Y
        {
            get
            {
                return (ushort)(RawY * 65535 + 0.5);
            }
        }

        public CieColor(double x, double y)
        {
            RawX = x;
            RawY = y;
        }
    }
}
