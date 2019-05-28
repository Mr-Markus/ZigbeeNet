using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SimpleAPI
{
    /// <summary>
    /// This callback indicates the data has been sent
    /// </summary>
    public class ZB_SEND_DATA_CONFIRM : ZToolPacket
    {
        /// <summary>
        /// Specifies the handle
        /// </summary>
        public byte Handle { get; private set; }

        /// <summary>
        /// This field indicates either SUCCESS (0) or FAILURE (1)
        /// </summary>
        public PacketStatus Status { get; private set; }

        public ZB_SEND_DATA_CONFIRM(byte[] framedata)
        {
            Handle = framedata[0];
            Status = (PacketStatus)framedata[1];

            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZB_SEND_DATA_CONFIRM), framedata);
        }
    }
}
