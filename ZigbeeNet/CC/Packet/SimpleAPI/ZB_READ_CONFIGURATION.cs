﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Packet.SimpleAPI
{
    /// <summary>
    /// This command is used to get a configuration property from non-volatile memory
    /// </summary>
    public class ZB_READ_CONFIGURATION : SynchronousRequest
    {
        /// <summary>
        /// Specifies the Identifier for the configuration property
        /// </summary>
        public CONFIG_ID ConfigId { get; private set; }

        public ZB_READ_CONFIGURATION(CONFIG_ID configId)
        {
            ConfigId = configId;

            byte[] framedata = new byte[] { (byte)configId };

            BuildPacket(CommandType.ZB_READ_CONFIGURATION, framedata);
        }
    }
}
