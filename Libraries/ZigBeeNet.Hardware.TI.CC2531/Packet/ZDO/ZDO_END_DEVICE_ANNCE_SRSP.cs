using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    public class ZDO_END_DEVICE_ANNCE_SRSP : ZToolPacket //// implements IRSPONSE,IZDO /// </summary>
    {
        /// <name>TI.ZPI1.ZDO_END_DEVICE_ANNCE_SRSP.Status</name>
        /// <summary>Status</summary>
        public int Status { get; private set; }

        /// <name>TI.ZPI1.ZDO_END_DEVICE_ANNCE_SRSP</name>
        /// <summary>Constructor</summary>
        public ZDO_END_DEVICE_ANNCE_SRSP()
        {
        }

        public ZDO_END_DEVICE_ANNCE_SRSP(byte[] framedata)
        {
            this.Status = framedata[0];
            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZDO_END_DEVICE_ANNCE_SRSP), framedata);
        }

        public override string ToString()
        {
            return "ZDO_END_DEVICE_ANNCE_SRSP{" + "Status=" + Status + '}';
        }
    }
}
