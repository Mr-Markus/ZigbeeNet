using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Util;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    public class ZDO_ACTIVE_EP_RSP : ZToolPacket
    {
        public ZToolAddress16 SrcAddr { get; private set; }

        public PacketStatus Status { get; private set; }

        public ZToolAddress16 NwkAddr { get; private set; }

        public byte ActiveEPCount { get; private set; }

        public byte[] ActiveEpList { get; private set; }

        public ZDO_ACTIVE_EP_RSP(byte[] framedata)
        {
            SrcAddr = new ZToolAddress16(framedata[1], framedata[0]);
            Status = (PacketStatus)framedata[2];
            NwkAddr = new ZToolAddress16(framedata[4], framedata[3]);

            ActiveEPCount = framedata[5];
            ActiveEpList = new byte[ActiveEPCount];
            for (int i = 0; i < ActiveEpList.Length; i++)
            {
                ActiveEpList[i] = framedata[i + 6];
            }

            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZDO_ACTIVE_EP_RSP), framedata);
        }
    }
}
