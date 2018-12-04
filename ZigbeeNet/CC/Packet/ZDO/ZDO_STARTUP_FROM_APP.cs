﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Packet.ZDO
{
    /**
     * This command starts the device in the network.
     *
     * @author alfiva
     */
    public class ZDO_STARTUP_FROM_APP : SynchronousRequest
    {
        public ZDO_STARTUP_FROM_APP()
        {
        }

        /**
         * Creates the ZDO_STARTUP_FROM_APP packet
         *
         * @param start_delay Specifies the time delay before the device starts in milliseconds.
         */
        public ZDO_STARTUP_FROM_APP(byte start_delay)
        {
            byte[] framedata = new byte[2];
            framedata[0] = (byte)(start_delay & 0xff);
            framedata[1] = (byte)((start_delay & 0xff) >> 8);

            BuildPacket(CommandType.ZDO_STARTUP_FROM_APP, framedata);
        }
    }
}
