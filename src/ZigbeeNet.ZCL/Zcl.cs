using BinarySerialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ZigbeeNet.ZCL
{
    public class Zcl
    {
        public void Parse(byte[] zclBuf, ushort clusterId, Action callback = null)
        {
            ZclPacket zclPacket = new ZclPacket();

            zclPacket.Parse(zclBuf, callback);

            if(zclPacket.Header.FrameControl.Type == FrameType.Global)
            {
                //TODO: Parse payload to ZclGlobalCommand
            } else {

            }
        }
    }
}
