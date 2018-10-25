using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Packet
{
    public class SynchronousRequest : SerialPacket
    {
        public SynchronousRequest()
        {

        }

        public SynchronousRequest(SubSystem subSystem, byte commandId, byte[] payload) : base(MessageType.SREQ, subSystem, commandId, payload)
        {
        }
    }
}
