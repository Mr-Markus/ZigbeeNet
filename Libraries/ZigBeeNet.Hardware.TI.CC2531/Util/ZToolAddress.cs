using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Util
{
    /// <summary>
     /// Represents a double byte XBeeApi Address.
     /// </summary>
    public abstract class ZToolAddress
    {
        public abstract byte[] Address { get; protected set; }

        public override string ToString()
        {
            return ByteUtils.ToBase16(Address);
        }
    }
}
