using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.AF
{
    /// <summary>
    /// This command is sent by the device to the user after it receives a data request.
    /// </summary>
    public class AF_DATA_CONFIRM : ZToolPacket
    {
        /// <summary>
        /// Endpoint of the device 
        /// </summary>
        public byte Endpoint { get; private set; }

        /// <summary>
        /// Status is either Success (0) or Failure (1). 
        /// </summary>
        public PacketStatus Status { get; private set; }

        /// <summary>
        /// Specified the transaction sequence number of the message 
        /// </summary>
        public byte TransId { get; private set; }

        public AF_DATA_CONFIRM(byte[] framedata)
        {
            Status = (PacketStatus)framedata[0];
            Endpoint = framedata[1];
            TransId = framedata[2];

            BuildPacket(new DoubleByte((ushort)ZToolCMD.AF_DATA_CONFIRM), framedata);
        }
    }
}
