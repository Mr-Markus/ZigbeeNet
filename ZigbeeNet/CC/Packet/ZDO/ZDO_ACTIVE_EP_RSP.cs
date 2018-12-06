using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CC.Packet.ZDO
{
    public class ZDO_ACTIVE_EP_RSP : AsynchronousRequest
    {
        public ZigBeeAddress16 SrcAddr { get; private set; }

        public PacketStatus Status { get; private set; }

        public ZigBeeAddress16 NwkAddr { get; private set; }

        public byte ActiveEPCount { get; private set; }

        public byte[] ActiveEpList { get; private set; }

        public ZDO_ACTIVE_EP_RSP(byte[] framedata)
        {
            SrcAddr = new ZigBeeAddress16(framedata[1], framedata[0]);
            Status = (PacketStatus)framedata[2];
            NwkAddr = new ZigBeeAddress16(framedata[4], framedata[3]);

            ActiveEPCount = framedata[5];
            ActiveEpList = new byte[ActiveEPCount];
            for (int i = 0; i < ActiveEpList.Length; i++)
            {
                ActiveEpList[i] = framedata[i + 6];
            }

            BuildPacket(CommandType.ZDO_ACTIVE_EP_RSP, framedata);
        }
    }
}
