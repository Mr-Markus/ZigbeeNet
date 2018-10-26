using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Packet.SimpleAPI
{
    /// <summary>
    /// This callback indicates another device attempted to bind to this device
    /// </summary>
    public class ZB_ALLOW_BIND_CONFIRM : AsynchronousRequest
    {
        /// <summary>
        /// Contains the address of the device attempted to bind to this device
        /// </summary>
        public ZAddress16 Source { get; private set; }

        public ZB_ALLOW_BIND_CONFIRM(ZAddress16 source)
        {
            Source = source;

            byte[] framedata = new byte[2];
            framedata[0] = Source.DoubleByte.Low;
            framedata[1] = Source.DoubleByte.High;

            BuildPacket(CommandType.ZB_ALLOW_BIND_CONFIRM, framedata);
        }
    }
}
