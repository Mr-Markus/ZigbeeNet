using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    public class ZDO_UNBIND_REQ_SRSP : ZToolPacket
    {
        /// <name>TI.ZPI1.ZDO_UNBIND_REQ_SRSP.Status</name>
        /// <summary>Status</summary>
        public int Status { get; private set; }

        /// <name>TI.ZPI1.ZDO_UNBIND_REQ_SRSP</name>
        /// <summary>Constructor</summary>
        public ZDO_UNBIND_REQ_SRSP()
        {
        }

        public ZDO_UNBIND_REQ_SRSP(byte[] framedata)
        {
            this.Status = framedata[0];
            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZDO_UNBIND_REQ_SRSP), framedata);
        }

        public override string ToString()
        {
            return "ZDO_UNBIND_REQ_SRSP{" + "Status=" + Status + '}';
        }
    }
}
