using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CC.Packet
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
