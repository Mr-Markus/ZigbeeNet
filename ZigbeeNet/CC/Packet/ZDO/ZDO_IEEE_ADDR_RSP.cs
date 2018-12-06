using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CC.Packet.ZDO
{
    /// <summary>
    /// This callback message is in response to the ZDO IEEE Address Request
    /// </summary>
    public class ZDO_IEEE_ADDR_RSP : AsynchronousRequest
    {
        /// <summary>
        /// This field indicates either SUCCESS or FAILURE
        /// </summary>
        public PacketStatus Status { get; private set; }

        /// <summary>
        /// 64 bit IEEE address of source device
        /// </summary>
        public ZigBeeAddress64 IeeeAddr { get; private set; }

        /// <summary>
        /// Specifies the short network address of responding device
        /// </summary>
        public ZigBeeAddress16 NwkAddr { get; private set; }

        /// <summary>
        /// Specifies the starting index into the list of associated devices for this report
        /// </summary>
        public byte StartIndex { get; private set; }

        /// <summary>
        /// Specifies the number of associated devices
        /// </summary>
        public byte NumAssocDev { get; private set; }

        /// <summary>
        /// Contains the list of network address for associated devices.  
        /// This list can be a partial list if the entire list doesn’t fit into a packet.  
        /// If it is a partial list, the starting index is StartIndex
        /// </summary>
        public List<ZigBeeAddress16> AssocDevList { get; private set; }

        public ZDO_IEEE_ADDR_RSP()
        {
            AssocDevList = new List<ZigBeeAddress16>();
        }

        public ZDO_IEEE_ADDR_RSP(byte[] data)
        {
            Status = (PacketStatus)data[0];
            IeeeAddr = new ZigBeeAddress64(BitConverter.ToUInt64(data, 1));
            NwkAddr = new ZigBeeAddress16(data[10], data[9]);
            StartIndex = data[11];
            NumAssocDev = data[12];

            AssocDevList = new List<ZigBeeAddress16>();
            for (int i = 0; i < Length; i++)
            {
                this.AssocDevList[i] = new ZigBeeAddress16(data[14 + (i * 2)], data[13 + (i * 2)]);
            }

            BuildPacket(CommandType.ZDO_IEEE_ADDR_RSP, data);
        }
    }
}
