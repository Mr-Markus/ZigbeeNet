using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SimpleAPI
{
    public class ZB_BIND_CONFIRM : ZToolPacket
    {
        /// <name>TI.ZPI2.ZB_BIND_CONFIRM.CommandId</name>
        /// <summary>CommandId</summary>
        public DoubleByte CommandId { get; set; }
        /// <name>TI.ZPI2.ZB_BIND_CONFIRM.Status</name>
        /// <summary>The immediate return value from executing the RPC.</summary>
        public int Status { get; set; }

        /// <name>TI.ZPI2.ZB_BIND_CONFIRM</name>
        /// <summary>Constructor</summary>
        public ZB_BIND_CONFIRM()
        {
        }

        /// <name>TI.ZPI2.ZB_BIND_CONFIRM</name>
        /// <summary>Constructor</summary>
        public ZB_BIND_CONFIRM(byte[] framedata)
        {
            this.CommandId = new DoubleByte(framedata[1], framedata[0]);
            this.Status = framedata[2];
            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZB_FIND_DEVICE_CONFIRM), framedata);
        }

        public override string ToString()
        {
            return "ZB_BIND_CONFIRM{" + "CommandId=" + CommandId + ", Status=" + Status + '}';
        }
    }
}
