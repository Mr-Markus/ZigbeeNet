using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CC.Packet.ZDO
{
    public class ZDO_MATCH_DESC_REQ_SRSP : ZToolPacket
    {
        /// <name>TI.ZPI1.ZDO_MATCH_DESC_REQ_SRSP.Status</name>
        /// <summary>Status</summary>
        public int Status { get; private set; }

        public ZDO_MATCH_DESC_REQ_SRSP(byte[] framedata)
        {
            this.Status = framedata[0];
            BuildPacket(new DoubleByte(ZToolCMD.ZDO_MATCH_DESC_REQ_SRSP), framedata);
        }

        public String toString()
        {
            return "ZDO_MATCH_DESC_REQ_SRSP{" + "Status=" + Status + '}';
        }
    }
}
