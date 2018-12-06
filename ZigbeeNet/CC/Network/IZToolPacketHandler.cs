using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.CC.Packet;

namespace ZigbeeNet.CC.Network
{
    public interface IZToolPacketHandler
    {
        /**
     * A callback used by {@link ZToolPacketParser} for notifying that a new packet is arrived
     * <b>NOTE</b>: Bad packet would not be notified
     *
     * @param response the new {@link ZToolPacket} parsed by {@link ZToolPacketParser}
     */
        void HandlePacket(SerialPacket response);

        /**
         * A callback used by {@link ZToolPacketParser} for notifying that an {@link Exception} has<br>
         * been thrown
         *
         * @param th
         */
        void Error(Exception th);
    }
}
