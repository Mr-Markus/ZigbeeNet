using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Network;
using ZigBeeNet;
using ZigBeeNet.Hardware.TI.CC2531.Packet;
using ZigBeeNet.Transport;
using ZigBeeNet.Hardware.TI.CC2531.Util;
using System.Threading;
using Serilog;

namespace ZigBeeNet.Hardware.TI.CC2531.Implementation
{
    /// <summary>
    /// ZigBeeSerialInterface is used to startup connection to ZigBee network.
    /// </summary>
    public class CommandInterfaceImpl : IZToolPacketHandler, ICommandInterface
    {

        /// <summary>
        /// The port interface.
        /// </summary>
        private IZigBeePort _port;
        /// <summary>
        /// The packet parser.
        /// </summary>
        private ZToolPacketParser _parser;
        /// <summary>
        /// Support parallel processing of different command types.
        /// Only one command per command ID can be in process at a time.
        /// </summary>
        private bool _supportMultipleSynchrounsCommand = false;
        /// <summary>
        /// Synchronous command listeners.
        /// </summary>
        private Dictionary<ushort, ISynchronousCommandListener> _synchronousCommandListeners = new Dictionary<ushort, ISynchronousCommandListener>();

        private static ManualResetEventSlim _commandListenerSync = new ManualResetEventSlim(true);

        /// <summary>
        /// Asynchronous command listeners.
        /// </summary>
        private List<IAsynchronousCommandListener> _asynchrounsCommandListeners = new List<IAsynchronousCommandListener>();
        /// <summary>
        /// Timeout times for synchronous command listeners.
        /// </summary>
        private Dictionary<ISynchronousCommandListener, long> _synchronousCommandListenerTimeouts = new Dictionary<ISynchronousCommandListener, long>();

        /// <summary>
        /// Constructor for configuring the ZigBee Network connection parameters.
        ///
        /// <param name="port">the ZigBee transport implementation.</param>
        /// </summary>
        public CommandInterfaceImpl(IZigBeePort port)
        {
            this._port = port;
        }

        /// <summary>
        /// Opens connection to ZigBee Network.
        ///
        /// <returns>true</returns>
        /// </summary>

        public bool Open()
        {
            if (!_port.Open())
            {
                return false;
            }
            _parser = new ZToolPacketParser(_port, this);
            return true;
        }

        /// <summary>
        /// Closes connection to ZigBee Network.
        /// </summary>
        public void Close()
        {
            lock (_port)
            {
                if (_parser != null)
                {
                    _parser.Close();
                }
                if (_port != null)
                {
                    _port.Close();
                }
            }
        }

        //// ZToolPacketHandler /// </summary>

        /// <summary>
        /// Exception in packet parsing.
        ///
        /// <param name="th">the exception</param>
        /// </summary>
        public void Error(Exception th)
        {
            if (th is IOException)
            {
                Log.Error("IO exception in packet parsing: ", th);
            }
            else
            {
                Log.Error("Unexpected exception in packet parsing: ", th);
            }
        }

        /// <summary>
        /// Handle parsed packet.
        ///
        /// <param name="packet">the packet</param>
        /// </summary>
        public void HandlePacket(ZToolPacket packet)
        {
            DoubleByte cmdId = packet.CMD;
            switch (cmdId.Msb & 0xE0)
            {
                // Received incoming message which can be either message from dongle or remote device.
                case 0x40:
                    Log.Debug("<-- Async incomming {Type} ({Packet})", packet.GetType().Name, ByteUtils.ToBase16(packet.Packet));
                    NotifyAsynchronousCommand(packet);
                    break;

                // Received synchronous command response.
                case 0x60:
                    Log.Debug("<-- Sync incomming {Type} ({Packet})", packet.GetType().Name, ByteUtils.ToBase16(packet.Packet));
                    NotifySynchronousCommand(packet);
                    break;

                default:
                    Log.Error("Received unknown packet. {Type}", packet.GetType().Name);
                    break;
            }
        }

        /// <summary>
        /// Send packet to dongle.
        /// </summary>
        public void SendPacket(ZToolPacket packet)
        {
            Log.Debug("-->  {Type} ({Packet}) ", packet.GetType().Name, packet);
            byte[] pck = packet.Packet;
            SendRaw(pck);
        }

        /// <summary>
        /// Cleans expired synchronous command listeners.
        /// </summary>
        private void CleanExpiredSynchronousCommandListeners()
        {
            long now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            List<ushort> expired = new List<ushort>();
            lock (_synchronousCommandListeners)
            {
                var i = _synchronousCommandListeners.GetEnumerator();
                while (i.MoveNext())
                {
                    var entry = i.Current;
                    long expiration = _synchronousCommandListenerTimeouts[entry.Value];
                    if (expiration != -1L && expiration < now)
                    {
                        expired.Add(entry.Key);
                    }
                }

                foreach (ushort key in expired)
                {
                    _synchronousCommandListeners.Remove(key);
                }
                _commandListenerSync.Set();
            }
        }

