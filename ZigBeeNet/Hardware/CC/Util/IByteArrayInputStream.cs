using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.CC.Util
{
    public interface IByteArrayInputStream
    {
        byte Read();

        byte Read(string s);
    }
}
