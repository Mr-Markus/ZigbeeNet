using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Packet.ZDO
{
    /// <summary>
    /// This callback indicates the ZDO End Device Announce
    /// </summary>
    public class ZDO_END_DEVICE_ANNCE_IND : AsynchronousRequest
    {
        /// <summary>
        /// Source address of the message
        /// </summary>
        public ZAddress16 SrcAddr { get; set; }

        /// <summary>
        /// Specifies the device’s short address
        /// </summary>
        public ZAddress16 NwkAddr { get; set; }

        /// <summary>
        /// Specifies the 64 bit IEEE address of source device
        /// </summary>
        public ZAddress64 IEEEAddr { get; set; }

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
        public byte Capabilities { get; set; }

        public ZDO_END_DEVICE_ANNCE_IND()
        {

        }
        public ZDO_END_DEVICE_ANNCE_IND(byte[] data)
        {
            SrcAddr = new ZAddress16(data[0], data[1]);
            NwkAddr = new ZAddress16(data[2], data[3]);
            IEEEAddr = new ZAddress64(BitConverter.ToUInt64(data, 4));
            Capabilities = data[12];

            BuildPacket(CommandType.ZDO_END_DEVICE_ANNCE_IND, data);
        }
    }
}
