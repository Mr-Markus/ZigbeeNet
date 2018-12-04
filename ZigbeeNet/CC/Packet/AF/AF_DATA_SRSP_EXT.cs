using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Packet.AF
{
    public class AF_DATA_SRSP_EXT : SynchronousResponse
    {
        /**
         * Response status.
         */
        public int Status { get; private set; }

        /**
         * Constructor which sets frame data.
         *
         * @param framedata the frame data
         */
        public AF_DATA_SRSP_EXT(byte[] framedata)
        {
            Status = framedata[0];
            BuildPacket(CommandType.AF_DATA_SRSP_EXT, framedata);
        }

        public override string ToString()
        {
            return "AF_DATA_SRSP_EXT(Status=" + Status + ')';
        }
    }
}
