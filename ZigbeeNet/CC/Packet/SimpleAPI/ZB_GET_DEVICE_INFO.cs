using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Packet.SimpleAPI
{
    /// <summary>
    /// This command retrieves a Device Information Property
    /// </summary>
    public class ZB_GET_DEVICE_INFO : SynchronousRequest
    {
        /// <summary>
        /// The Identifier for the device information
        /// </summary>
        public DEV_INFO_TYPE Param { get; private set; }

        public ZB_GET_DEVICE_INFO(DEV_INFO_TYPE param)
        {
            Param = param;
            byte[] framedata = new byte[] { (byte)param };

            BuildPacket(CommandType.ZB_GET_DEVICE_INFO, framedata);
        }

        
    }
}
