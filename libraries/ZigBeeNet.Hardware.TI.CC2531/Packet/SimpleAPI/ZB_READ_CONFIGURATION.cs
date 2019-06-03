using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SimpleAPI
{
    /// <summary>
    /// This command is used to get a configuration property from non-volatile memory
    /// </summary>
    public class ZB_READ_CONFIGURATION : ZToolPacket
    {
        /// <summary>
        /// Specifies the Identifier for the configuration property
        /// </summary>
        public ZB_WRITE_CONFIGURATION.CONFIG_ID ConfigId { get; private set; }

        public ZB_READ_CONFIGURATION(ZB_WRITE_CONFIGURATION.CONFIG_ID configId)
        {
            ConfigId = configId;

            byte[] framedata = new byte[] { (byte)configId };

            BuildPacket(new DoubleByte((ushort)ZToolCMD.ZB_READ_CONFIGURATION), framedata);
        }
    }
}
