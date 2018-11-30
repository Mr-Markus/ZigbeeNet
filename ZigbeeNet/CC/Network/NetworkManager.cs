using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.CC.Packet.SimpleAPI;
using ZigbeeNet.Logging;

namespace ZigbeeNet.CC.Network
{
    public class NetworkManager
    {
        private readonly ILog _logger = LogProvider.For<NetworkManager>();

        private const int DEFAULT_TIMEOUT = 8000;
        private const string TIMEOUT_KEY = "zigbee.driver.cc2531.timeout";

        private const int RESET_TIMEOUT_DEFAULT = 5000;
        private const string TESET_TIMEOUT_KEY = "tigbee.driver.cc2531.reset.timeout";

        private const int STARTUP_TIMEOUT_DEFAULT = 10000;
        private const string STARTUP_TIMEOUT_KEY = "zigbee.driver.cc2531.startup.timeout";

        private const int RESEND_TIMEOUT_DEFAULT = 1000;

        private const int RESEND_MAX_RETRY_DEFAULT = 3;

        private const bool RESEND_ONLY_EXCEPTION_DEFAULT = true;
        private const string RESEND_ONLY_EXCEPTION_KEY = "zigbee.driver.cc2531.resend.exceptionally";

        private const byte BOOTLOADER_MAGIC_BYTE_DEFAULT = 0xef;

        private readonly int Timeout;
        private readonly int ResetTimeout;
        private readonly int StartupTimeout;
        private readonly bool ResendOnlyException;

        private byte BOOTLOADER_MAGIC_BYTE = BOOTLOADER_MAGIC_BYTE_DEFAULT;
        private int RESEND_TIMEOUT = RESEND_TIMEOUT_DEFAULT;
        private int RESEND_MAX_RETRY = RESEND_MAX_RETRY_DEFAULT;

        private const byte ZNP_DEFAULT_CHANNEL = 0x08;

        private const byte ZNP_CHANNEL_MASK0 = 0x00;
        private const byte ZNP_CHANNEL_MASK1 = 0xf8;
        private const byte ZNP_CHANNEL_MASK2 = 0xff;
        private const byte ZNP_CHANNEL_MASK3 = 0x0f;

        private const byte ZNP_CHANNEL_DEFAULT0 = 0x00;
        private const byte ZNP_CHANNEL_DEFAULT1 = 0x08;
        private const byte ZNP_CHANNEL_DEFAULT2 = 0x00;
        private const byte ZNP_CHANNEL_DEFAULT3 = 0x00;

        // Dongle startup options
        private const ulong STARTOPT_CLEAR_CONFIG = 0x00000001;
        private const ulong STARTOPT_CLEAR_STATE = 0x00000002;

        // The dongle will automatically pickup a random, not conflicting PAN ID
        private const ushort AUTO_PANID = (ushort)0xffff;

        private ICommandInterface _commandInterface;
        private DriverStatus _state;
        private NetworkMode _mode;
        private ushort _pan = AUTO_PANID;
        private byte _channel = ZNP_DEFAULT_CHANNEL;
        private ZigbeeAddress64 _extendedPanId; // do not initialize to use dongle defaults (the IEEE address)
        private byte[] networkKey; // 16 byte network key
        private bool distributeNetworkKey = true; // distribute network key in clear (be careful)
        private int securityMode = 1;

        private byte[] _ep;
        private byte[] prof;
        private byte[] dev;
        private byte[] ver;
        private ushort[][] inp;
        private ushort[][] output;

        private NetworkStateListener _announceListenerFilter = new NetworkStateListener();

        private List<IApplicationFrameworkMessageListener> _messageListeners = new List<IApplicationFrameworkMessageListener>();
        private AFMessageListenerFilter _afMessageListenerFilter;

        public NetworkManager(ICommandInterface _commandInterface, NetworkMode _mode, long timeout)
        {
            //_announceListenerFilter.OnStateChanged += (object sender, DriverStatus status) => SetState(status);
            _afMessageListenerFilter = new AFMessageListenerFilter(_messageListeners);

            _mode = _mode;
            _commandInterface = _commandInterface;

            Timeout = DEFAULT_TIMEOUT;
            ResetTimeout = RESET_TIMEOUT_DEFAULT;
            StartupTimeout = STARTUP_TIMEOUT_DEFAULT;
            ResendOnlyException = RESEND_ONLY_EXCEPTION_DEFAULT;

            _state = DriverStatus.CLOSED;
        }

        /// <summary>
        /// Different hardware may use a different "Magic Number" to skip waiting in the bootloader. Otherwise
        /// the dongle may wait in the bootloader for 60 seconds after it's powered on or reset.
        /// This method allows the user to change the magic number which may be required when using different
        /// 
        /// This method allows the user to change the magic number which may be required when using different
        /// sticks.
        /// </summary>
        public void SetMagicNumber(byte magicNumber)
        {
            BOOTLOADER_MAGIC_BYTE = magicNumber;
        }

        /**
         * Set timeout and retry count
         *
         * @param retries the maximum number of retries to perform
         * @param timeout the maximum timeout between retries
         */
        public void SetRetryConfiguration(int retries, int timeout)
        {
            RESEND_MAX_RETRY = retries;
            if (RESEND_MAX_RETRY < 1 || RESEND_MAX_RETRY > 5)
            {
                RESEND_MAX_RETRY = RESEND_MAX_RETRY_DEFAULT;
            }

            RESEND_TIMEOUT = timeout;
            if (RESEND_TIMEOUT < 0 || RESEND_TIMEOUT > 5000)
            {
                RESEND_TIMEOUT = RESEND_TIMEOUT_DEFAULT;
            }
        }

