using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet
{
    public enum ZigBeeNwkAddressMode
    {
        /// <summary>
        /// Address is multicast group address
        /// </summary>
        Group = 0x01,
        /// <summary>
        /// Address is 16-bit network address of a device or a 16-bit broadcast address
        /// </summary>
        Device = 0x02
    }
}
