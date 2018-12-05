using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CC.Packet.SimpleAPI
{
    /// <summary>
    /// This callback indicates another device attempted to bind to this device
    /// </summary>
    public class ZB_ALLOW_BIND_CONFIRM : AsynchronousRequest
    {
        /// <summary>
        /// Contains the address of the device attempted to bind to this device
        /// </summary>
        public ZigbeeAddress16 Source { get; private set; }

        public ZB_ALLOW_BIND_CONFIRM(ZigbeeAddress16 source)
        {
            Source = source;

            byte[] framedata = new byte[2];
            framedata[0] = Source.DoubleByte.Lsb;
            framedata[1] = Source.DoubleByte.Msb;

            BuildPacket(CommandType.ZB_ALLOW_BIND_CONFIRM, framedata);
        }
    }
}