        /// <summary>
        /// Sends synchronous command and adds listener.
        ///
        /// <param name="packet">the command packet</param>
        /// <param name="listener">the synchronous command response listener</param>
        /// <param name="timeoutMillis">the timeout</param>
        /// @throws IOException if IO exception occurs in packet sending
        /// </summary>
        public void SendSynchronousCommand(ZToolPacket packet, ISynchronousCommandListener listener, long timeoutMillis)
        {
            if (timeoutMillis == -1L)
            {
                _synchronousCommandListenerTimeouts[listener] = -1L;
            }
            else
            {
                long expirationTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + timeoutMillis;
                _synchronousCommandListenerTimeouts[listener] = expirationTime;
            }

            DoubleByte cmdId = packet.CMD;
            int value = (cmdId.Msb & 0xE0);
            if (value != 0x20)
            {
                throw new ArgumentException("You are trying to send a non SREQ packet as synchronous command. " + "Evaluated " + value
                                + " instead of " + 0x20 + "\nPacket " + packet.GetType().Name + "\n" + packet);
            }

            CleanExpiredSynchronousCommandListeners();

            if (_supportMultipleSynchrounsCommand)
            {
                lock (_synchronousCommandListeners)
                {
                    ushort id = (ushort)(cmdId.Value & 0x1FFF);
                    while (_synchronousCommandListeners.ContainsKey(cmdId.Value))
                    {
                        try
                        {
                            Log.Verbose("Waiting for other request {Command} to complete", id);
                            _commandListenerSync.Wait(500);
                            CleanExpiredSynchronousCommandListeners();
                        }
                        catch (Exception ignored)
                        {
                        }
                    }
                    _synchronousCommandListeners[id] = listener;
                }
            }
            else
            {
                lock (_synchronousCommandListeners)
                {
                    ushort id = (ushort)(cmdId.Value & 0x1FFF);
                    while (!(_synchronousCommandListeners.Count == 0))
                    {
                        try
                        {
                            Log.Verbose("Waiting for other request to complete");
                            _commandListenerSync.Wait(500);
                            CleanExpiredSynchronousCommandListeners();
                        }
                        catch (Exception ignored)
                        {
                        }
                    }
                    Log.Verbose("Put synchronousCommandListeners listener for {Command} command", id);
                    _synchronousCommandListeners[id] = listener;
                }
            }
            Log.Verbose("Sending SynchronousCommand {Packet} ", packet);
            SendPacket(packet);
        }

        /// <summary>
        /// Sends asynchronous command.
        ///
        /// <param name="packet">the packet.</param>
        /// @throws IOException if IO exception occurs in packet sending.
        /// </summary>
        public void SendAsynchronousCommand(ZToolPacket packet)
        {
            byte value = (byte)(packet.CMD.Msb & 0xE0);
            if (value != 0x40)
            {
                throw new ArgumentException("You are trying to send a non AREQ packet. " + "Evaluated " + value
                        + " instead of " + 0x40 + "\nPacket " + packet.GetType().Name + "\n" + packet);
            }
            SendPacket(packet);
        }

        /// <summary>
        /// Send raw bytes to output stream.
        ///
        /// <param name="packet">the byte buffer</param>
        /// @throws IOException if IO exception occurs when writing or flushing bytes.
        /// </summary>
        public void SendRaw(byte[] packet)
        {
            lock (_port)
            {
                _port.Write(packet);
            }
        }

        /// <summary>
        /// Notifies listeners about synchronous command response.
        ///
        /// <param name="packet">the received packet</param>
        /// </summary>
        private void NotifySynchronousCommand(ZToolPacket packet)
        {
            DoubleByte cmdId = packet.CMD;
            lock (_synchronousCommandListeners)
            {
                ushort id = (ushort)(cmdId.Value & 0x1FFF);
                _synchronousCommandListeners.TryGetValue(id, out ISynchronousCommandListener listener);
                if (listener != null)
                {
                    listener.ReceivedCommandResponse(packet);
                    _synchronousCommandListeners.Remove(id);
                    _commandListenerSync.Set();
                }
                else
                {
                    // Notify asynchronous command listeners of unclaimed asynchronous command responses.
                    IAsynchronousCommandListener[] listeners;
                    lock (_asynchrounsCommandListeners)
                    {
                        listeners = _asynchrounsCommandListeners.ToArray();
                    }
                    foreach (IAsynchronousCommandListener asynchronousCommandListener in listeners)
                    {
                        try
                        {
                            asynchronousCommandListener.ReceivedUnclaimedSynchronousCommandResponse(packet);
                        }
                        catch (Exception e)
                        {
                            Log.Error("Error in incoming asynchronous message processing: {Error}", e);
                        }
                    }
                }

            }
        }

        /// <summary>
        /// Adds asynchronous command listener.
        ///
        /// <param name="listener">the listener</param>
        /// <returns>true if listener did not already exist.</returns>
        /// </summary>
        public bool AddAsynchronousCommandListener(IAsynchronousCommandListener listener)
        {
            lock (_asynchrounsCommandListeners)
            {
                if (_asynchrounsCommandListeners.Contains(listener) == false)
                {
                    _asynchrounsCommandListeners.Add(listener);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Removes asynchronous command listener.
        ///
        /// <param name="listener">the listener</param>
        /// <returns>true if listener did not already exist.</returns>
        /// </summary>
        public bool RemoveAsynchronousCommandListener(IAsynchronousCommandListener listener)
        {
            bool result;
            lock (_asynchrounsCommandListeners)
            {
                result = _asynchrounsCommandListeners.Remove(listener);
            }
            return result;
        }

        /// <summary>
        /// Notifies listeners about asynchronous message.
        ///
        /// <param name="packet">the packet containing the message</param>
        /// </summary>
        private void NotifyAsynchronousCommand(ZToolPacket packet)
        {
            IAsynchronousCommandListener[] listeners;

            lock (_asynchrounsCommandListeners)
            {
                listeners = _asynchrounsCommandListeners.ToArray();
            }

            Log.Debug("Received Async Cmd: {Packet}", packet);

            foreach (IAsynchronousCommandListener listener in listeners)
            {
                try
                {
                    listener.ReceivedAsynchronousCommand(packet);
                }
                catch (Exception e)
                {
                    Log.Error("Error in incoming asynchronous message processing: {Error}", e);
                }
            }
        }
    }
}
