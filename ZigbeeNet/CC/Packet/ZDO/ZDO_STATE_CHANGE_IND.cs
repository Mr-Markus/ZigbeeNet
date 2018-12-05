using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CC.Packet.ZDO
{
    /// <summary>
    /// This callback message indicates the ZDO state change.
    /// </summary>
    public class ZDO_STATE_CHANGE_IND : AsynchronousRequest
    {
        /// <summary>
        /// Specifies the changed ZDO state
        /// </summary>
        public DeviceState Status { get; private set; }

        public ZDO_STATE_CHANGE_IND(byte[] data)
        {
            Status = (DeviceState)data[0];

            BuildPacket(CommandType.ZDO_STATE_CHANGE_IND, data);
        }
    }
}
