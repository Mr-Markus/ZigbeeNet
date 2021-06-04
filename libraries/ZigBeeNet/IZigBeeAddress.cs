using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet
{
    public interface IZigBeeAddress
    {
        /// <summary>
         /// The network address for this address.
         /// </summary>
        ushort Address { get; }

        /// <summary>
         /// Check whether this address is ZigBee group.
         /// </summary>
        bool IsGroup { get; }
    }
}
