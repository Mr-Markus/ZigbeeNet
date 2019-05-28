using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Packet;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet
{
    public interface IZToolPacketHandler
    {
        /// <summary>
        /// A callback used by <see cref="ZToolPacketParser"> for notifying that a new packet is arrived
        /// <b>NOTE</b>: Bad packet would not be notified
        ///
        /// <param name="response">the new <see cref="ZToolPacket"> parsed by <see cref="ZToolPacketParser"></param>
        /// </summary>
        void HandlePacket(ZToolPacket response);

        /// <summary>
        /// A callback used by <see cref="ZToolPacketParser"> for notifying that an <see cref="Exception"> has<br>
        /// been thrown
        ///
        /// <param name="th"></param>
        /// </summary>
        void Error(Exception th);
    }
}
