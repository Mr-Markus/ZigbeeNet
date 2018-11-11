using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Packet.ZDO
{
    /// <summary>
    /// This command is generated to set the Permit Join for the destination device
    /// </summary>
    public class ZDO_MGMT_PERMIT_JOIN_REQ : SynchronousRequest
    {
        /// <summary>
        /// Destination address type: 0x02 – Address 16 bit, 0xFF – Broadcast
        /// </summary>
        public byte AddrMode { get; private set; }

        /// <summary>
        /// Specifies the network address of the destination device whose Permit Join information is to be modified. 
        /// </summary>
        public ZigbeeAddress16 DstAddr { get; private set; }

        /// <summary>
        /// Specifies the duration to permit joining.  0 = join disabled.  0xff = join enabled. 0x01-0xfe = number of seconds to permit joining
        /// </summary>
        public byte Duration { get; private set; }

        /// <summary>
        /// Trust Center Significance
        /// </summary>
        public byte TCSignificance { get; private set; }


        public ZDO_MGMT_PERMIT_JOIN_REQ(byte addrMode, ZigbeeAddress16 dstAddr, byte duration, bool tcsSignificant)
        {
            AddrMode = addrMode;
            DstAddr = dstAddr;
            Duration = duration;
            TCSignificance = tcsSignificant ? (byte)0x01 : (byte)0x00;

            List<byte> data = new List<byte>();
            data.Add(AddrMode);
            data.AddRange(DstAddr.ToByteArray());
            data.Add(Duration);
            data.Add(TCSignificance);

            BuildPacket(CommandType.ZDO_MGMT_PERMIT_JOIN_REQ, data.ToArray());
        }
    }
}
