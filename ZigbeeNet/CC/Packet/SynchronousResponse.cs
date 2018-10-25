using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Packet
{
    public class SynchronousResponse : SerialPacket
    {
        public SynchronousResponse()
        {

        }
        public SynchronousResponse(SubSystem subSystem, byte commandId, byte[] payload) : base(MessageType.SRSP, subSystem, commandId, payload)
        {

        }
    }
}
