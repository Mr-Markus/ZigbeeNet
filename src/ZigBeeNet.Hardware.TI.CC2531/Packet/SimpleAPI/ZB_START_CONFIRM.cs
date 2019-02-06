using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SimpleAPI
{
    /// <summary>
    /// This callback is called by the ZigBee stack after a start request operation completes
    /// </summary>
    public class ZB_START_CONFIRM : ZToolPacket
    {
        /// <summary>
        /// This field indicates either SUCCESS (0) or FAILURE (1)
        /// </summary>
        public PacketStatus Status { get; private set; }

        public ZB_START_CONFIRM(byte[] framedata)
        {
            Status = (PacketStatus)framedata[0];

            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZB_START_CONFIRM), framedata);
        }
    }
}
