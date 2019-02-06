using System;
using System.Collections.Generic;
using System.Text;
using static ZigBeeNet.Hardware.TI.CC2531.Packet.SimpleAPI.ZB_GET_DEVICE_INFO;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SimpleAPI
{
    public class ZB_GET_DEVICE_INFO_RSP : ZToolPacket
    {
        public DEV_INFO_TYPE Param { get; private set; }

        public byte[] Value { get; private set; }

        public ZB_GET_DEVICE_INFO_RSP(byte[] framedata)
        {
            Param = (DEV_INFO_TYPE)framedata[0];
            this.Value = new byte[8];

            for (int i = 0; i < 8; i++)
            {
                this.Value[i] = framedata[i + 1];
            }

            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZB_GET_DEVICE_INFO_RSP), framedata);
        }
    }
}
