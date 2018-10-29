using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Packet.ZDO
{
    public class ZDO_SIMPLE_DESC_RSP : AsynchronousRequest
    {
        private const int MIN_DESC_LEN = 8;

        public ZAddress16 SrcAddr { get; private set; }

        public PacketStatus Status { get; private set; }

        public ZAddress16 NwkAddr { get; private set; }

        public byte Len { get; private set; }

        public byte Endpoint { get; private set; }

        public DoubleByte ProfileId { get; private set; }

        public DoubleByte DeviceId { get; private set; }

        public byte DeviceVersion { get; private set; }

        public byte NumInClusters { get; private set; }

        public DoubleByte[] InClusterList { get; private set; }

        public byte NumOutClusters { get; private set; }

        public DoubleByte[] OutClusterList { get; private set; }

        public ZDO_SIMPLE_DESC_RSP(byte[] framedata)
        {
            SrcAddr = new ZAddress16(framedata[1], framedata[0]);
            Status = (PacketStatus)framedata[2];
            NwkAddr = new ZAddress16(framedata[4], framedata[3]);
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

                BuildPacket(CommandType.ZDO_SIMPLE_DESC_RSP, framedata);
            }
        }
    }
}
