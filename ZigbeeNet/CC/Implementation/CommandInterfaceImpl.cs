using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ZigbeeNet.CC.Network;
using ZigBeeNet;
using ZigBeeNet.CC.Network;
using ZigBeeNet.CC.Packet;
using ZigBeeNet.Logging;
using ZigBeeNet.Transport;

namespace ZigbeeNet.CC.Implementation
{
    /**
 * ZigBeeSerialInterface is used to startup connection to ZigBee network.
 *
 * @author <a href="mailto:stefano.lenzi@isti.cnr.it">Stefano "Kismet" Lenzi</a>
 * @author <a href="mailto:tommi.s.e.laukkanen@gmail.com">Tommi S.E. Laukkanen</a>
 * @author <a href="mailto:christopherhattonuk@gmail.com">Chris Hatton</a>
 * @author Chris Jackson
 */
    public class CommandInterfaceImpl : IZToolPacketHandler, ICommandInterface
    {
        /**
         * The logger.
         */
        private readonly ILog _logger = LogProvider.For<CommandInterfaceImpl>();

        /**
         * The port interface.
         */
        private IZigBeePort port;
        /**
         * The packet parser.
         */
        private ZToolPacketParser parser;
        /**
         * Support parallel processing of different command types.
         * Only one command per command ID can be in process at a time.
         */
        private bool supportMultipleSynchrounsCommand = false;
        /**
         * Synchronous command listeners.
         */
        private Hashtable<Short, SynchronousCommandListener> synchronousCommandListeners = new Hashtable<Short, SynchronousCommandListener>();
        /**
         * Asynchronous command listeners.
         */
        private HashSet<AsynchronousCommandListener> asynchrounsCommandListeners = new HashSet<AsynchronousCommandListener>();
        /**
         * Timeout times for synchronous command listeners.
         */
        private HashMap<SynchronousCommandListener, Long> synchronousCommandListenerTimeouts = new HashMap<SynchronousCommandListener, Long>();

        /**
         * Constructor for configuring the ZigBee Network connection parameters.
         *
         * @param port the ZigBee transport implementation.
         */
        public CommandInterfaceImpl(IZigBeePort port)
        {
            this.port = port;
        }

        /**
         * Opens connection to ZigBee Network.
         *
         * @return true if connection startup was success.
         */

        public bool Open()
        {
            if (!port.Open())
            {
                return false;
            }
            parser = new ZToolPacketParser(port, this);
            return true;
        }

