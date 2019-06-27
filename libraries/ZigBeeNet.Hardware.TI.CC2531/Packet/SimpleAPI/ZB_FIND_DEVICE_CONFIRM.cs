using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SimpleAPI
{
    /// <summary>
    /// This callback is called by the ZigBee stack when a find device operation completes
    /// </summary>
    public class ZB_FIND_DEVICE_CONFIRM : ZToolPacket
    {
        /// <summary>
        /// The type of search that was performed
        /// </summary>
        public byte SearchType { get; private set; }

        /// <summary>
        /// Value that the search was executed on
        /// </summary>
        public DoubleByte SearchKey { get; private set; }

        /// <summary>
        /// The result of the search
        /// </summary>
        public byte[] Result { get; private set; }

        public ZB_FIND_DEVICE_CONFIRM(byte[] framedata)
        {
            SearchType = framedata[0];
            SearchKey = new DoubleByte(framedata[1], framedata[2]);
            Result = new byte[8];

            for (int i = 0; i < 8; i++)
            {
                this.Result[i] = framedata[i + 3];
            }

            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZB_FIND_DEVICE_CONFIRM), framedata);
        }
    }
}
