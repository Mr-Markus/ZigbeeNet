using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet
{
    public interface IZigBeeAddress : IComparable<IZigBeeAddress>
    {
        /**
         * The network address for this address.
         */
        ushort Address { get; set; }

        /**
         * Check whether this address is ZigBee group.
         */
        bool IsGroup { get; }
    }
}
