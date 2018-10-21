using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC
{
    public class SynchronousRequest : SerialPacket
    {
        public SynchronousRequest(SubSystem subSystem, byte commandId, byte[] payload) : base(MessageType.SREQ, subSystem, commandId, payload)
        {
        }
    }

    public class SynchronousResponse : SerialPacket
    {
        public SynchronousResponse(SubSystem subSystem, byte commandId, byte[] payload): base(MessageType.SRSP, subSystem, commandId, payload)
        {
            
        }
    }

    public class AsynchronousRequest : SerialPacket
    {
        public AsynchronousRequest(SubSystem subSystem, byte commandId, byte[] payload): base(MessageType.AREQ, subSystem, commandId, payload)
        {
            
        }
    }
}
