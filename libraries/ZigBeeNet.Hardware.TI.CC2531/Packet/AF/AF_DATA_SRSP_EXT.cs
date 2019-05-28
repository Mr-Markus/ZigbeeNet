using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.AF
{
    public class AF_DATA_SRSP_EXT : ZToolPacket
    {
        /// <summary>
        /// Response status.
        /// </summary>
        public int Status { get; private set; }

        /// <summary>
        /// Constructor which sets frame data.
        ///
        /// <param name="framedata">the frame data</param>
        /// </summary>
        public AF_DATA_SRSP_EXT(byte[] framedata)
        {
            Status = framedata[0];
            BuildPacket(new DoubleByte((ushort)ZToolCMD.AF_DATA_SRSP_EXT), framedata);
        }

        public override string ToString()
        {
            return "AF_DATA_SRSP_EXT(Status=" + Status + ')';
        }
    }
}
