using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ZigBeeNet.Extensions;
using ZigBeeNet.Hardware.TI.CC2531.Util;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.AF
{
    /// <summary>
    /// This command is used by the tester to build and send a message through AF layer
    /// </summary>
    public class AF_DATA_REQUEST : ZToolPacket
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
        /// This parameter shall be present if, and only if,
        /// the DstAddrMode parameter has a value of 0x02 or 0x03 and, if present, 
        /// shall be either the number of the individual endpoint of the entity
        /// to which the ADSDU is beeing transferred ot the broadcast endpoint (0xff)
        /// </summary>
        public byte DstEndpoint { get; private set; }

        /// <summary>
        /// The individual endpoint of the entity from which
        /// the ASDU is beeing trasferred
        /// </summary>
        public byte SrcEndpoint { get; private set; }

        /// <summary>
        /// The individual device address or group
        /// address of the entity to which the ASDU is being transferred
        /// </summary>
        public ZToolAddress16 DstAddr { get; private set; }

        /// <summary>
        /// Length of the data.
        /// </summary>
        public byte Len { get; private set; }

        /// <summary>
        /// Transmit options bit mask according to the following defines from AF.h: 
        /// bit 1: sets ‘Wildcard Profile ID’; 
        /// bit 2: Use NWK key
        /// bit 4: turns on/off ‘APS ACK’; 
        /// bit 5 sets ‘discover route’; 
        /// bit 6 sets ‘APS security’; bit 7 sets ‘skip routing’. 
        /// </summary>
        public byte Options { get; private set; }

        /// <summary>
        /// The distance, in hops, that a transmitted frame will be
        /// allowed to travel through the network
        /// </summary>
        public byte Radius { get; private set; }

        /// <summary>
        /// Specifies the transaction sequence number of the message
        /// </summary>
        public byte TransId { get; private set; }

        public AF_DATA_REQUEST(ushort nwkDstAddr, byte dstEndpoint, byte srcEndpoint, ushort clusterId, byte transId, byte options, byte radius, byte[] data)
        {
            if (data.Length > 128)
            {
                throw new InvalidDataException("Data can only be up to 128 bytes");
            }

            byte[] framedata = new byte[data.Length + 10];
            framedata[0] = nwkDstAddr.GetByte(0);
            framedata[1] = nwkDstAddr.GetByte(1);
            framedata[2] = dstEndpoint;
            framedata[3] = srcEndpoint;
            framedata[4] = clusterId.GetByte(0);
            framedata[5] = clusterId.GetByte(1);
            framedata[6] = transId;
            framedata[7] = options;
            framedata[8] = radius;
            framedata[9] = (byte)data.Length;
            for (int i = 0; i < data.Length; i++)
            {
                framedata[10 + i] = data[i];
            }

            BuildPacket(new DoubleByte((ushort)ZToolCMD.AF_DATA_REQUEST), framedata);
        }
    }
}