        /**
         * Closes connection to ZigBee Network.
         */
        public void Close()
        {
            lock (port)
            {
                if (parser != null)
                {
                    parser.setClosing();
                }
                if (port != null)
                {
                    port.Close();
                }
                if (parser != null)
                {
                    parser.close();
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
            if (th is IOException) {
                _logger.ErrorException("IO exception in packet parsing: ", th);
            } else {
                _logger.ErrorException("Unexpected exception in packet parsing: ", th);
            }
        }

        /**
         * Handle parsed packet.
         *
         * @param packet the packet
         */
    public void handlePacket(SerialPacket packet)
        {
            DoubleByte cmdId = packet.getCMD();
            switch (cmdId.getMsb() & 0xE0)
            {
                // Received incoming message which can be either message from dongle or remote device.
                case 0x40:
                    _logger.Debug("<-- {} ({})", packet.getClass().getSimpleName(), ByteUtils.toBase16(packet.getPacket()));
                    NotifyAsynchronousCommand(packet);
                    break;

                // Received synchronous command response.
                case 0x60:
                    _logger.Debug("<-  {} ({})", packet.getClass().getSimpleName(), ByteUtils.toBase16(packet.getPacket()));
                    NotifySynchronousCommand(packet);
                    break;

                default:
                    _logger.Error("Received unknown packet. {}", packet.getClass().getSimpleName());
                    break;
            }
        }

        /**
         * Send packet to dongle.
         *
         * @param packet the packet
         * @throws IOException if IO exception occurs while sending packet
         */
        @Override
    public void sendPacket(ZToolPacket packet) throws IOException
        {
            logger.debug("->  {} ({}) ", packet.getClass().getSimpleName(), packet);
        int[]
        pck = packet.getPacket();
        sendRaw(pck);
    }

    /**
     * Cleans expired synchronous command listeners.
     */
    private void cleanExpiredSynchronousCommandListeners()
    {
        long now = System.currentTimeMillis();
        ArrayList<Short> expired = new ArrayList<Short>();
        synchronized(synchronousCommandListeners) {
            Iterator<Map.Entry<Short, SynchronousCommandListener>> i = synchronousCommandListeners.entrySet()
                    .iterator();
            while (i.hasNext())
            {
                Map.Entry<Short, SynchronousCommandListener> entry = i.next();

                long expiration = synchronousCommandListenerTimeouts.get(entry.getValue());
                if (expiration != -1L && expiration < now)
                {
                    expired.add(entry.getKey());
                }
            }

            for (Short key : expired)
            {
                synchronousCommandListeners.remove(key);
            }
            synchronousCommandListeners.notifyAll();
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
    @Override
    public void sendSynchronousCommand(ZToolPacket packet, SynchronousCommandListener listener,
            long timeoutMillis) throws IOException
    {
        if (timeoutMillis == -1L) {
            synchronousCommandListenerTimeouts.put(listener, -1L);
        } else {
            long expirationTime = System.currentTimeMillis() + timeoutMillis;
            synchronousCommandListenerTimeouts.put(listener, expirationTime);
        }

        DoubleByte cmdId = packet.getCMD();
        int value = (cmdId.getMsb() & 0xE0);
        if (value != 0x20) {
            throw new IllegalArgumentException(
                    "You are trying to send a non SREQ packet as synchronous command. " + "Evaluated " + value
                            + " instead of " + 0x20 + "\nPacket " + packet.getClass().getName() + "\n" + packet);
        }

        cleanExpiredSynchronousCommandListeners();

        if (supportMultipleSynchrounsCommand) {
            synchronized(synchronousCommandListeners) {
                short id = (short)(cmdId.get16BitValue() & 0x1FFF);
                while (synchronousCommandListeners.get(cmdId) != null)
                {
                    try
                    {
                        logger.trace("Waiting for other request {} to complete", id);
                        synchronousCommandListeners.wait(500);
                        cleanExpiredSynchronousCommandListeners();
                    }
                    catch (InterruptedException ignored)
                    {
                    }
                }
                synchronousCommandListeners.put(id, listener);
            }
        } else {
            synchronized(synchronousCommandListeners) {
                short id = (short)(cmdId.get16BitValue() & 0x1FFF);
                while (!synchronousCommandListeners.isEmpty())
                {
                    try
                    {
                        logger.trace("Waiting for other request to complete");
                        synchronousCommandListeners.wait(500);
                        cleanExpiredSynchronousCommandListeners();
                    }
                    catch (InterruptedException ignored)
                    {
                    }
                }
                logger.trace("Put synchronousCommandListeners listener for {} command", id);
                synchronousCommandListeners.put(id, listener);
            }
        }
        logger.trace("Sending SynchronousCommand {} ", packet);
        sendPacket(packet);
    }

    /**
     * Sends asynchronous command.
     *
     * @param packet the packet.
     * @throws IOException if IO exception occurs in packet sending.
     */
    @Override
    public void sendAsynchronousCommand(ZToolPacket packet) throws IOException
    {
        int value = (packet.getCMD().getMsb() & 0xE0);
        if (value != 0x40) {
            throw new IllegalArgumentException("You are trying to send a non AREQ packet. " + "Evaluated " + value
                    + " instead of " + 0x40 + "\nPacket " + packet.getClass().getName() + "\n" + packet);
        }
        sendPacket(packet);
    }

    /**
     * Send raw bytes to output stream.
     *
     * @param packet the byte buffer
     * @throws IOException if IO exception occurs when writing or flushing bytes.
     */
    public void sendRaw(int[] packet)
    {
        lock (port) {
            for (int i = 0; i < packet.length; i++)
            {
                port.write(packet[i]);
            }
        }
    }

    /**
     * Notifies listeners about synchronous command response.
     *
     * @param packet the received packet
     */
    private void notifySynchronousCommand(SerialPacket packet)
    {
        DoubleByte cmdId = packet.getCMD();
        synchronized(synchronousCommandListeners) {
            short id = (short)(cmdId.get16BitValue() & 0x1FFF);
            SynchronousCommandListener listener = synchronousCommandListeners.get(id);
            if (listener != null)
            {
                listener.receivedCommandResponse(packet);
                synchronousCommandListeners.remove(id);
                synchronousCommandListeners.notifyAll();
            }
            else
            {
                // Notify asynchronous command listeners of unclaimed asynchronous command responses.
                AsynchronousCommandListener[] listeners;
                synchronized(asynchrounsCommandListeners) {
                    listeners = asynchrounsCommandListeners.toArray(new AsynchronousCommandListener[] { });
                }
                for (AsynchronousCommandListener asynchronousCommandListener : listeners)
                {
                    try
                    {
                        asynchronousCommandListener.receivedUnclaimedSynchronousCommandResponse(packet);
                    }
                    catch (Throwable e)
                    {
                        logger.error("Error in incoming asynchronous message processing: ", e);
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
    @Override
    public boolean addAsynchronousCommandListener(AsynchronousCommandListener listener)
    {
        boolean result;
        synchronized(asynchrounsCommandListeners) {
            result = asynchrounsCommandListeners.add(listener);
        }
        return result;
    }

    /**
     * Removes asynchronous command listener.
     *
     * @param listener the listener
     * @return true if listener did not already exist.
     */
    @Override
    public boolean removeAsynchronousCommandListener(AsynchronousCommandListener listener)
    {
        boolean result;
        synchronized(asynchrounsCommandListeners) {
            result = asynchrounsCommandListeners.remove(listener);
        }
        return result;
    }

    /**
     * Notifies listeners about asynchronous message.
     *
     * @param packet the packet containing the message
     */
    private void notifyAsynchronousCommand(ZToolPacket packet)
    {
        AsynchronousCommandListener[] listeners;

        synchronized(asynchrounsCommandListeners) {
            listeners = asynchrounsCommandListeners.toArray(new AsynchronousCommandListener[] { });
        }

        logger.debug("Received Async Cmd: {}", packet);

        for (AsynchronousCommandListener listener : listeners)
        {
            try
            {
                listener.receivedAsynchronousCommand(packet);
            }
            catch (Throwable e)
            {
                logger.error("Error in incoming asynchronous message processing: ", e);
            }
        }
    }
}
}
