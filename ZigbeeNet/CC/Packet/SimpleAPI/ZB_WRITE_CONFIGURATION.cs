using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Packet.SimpleAPI
{
    /// <summary>
    /// This command is used to write a Configuration Property to non-volatile memory
    /// </summary>
    public class ZB_WRITE_CONFIGURATION : SynchronousRequest
    {
        public ZB_WRITE_CONFIGURATION(CONFIG_ID configId, byte[] nvItemValue)
        {
            byte[] framedata = new byte[nvItemValue.Length + 2];
            framedata[0] = (byte)configId;
            framedata[1] = (byte)nvItemValue.Length;

            for (int i = 0; i < nvItemValue.Length; i++)
            {
                framedata[i + 2] = nvItemValue[i];
            }

            BuildPacket(CommandType.ZB_WRITE_CONFIGURATION, framedata);
        }
    }
}
