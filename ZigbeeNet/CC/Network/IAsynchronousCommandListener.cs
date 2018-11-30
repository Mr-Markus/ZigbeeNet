using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.CC.Packet;

namespace ZigbeeNet.CC.Network
{
    public interface IAsynchronousCommandListener
    {
        /// <summary>
        /// Called when asynchronous command has been received.
        /// </summary>
        /// <param name="packet"></param>
        void ReceivedAsynchronousCommand(AsynchronousRequest packet);

        /// <summary>
        /// Called when unclaimed synchronous command response has been received.
        /// </summary>
        /// <param name="packet"></param>
        void ReceivedUnclaimedSynchronousCommandResponse(SerialPacket packet);
    }
}
