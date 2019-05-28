using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    public class ZDO_USER_DESC_REQ_SRSP : ZToolPacket
    {
        /// <name>TI.ZPI1.ZDO_USER_DESC_REQ_SRSP.Status</name>
        /// <summary>Status</summary>
        public int Status { get; private set; }

        public ZDO_USER_DESC_REQ_SRSP(byte[] framedata)
        {
            this.Status = framedata[0];
            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZDO_USER_DESC_REQ_SRSP), framedata);
        }

        public override string ToString()
        {
            return "ZDO_USER_DESC_REQ_SRSP{" + "Status=" + Status + '}';
        }
    }
}
