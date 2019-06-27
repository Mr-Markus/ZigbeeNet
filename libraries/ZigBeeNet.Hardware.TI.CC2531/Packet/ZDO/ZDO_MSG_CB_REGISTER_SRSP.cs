using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    public class ZDO_MSG_CB_REGISTER_SRSP : ZToolPacket
    {
        /// <name>TI.ZPI2.ZDO_MSG_CB_REGISTER_SRSP.Status</name>
        /// <summary>this field indicates either SUCCESS or FAILURE.</summary>
        public PacketStatus Status;

        /// <name>TI.ZPI2.ZDO_MSG_CB_REGISTER_SRSP</name>
        /// <summary>Constructor</summary>
        public ZDO_MSG_CB_REGISTER_SRSP()
        {
        }

        public ZDO_MSG_CB_REGISTER_SRSP(byte[] framedata)
        {
            Status = (PacketStatus)framedata[0];

            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZDO_MSG_CB_REGISTER_SRSP), framedata);
        }

        public override string ToString()
        {
            return $"ZDO_MSG_CB_REGISTER_SRSP(Status={Status})";
        }
    }
}
