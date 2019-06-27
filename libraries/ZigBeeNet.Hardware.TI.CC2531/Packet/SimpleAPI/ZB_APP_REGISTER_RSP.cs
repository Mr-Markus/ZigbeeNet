using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SimpleAPI
{
    public class ZB_APP_REGISTER_RSP : ZToolPacket
    {
        /// <name>TI.ZPI2.ZB_APP_REGISTER_RSP.Status</name>
        /// <summary>Status</summary>
        public int Status { get; private set; }

        /// <name>TI.ZPI2.ZB_APP_REGISTER_RSP</name>
        /// <summary>Constructor</summary>
        public ZB_APP_REGISTER_RSP()
        {
        }

        public ZB_APP_REGISTER_RSP(byte[] framedata)
        {
            this.Status = framedata[0];
            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZB_APP_REGISTER_RSP), framedata);
        }

        public override string ToString()
        {
            return "ZB_APP_REGISTER_RSP{" +
                    "Status=" + Status +
                    '}';
        }
    }
}
