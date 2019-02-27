using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet
{
    public interface IZigBeeAddress : IComparable<IZigBeeAddress>
    {
        /// <summary>
         /// The network address for this address.
         /// </summary>
        ushort Address { get; set; }

        /// <summary>
         /// Check whether this address is ZigBee group.
         /// </summary>
        bool IsGroup { get; }
    }
}
