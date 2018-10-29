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

        public AsynchronousRequest(CommandType commandId, byte[] payload) : base(commandId, payload)
        {

        }
    }
}
