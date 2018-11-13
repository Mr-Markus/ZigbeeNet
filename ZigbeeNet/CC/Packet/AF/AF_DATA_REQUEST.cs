using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ZigbeeNet.CC.Packet.AF
{
    /// <summary>
    /// This command is used by the tester to build and send a message through AF layer
    /// </summary>
    public class AF_DATA_REQUEST : SynchronousRequest
    {
        /// <summary>
        /// Specifies the cluster ID 
        /// </summary>
        public DoubleByte ClusterId { get; private set; }

        /// <summary>
        /// 0-128 bytes data 
        /// </summary>
        public byte[] Data { get; private set; }

        /// <summary>
        /// Endpoint of the destination device 
        /// </summary>
        public byte DstEndpoint { get; private set; }

        /// <summary>
        /// Endpoint of the source device 
        /// </summary>
        public byte SrcEndpoint { get; private set; }

        /// <summary>
        /// Short address of the destination device 
        /// </summary>
        public ZigbeeAddress16 DstAddr { get; private set; }

        /// <summary>
        /// Length of the data.
        /// </summary>
        public byte Len { get; private set; }

        /// <summary>
        /// Transmit options bit mask according to the following defines from AF.h: 
        /// bit 1: sets ‘Wildcard Profile ID’; 
        /// bit 4: turns on/off ‘APS ACK’; 
        /// bit 5 sets ‘discover route’; 
        /// bit 6 sets ‘APS security’; bit 7 sets ‘skip routing’. 
        /// </summary>
        public byte Options { get; private set; }

        /// <summary>
        /// Specifies the number of hops allowed delivering the message (see AF_DEFAULT_RADIUS.)
        /// </summary>
        public byte Radius { get; private set; }

        /// <summary>
        /// Specifies the transaction sequence number of the message
        /// </summary>
        public byte TransId { get; private set; }

        public AF_DATA_REQUEST(ZigbeeAddress16 nwkDstAddr, byte dstEndpoint, byte srcEndpoint, DoubleByte clusterId, byte transId, byte options, byte radius, byte[] data)
        {
            if (data.Length > 128)
            {
                throw new InvalidDataException("Data can only be up to 128 bytes");
            }

            byte[] framedata = new byte[data.Length + 10];
            framedata[0] = nwkDstAddr.DoubleByte.Lsb;
            framedata[1] = nwkDstAddr.DoubleByte.Msb;
            framedata[2] = dstEndpoint;
            framedata[3] = srcEndpoint;
            framedata[4] = clusterId.Lsb;
            framedata[5] = clusterId.Msb;
            framedata[6] = transId;
            framedata[7] = options;
            framedata[8] = radius;
            framedata[9] = (byte)data.Length;
            for (int i = 0; i < data.Length; i++)
            {
                framedata[10 + i] = data[i];
            }

            BuildPacket(CommandType.AF_DATA_REQUEST, framedata);
        }
    }
}
