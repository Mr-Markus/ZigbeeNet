using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Util;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    /// <summary>
    /// This command is generated to set the Permit Join for the destination device
    /// </summary>
    public class ZDO_MGMT_PERMIT_JOIN_REQ : ZToolPacket
    {
        /// <summary>
        /// Destination address type: 0x02 – Address 16 bit, 0xFF – Broadcast
        /// </summary>
        public byte AddrMode { get; private set; }

        /// <summary>
        /// Specifies the network address of the destination device whose Permit Join information is to be modified. 
        /// </summary>
        public ZToolAddress16 DstAddr { get; private set; }

        /// <summary>
        /// Specifies the duration to permit joining.  0 = join disabled.  0xff = join enabled. 0x01-0xfe = number of seconds to permit joining
        /// </summary>
        public byte Duration { get; private set; }

        /// <summary>
        /// Trust Center Significance
        /// </summary>
        public byte TCSignificance { get; private set; }


        public ZDO_MGMT_PERMIT_JOIN_REQ(byte addrMode, ZToolAddress16 dstAddr, byte duration, bool tcsSignificant)
        {
            AddrMode = addrMode;
            DstAddr = dstAddr;
            Duration = duration;
            TCSignificance = tcsSignificant ? (byte)0x01 : (byte)0x00;

            List<byte> data = new List<byte>();
            data.Add(AddrMode);
            data.AddRange(DstAddr.Address);
            data.Add(Duration);
            data.Add(TCSignificance);

            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZDO_MGMT_PERMIT_JOIN_REQ), data.ToArray());
        }
    }
}
