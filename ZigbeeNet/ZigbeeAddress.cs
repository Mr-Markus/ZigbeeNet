using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet
{
    public interface IZigBeeAddress : IComparable<IZigBeeAddress>
    {
        /**
         * The network address for this address.
         */
        int Address { get; set; }

        /**
         * Check whether this address is ZigBee group.
         */
        bool IsGroup();
    }
}
