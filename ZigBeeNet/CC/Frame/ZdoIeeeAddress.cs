using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.CC.Packet;
using ZigBeeNet.ZDO;

namespace ZigBeeNet.CC.Frame
{
    public class ZdoIeeeAddress : TiDongleReceivePacket
    {
        public static ZigBeeApsFrame Create(ZToolPacket packet)
        {
            ZigBeeApsFrame apsFrame = new ZigBeeApsFrame();
            apsFrame.Cluster = ZdoCommandType.GetValueByType(ZdoCommandType.CommandType.IEEE_ADDRESS_RESPONSE).ClusterId;
            apsFrame.DestinationEndpoint = 0;
            apsFrame.SourceAddress = (ushort)(packet.Packet[13] | (packet.Packet[14] << 8));
            apsFrame.SourceEndpoint = 0;
            apsFrame.Profile = 0;

            byte[] temp = new byte[packet.Packet.Length - 1];
            Array.Copy(packet.Packet, 3, temp, 0, packet.Packet.Length - 4);

            byte a = temp[12];
            temp[12] = temp[13];
            temp[13] = a;
            temp[0] = 0;
            apsFrame.Payload = temp;

            return apsFrame;
        }
    }
}
