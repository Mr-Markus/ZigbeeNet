using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SimpleAPI
{
    /// <summary>
    /// This command puts the device into the Allow Binding Mode for a given period of time.  
    /// A peer device can establish a binding to a device in the Allow Binding Mode by calling 
    /// zb_BindDevice with a destination address of NULL. 
    /// </summary>
    public class ZB_ALLOW_BIND : ZToolPacket
    {
        /// <summary>
        /// The number of seconds to remain in the allow binding mode.  
        /// Valid values range from 1 through 65. 
        /// If 0, the Allow Bind mode will be set false without timeout. 
        /// If greater than 64, the Allow Bind mode will be true
        /// </summary>
        public byte Timeout { get; private set; }

        public ZB_ALLOW_BIND(byte timeout)
        {
            Timeout = timeout;

            byte[] framedata = new byte[1];
            framedata[0] = Timeout;

            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZB_ALLOW_BIND), framedata);
        }
    }
}
