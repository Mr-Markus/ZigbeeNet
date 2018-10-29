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

        public SynchronousRequest(CommandType commandId, byte[] payload) : base(commandId, payload)
        {

        }
    }
}
