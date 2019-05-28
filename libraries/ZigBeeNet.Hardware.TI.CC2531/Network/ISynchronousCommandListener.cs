using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Packet;

namespace ZigBeeNet.Hardware.TI.CC2531.Network
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
