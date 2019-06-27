using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Transport
{
    /// <summary>
    /// Interface for a generic port used for the ZigBee API. The stack will call the
    /// interface to open and close the port, and to read and write data
    /// </summary>
    public interface IZigBeePort
    {
        /// <summary>
        /// Open the port.
        /// </summary>
        /// <returns>true if port was opened successfully.</returns>
        bool Open();

        /// <summary>
        /// Open the port with the specified baud rate.
        /// 
        /// This method allows the transport to override the baud rate if required - for example
        /// when entering a bootloader that may operate at a different speed to the coordinator.
        /// </summary>
        /// <param name="baudrate">the speed to use when opening the port</param>
        /// <returns>true if port was opened successfully.</returns>
        bool Open(int baudrate);

        /// <summary>
        /// Close the port. Closing the port should abort any read and write operations to allow a clean closure of the port.
        /// </summary>
        void Close();

        /// <summary>
        /// Write a data bytes to serial port. This should be non-blocking.
        /// </summary>
        /// <param name="value"></param>
        void Write(byte[] value);

        /// <summary>
        /// Read a value from the port. This should block until a byte is available.
        /// </summary>
        /// <returns>the data byte (integer) read from the port</returns>
        byte? Read();

        /// <summary>
        /// Read a value from the port. This will block until a byte is available or the timeout period is reached.
        /// </summary>
        /// <param name="timeout">the timeout in milliseconds to wait. If not data is received, -1 is returned.</param>
        /// <returns>the data byte read from the port</returns>
        byte? Read(int timeout);

        /// <summary>
        /// Purge all data currently in the receive buffer
        /// </summary>
        void PurgeRxBuffer();
    }
}
