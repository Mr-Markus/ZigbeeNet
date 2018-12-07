using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CC.Packet.ZDO
{
    /// <summary>
    /// This callback indicates the ZDO End Device Announce
    /// </summary>
    public class ZDO_END_DEVICE_ANNCE_IND : ZToolPacket
    {
        /// <summary>
        /// Source address of the message
        /// </summary>
        public ZigBeeAddress16 SrcAddr { get; private set; }

        /// <summary>
        /// Specifies the device’s short address
        /// </summary>
        public ZigBeeAddress16 NwkAddr { get; private set; }

        /// <summary>
        /// Specifies the 64 bit IEEE address of source device
        /// </summary>
        public ZigBeeAddress64 IEEEAddr { get; private set; }

        /// <summary>
        /// Specifies the MAC capabilities of the device. 
        ///    Bit: 0 – Alternate PAN Coordinator        
        ///         1 – Device type: 1- ZigBee Router; 0 – End Device        
        ///         2 – Power Source: 1 Main powered         
        ///         3 – Receiver on when Idle        
        ///         4 – Reserved          
        ///         5 – Reserved         
        ///         6 – Security capability        
        ///         7 – Reserved 
        /// </summary>
        public byte Capabilities { get; private set; }

        public ZDO_END_DEVICE_ANNCE_IND(byte[] framedata)
        {
            SrcAddr = new ZigBeeAddress16(framedata[1], framedata[0]);
            NwkAddr = new ZigBeeAddress16(framedata[3], framedata[2]);
            IEEEAddr = new ZigBeeAddress64(BitConverter.ToUInt64(framedata, 4));
            Capabilities = framedata[12];

            BuildPacket(new DoubleByte(ZToolCMD.ZDO_END_DEVICE_ANNCE_IND), framedata);
        }
    }
}
