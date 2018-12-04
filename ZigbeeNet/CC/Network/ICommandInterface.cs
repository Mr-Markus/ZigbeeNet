using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.CC.Packet;

namespace ZigbeeNet.CC.Network
{
    /// <summary>
    /// The command interface for sending and receiving ZNP serial interface commands
    /// </summary>
    public interface ICommandInterface
    {
        /// <summary>
        /// Opens the command interface.
        /// </summary>
        /// <returns>true if open was successful.</returns>
        bool Open();

        /// <summary>
        /// Closes command interface.
        /// </summary>
        void Close();

        /// <summary>
        /// Sends packet.
        /// </summary>
        /// <param name="packet">the packet</param>
        void SendPacket(SerialPacket packet);

        /// <summary>
        /// Sends synchronous command packet.
        /// </summary>
        /// <param name="packet">the command packet</param>
        /// <param name="listener">the synchronous command listener</param>
        /// <param name="timeoutMillis">the timeout in milliseconds.</param>
        void SendSynchronousCommand(SerialPacket packet, ISynchronousCommandListener listener, long timeoutMillis);

        /// <summary>
        /// Sends asynchronous command packet.
        /// </summary>
        /// <param name="packet">the command packet</param>
        void SendAsynchronousCommand(SerialPacket packet);

        /// <summary>
        /// Sends raw command packet
        /// </summary>
        /// <param name="packet">the raw packet</param>
        void SendRaw(byte[] packet);

        /// <summary>
        /// Adds asynchronous command listener
        /// </summary>
        /// <param name="listener">the asynchronous command listener</param>
        /// <returns>true if listener add was successful</returns>
        bool AddAsynchronousCommandListener(IAsynchronousCommandListener listener);

        /// <summary>
        /// Removes asynchronous command listener
        /// </summary>
        /// <param name="listener">the asynchronous command listener</param>
        /// <returns>true if listener remove was successful</returns>
        bool RemoveAsynchronousCommandListener(IAsynchronousCommandListener listener);
    }
}
