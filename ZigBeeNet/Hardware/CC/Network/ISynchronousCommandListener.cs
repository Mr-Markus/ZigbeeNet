using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.CC.Packet;

namespace ZigBeeNet.Hardware.CC.Network
{
    public interface ISynchronousCommandListener
    {
        /// <summary>
        /// Receives command response.
        /// </summary>
        /// <param name="packet">the command packet</param>
        void ReceivedCommandResponse(ZToolPacket packet);
    }
}
