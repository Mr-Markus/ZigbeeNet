using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    public class ZDO_NODE_DESC_REQ_SRSP : ZToolPacket
    {
        /// <summary>
        /// Status is either Success (0) or Failure (1)
        /// </summary>
        public PacketStatus Status { get; private set; }

        public ZDO_NODE_DESC_REQ_SRSP(byte[] framedata)
        {
            Status = (PacketStatus)framedata[0];

            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZDO_NODE_DESC_REQ_SRSP), framedata);
        }
    }
}
