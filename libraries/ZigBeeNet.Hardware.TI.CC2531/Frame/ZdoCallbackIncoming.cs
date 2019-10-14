using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Packet;
using System.Linq;

namespace ZigBeeNet.Hardware.TI.CC2531.Frame
{
    public class ZdoCallbackIncoming : TiDongleReceivePacket
    {
        public static ZigBeeApsFrame Create(ZToolPacket packet)
        {
            ZigBeeApsFrame apsFrame = new ZigBeeApsFrame();
            apsFrame.Cluster = (ushort)(packet.Packet[7] | (packet.Packet[8] << 8));
            apsFrame.DestinationAddress = (ushort)(packet.Packet[11] | (packet.Packet[12] << 8));
            apsFrame.DestinationEndpoint = 0;
            apsFrame.SourceAddress = (ushort)(packet.Packet[4] | (packet.Packet[5] << 8));
            apsFrame.SourceEndpoint = 0;
            apsFrame.Profile = 0;

            apsFrame.Payload = packet.Packet.Skip(12).ToArray();
            apsFrame.Payload = apsFrame.Payload.Take(apsFrame.Payload.Count() - 1).ToArray();

            return apsFrame;
        }
    }
}
