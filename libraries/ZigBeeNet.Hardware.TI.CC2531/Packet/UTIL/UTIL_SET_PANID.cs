using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Extensions;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.UTIL
{
    public class UTIL_SET_PANID : ZToolPacket
    {
    /// <name>TI.ZPI1.SYS_SET_PANID.PanID</name>
    /// <summary>PanID.</summary>
    public ushort PanID { get; set; }

    /// <name>TI.ZPI1.SYS_SET_PANID</name>
    /// <summary>Constructor</summary>
    public UTIL_SET_PANID()
    {
    }

    /// <name>TI.ZPI1.SYS_SET_PANID</name>
    /// <summary>Constructor</summary>
    public UTIL_SET_PANID(ushort num1)
    {
        this.PanID = num1;

        byte[] framedata = new byte[1];
        //Inversed because at first glance order wasn't valid
        framedata[0] = this.PanID.GetLSB();
        framedata[1] = this.PanID.GetMSB();

        BuildPacket((ushort)ZToolCMD.UTIL_SET_PANID, framedata);
    }

    public UTIL_SET_PANID(byte[] framedata)
    {
        this.PanID = ByteHelper.ShortFromBytes(framedata[1], framedata[0]);

        BuildPacket((ushort)ZToolCMD.UTIL_SET_PANID, framedata);
    }
}
}
