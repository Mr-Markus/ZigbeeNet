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

        public SynchronousResponse(CommandType commandId, byte[] payload) : base(commandId, payload)
        {

        }
    }
}
