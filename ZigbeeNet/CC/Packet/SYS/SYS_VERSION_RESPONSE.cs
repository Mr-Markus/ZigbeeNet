using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CC.Packet.SYS
{
    public class SYS_VERSION_RESPONSE : SynchronousResponse /* implements /*IRESPONSE,ISYTEM */
    {
    /// <name>TI.ZPI2.SYS_VERSION_RESPONSE.HwRev</name>
    /// <summary>Hardware revision</summary>
    public byte HwRev;
    /// <name>TI.ZPI2.SYS_VERSION_RESPONSE.MajorRel</name>
    /// <summary>Major release number</summary>
    public byte MajorRel;
    /// <name>TI.ZPI2.SYS_VERSION_RESPONSE.MinorRel</name>
    /// <summary>Minor release number</summary>
    public byte MinorRel;
    /// <name>TI.ZPI2.SYS_VERSION_RESPONSE.Product</name>
    /// <summary>Product PROFILE_ID_HOME_AUTOMATION</summary>
    public byte Product;
    /// <name>TI.ZPI2.SYS_VERSION_RESPONSE.TransportRev</name>
    /// <summary>Transport revision</summary>
    public byte TransportRev;

    /// <name>TI.ZPI2.SYS_VERSION_RESPONSE</name>
    /// <summary>Constructor</summary>
    public SYS_VERSION_RESPONSE()
    {
    }

    /// <name>TI.ZPI2.SYS_VERSION_RESPONSE</name>
    /// <summary>Constructor</summary>
    public SYS_VERSION_RESPONSE(byte[] framedata)
    {
        TransportRev = framedata[0];
        Product = framedata[1];
        MajorRel = framedata[2];
        MinorRel = framedata[3];
        HwRev = framedata[4];

        BuildPacket(CommandType.SYS_VERSION_RESPONSE, framedata);
    }
}
}
