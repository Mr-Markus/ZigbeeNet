﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Packet.SimpleAPI
{
    /// <summary>
    /// This command starts the ZigBee stack.  When the ZigBee stack starts, the device reads configuration parameters 
    /// from nonvolatile memory and the device joins its network.  The ZigBee stack calls the zb_StartConfirm callback function 
    /// when the startup process completes. After the start request process completes, the device is ready to send, receive, and route network traffic
    /// </summary>
    public class ZB_START_REQUEST : SynchronousRequest
    {
        public ZB_START_REQUEST()
        {
            BuildPacket(CommandType.ZB_START_REQUEST, new byte[0]);
        }
    }
}
