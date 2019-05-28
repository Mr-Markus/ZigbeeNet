using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.UTIL
{
    public class UTIL_SET_SECURITY_LEVEL_RESPONSE : ZToolPacket
    {
    /// <name>TI.ZPI1.SYS_SET_SECURITY_LEVEL_RESPONSE.Status</name>
    /// <summary>Status</summary>
    public byte Status { get; private set; }

    /// <name>TI.ZPI1.SYS_SET_SECURITY_LEVEL_RESPONSE</name>
    /// <summary>Constructor</summary>
    public UTIL_SET_SECURITY_LEVEL_RESPONSE()
    {
    }

    public UTIL_SET_SECURITY_LEVEL_RESPONSE(byte cmd_status1)
    {
        this.Status = cmd_status1;

        byte[] framedata = new byte[1];
        framedata[0] = this.Status;

        BuildPacket(new DoubleByte((ushort)ZToolCMD.UTIL_SET_SECURITY_LEVEL_RESPONSE), framedata);
    }

    public UTIL_SET_SECURITY_LEVEL_RESPONSE(byte[] framedata)
    {
        this.Status = framedata[0];

        BuildPacket(new DoubleByte((ushort)ZToolCMD.UTIL_SET_SECURITY_LEVEL_RESPONSE), framedata);
    }

    public override string ToString()
    {
        return "UTIL_SET_SECURITY_LEVEL_RESPONSE{" + "Status=" + Status + '}';
    }
}
}