        public string Startup()
        {
            // Called when the network is first started
            if (_state != DriverStatus.CLOSED)
            {
                throw new InvalidOperationException("Driver already opened, current status is:" + _state);
            }

            _state = DriverStatus.CREATED;
            _logger.Trace("Initializing hardware.");

            // Open the hardware port
            SetState(DriverStatus.HARDWARE_INITIALIZING);
            if (!InitializeHardware())
            {
                Shutdown();
                return null;
            }

            // Now reset the dongle
            SetState(DriverStatus.HARDWARE_OPEN);
            if (!dongleReset())
            {
                _logger.Warn("Dongle reset failed. Assuming bootloader is running and sending magic byte {}.",
                        string.Format("0x%02x", BOOTLOADER_MAGIC_BYTE));
                if (!bootloaderGetOut(BOOTLOADER_MAGIC_BYTE))
                {
                    _logger.Warn("Attempt to get out from bootloader failed.");
                    Shutdown();
                    return null;
                }
            }

            SetState(DriverStatus.HARDWARE_READY);

            string version = getStackVersion();
            if (version == null)
            {
                _logger.Debug("Failed to get CC2531 version");

            }
            else
            {
                _logger.Debug("CC2531 version is {version}", version);
            }

            return version;
        }

        public void Shutdown()
        {
            if (_state == DriverStatus.CLOSED)
            {
                _logger.Debug("Already CLOSED");
                return;
            }
            if (_state == DriverStatus.NETWORK_READY)
            {
                _logger.Trace("Closing NETWORK");
                SetState(DriverStatus.HARDWARE_READY);
            }
            if (_state == DriverStatus.HARDWARE_OPEN || _state == DriverStatus.HARDWARE_READY
                    || _state == DriverStatus.NETWORK_INITIALIZING)
            {
                _logger.Trace("Closing HARDWARE");
                _commandInterface.Close();
                SetState(DriverStatus.CREATED);
            }
            SetState(DriverStatus.CLOSED);
        }

        private bool InitializeHardware()
        {
            if (_commandInterface == null)
            {
                _logger.Error("Command interface must be configured");
                return false;
            }

            if (!_commandInterface.Open())
            {
                _logger.Error("Failed to open the dongle.");
                return false;
            }

            return true;
        }

        public bool InitializeZigBeeNetwork(bool cleanStatus)
        {
            _logger.Trace("Initializing network.");

            SetState(DriverStatus.NETWORK_INITIALIZING);

            if (cleanStatus && !configureZigBeeNetwork())
            {
                Shutdown();
                return false;
            }

            if (!CreateZigBeeNetwork())
            {
                _logger.Error("Failed to start zigbee network.");
                Shutdown();
                return false;
            }
            // if (checkZigBeeNetworkConfiguration()) {
            // _logger.Error("Dongle configuration does not match the specified configuration.");
            // shutdown();
            // return false;
            // }
            return true;
        }

        private bool CreateZigBeeNetwork()
        {
            createCustomDevicesOnDongle();
            _logger.Debug($"Creating network as {_mode}");

            ushort ALL_CLUSTERS = 0xFFFF;

            _logger.Trace("Reset seq: Trying MSG_CB_REGISTER");
            ZDO_MSG_CB_REGISTER_SRSP responseCb = (ZDO_MSG_CB_REGISTER_SRSP)sendSynchronous(
                    new ZDO_MSG_CB_REGISTER(new DoubleByte(ALL_CLUSTERS)));
            if (responseCb == null)
            {
                return false;
            }

            ZB_WRITE_CONFIGURATION_RSP responseCfg;
            responseCfg = (ZB_WRITE_CONFIGURATION_RSP)sendSynchronous(
                    new ZB_WRITE_CONFIGURATION(ZB_WRITE_CONFIGURATION.CONFIG_ID.ZCD_NV_ZDO_DIRECT_CB, new int[] { 1 }));
            if (responseCfg == null)
            {
                return false;
            }

            int instantStartup = 0;

            ZDO_STARTUP_FROM_APP_SRSP response = (ZDO_STARTUP_FROM_APP_SRSP)sendSynchronous(
                    new ZDO_STARTUP_FROM_APP(instantStartup), STARTUP_TIMEOUT);
            if (response == null)
            {
                return false;
            }

            switch (response.Status)
            {
                case 0:
                    {
                        _logger.Info("Initialized ZigBee network with existing network _state.");
                        return true;
                    }
                case 1:
                    {
                        _logger.Info("Initialized ZigBee network with new or reset network _state.");
                        return true;
                    }
                case 2:
                    {
                        _logger.Warn("Initializing ZigBee network failed.");
                        return false;
                    }
                default:
                    {
                        _logger.Error("Unexpected response _state for ZDO_STARTUP_FROM_APP {response}", response.Status);
                        return false;
                    }
            }
        }

