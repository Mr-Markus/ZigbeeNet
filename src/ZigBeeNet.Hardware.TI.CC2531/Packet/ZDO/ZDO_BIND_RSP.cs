using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Util;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    public class ZDO_BIND_RSP : ZToolPacket
    {
        public ZToolAddress16 SrcAddr { get; private  set; }

        public PacketStatus Status { get; private set; }


        public ZDO_BIND_RSP(byte[] data)
        {
            SrcAddr = new ZToolAddress16(data[1], data[0]);
            Status = (PacketStatus)data[2];

            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZDO_BIND_RSP), data);
        }
    }
}
