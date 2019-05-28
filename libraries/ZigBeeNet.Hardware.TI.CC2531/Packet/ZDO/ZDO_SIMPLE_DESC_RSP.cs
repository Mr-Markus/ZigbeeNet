using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Util;
using ZigBeeNet.ZCL;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    /// <summary>
    /// This callback message is in response to the ZDO Simple Descriptor Request 
    /// </summary>
    public class ZDO_SIMPLE_DESC_RSP : ZToolPacket
    {
        private const int MIN_DESC_LEN = 8;

        /// <summary>
        /// Specifies the message’s source network address
        /// </summary>
        public ZToolAddress16 SrcAddr { get; private set; }

        /// <summary>
        /// This field indicates either SUCCESS or FAILURE
        /// </summary>
        public PacketStatus Status { get; private set; }

        /// <summary>
        /// Specifies Device’s short address that this response describes
        /// </summary>
        public ZToolAddress16 NwkAddr { get; private set; }

        /// <summary>
        /// Specifies the length of the simple descriptor 
        /// </summary>
        public byte Len { get; private set; }

        /// <summary>
        /// Specifies Endpoint of the device 
        /// </summary>
        public byte Endpoint { get; private set; }

        /// <summary>
        /// The profile Id for this endpoint
        /// </summary>
        public DoubleByte ProfileId { get; private set; }

        /// <summary>
        /// The Device Description Id for this endpoint
        /// </summary>
        public DoubleByte DeviceId { get; private set; }

        /// <summary>
        /// Defined as the following format 0 – Version 1.00 0x01-0x0F – Reserve
        /// </summary>
        public byte DeviceVersion { get; private set; }

        /// <summary>
        /// The number of input clusters in the InClusterList
        /// </summary>
        public byte NumInClusters { get; private set; }

        /// <summary>
        /// List of input cluster Id’s supported
        /// </summary>
        public DoubleByte[] InClusterList { get; private set; }

        /// <summary>
        /// The number of output clusters in the OutClusterLis
        /// </summary>
        public byte NumOutClusters { get; private set; }

        /// <summary>
        /// List of output cluster Id’s supported
        /// </summary>
        public DoubleByte[] OutClusterList { get; private set; }

        public ZDO_SIMPLE_DESC_RSP(byte[] framedata)
        {
            SrcAddr = new ZToolAddress16(framedata[1], framedata[0]);
            Status = (PacketStatus)framedata[2];
            NwkAddr = new ZToolAddress16(framedata[4], framedata[3]);
            Len = framedata[5];

            if (Len >= MIN_DESC_LEN)
            {
                Endpoint = framedata[6];
                ProfileId = new DoubleByte(framedata[8], framedata[7]);
                DeviceId = new DoubleByte(framedata[10], framedata[9]);
                DeviceVersion = framedata[11];

                NumInClusters = framedata[12];
                InClusterList = new DoubleByte[NumInClusters];

                for (int i = 0; i < NumInClusters; i++)
                {
                    InClusterList[i] = new DoubleByte(framedata[(i * 2) + 14], framedata[(i * 2) + 13]);
                }

                NumOutClusters = framedata[((NumInClusters) * 2) + 13];
                OutClusterList = new DoubleByte[NumOutClusters];

                for (int i = 0; i < NumOutClusters; i++)
                {
                    OutClusterList[i] = new DoubleByte(framedata[(i * 2) + ((NumInClusters) * 2) + 15],
                            framedata[(i * 2) + ((NumInClusters) * 2) + 14]);
                }

                BuildPacket(new DoubleByte((ushort)ZToolCMD.ZDO_SIMPLE_DESC_RSP), framedata);
            }
        }
    }
}
