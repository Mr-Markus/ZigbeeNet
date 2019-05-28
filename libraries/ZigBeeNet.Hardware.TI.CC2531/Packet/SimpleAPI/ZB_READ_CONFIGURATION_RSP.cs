using System;
using System.Collections.Generic;
using System.Text;
using static ZigBeeNet.Hardware.TI.CC2531.Packet.SimpleAPI.ZB_WRITE_CONFIGURATION;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SimpleAPI
{
    /// <summary>
    /// This command is used to get a configuration property from non-volatile memory
    /// </summary>
    public class ZB_READ_CONFIGURATION_RSP : ZToolPacket
    {
        /// <summary>
        /// This field indicates either SUCCESS (0) or FAILURE (1)
        /// </summary>
        public PacketStatus Status { get; private set; }

        /// <summary>
        /// Specifies the Identifier for the configuration property
        /// </summary>
        public CONFIG_ID ConfigId { get; private set; }

        /// <summary>
        /// Specifies the size of the Value buffer in bytes
        /// </summary>
        public byte Len { get; private set; }

        /// <summary>
        /// Buffer to hold the configuration property
        /// </summary>
        public byte[] Value { get; private set; }

        public ZB_READ_CONFIGURATION_RSP(byte[] framedata)
        {
            Status = (PacketStatus)framedata[0];
            ConfigId = (CONFIG_ID)framedata[1];
            Len = framedata[2];
            Value = new byte[framedata.Length - 3];

            for (int i = 0; i < Value.Length; i++)
            {
                this.Value[i] = framedata[i + 3];
            }

            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZB_READ_CONFIGURATION_RSP), framedata);
        }
    }
}
