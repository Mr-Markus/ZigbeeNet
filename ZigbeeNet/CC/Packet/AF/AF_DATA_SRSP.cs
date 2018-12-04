using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Packet.AF
{
    public class AF_DATA_SRSP : SynchronousResponse
    {
    /// <name>TI.ZPI2.AF_DATA_SRSP.Status</name>
    /// <summary>Status</summary>
    public int Status;

    /// <name>TI.ZPI2.AF_DATA_SRSP</name>
    /// <summary>Constructor</summary>
    public AF_DATA_SRSP()
    {
    }

    public AF_DATA_SRSP(byte[] framedata)
    {
        Status = framedata[0];
        BuildPacket(CommandType.AF_DATA_SRSP, framedata);
    }

    public override string ToString()
    {
        return "AF_DATA_SRSP(Status=" + Status + ')';
    }
}
}
