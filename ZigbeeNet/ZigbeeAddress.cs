using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet
{
    public abstract class ZigbeeAddress
    {
        public abstract byte[] ToByteArray();
    }
}