        private bool configureZigBeeNetwork()
        {
            _logger.Debug("Resetting network stack.");

            // Make sure we start clearing configuration and _state
            if (!dongleSetStartupOption(STARTOPT_CLEAR_CONFIG | STARTOPT_CLEAR_STATE))
            {
                _logger.Error("Unable to set clean _state for dongle");
                return false;
            }
            _logger.Debug("Changing the Network Mode to {}.", _mode);
            if (!dongleSetNetworkMode())
            {
                _logger.Error("Unable to set NETWORK_MODE for ZigBee Network");
                return false;
            }
            else
            {
                _logger.Trace("NETWORK_MODE set");
            }
            // A dongle reset is needed to put into effect
            // configuration clear and network _mode.
            _logger.Debug("Resetting CC2531 dongle.");
            if (!dongleReset())
            {
                _logger.Error("Unable to reset dongle");
                return false;
            }

            _logger.Debug("Setting channel to {Channel}.", _channel);
            if (!dongleSetChannel())
            {
                _logger.Error("Unable to set CHANNEL for ZigBee Network");
                return false;
            }
            else
            {
                _logger.Trace("CHANNEL set");
            }

            _logger.Debug("Setting PAN to {}.", string.Format("%04X", pan & 0x0000ffff));
            if (!dongleSetPanId())
            {
                _logger.Error("Unable to set PANID for ZigBee Network");
                return false;
            }
            else
            {
                _logger.Trace("PANID set");
            }
            if (extendedPanId != null)
            {
                _logger.Debug("Setting Extended PAN ID to {}.", extendedPanId);
                if (!dongleSetExtendedPanId())
                {
                    _logger.Error("Unable to set EXT_PANID for ZigBee Network");
                    return false;
                }
                else
                {
                    _logger.Trace("EXT_PANID set");
                }
            }
            if (networkKey != null)
            {
                _logger.Debug("Setting NETWORK_KEY.");
                if (!dongleSetNetworkKey())
                {
                    _logger.Error("Unable to set NETWORK_KEY for ZigBee Network");
                    return false;
                }
                else
                {
                    _logger.Trace("NETWORK_KEY set");
                }
            }
            _logger.Debug("Setting Distribute Network Key to {}.", distributeNetworkKey);
            if (!dongleSetDistributeNetworkKey())
            {
                _logger.Error("Unable to set DISTRIBUTE_NETWORK_KEY for ZigBee Network");
                return false;
            }
            else
            {
                _logger.Trace("DISTRIBUTE_NETWORK_KEY set");
            }
            _logger.Debug("Setting Security Mode to {}.", securityMode);
            if (!dongleSetSecurityMode())
            {
                _logger.Error("Unable to set SECURITY_MODE for ZigBee Network");
                return false;
            }
            else
            {
                _logger.Trace("SECURITY_MODE set");
            }
            return true;
        }

        private void SetState(DriverStatus value)
        {
            _logger.Trace("{} -> {}", _state, value);
            synchronized(this) {
                _state = value;
                notifyAll();
            }
            if (_state == DriverStatus.HARDWARE_READY)
            {
                postHardwareEnabled();
            }
        }

        private void postHardwareEnabled()
        {
            if (!messageListeners.contains(afMessageListenerFilter))
            {
                _commandInterface.addAsynchronousCommandListener(afMessageListenerFilter);
            }
            // if (!announceListeners.contains(announceListenerFilter)) {
            _commandInterface.addAsynchronousCommandListener(announceListenerFilter);
            // }
        }

        private bool waitForHardware()
        {
            synchronized(this) {
                while (_state == DriverStatus.CREATED || _state == DriverStatus.CLOSED)
                {
                    _logger.Debug("Waiting for hardware to become ready");
                    try
                    {
                        wait();
                    }
                    catch (InterruptedException ignored)
                    {
                    }
                }
                return isHardwareReady();
            }
        }

        private bool waitForNetwork()
        {
            long before = System.currentTimeMillis();
            bool timedOut = false;
            synchronized(this) {
                while (_state != DriverStatus.NETWORK_READY && _state != DriverStatus.CLOSED && !timedOut)
                {
                    _logger.Debug("Waiting for network to become ready");
                    try
                    {
                        long now = System.currentTimeMillis();
                        long timeout = STARTUP_TIMEOUT - (now - before);
                        if (timeout > 0)
                        {
                            wait(timeout);
                        }
                        else
                        {
                            timedOut = true;
                        }
                    }
                    catch (InterruptedException ignored)
                    {
                    }

                }
                return isNetworkReady();
            }
        }

        public void setZigBeeNodeMode(NetworkMode networkMode)
        {
            _mode = networkMode;
        }

        public void setZigBeeNetworkKey(byte[] networkKey)
        {
            this.networkKey = networkKey;
            dongleSetNetworkKey();
        }

        public bool setZigBeePanId(int panId)
        {
            pan = panId;

            return dongleSetPanId();
        }

        public bool setZigBeeChannel(int channel)
        {
            this.channel = channel;

            return dongleSetChannel();
        }

        public bool setZigBeeExtendedPanId(ExtendedPanId panId)
        {
            this.extendedPanId = panId;
            return dongleSetExtendedPanId();
        }

        public bool setNetworkKey(byte[] networkKey)
        {
            this.networkKey = networkKey;

            return dongleSetNetworkKey();
        }

        public bool setDistributeNetworkKey(bool distributeNetworkKey)
        {
            this.distributeNetworkKey = distributeNetworkKey;

            return dongleSetDistributeNetworkKey();
        }

