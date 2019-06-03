using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Util;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    /// <summary>
    /// This callback message is in response to the ZDO IEEE Address Request
    /// </summary>
    public class ZDO_IEEE_ADDR_RSP : ZToolPacket
    {
        /// <summary>
        /// This field indicates either SUCCESS or FAILURE
        /// </summary>
        public PacketStatus Status { get; private set; }

        /// <summary>
        /// 64 bit IEEE address of source device
        /// </summary>
        public ZToolAddress64 IeeeAddr { get; private set; }

        /// <summary>
        /// Specifies the short network address of responding device
        /// </summary>
        public ZToolAddress16 NwkAddr { get; private set; }

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
        public List<ZToolAddress16> AssocDevList { get; private set; }

        public ZDO_IEEE_ADDR_RSP()
        {
            AssocDevList = new List<ZToolAddress16>();
        }

        public ZDO_IEEE_ADDR_RSP(byte[] data)
        {
            Status = (PacketStatus)data[0];
            IeeeAddr = new ZToolAddress64(BitConverter.ToInt64(data, 1));
            NwkAddr = new ZToolAddress16(data[10], data[9]);
            StartIndex = data[11];
            NumAssocDev = data[12];

            AssocDevList = new List<ZToolAddress16>();
            for (int i = 0; i < AssocDevList.Count; i++)
            {
                this.AssocDevList[i] = new ZToolAddress16(data[14 + (i * 2)], data[13 + (i * 2)]);
            }

            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZDO_IEEE_ADDR_RSP), data);
        }
    }
}
