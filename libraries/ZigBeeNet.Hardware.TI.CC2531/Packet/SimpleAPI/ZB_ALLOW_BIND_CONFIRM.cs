using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Util;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SimpleAPI
{
    /// <summary>
    /// This callback indicates another device attempted to bind to this device
    /// </summary>
    public class ZB_ALLOW_BIND_CONFIRM : ZToolPacket
    {
        /// <summary>
        /// Contains the address of the device attempted to bind to this device
        /// </summary>
        public ZToolAddress16 Source { get; private set; }

        public ZB_ALLOW_BIND_CONFIRM(byte[] framedata)
        {
            Source = new ZToolAddress16(framedata[1], framedata[0]);


            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZB_ALLOW_BIND_CONFIRM), framedata);
        }
    }
}
