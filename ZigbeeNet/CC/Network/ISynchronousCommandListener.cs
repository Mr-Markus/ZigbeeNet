using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.CC.Packet;

namespace ZigBeeNet.CC.Network
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
