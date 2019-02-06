using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Packet;
using ZigBeeNet.ZDO;

namespace ZigBeeNet.Hardware.TI.CC2531.Frame
{
    public class ZdoPowerDescriptor : TiDongleReceivePacket
    {

        public static ZigBeeApsFrame Create(ZToolPacket packet)
        {
            ZigBeeApsFrame apsFrame = new ZigBeeApsFrame();
            apsFrame.Cluster = ZdoCommandType.GetValueByType(ZdoCommandType.CommandType.POWER_DESCRIPTOR_RESPONSE).ClusterId;
            apsFrame.DestinationEndpoint = 0;
            apsFrame.SourceAddress = (ushort)(packet.Packet[4] | (packet.Packet[5] << 8));
            apsFrame.SourceEndpoint = 0;
            apsFrame.Profile = 0;

            apsFrame.Payload = new byte[packet.Packet.Length - 7];

            Array.Copy(packet.Packet, 6, apsFrame.Payload, 0, packet.Packet.Length - 7);

            return apsFrame;
        }
    }
}
