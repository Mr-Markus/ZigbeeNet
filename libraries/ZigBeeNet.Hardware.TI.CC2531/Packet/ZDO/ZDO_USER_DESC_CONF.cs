using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Util;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    public class ZDO_USER_DESC_CONF : ZToolPacket
    {
        /// <name>TI.ZPI1.ZDO_USER_DESC_CONF.SrcAddress</name>
        /// <summary>Source address of the message</summary>
        public ZToolAddress16 SrcAddress { get; set; }
        /// <name>TI.ZPI1.ZDO_USER_DESC_CONF.Status</name>
        /// <summary>this field indicates status of the request</summary>
        public int Status { get; set; }
        public ZToolAddress16 NwkAddr { get; set; }

        /// <name>TI.ZPI1.ZDO_USER_DESC_CONF</name>
        /// <summary>Constructor</summary>
        public ZDO_USER_DESC_CONF()
        {
        }

        public ZDO_USER_DESC_CONF(byte[] framedata)
        {
            this.SrcAddress = new ZToolAddress16(framedata[1], framedata[0]);
            this.Status = framedata[2];
            if (framedata.Length == 5)
            {
                this.NwkAddr = new ZToolAddress16(framedata[4], framedata[3]);
            }
            else
            {
                this.NwkAddr = new ZToolAddress16();
            }
            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZDO_USER_DESC_CONF), framedata);
        }
    }
}
