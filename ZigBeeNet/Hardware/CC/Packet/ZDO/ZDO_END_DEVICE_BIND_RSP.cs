using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.CC.Util;

namespace ZigBeeNet.Hardware.CC.Packet.ZDO
{
    public class ZDO_END_DEVICE_BIND_RSP : ZToolPacket /* implements IRESPONE_CALLBACK,IZDO */
    {
        /// <name>TI.ZPI1.ZDO_END_DEVICE_BIND_RSP.SrcAddress</name>
        /// <summary>the message's source network address</summary>
        public ZToolAddress16 SrcAddress { get; private set; }
        /// <name>TI.ZPI1.ZDO_END_DEVICE_BIND_RSP.Status</name>
        /// <summary>this field indicates status of the bind request</summary>
        public int Status { get; private set; }

        public ZDO_END_DEVICE_BIND_RSP(byte[] framedata)
        {
            this.SrcAddress = new ZToolAddress16(framedata[1], framedata[0]);
            this.Status = framedata[2];
            BuildPacket(new DoubleByte(ZToolCMD.ZDO_END_DEVICE_BIND_RSP), framedata);
        }

        public override string ToString()
        {
            return "ZDO_END_DEVICE_BIND_RSP{" + "SrcAddress=" + SrcAddress + ", Status=" + Status
                    + '}';
        }
    }
}
