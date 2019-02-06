using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SYS
{
    /// <summary>
    /// This callback is sent by the device to indicate that a reset has occurred. 
    /// </summary>
    public class SYS_RESET_RESPONSE : ZToolPacket
    {
        /// <summary>
        /// Hardware revision number
        /// </summary>
        public byte HwRev { get; private set; }

        /// <summary>
        /// Major release number
        /// </summary>
        public byte MajorRel{ get; private set; }

        /// <summary>
        /// Minor release number
        /// </summary>
        public byte MinorRel { get; private set; }

        /// <summary>
        /// Product
        /// </summary>
        public byte Product { get; private set; }

        /// <summary>
        /// Reason for the reset
        /// </summary>
        public ResetType Reason { get; private set; }

        /// <summary>
        /// Transport protocol revision
        /// </summary>
        public byte TransportRev { get; private set; }

        public SYS_RESET_RESPONSE(byte[] framedata)
        {
            Reason = (ResetType)framedata[0];
            TransportRev = framedata[1];
            Product = framedata[2];
            MajorRel = framedata[3];
            MinorRel = framedata[4];
            HwRev = framedata[5];

            BuildPacket(new DoubleByte((ushort)ZToolCMD.SYS_RESET_RESPONSE), framedata);
        }

        public enum ResetType : byte
        {
            PowerUp = 0x00,
            External = 0x01,
            WatchDog = 0x02
        }
    }
}
