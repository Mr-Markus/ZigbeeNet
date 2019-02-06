using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Network;
using ZigBeeNet;
using ZigBeeNet.Hardware.TI.CC2531.Packet;
using ZigBeeNet.Logging;
using ZigBeeNet.Transport;
using ZigBeeNet.Hardware.TI.CC2531.Util;
using System.Threading;

namespace ZigBeeNet.Hardware.TI.CC2531.Implementation
{
    /// <summary>
    /// ZigBeeSerialInterface is used to startup connection to ZigBee network.
    /// </summary>
    public class CommandInterfaceImpl : IZToolPacketHandler, ICommandInterface
    {
        /**
         * The logger.
         */
        private readonly ILog _logger = LogProvider.For<CommandInterfaceImpl>();

        /**
         * The port interface.
         */
        private IZigBeePort _port;
        /**
         * The packet parser.
         */
        private ZToolPacketParser _parser;
        /**
         * Support parallel processing of different command types.
         * Only one command per command ID can be in process at a time.
         */
        private bool _supportMultipleSynchrounsCommand = false;
        /**
         * Synchronous command listeners.
         */
        private Dictionary<ushort, ISynchronousCommandListener> _synchronousCommandListeners = new Dictionary<ushort, ISynchronousCommandListener>();

        private static ManualResetEventSlim _commandListenerSync = new ManualResetEventSlim(true);

        /**
         * Asynchronous command listeners.
         */
        private List<IAsynchronousCommandListener> _asynchrounsCommandListeners = new List<IAsynchronousCommandListener>();
        /**
         * Timeout times for synchronous command listeners.
         */
        private Dictionary<ISynchronousCommandListener, long> _synchronousCommandListenerTimeouts = new Dictionary<ISynchronousCommandListener, long>();

        /**
         * Constructor for configuring the ZigBee Network connection parameters.
         *
         * @param port the ZigBee transport implementation.
         */
        public CommandInterfaceImpl(IZigBeePort port)
        {
            this._port = port;
        }

        /**
         * Opens connection to ZigBee Network.
         *
         * @return true if connection startup was success.
         */

        public bool Open()
        {
            if (!_port.Open())
            {
                return false;
            }
            _parser = new ZToolPacketParser(_port, this);
            return true;
        }

        /**
         * Closes connection to ZigBee Network.
         */
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

        /* ZToolPacketHandler */

        /**
         * Exception in packet parsing.
         *
         * @param th the exception
         */
        public void Error(Exception th)
        {
            if (th is IOException)
            {
                _logger.ErrorException("IO exception in packet parsing: ", th);
            }
            else
            {
                _logger.ErrorException("Unexpected exception in packet parsing: ", th);
            }
        }

        /**
         * Handle parsed packet.
         *
         * @param packet the packet
         */
        public void HandlePacket(ZToolPacket packet)
        {
            DoubleByte cmdId = packet.CMD;
            switch (cmdId.Msb & 0xE0)
            {
                // Received incoming message which can be either message from dongle or remote device.
                case 0x40:
                    _logger.Debug("<-- Async incomming {Type} ({Packet})", packet.GetType().Name, ByteUtils.ToBase16(packet.Packet));
                    NotifyAsynchronousCommand(packet);
                    break;

                // Received synchronous command response.
                case 0x60:
                    _logger.Debug("<-- Sync incomming {Type} ({Packet})", packet.GetType().Name, ByteUtils.ToBase16(packet.Packet));
                    NotifySynchronousCommand(packet);
                    break;

                default:
                    _logger.Error("Received unknown packet. {Type}", packet.GetType().Name);
                    break;
            }
        }

        /**
         * Send packet to dongle.
         */
        public void SendPacket(ZToolPacket packet)
        {
            _logger.Debug("-->  {Type} ({Packet}) ", packet.GetType().Name, packet);
            byte[] pck = packet.Packet;
            SendRaw(pck);
        }

        /**
         * Cleans expired synchronous command listeners.
         */
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

        /**
         * Sends synchronous command and adds listener.
         *
         * @param packet the command packet
         * @param listener the synchronous command response listener
         * @param timeoutMillis the timeout
         * @throws IOException if IO exception occurs in packet sending
         */
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
                            _logger.Trace("Waiting for other request {Command} to complete", id);
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
                            _logger.Trace("Waiting for other request to complete");
                            _commandListenerSync.Wait(500);
                            CleanExpiredSynchronousCommandListeners();
                        }
                        catch (Exception ignored)
                        {
                        }
                    }
                    _logger.Trace("Put synchronousCommandListeners listener for {Command} command", id);
                    _synchronousCommandListeners[id] = listener;
                }
            }
            _logger.Trace("Sending SynchronousCommand {Packet} ", packet);
            SendPacket(packet);
        }

        /**
         * Sends asynchronous command.
         *
         * @param packet the packet.
         * @throws IOException if IO exception occurs in packet sending.
         */
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

        /**
         * Send raw bytes to output stream.
         *
         * @param packet the byte buffer
         * @throws IOException if IO exception occurs when writing or flushing bytes.
         */
        public void SendRaw(byte[] packet)
        {
            lock (_port)
            {
                _port.Write(packet);
            }
        }

        /**
         * Notifies listeners about synchronous command response.
         *
         * @param packet the received packet
         */
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
                            _logger.Error("Error in incoming asynchronous message processing: {Error}", e);
                        }
                    }
                }

            }
        }

        /**
         * Adds asynchronous command listener.
         *
         * @param listener the listener
         * @return true if listener did not already exist.
         */
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

        /**
         * Removes asynchronous command listener.
         *
         * @param listener the listener
         * @return true if listener did not already exist.
         */
        public bool RemoveAsynchronousCommandListener(IAsynchronousCommandListener listener)
        {
            bool result;
            lock (_asynchrounsCommandListeners)
            {
                result = _asynchrounsCommandListeners.Remove(listener);
            }
            return result;
        }

        /**
         * Notifies listeners about asynchronous message.
         *
         * @param packet the packet containing the message
         */
        private void NotifyAsynchronousCommand(ZToolPacket packet)
        {
            IAsynchronousCommandListener[] listeners;

            lock (_asynchrounsCommandListeners)
            {
                listeners = _asynchrounsCommandListeners.ToArray();
            }

            _logger.Debug("Received Async Cmd: {Packet}", packet);

            foreach (IAsynchronousCommandListener listener in listeners)
            {
                try
                {
                    listener.ReceivedAsynchronousCommand(packet);
                }
                catch (Exception e)
                {
                    _logger.Error("Error in incoming asynchronous message processing: {Error}", e);
                }
            }
        }
    }
}