        public bool setSecurityMode(int securityMode)
        {
            this.securityMode = securityMode;

            return dongleSetSecurityMode();
        }

        public void addAsynchronousCommandListener(final AsynchronousCommandListener asynchronousCommandListener)
        {
            _commandInterface.addAsynchronousCommandListener(asynchronousCommandListener);
        }

        public <REQUEST extends ZToolPacket, RESPONSE extends ZToolPacket> RESPONSE sendLocalRequest(REQUEST request)
        {
            if (!waitForNetwork())
            {
                return null;
            }
            RESPONSE result = (RESPONSE)sendSynchronous(request);
            if (result == null)
            {
                _logger.Error("{} timed out waiting for synchronous local response.", request.getClass().getSimpleName());
            }
            return result;
        }

        public <REQUEST extends ZToolPacket, RESPONSE extends ZToolPacket> RESPONSE sendRemoteRequest(REQUEST request)
        {
            if (!waitForNetwork())
            {
                return null;
            }
            RESPONSE result;

            waitAndLock3WayConversation(request);
            final BlockingCommandReceiver waiter = new BlockingCommandReceiver(ZToolCMD.ZDO_MGMT_PERMIT_JOIN_RSP,
                    _commandInterface);

            _logger.Trace("Sending {}", request);
            ZToolPacket response = sendSynchronous(request);
            if (response == null)
            {
                _logger.Error("{} timed out waiting for synchronous local response.", request.getClass().getSimpleName());
                waiter.cleanup();
                return null;
            }
            else
            {
                _logger.Error("{} timed out waiting for asynchronous remote response.", request.getClass().getSimpleName());
                result = (RESPONSE)waiter.getCommand(TIMEOUT);
                unLock3WayConversation(request);
                return result;
            }
        }

        /**
         * @param request
         */
        private void waitAndLock3WayConversation(ZToolPacket request)
        {
            synchronized(conversation3Way) {
                Class <?> clz = request.getClass();
                Thread requestor;
                while ((requestor = conversation3Way.get(clz)) != null)
                {
                    if (!requestor.isAlive())
                    {
                        _logger.Error("Thread {} whom requested {} DIED before unlocking the conversation");
                        _logger.Debug(
                                "The thread {} who was waiting for {} to complete DIED, so we have to remove the lock");
                        conversation3Way.put(clz, null);
                        break;
                    }
                    _logger.Trace("{} is waiting for {} to complete which was issued by {} to complete",
                            new Object[] { Thread.currentThread(), clz, requestor });
                    try
                    {
                        conversation3Way.wait();
                    }
                    catch (InterruptedException ex)
                    {
                    }
                    catch (IllegalMonitorStateException ex)
                    {
                        _logger.Error("Error in 3 way conversation.", ex);
                    }
                }
                conversation3Way.put(clz, Thread.currentThread());
            }
        }

        /**
         * Release the lock held for the 3-way communication
         *
         * @param request
         */
        private void unLock3WayConversation(ZToolPacket request)
        {
            Class <?> clz = request.getClass();
            Thread requestor;
            synchronized(conversation3Way) {
                requestor = conversation3Way.get(clz);
                conversation3Way.put(clz, null);
                conversation3Way.notify();
            }
            if (requestor == null)
            {
                _logger.Error("LOCKING BROKEN - SOMEONE RELEASE THE LOCK WITHOUT LOCKING IN ADVANCE for {}", clz);
            }
            else if (requestor != Thread.currentThread())
            {
                _logger.Error("Thread {} stolen the answer of {} waited by {}",
                        new Object[] { Thread.currentThread(), clz, requestor });
            }
        }

        private bool bootloaderGetOut(int magicByte)
        {
            final BlockingCommandReceiver waiter = new BlockingCommandReceiver(ZToolCMD.SYS_RESET_RESPONSE,
                    _commandInterface);

            try
            {
                _commandInterface.sendRaw(new int[] { magicByte });
            }
            catch (IOException e)
            {
                _logger.Error("Failed to send bootloader magic byte", e);
            }

            SYS_RESET_RESPONSE response = (SYS_RESET_RESPONSE)waiter.getCommand(RESET_TIMEOUT);

            return response != null;
        }

        private String getStackVersion()
        {
            if (!waitForHardware())
            {
                _logger.Info("Failed to reach the {} level: getStackVerion() failed", DriverStatus.NETWORK_READY);
                return null;
            }

            SYS_VERSION_RESPONSE response = (SYS_VERSION_RESPONSE)sendSynchronous(new SYS_VERSION());
            if (response == null)
            {
                return null;
            }
            else
            {
                StringBuilder builder = new StringBuilder();
                builder.append("Software=");
                builder.append(response.MajorRel);
                builder.append(".");
                builder.append(response.MinorRel);
                builder.append(" Product=");
                builder.append(response.Product);
                builder.append(" Hardware=");
                builder.append(response.HwRev);
                builder.append(" Transport=");
                builder.append(response.TransportRev);
                return builder.toString();
            }
        }

        private bool dongleReset()
        {
            final BlockingCommandReceiver waiter = new BlockingCommandReceiver(ZToolCMD.SYS_RESET_RESPONSE,
                    _commandInterface);

            try
            {
                _commandInterface.sendAsynchronousCommand(new SYS_RESET(SYS_RESET.RESET_TYPE.SERIAL_BOOTLOADER));
            }
            catch (IOException e)
            {
                _logger.Error("Failed to send SYS_RESET", e);
                return false;
            }

            SYS_RESET_RESPONSE response = (SYS_RESET_RESPONSE)waiter.getCommand(RESET_TIMEOUT);

            return response != null;
        }

