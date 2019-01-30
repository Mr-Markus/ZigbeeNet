using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.CC.Packet;

namespace ZigBeeNet.Hardware.CC.Network
{
    public interface IAsynchronousCommandListener
    {
        /// <summary>
        /// Called when asynchronous command has been received.
        /// </summary>
        /// <param name="packet"></param>
        void ReceivedAsynchronousCommand(ZToolPacket packet);

        /// <summary>
        /// Called when unclaimed synchronous command response has been received.
        /// </summary>
        /// <param name="packet"></param>
        void ReceivedUnclaimedSynchronousCommandResponse(ZToolPacket packet);
    }
}
