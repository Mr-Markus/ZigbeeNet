using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO
{
    /// <summary>
    /// This callback message indicates the ZDO state change.
    /// </summary>
    public class ZDO_STATE_CHANGE_IND : ZToolPacket
    {
        /// <summary>
        /// Specifies the changed ZDO state
        /// </summary>
        public DeviceState Status { get; private set; }

        public ZDO_STATE_CHANGE_IND(byte[] data)
        {
            Status = (DeviceState)data[0];

            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZDO_STATE_CHANGE_IND), data);
        }
    }
}
