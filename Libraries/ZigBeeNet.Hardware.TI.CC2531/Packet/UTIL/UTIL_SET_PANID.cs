using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.UTIL
{
    public class UTIL_SET_PANID : ZToolPacket
    {
    /// <name>TI.ZPI1.SYS_SET_PANID.PanID</name>
    /// <summary>PanID.</summary>
    public DoubleByte PanID { get; set; }

    /// <name>TI.ZPI1.SYS_SET_PANID</name>
    /// <summary>Constructor</summary>
    public UTIL_SET_PANID()
    {
    }

    /// <name>TI.ZPI1.SYS_SET_PANID</name>
    /// <summary>Constructor</summary>
    public UTIL_SET_PANID(DoubleByte num1)
    {
        this.PanID = num1;

        byte[] framedata = new byte[1];
        framedata[0] = this.PanID.Msb;
        framedata[1] = this.PanID.Lsb;

        BuildPacket(new DoubleByte((ushort)ZToolCMD.UTIL_SET_PANID), framedata);
    }

    public UTIL_SET_PANID(byte[] framedata)
    {
        this.PanID = new DoubleByte(framedata[1], framedata[0]);

        BuildPacket(new DoubleByte((ushort)ZToolCMD.UTIL_SET_PANID), framedata);
    }
}
}
