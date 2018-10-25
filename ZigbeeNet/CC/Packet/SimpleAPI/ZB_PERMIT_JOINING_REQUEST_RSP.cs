using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.CC.Extensions;

namespace ZigbeeNet.CC.Packet.SimpleAPI
{
    public class ZB_PERMIT_JOINING_REQUEST_RSP : SynchronousRequest
    {
        public byte Status { get; set; }

        public ZB_PERMIT_JOINING_REQUEST_RSP()
        {

        }

        public ZB_PERMIT_JOINING_REQUEST_RSP(byte[] data)
        {
            Status = data[0];
            
            BuildPacket(CommandType.ZB_PERMIT_JOINING_REQUEST_RSP, data);
        }
    }
}
