using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.CC.Extensions;

namespace ZigBeeNet.Hardware.CC.Packet.SimpleAPI
{
    public class ZB_PERMIT_JOINING_REQUEST_RSP : ZToolPacket
    {
        public byte Status { get; private set; }

        public ZB_PERMIT_JOINING_REQUEST_RSP(byte[] data)
        {
            Status = data[0];
            
            BuildPacket(new DoubleByte(ZToolCMD.ZB_PERMIT_JOINING_REQUEST_RSP), data);
        }
    }
}
