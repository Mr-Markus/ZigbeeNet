using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.CC.Packet;

namespace ZigbeeNet.CC.Network
{
    public interface ISynchronousCommandListener
    {
        /// <summary>
        /// Receives command response.
        /// </summary>
        /// <param name="packet">the command packet</param>
        void ReceivedCommandResponse(SerialPacket packet);
    }
}
