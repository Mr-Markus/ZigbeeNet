﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Packet.ZDO
{
    public class ZDO_SIMPLE_DESC_REQ_SRSP : SynchronousResponse
    {
        public PacketStatus Status { get; private set; }

        public ZDO_SIMPLE_DESC_REQ_SRSP(byte[] framedata)
        {
            Status = (PacketStatus)framedata[0];

            BuildPacket(CommandType.ZDO_SIMPLE_DESC_REQ_SRSP, framedata);
        }
    }
}