using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.UTIL
{
    public class UTIL_SET_CHANNELS_RESPONSE : ZToolPacket 
    {
    /// <name>TI.ZPI1.SYS_SET_CHANNELS_RESPONSE.Status</name>
    /// <summary>Status</summary>
    public byte Status { get; private set; }

    public UTIL_SET_CHANNELS_RESPONSE(byte[] framedata)
    {
        this.Status = framedata[0];

        BuildPacket(new DoubleByte((ushort)ZToolCMD.UTIL_SET_CHANNELS_RESPONSE), framedata);
    }

    public override string ToString()
    {
        return "UTIL_SET_CHANNELS_RESPONSE{" + "Status=" + Status + '}';
    }
}
}
