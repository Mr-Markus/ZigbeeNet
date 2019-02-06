using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ZigBeeNet.Hardware.TI.CC2531.Network;
using ZigBeeNet.Hardware.TI.CC2531.Packet;
using ZigBeeNet.Logging;

namespace ZigBeeNet.Hardware.TI.CC2531.Implementation
{
    /**
 * Blocking receiver for asynchronous commands.
 */
    public class BlockingCommandReceiver : IAsynchronousCommandListener
    {
        /**
         * The logger.
         */
        private readonly ILog _logger = LogProvider.For<BlockingCommandReceiver>();

        private object _getCommandLockObject;

        private static ManualResetEvent _commandSync = new ManualResetEvent(true);

        /**
         * The command interface.
         */
        private ICommandInterface _commandInterface;
        /**
         * The command ID to wait for.
         */
        private ZToolCMD _commandId;
        /**
         * The command packet.
         */
        private ZToolPacket _commandPacket = null;

        /**
         * The constructor for setting expected command ID and command interface.
         * Sets self as listener for command in command interface.
         *
         * @param commandId the command ID
         * @param commandInterface the command interface
         */
        public BlockingCommandReceiver(ZToolCMD commandId, ICommandInterface commandInterface)
        {
            _getCommandLockObject = new object();

            _commandId = commandId;
            _commandInterface = commandInterface;
            _logger.Trace("Waiting for asynchronous response message {}.", commandId);
            _commandInterface.AddAsynchronousCommandListener(this);
        }

        /**
         * Gets command packet and blocks until the command packet is available or timeoutMillis occurs.
         * 
         * @param timeoutMillis the timeout in milliseconds
         * @return the command packet or null if time out occurs.
         */
        public ZToolPacket GetCommand(long timeoutMillis)
        {
            lock (_getCommandLockObject) {
                long wakeUpTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + timeoutMillis;
                while (_commandPacket == null && wakeUpTime > DateTimeOffset.UtcNow.ToUnixTimeMilliseconds())
                {
                    try
                    {
                        _commandSync.WaitOne(TimeSpan.FromMilliseconds(wakeUpTime - DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()));
                    }
                    catch (Exception e)
                    {
                        _logger.Trace("Blocking command receive timed out.", e);
                    }
                }
            }
            if (_commandPacket == null)
            {
                _logger.Trace("Timeout {} expired and no packet with {} received", timeoutMillis, _commandId);
            }
            Cleanup();
            return _commandPacket;
        }

        /**
         * Clean up asynchronous command listener from command interface.
         */
        public void Cleanup()
        {
            lock (_getCommandLockObject) {
                _commandInterface.RemoveAsynchronousCommandListener(this);
                _commandSync.Reset();
            }
        }

        public void ReceivedAsynchronousCommand(ZToolPacket packet)
        {
            _logger.Trace("Received a packet {} and waiting for {}", packet.CMD, _commandId);
            _logger.Trace("received {} {}", packet.GetType(), packet.ToString());
            //if (packet.isError())
            //{
            //    return;
            //}
            if ((ZToolCMD)packet.CMD.Value != _commandId)
            {
                _logger.Trace("Received unexpected packet: " + packet.GetType().Name);
                return;
            }
            lock(typeof(BlockingCommandReceiver)) {
                _commandPacket = packet;
                _logger.Trace("Received expected response: {}", packet.GetType().Name);
                Cleanup();
            }
        }

        public void ReceivedUnclaimedSynchronousCommandResponse(ZToolPacket packet)
        {
            // Response handler not required
        }
    }
}
