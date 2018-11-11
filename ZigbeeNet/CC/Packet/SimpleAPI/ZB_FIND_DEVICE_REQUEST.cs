using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Packet.SimpleAPI
{
    /// <summary>
    /// This command is used to determine the short address for a device in the network.  
    /// The device initiating a call to zb_FindDeviceRequest and the device being discovered 
    /// must both be a member of the same network.  When the search is complete, 
    /// the zv_FindDeviceConfirm callback function is called
    /// </summary>
    public class ZB_FIND_DEVICE_REQUEST : SynchronousRequest
    {
        /// <summary>
        /// Specifies the value to search on
        /// </summary>
        public ZigbeeAddress64 SearchKey { get; private set; }

        public ZB_FIND_DEVICE_REQUEST(ZigbeeAddress64 searchKey)
        {
            SearchKey = searchKey;

            BuildPacket(CommandType.ZB_FIND_DEVICE_REQUEST, searchKey.ToByteArray());
        }
    }
}
