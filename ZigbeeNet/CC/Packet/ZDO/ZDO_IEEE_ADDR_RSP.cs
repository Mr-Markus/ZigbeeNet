using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Packet.ZDO
{
    /// <summary>
    /// This callback message is in response to the ZDO IEEE Address Request
    /// </summary>
    public class ZDO_IEEE_ADDR_RSP : AsynchronousRequest
    {
        /// <summary>
        /// This field indicates either SUCCESS or FAILURE
        /// </summary>
        public PacketStatus Status { get; set; }

        /// <summary>
        /// 64 bit IEEE address of source device
        /// </summary>
        public ZAddress64 IeeeAddr { get; set; }

        /// <summary>
        /// Specifies the short network address of responding device
        /// </summary>
        public ZAddress16 NwkAddr { get; set; }

        /// <summary>
        /// Specifies the starting index into the list of associated devices for this report
        /// </summary>
        public byte StartIndex { get; set; }

        /// <summary>
        /// Specifies the number of associated devices
        /// </summary>
        public byte NumAssocDev { get; set; }

        /// <summary>
        /// Contains the list of network address for associated devices.  
        /// This list can be a partial list if the entire list doesn’t fit into a packet.  
        /// If it is a partial list, the starting index is StartIndex
        /// </summary>
        public List<ZAddress16> AssocDevList { get; set; }

        public ZDO_IEEE_ADDR_RSP()
        {
            AssocDevList = new List<ZAddress16>();
        }

        public ZDO_IEEE_ADDR_RSP(byte[] data)
        {
            Status = (PacketStatus)data[0];
            IeeeAddr = new ZAddress64(BitConverter.ToUInt64(data, 1));
            NwkAddr = new ZAddress16(data[9], data[10]);
            StartIndex = data[11];
            NumAssocDev = data[12];

            AssocDevList = new List<ZAddress16>();
            for (int i = 0; i < Length; i++)
            {
                this.AssocDevList[i] = new ZAddress16(data[13 + (i * 2)], data[14 + (i * 2)]);
            }

            BuildPacket(CommandType.ZDO_IEEE_ADDR_RSP, data);
        }
    }
}
