using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SYS
{
    public class SYS_RPC_ERROR : ZToolPacket
    {
        /// <name>TI.ZPI2.SYS_RPC_ERROR.ErrCmd0</name>
        /// <summary>Command byte 0 of the message causing an error.</summary>
        public int ErrCmd0 { get; set; }
        /// <name>TI.ZPI2.SYS_RPC_ERROR.ErrCmd1</name>
        /// <summary>Command byte 1 of the message causing an error.</summary>
        public int ErrCmd1 { get; set; }
        /// <name>TI.ZPI2.SYS_RPC_ERROR.Status</name>
        /// <summary>Status</summary>
        public int Status { get; set; }

        /// <name>TI.ZPI2.SYS_RPC_ERROR</name>
        /// <summary>Constructor</summary>
        public SYS_RPC_ERROR()
        {
        }

        /// <name>TI.ZPI2.SYS_RPC_ERROR</name>
        /// <summary>Constructor</summary>
        public SYS_RPC_ERROR(byte num1, byte num2, byte num3)
        {
            this.Status = num1;
            this.ErrCmd0 = num2;
            this.ErrCmd1 = num3;
            byte[] framedata = { num1, num2, num3 };
            BuildPacket(new DoubleByte((ushort)ZToolCMD.SYS_RPC_ERROR), framedata);
        }

        public SYS_RPC_ERROR(byte[] framedata)
        {
            this.Status = framedata[0];
            this.ErrCmd0 = framedata[1];
            this.ErrCmd1 = framedata[3];
            BuildPacket(new DoubleByte((ushort)ZToolCMD.SYS_RPC_ERROR), framedata);
        }

        public override string ToString()
        {
            return "SYS_RPC_ERROR{" +
                    "ErrCmd0=" + ErrCmd0 +
                    ", ErrCmd1=" + ErrCmd1 +
                    ", Status=" + Status +
                    '}';
        }
    }
}
