using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Packet
{
    public class AsynchronousRequest : SerialPacket
    {
        public AsynchronousRequest()
        {

        }
        public AsynchronousRequest(SubSystem subSystem, byte commandId, byte[] payload) : base(MessageType.AREQ, subSystem, commandId, payload)
        {

        }
    }
}
