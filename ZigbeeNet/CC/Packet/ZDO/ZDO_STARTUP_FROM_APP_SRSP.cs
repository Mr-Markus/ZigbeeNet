﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Packet.ZDO
{
    public class ZDO_STARTUP_FROM_APP_SRSP : SynchronousResponse
    {
        /// <name>TI.ZPI1.ZDO_STARTUP_FROM_APP_SRSP.Status</name>
        /// <summary>Status</summary>
        public byte Status;

        /// <name>TI.ZPI1.ZDO_STARTUP_FROM_APP_SRSP</name>
        /// <summary>Constructor</summary>
        public ZDO_STARTUP_FROM_APP_SRSP()
        {
        }

        public ZDO_STARTUP_FROM_APP_SRSP(byte[] framedata)
        {
            Status = framedata[0];
            BuildPacket(CommandType.ZDO_STARTUP_FROM_APP_SRSP, framedata);
        }


        public override string ToString()
        {
            return "ZDO_STARTUP_FROM_APP_SRSP{" +
                    "Status=" + Status +
                    '}';
        }

        public class STATUS_TYPE
        {
            public const byte RESTORED_NETWORK = 0;
            public const byte NEW_NETWORK = 1;
            public const byte NOT_STARTED = 2;
        }
    }
}