        private bool dongleSetStartupOption(int mask)
        {
            if ((mask & ~(STARTOPT_CLEAR_CONFIG | STARTOPT_CLEAR_STATE)) != 0)
            {
                _logger.Warn("Invalid ZCD_NV_STARTUP_OPTION mask {}.", String.format("%08X", mask));
                return false;
            }

            ZB_WRITE_CONFIGURATION_RSP response;
            response = (ZB_WRITE_CONFIGURATION_RSP)sendSynchronous(
                    new ZB_WRITE_CONFIGURATION(ZB_WRITE_CONFIGURATION.CONFIG_ID.ZCD_NV_STARTUP_OPTION, new int[] { mask }));

            if (response == null || response.Status != 0)
            {
                _logger.Warn("Couldn't set ZCD_NV_STARTUP_OPTION mask {}", String.format("%08X", mask));
                return false;
            }
            else
            {
                _logger.Trace("Set ZCD_NV_STARTUP_OPTION mask {}", String.format("%08X", mask));
            }

            return true;
        }

        private final int[] buildChannelMask(int channel)
        {
            if (channel < 11 || channel > 27)
            {
                return new int[] { 0, 0, 0, 0 };
            }

            int channelMask = 1 << channel;
            int[] mask = new int[4];

            for (int i = 0; i < mask.length; i++)
            {
                mask[i] = Integers.getByteAsInteger(channelMask, i);
            }
            return mask;
        }

        /**
         * Sets the ZigBee RF channel. The allowable channel range is 11 to 26.
         * <p>
         * This method will sanity check the channel and if the mask is invalid
         * the default channel will be used.
         *
         * @param channelMask
         * @return
         */
        private bool dongleSetChannel(int[] channelMask)
        {
            // Error check the channels.
            // Incorrectly setting the channel can cause the stick to hang!!

            // Mask out any invalid channels
            channelMask[0] &= ZNP_CHANNEL_MASK0;
            channelMask[1] &= ZNP_CHANNEL_MASK1;
            channelMask[2] &= ZNP_CHANNEL_MASK2;
            channelMask[3] &= ZNP_CHANNEL_MASK3;

            // If there's no channels set, then we go for the default
            if (channelMask[0] == 0 && channelMask[1] == 0 && channelMask[2] == 0 && channelMask[3] == 0)
            {
                channelMask[0] = ZNP_CHANNEL_DEFAULT0;
                channelMask[1] = ZNP_CHANNEL_DEFAULT1;
                channelMask[2] = ZNP_CHANNEL_DEFAULT2;
                channelMask[3] = ZNP_CHANNEL_DEFAULT3;
            }

            _logger.Trace("Setting the channel to {}{}{}{}",
                    new Object[] { Integer.toHexString(channelMask[0]), Integer.toHexString(channelMask[1]),
                        Integer.toHexString(channelMask[2]), Integer.toHexString(channelMask[3]) });

            ZB_WRITE_CONFIGURATION_RSP response = (ZB_WRITE_CONFIGURATION_RSP)sendSynchronous(
                    new ZB_WRITE_CONFIGURATION(ZB_WRITE_CONFIGURATION.CONFIG_ID.ZCD_NV_CHANLIST, channelMask));

            return response != null && response.Status == 0;
        }

        private bool dongleSetChannel()
        {
            int[] channelMask = buildChannelMask(channel);

            return dongleSetChannel(channelMask);
        }

        private bool dongleSetNetworkMode()
        {
            ZB_WRITE_CONFIGURATION_RSP response = (ZB_WRITE_CONFIGURATION_RSP)sendSynchronous(new ZB_WRITE_CONFIGURATION(
                    ZB_WRITE_CONFIGURATION.CONFIG_ID.ZCD_NV_LOGICAL_TYPE, new int[] { _mode.ordinal() }));

            return response != null && response.Status == 0;
        }

        private bool dongleSetPanId()
        {
            currentPanId = -1;

            ZB_WRITE_CONFIGURATION_RSP response = (ZB_WRITE_CONFIGURATION_RSP)sendSynchronous(
                    new ZB_WRITE_CONFIGURATION(ZB_WRITE_CONFIGURATION.CONFIG_ID.ZCD_NV_PANID,
                            new int[] { Integers.getByteAsInteger(pan, 0), Integers.getByteAsInteger(pan, 1) }));

            return response != null && response.Status == 0;
        }

        private bool dongleSetExtendedPanId()
        {
            ZB_WRITE_CONFIGURATION_RSP response = (ZB_WRITE_CONFIGURATION_RSP)sendSynchronous(
                    new ZB_WRITE_CONFIGURATION(ZB_WRITE_CONFIGURATION.CONFIG_ID.ZCD_NV_EXTPANID, extendedPanId.getValue()));

            return response != null && response.Status == 0;
        }

