using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol
{
    /// <summary>
    /// Interface for the XBee command
    /// </summary>
    public interface IXBeeCommand
    {
        /// <summary>
        /// Sets the frame ID used to correlate responses with commands.
        /// </summary>
        /// <param name="frameId">The frame ID.</param>
        void SetFrameId(int frameId);

        /// <summary>
        /// Serializes the command to the int[] array for sending to the XBee stick.
        /// </summary>
        /// <returns>Containing the data to send</returns>
        int[] Serialize();
    }
}
