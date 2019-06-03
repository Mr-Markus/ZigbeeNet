using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Util;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SimpleAPI
{
    /// <summary>
    /// This command is used to determine the short address for a device in the network.  
    /// The device initiating a call to zb_FindDeviceRequest and the device being discovered 
    /// must both be a member of the same network.  When the search is complete, 
    /// the zv_FindDeviceConfirm callback function is called
    /// </summary>
    public class ZB_FIND_DEVICE_REQUEST : ZToolPacket
    {
        /// <summary>
        /// Specifies the value to search on
        /// </summary>
        public ulong SearchKey { get; private set; }

        public ZB_FIND_DEVICE_REQUEST(ulong searchKey)
        {
            SearchKey = searchKey;
            byte[] framedata = ByteUtils.ConvertLongtoMultiByte(SearchKey);
            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZB_FIND_DEVICE_REQUEST), framedata);
        }
    }
}