        private bool dongleSetNetworkKey()
        {
            ZB_WRITE_CONFIGURATION_RSP response = (ZB_WRITE_CONFIGURATION_RSP)sendSynchronous(new ZB_WRITE_CONFIGURATION(
                    ZB_WRITE_CONFIGURATION.CONFIG_ID.ZCD_NV_PRECFGKEY,
                    new int[] { networkKey[0], networkKey[1], networkKey[2], networkKey[3], networkKey[4], networkKey[5],
                        networkKey[6], networkKey[7], networkKey[8], networkKey[9], networkKey[10], networkKey[11],
                        networkKey[12], networkKey[13], networkKey[14], networkKey[15] }));

            return response != null && response.Status == 0;
        }

        private bool dongleSetDistributeNetworkKey()
        {
            ZB_WRITE_CONFIGURATION_RSP response = (ZB_WRITE_CONFIGURATION_RSP)sendSynchronous(new ZB_WRITE_CONFIGURATION(
                    ZB_WRITE_CONFIGURATION.CONFIG_ID.ZCD_NV_PRECFGKEYS_ENABLE, new int[] { distributeNetworkKey ? 0 : 1 }));

            return response != null && response.Status == 0;
        }

        private bool dongleSetSecurityMode()
        {
            ZB_WRITE_CONFIGURATION_RSP response = (ZB_WRITE_CONFIGURATION_RSP)sendSynchronous(new ZB_WRITE_CONFIGURATION(
                    ZB_WRITE_CONFIGURATION.CONFIG_ID.ZCD_NV_SECURITY_MODE, new int[] { securityMode }));

            return response != null && response.Status == 0;
        }

        /**
         * Sends a command without waiting for the response
         *
         * @param request {@link ZToolPacket}
         */
        public void sendCommand(final ZToolPacket request)
        {
            sendSynchronous(request);
        }

        private ZToolPacket sendSynchronous(final ZToolPacket request)
        {
            return sendSynchronous(request, RESEND_TIMEOUT);
        }

        private ZToolPacket sendSynchronous(final ZToolPacket request, int timeout)
        {
            final ZToolPacket[] response = new ZToolPacket[] { null };
            // final int RESEND_MAX_RETRY = 3;
            int sending = 1;

            _logger.Trace("{} sending as synchronous command.", request.getClass().getSimpleName());

            SynchronousCommandListener listener = new SynchronousCommandListener() {
            @Override
            public void receivedCommandResponse(ZToolPacket packet)
            {
                _logger.Trace(" {} received as synchronous command.", packet.getClass().getSimpleName());
                synchronized(response) {
                    // Do not set response[0] again.
                    response[0] = packet;
                    response.notify();
                }
            }
        };

        while (sending <= RESEND_MAX_RETRY) {
            try {
                try {
                    _commandInterface.sendSynchronousCommand(request, listener, timeout);
                } catch (IOException e) {
                    _logger.Error("Synchronous command send failed due to IO exception. ", e);
                    break;
                } catch (Exception ex) {
                    _logger.Error("Synchronous command send failed due to unexpected exception.", ex);
                }
                _logger.Trace("{} sent (synchronous command, attempt {}).", request.getClass().getSimpleName(), sending);
                synchronized(response)
{
    long wakeUpTime = System.currentTimeMillis() + timeout;
    while (response[0] == null && wakeUpTime > System.currentTimeMillis())
    {
        final long sleeping = wakeUpTime - System.currentTimeMillis();
        _logger.Trace("Waiting for synchronous command up to {}ms till {} Unixtime", sleeping,
                wakeUpTime);
        if (sleeping <= 0)
        {
            break;
        }
        try
        {
            response.wait(sleeping);
        }
        catch (InterruptedException ignored)
        {
        }
    }
}
                if (response[0] != null) {
                    _logger.Trace("{} -> {}", request.getClass().getSimpleName(),
                            response[0].getClass().getSimpleName());
                    break; // Break out as we have response.
                } else {
                    _logger.Debug("{} executed and timed out while waiting for response.",
                            request.getClass().getSimpleName());
                }
                if (RESEND_ONLY_EXCEPTION) {
                    break;
                } else {
                    _logger.Debug("Failed to send {} [attempt {}]", request.getClass().getSimpleName(), sending);
                    sending++;
                }
            } catch (Exception ignored) {
                _logger.Debug("Failed to send {} [attempt {}]", request.getClass().getSimpleName(), sending);
                _logger.Trace("Sending operation failed due to ", ignored);
                sending++;
            }
        }

        return response[0];
    }

