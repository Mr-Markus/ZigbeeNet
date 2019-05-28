using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Packet;
using ZigBeeNet.ZDO;
using System.Linq;

namespace ZigBeeNet.Hardware.TI.CC2531.Frame
{
    public class ZdoIeeeAddress : TiDongleReceivePacket
    {
        public static ZigBeeApsFrame Create(ZToolPacket packet)
        {
            ZigBeeApsFrame apsFrame = new ZigBeeApsFrame();
            apsFrame.Cluster = ZdoCommandType.GetValueByType(ZdoCommandType.CommandType.IEEE_ADDRESS_RESPONSE).ClusterId;
            apsFrame.DestinationEndpoint = 0;
            apsFrame.SourceAddress = BitConverter.ToUInt16(packet.Packet, 13);
            apsFrame.SourceEndpoint = 0;
            apsFrame.Profile = 0;

            apsFrame.Payload = packet.Packet.Skip(3).Take(packet.Packet[1] - 1).ToArray();

            return apsFrame;
        }
    }
}
