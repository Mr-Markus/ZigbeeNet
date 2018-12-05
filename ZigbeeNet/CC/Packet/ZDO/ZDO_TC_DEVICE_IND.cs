using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CC.Packet.ZDO
{
    /// <summary>
    /// This command will be received by the ZNP coordinator as an indication that a new device has joined the network. 
    /// On your ZAP you don't need to take any action related to this command it will all depend if you want to use this 
    /// information in your application. If you have no use for it then do nothing when this command is received.
    /// </summary>
    public class ZDO_TC_DEVICE_IND : AsynchronousRequest
    {
        public ZigBeeAddress64 IeeeAddr { get; private set; }

        public ZigbeeAddress16 NwkAddr { get; private set; }

        public ZigbeeAddress16 SrcAddr { get; private set; }

        public ZDO_TC_DEVICE_IND(byte[] framedata)
        {
            SrcAddr = new ZigbeeAddress16(framedata[1], framedata[0]);
            byte[] bytes = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                bytes[i] = framedata[9 - i];
            }
            IeeeAddr = new ZigBeeAddress64(bytes);
            NwkAddr = new ZigbeeAddress16(framedata[11], framedata[10]);

            BuildPacket(CommandType.ZDO_TC_DEVICE_IND, framedata);
        }
    }
}