    public AF_REGISTER_SRSP sendAFRegister(AF_REGISTER request)
{
    if (!waitForNetwork())
    {
        return null;
    }

    AF_REGISTER_SRSP response = (AF_REGISTER_SRSP)sendSynchronous(request);
    return response;
}

public AF_DATA_CONFIRM sendAFDataRequest(AF_DATA_REQUEST request)
{
    if (!waitForNetwork())
    {
        return null;
    }
    AF_DATA_CONFIRM result = null;

    waitAndLock3WayConversation(request);
    final BlockingCommandReceiver waiter = new BlockingCommandReceiver(ZToolCMD.AF_DATA_CONFIRM, _commandInterface);

    AF_DATA_SRSP response = (AF_DATA_SRSP)sendSynchronous(request);
    if (response == null || response.Status != 0)
    {
        waiter.cleanup();
    }
    else
    {
        result = (AF_DATA_CONFIRM)waiter.getCommand(TIMEOUT);
    }
    unLock3WayConversation(request);

    return result;
}

/**
 * Sends an Application Framework data request and waits for the response.
 *
 * @param request {@link AF_DATA_REQUEST}
 */
public AF_DATA_SRSP_EXT sendAFDataRequestExt(AF_DATA_REQUEST_EXT request)
{
    if (!waitForNetwork())
    {
        return null;
    }
    AF_DATA_SRSP_EXT response = (AF_DATA_SRSP_EXT)sendSynchronous(request);
    return response;
}

/**
 * Removes an Application Framework message listener that was previously added with the addAFMessageListener method
 *
 * @param listener a class that implements the {@link ApplicationFrameworkMessageListener} interface
 * @return true if the listener was added
 */
public bool removeAFMessageListener(ApplicationFrameworkMessageListener listener)
{
    bool result;
    synchronized(messageListeners) {
        result = messageListeners.remove(listener);
    }

    if (messageListeners.isEmpty() && isHardwareReady())
    {
        if (_commandInterface.removeAsynchronousCommandListener(afMessageListenerFilter))
        {
            _logger.Trace("Removed AsynchrounsCommandListener {} to ZigBeeSerialInterface",
                    afMessageListenerFilter.getClass().getName());
        }
        else
        {
            _logger.Warn("Could not remove AsynchrounsCommandListener {} to ZigBeeSerialInterface",
                    afMessageListenerFilter.getClass().getName());
        }
    }
    if (result)
    {
        _logger.Trace("Removed ApplicationFrameworkMessageListener {}:{}", listener,
                listener.getClass().getSimpleName());
        return true;
    }
    else
    {
        _logger.Warn("Could not remove ApplicationFrameworkMessageListener {}:{}", listener,
                listener.getClass().getSimpleName());
        return false;
    }
}

/**
 * Adds an Application Framework message listener
 *
 * @param listener a class that implements the {@link ApplicationFrameworkMessageListener} interface
 * @return true if the listener was added
 */
public bool addAFMessageListener(ApplicationFrameworkMessageListener listener)
{
    synchronized(messageListeners) {
        if (messageListeners.contains(listener))
        {
            return true;
        }
    }
    if (messageListeners.isEmpty() && isHardwareReady())
    {
        if (_commandInterface.addAsynchronousCommandListener(afMessageListenerFilter))
        {
            _logger.Trace("Added AsynchrounsCommandListener {} to ZigBeeSerialInterface",
                    afMessageListenerFilter.getClass().getSimpleName());
        }
        else
        {
            _logger.Trace("Could not add AsynchrounsCommandListener {} to ZigBeeSerialInterface",
                    afMessageListenerFilter.getClass().getSimpleName());
        }
    }
    bool result;
    synchronized(messageListeners) {
        result = messageListeners.add(listener);
    }

    if (result)
    {
        _logger.Trace("Added ApplicationFrameworkMessageListener {}:{}", listener,
                listener.getClass().getSimpleName());
        return true;
    }
    else
    {
        _logger.Warn("Could not add ApplicationFrameworkMessageListener {}:{}", listener,
                listener.getClass().getSimpleName());
        return false;
    }
}

private bool isNetworkReady()
{
    synchronized(this) {
        return _state.ordinal() >= DriverStatus.NETWORK_READY.ordinal()
                && _state.ordinal() < DriverStatus.CLOSED.ordinal();
    }
}

private bool isHardwareReady()
{
    synchronized(this) {
        return _state.ordinal() >= DriverStatus.HARDWARE_READY.ordinal()
                && _state.ordinal() < DriverStatus.CLOSED.ordinal();
    }
}

/**
 * Gets the extended PAN ID
 *
 * @return the PAN ID or -1 on failure
 */
public ExtendedPanId getCurrentExtendedPanId()
{
    if (!waitForHardware())
    {
        _logger.Info("Failed to reach the {} level: getExtendedPanId() failed", DriverStatus.HARDWARE_READY);
        return new ExtendedPanId();
    }

    int[] result = getDeviceInfo(ZB_GET_DEVICE_INFO.DEV_INFO_TYPE.EXT_PAN_ID);

    if (result == null)
    {
        // luckily -1 (aka 0xffffffffffffffffL) is not a valid extended PAN ID value
        return new ExtendedPanId();
    }
    else
    {
        return new ExtendedPanId(result);
    }
}

/**
 * Gets the IEEE address of our node on the network
 *
 * @return the IEEE address as a long or -1 on failure
 */
public long getIeeeAddress()
{
    if (ieeeAddress != -1)
    {
        return ieeeAddress;
    }

    if (!waitForHardware())
    {
        _logger.Info("Failed to reach the {} level: getIeeeAddress() failed", DriverStatus.HARDWARE_READY);
        return -1;
    }

    int[] result = getDeviceInfo(ZB_GET_DEVICE_INFO.DEV_INFO_TYPE.IEEE_ADDR);

    if (result == null)
    {
        return -1;
    }
    else
    {
        ieeeAddress = Integers.longFromInts(result, 7, 0);
        return ieeeAddress;
    }
}

/**
 * Gets the current PAN ID
 *
 * @return current PAN ID as an int or -1 on failure
 */
public int getCurrentPanId()
{
    if (!waitForHardware())
    {
        _logger.Info("Failed to reach the {} level: getCurrentPanId() failed", DriverStatus.NETWORK_READY);
        return -1;
    }

    if (currentPanId != -1)
    {
        return currentPanId;
    }

    int[] result = getDeviceInfo(ZB_GET_DEVICE_INFO.DEV_INFO_TYPE.PAN_ID);
    if (result == null)
    {
        return -1;
    }
    else
    {
        currentPanId = Integers.shortFromInts(result, 1, 0);
        return Integers.shortFromInts(result, 1, 0);
    }
}

/**
 * Gets the current ZigBee channe number
 *
 * @return the current channel as an int, or -1 on failure
 */
public int getCurrentChannel()
{
    if (!waitForHardware())
    {
        _logger.Info("Failed to reach the {} level: getCurrentChannel() failed", DriverStatus.HARDWARE_READY);
        return -1;
    }

    int[] result = getDeviceInfo(ZB_GET_DEVICE_INFO.DEV_INFO_TYPE.CHANNEL);
    if (result == null)
    {
        return -1;
    }
    else
    {
        return result[0];
    }
}

private int[] getDeviceInfo(int type)
{
    ZB_GET_DEVICE_INFO_RSP response = (ZB_GET_DEVICE_INFO_RSP)sendSynchronous(new ZB_GET_DEVICE_INFO(type));

    if (response == null)
    {
        _logger.Warn("Failed getDeviceInfo for {} due to null value", type);
        return null;
    }
    else if (response.Param != type)
    {
        _logger.Warn("Failed getDeviceInfo for {} non matching response returned {}", type, response.Param);
        return null;
    }
    else
    {
        _logger.Trace("getDeviceInfo for {} done", type);
        return response.Value;
    }
}

public int getZigBeeNodeMode()
{
    ZB_READ_CONFIGURATION_RSP response = (ZB_READ_CONFIGURATION_RSP)sendSynchronous(
            new ZB_READ_CONFIGURATION(ZB_WRITE_CONFIGURATION.CONFIG_ID.ZCD_NV_LOGICAL_TYPE));
    if (response != null && response.Status == 0)
    {
        return response.Value[0];
    }
    else
    {
        return -1;
    }
}

public byte[] getZigBeeNetworkKey()
{
    ZB_READ_CONFIGURATION_RSP response = (ZB_READ_CONFIGURATION_RSP)sendSynchronous(
            new ZB_READ_CONFIGURATION(ZB_WRITE_CONFIGURATION.CONFIG_ID.ZCD_NV_PRECFGKEY));
    if (response != null && response.Status == 0)
    {
        byte[] readNetworkKey = new byte[16];
        readNetworkKey[0] = (byte)response.Value[0];
        readNetworkKey[1] = (byte)response.Value[1];
        readNetworkKey[2] = (byte)response.Value[2];
        readNetworkKey[3] = (byte)response.Value[3];
        readNetworkKey[4] = (byte)response.Value[4];
        readNetworkKey[5] = (byte)response.Value[5];
        readNetworkKey[6] = (byte)response.Value[6];
        readNetworkKey[7] = (byte)response.Value[7];
        readNetworkKey[8] = (byte)response.Value[8];
        readNetworkKey[9] = (byte)response.Value[9];
        readNetworkKey[10] = (byte)response.Value[10];
        readNetworkKey[11] = (byte)response.Value[11];
        readNetworkKey[12] = (byte)response.Value[12];
        readNetworkKey[13] = (byte)response.Value[13];
        readNetworkKey[14] = (byte)response.Value[14];
        readNetworkKey[15] = (byte)response.Value[15];
        return readNetworkKey;
    }
    else
    {
        _logger.Error("Error reading zigbee network key: " + ResponseStatus.getStatus(response.Status));
        return null;
    }
}

public DriverStatus getDriverStatus()
{
    return _state;
}

private void createCustomDevicesOnDongle()
{
    int[] input;
    int[] output;

    if (this.ep != null)
    {
        for (int i = 0; i < this.ep.length; i++)
        {
            // input
            int size = 0;
            for (int j = 0; j < this.inp[i].length; j++)
            {

                if (this.inp[i][j] != 0 && this.inp[i][j] != -1)
                {
                    size++;
                }
            }

            input = new int[size];
            for (int j = 0; j < this.inp[i].length; j++)
            {

                if (this.inp[i][j] != 0 && this.inp[i][j] != -1)
                {
                    input[j] = this.inp[i][j];
                }
            }

            // output
            size = 0;
            for (int j = 0; j < this.out[i].length; j++) {

            if (this.out[i] [j] != 0 && this.out[i][j] != -1) {
    size++;
}
}

output = new int[size];
                for (int j = 0; j< this.out[i].length; j++) {

                    if (this.out[i][j] != 0 && this.out[i][j] != -1) {
    output[j] = this.out[i]
[j];
}
                }

                if (newDevice(new AF_REGISTER(Byte.valueOf(this.ep[i] + ""), this.prof[i],
                        Short.valueOf(this.dev[i] + ""), Byte.valueOf(this.ver [i] + ""), input, output))) {
    _logger.Debug("Custom device {} registered at endpoint {}", this.dev[i], this.ep[i]);
} else {
    _logger.Debug("Custom device {} registration failed at endpoint {}", this.dev[i], this.ep[i]);
}
}
        }
    }

    private bool newDevice(AF_REGISTER request)
{
    try
    {
        AF_REGISTER_SRSP response = (AF_REGISTER_SRSP)sendSynchronous(request);
        if (response != null && response.Status == 0)
        {
            return true;
        }
    }
    catch (Exception e)
    {
        _logger.Error("Error in device register.", e);
    }

    return false;
}
    }
}
