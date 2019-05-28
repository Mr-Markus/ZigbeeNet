using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SimpleAPI
{
    /// <summary>
    /// This command retrieves a Device Information Property
    /// </summary>
    public class ZB_GET_DEVICE_INFO : ZToolPacket
    {
        /// <summary>
        /// The Identifier for the device information
        /// </summary>
        public DEV_INFO_TYPE Param { get; private set; }

        public ZB_GET_DEVICE_INFO(DEV_INFO_TYPE param)
        {
            Param = param;
            byte[] framedata = new byte[] { (byte)param };

            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZB_GET_DEVICE_INFO), framedata);
        }    
        
        public enum DEV_INFO_TYPE : byte
        {
            CHANNEL = 0x05,
            EXT_PAN_ID = 0x07,
            IEEE_ADDR = 0x01,
            PAN_ID = 0x06,
            PARENT_IEEE_ADDR = 0x04,
            PARENT_SHORT_ADDR = 0x03,
            SHORT_ADDR = 0x02,
            STATE = 0x00
        }
    }
}
