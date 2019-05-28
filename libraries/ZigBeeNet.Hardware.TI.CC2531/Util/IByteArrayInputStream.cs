using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Util
{
    public interface IByteArrayInputStream
    {
        byte Read();

        byte Read(string s);
    }
}
