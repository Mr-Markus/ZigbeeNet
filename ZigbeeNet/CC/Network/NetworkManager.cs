﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using ZigBeeNet.CC.Implementation;
using ZigBeeNet.CC.Packet;
using ZigBeeNet.CC.Packet.AF;
using ZigBeeNet.CC.Packet.SimpleAPI;
using ZigBeeNet.CC.Packet.SYS;
using ZigBeeNet.CC.Packet.ZDO;
using ZigBeeNet.Logging;
using ZigBeeNet.ZCL;

namespace ZigBeeNet.CC.Network
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
        private ExtendedPanId _extendedPanId; // do not initialize to use dongle defaults (the IEEE address)
        private byte[] _networkKey; // 16 byte network key
        private bool _distributeNetworkKey = true; // distribute network key in clear (be careful)
        private int _securityMode = 1;

        private byte[] _ep;
        private byte[] _prof;
        private byte[] _dev;
        private byte[] _ver;
        private ushort[][] _inp;
        private ushort[][] _out;

        private NetworkStateListener _announceListenerFilter = new NetworkStateListener();

        private List<IApplicationFrameworkMessageListener> _messageListeners = new List<IApplicationFrameworkMessageListener>();
        private AFMessageListenerFilter _afMessageListenerFilter;

        private ulong _ieeeAddress = ulong.MaxValue;
        private ushort _currentPanId = ushort.MaxValue;

        private Dictionary<Type, Thread> _conversation3Way = new Dictionary<Type, Thread>();

        public NetworkManager(ICommandInterface commandInterface, NetworkMode mode, long timeout)
        {
            _announceListenerFilter.OnStateChanged += (object sender, DriverStatus status) => SetState(status);
            _afMessageListenerFilter = new AFMessageListenerFilter(_messageListeners);

            _mode = mode;
            _commandInterface = commandInterface;

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
            if (!DongleReset())
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

            string version = GetStackVersion();
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

            if (cleanStatus && !ConfigureZigBeeNetwork())
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
            CreateCustomDevicesOnDongle();
            _logger.Debug($"Creating network as {_mode}");

            ushort ALL_CLUSTERS = 0xFFFF;

            _logger.Trace("Reset seq: Trying MSG_CB_REGISTER");
            ZDO_MSG_CB_REGISTER_SRSP responseCb = (ZDO_MSG_CB_REGISTER_SRSP)SendSynchronous(
                    new ZDO_MSG_CB_REGISTER(new DoubleByte(ALL_CLUSTERS)));
            if (responseCb == null)
            {
                return false;
            }

            ZB_WRITE_CONFIGURATION_RSP responseCfg;
            responseCfg = (ZB_WRITE_CONFIGURATION_RSP)SendSynchronous(
                    new ZB_WRITE_CONFIGURATION(ZB_WRITE_CONFIGURATION.CONFIG_ID.ZCD_NV_ZDO_DIRECT_CB, new byte[] { 1 }));
            if (responseCfg == null)
            {
                return false;
            }

            byte instantStartup = 0;

            ZDO_STARTUP_FROM_APP_SRSP response = (ZDO_STARTUP_FROM_APP_SRSP)SendSynchronous(
                    new ZDO_STARTUP_FROM_APP(instantStartup), StartupTimeout);
            if (response == null)
            {
                return false;
            }

            switch (response.Status)
            {
                case 0:
                    _logger.Info("Initialized ZigBee network with existing network _state.");
                    return true;
                case 1:
                    _logger.Info("Initialized ZigBee network with new or reset network _state.");
                    return true;
                case 2:
                    _logger.Warn("Initializing ZigBee network failed.");
                    return false;
                default:
                    _logger.Error("Unexpected response _state for ZDO_STARTUP_FROM_APP {response}", response.Status);
                    return false;
            }
        }

        private bool ConfigureZigBeeNetwork()
        {
            _logger.Debug("Resetting network stack.");

            // Make sure we start clearing configuration and _state
            if (!DongleSetStartupOption(STARTOPT_CLEAR_CONFIG | STARTOPT_CLEAR_STATE))
            {
                _logger.Error("Unable to set clean _state for dongle");
                return false;
            }
            _logger.Debug("Changing the Network Mode to {Mode}.", _mode);
            if (!DongleSetNetworkMode())
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
            if (!DongleReset())
            {
                _logger.Error("Unable to reset dongle");
                return false;
            }

            _logger.Debug("Setting channel to {Channel}.", _channel);
            if (!DongleSetChannel())
            {
                _logger.Error("Unable to set CHANNEL for ZigBee Network");
                return false;
            }
            else
            {
                _logger.Trace("CHANNEL set");
            }

            _logger.Debug("Setting PAN to {Pan}.", string.Format("%04X", _pan & 0x0000ffff));
            if (!DongleSetPanId())
            {
                _logger.Error("Unable to set PANID for ZigBee Network");
                return false;
            }
            else
            {
                _logger.Trace("PANID set");
            }
            if (_extendedPanId != null)
            {
                _logger.Debug("Setting Extended PAN ID to {}.", _extendedPanId);
                if (!DongleSetExtendedPanId())
                {
                    _logger.Error("Unable to set EXT_PANID for ZigBee Network");
                    return false;
                }
                else
                {
                    _logger.Trace("EXT_PANID set");
                }
            }
            if (_networkKey != null)
            {
                _logger.Debug("Setting NETWORK_KEY.");
                if (!DongleSetNetworkKey())
                {
                    _logger.Error("Unable to set NETWORK_KEY for ZigBee Network");
                    return false;
                }
                else
                {
                    _logger.Trace("NETWORK_KEY set");
                }
            }
            _logger.Debug("Setting Distribute Network Key to {}.", _distributeNetworkKey);
            if (!dongleSetDistributeNetworkKey())
            {
                _logger.Error("Unable to set DISTRIBUTE_NETWORK_KEY for ZigBee Network");
                return false;
            }
            else
            {
                _logger.Trace("DISTRIBUTE_NETWORK_KEY set");
            }
            _logger.Debug("Setting Security Mode to {}.", _securityMode);
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
            _logger.Trace("{State} -> {Value}", _state, value);

            lock (typeof(NetworkManager))
            {
                _state = value;
                Monitor.PulseAll(this);
            }

            if (_state == DriverStatus.HARDWARE_READY)
            {
                PostHardwareEnabled();
            }
        }

        private void PostHardwareEnabled()
        {
            //if (!_messageListeners.Contains(_afMessageListenerFilter))
            //{
                _commandInterface.AddAsynchronousCommandListener(_afMessageListenerFilter);
            //}
            // if (!announceListeners.contains(announceListenerFilter)) {
            _commandInterface.AddAsynchronousCommandListener(_announceListenerFilter);
            // }
        }

        private bool WaitForHardware()
        {
            lock (typeof(NetworkManager))
            {
                while (_state == DriverStatus.CREATED || _state == DriverStatus.CLOSED)
                {
                    _logger.Debug("Waiting for hardware to become ready");
                    try
                    {
                        Monitor.Wait(this);
                    }
                    catch (Exception ignored)
                    {
                    }
                }
                return IsHardwareReady();
            }
        }

        private bool WaitForNetwork()
        {
            long before = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            bool timedOut = false;
            lock (typeof(NetworkManager))
            {
                while (_state != DriverStatus.NETWORK_READY && _state != DriverStatus.CLOSED && !timedOut)
                {
                    _logger.Debug("Waiting for network to become ready");
                    try
                    {
                        long now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                        long timeout = StartupTimeout - (now - before);
                        if (timeout > 0)
                        {
                            Monitor.Wait(timeout);
                        }
                        else
                        {
                            timedOut = true;
                        }
                    }
                    catch (Exception ignored)
                    {
                    }

                }
                return IsNetworkReady();
            }
        }

        public void SetZigBeeNodeMode(NetworkMode networkMode)
        {
            _mode = networkMode;
        }

        public void SetZigBeeNetworkKey(byte[] networkKey)
        {
            _networkKey = networkKey;
            DongleSetNetworkKey();
        }

        public bool SetZigBeePanId(ushort panId)
        {
            _pan = panId;

            return DongleSetPanId();
        }

        public bool SetZigBeeChannel(byte channel)
        {
            _channel = channel;

            return DongleSetChannel();
        }

        public bool SetZigBeeExtendedPanId(ExtendedPanId panId)
        {
            _extendedPanId = panId;
            return DongleSetExtendedPanId();
        }

        public bool SetNetworkKey(byte[] networkKey)
        {
            _networkKey = networkKey;

            return DongleSetNetworkKey();
        }

        public bool SetDistributeNetworkKey(bool distributeNetworkKey)
        {
            _distributeNetworkKey = distributeNetworkKey;

            return dongleSetDistributeNetworkKey();
        }

        public bool SetSecurityMode(int securityMode)
        {
            _securityMode = securityMode;

            return dongleSetSecurityMode();
        }

        public void AddAsynchronousCommandListener(IAsynchronousCommandListener asynchronousCommandListener)
        {
            _commandInterface.AddAsynchronousCommandListener(asynchronousCommandListener);
        }

        //public <REQUEST extends ZToolPacket, RESPONSE extends ZToolPacket> RESPONSE sendLocalRequest(REQUEST request)
        //{
        //    if (!WaitForNetwork())
        //    {
        //        return null;
        //    }
        //    RESPONSE result = (RESPONSE)SendSynchronous(request);
        //    if (result == null)
        //    {
        //        _logger.Error("{} timed out waiting for synchronous local response.", request.GetType().Name);
        //    }
        //    return result;
        //}

        //public <REQUEST extends ZToolPacket, RESPONSE extends ZToolPacket> RESPONSE sendRemoteRequest(REQUEST request)
        //{
        //    if (!WaitForNetwork())
        //    {
        //        return null;
        //    }
        //    RESPONSE result;

        //    waitAndLock3WayConversation(request);
        //    final BlockingCommandReceiver waiter = new BlockingCommandReceiver(ZToolCMD.ZDO_MGMT_PERMIT_JOIN_RSP,
        //            _commandInterface);

        //    _logger.Trace("Sending {}", request);
        //    ZToolPacket response = SendSynchronous(request);
        //    if (response == null)
        //    {
        //        _logger.Error("{} timed out waiting for synchronous local response.", request.GetType().Name);
        //        waiter.cleanup();
        //        return null;
        //    }
        //    else
        //    {
        //        _logger.Error("{} timed out waiting for asynchronous remote response.", request.GetType().Name);
        //        result = (RESPONSE)waiter.getCommand(TIMEOUT);
        //        unLock3WayConversation(request);
        //        return result;
        //    }
        //}

        /**
         * @param request
         */
        private void WaitAndLock3WayConversation(SerialPacket request)
        {
            lock (_conversation3Way)
            {
                Type clz = request.GetType();
                Thread requestor;
                while ((requestor = _conversation3Way[clz]) != null)
                {
                    if (!requestor.IsAlive)
                    {
                        _logger.Error("Thread {} whom requested {} DIED before unlocking the conversation");
                        _logger.Debug(
                                "The thread {} who was waiting for {} to complete DIED, so we have to remove the lock");
                        _conversation3Way[clz] = null;
                        break;
                    }
                    _logger.Trace("{} is waiting for {} to complete which was issued by {} to complete",
                            new Object[] { Thread.CurrentThread, clz, requestor });
                    try
                    {
                        Monitor.Wait(_conversation3Way);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Error in 3 way conversation.", ex);
                    }
                }
                _conversation3Way[clz] = Thread.CurrentThread;
            }
        }

        /**
         * Release the lock held for the 3-way communication
         *
         * @param request
         */
        private void UnLock3WayConversation(SerialPacket request)
        {
            Type clz = request.GetType();
            Thread requestor;
            lock (_conversation3Way)
            {
                requestor = _conversation3Way[clz];
                _conversation3Way[clz] = null;
                Monitor.Pulse(_conversation3Way);
            }
            if (requestor == null)
            {
                _logger.Error("LOCKING BROKEN - SOMEONE RELEASE THE LOCK WITHOUT LOCKING IN ADVANCE for {}", clz);
            }
            else if (requestor != Thread.CurrentThread)
            {
                _logger.Error("Thread {} stolen the answer of {} waited by {}",
                        new object[] { Thread.CurrentThread, clz, requestor });
            }
        }

        private bool bootloaderGetOut(byte magicByte)
        {
            BlockingCommandReceiver waiter = new BlockingCommandReceiver(CommandType.SYS_RESET_RESPONSE, _commandInterface);

            try
            {
                _commandInterface.SendRaw(new byte[] { magicByte });
            }
            catch (IOException e)
            {
                _logger.Error("Failed to send bootloader magic byte", e);
            }

            SYS_RESET_RESPONSE response = (SYS_RESET_RESPONSE)waiter.GetCommand(ResetTimeout);

            return response != null;
        }

        private string GetStackVersion()
        {
            if (!WaitForHardware())
            {
                _logger.Info("Failed to reach the {} level: getStackVerion() failed", DriverStatus.NETWORK_READY);
                return null;
            }

            SYS_VERSION_RESPONSE response = (SYS_VERSION_RESPONSE)SendSynchronous(new SYS_VERSION());
            if (response == null)
            {
                return null;
            }
            else
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("Software=");
                builder.Append(response.MajorRel);
                builder.Append(".");
                builder.Append(response.MinorRel);
                builder.Append(" Product=");
                builder.Append(response.Product);
                builder.Append(" Hardware=");
                builder.Append(response.HwRev);
                builder.Append(" Transport=");
                builder.Append(response.TransportRev);
                return builder.ToString();
            }
        }

        private bool DongleReset()
        {
            BlockingCommandReceiver waiter = new BlockingCommandReceiver(CommandType.SYS_RESET_RESPONSE, _commandInterface);

            try
            {
                _commandInterface.SendAsynchronousCommand(new SYS_RESET(SYS_RESET.RESET_TYPE.SERIAL_BOOTLOADER));
            }
            catch (IOException e)
            {
                _logger.Error("Failed to send SYS_RESET", e);
                return false;
            }

            SYS_RESET_RESPONSE response = (SYS_RESET_RESPONSE)waiter.GetCommand(ResetTimeout);

            return response != null;
        }

        private bool DongleSetStartupOption(ulong mask)
        {
            if ((mask & ~(STARTOPT_CLEAR_CONFIG | STARTOPT_CLEAR_STATE)) != 0)
            {
                _logger.Warn("Invalid ZCD_NV_STARTUP_OPTION mask {}.", String.Format("%08X", mask));
                return false;
            }

            ZB_WRITE_CONFIGURATION_RSP response;
            response = (ZB_WRITE_CONFIGURATION_RSP)SendSynchronous(
                    new ZB_WRITE_CONFIGURATION(ZB_WRITE_CONFIGURATION.CONFIG_ID.ZCD_NV_STARTUP_OPTION, BitConverter.GetBytes(mask)));

            if (response == null || response.Status != 0)
            {
                _logger.Warn("Couldn't set ZCD_NV_STARTUP_OPTION mask {}", string.Format("%08X", mask));
                return false;
            }
            else
            {
                _logger.Trace("Set ZCD_NV_STARTUP_OPTION mask {}", string.Format("%08X", mask));
            }

            return true;
        }

        private byte[] BuildChannelMask(int channel)
        {
            if (channel < 11 || channel > 27)
            {
                return new byte[] { 0, 0, 0, 0 };
            }

            int channelMask = 1 << channel;
            byte[] mask = new byte[4];

            for (int i = 0; i < mask.Length; i++)
            {
                mask[i] = BitConverter.GetBytes(channelMask)[i];
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
        private bool DongleSetChannel(byte[] channelMask)
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

            //_logger.Trace("Setting the channel to {}{}{}{}",
            //        new Object[] { Integer.toHexString(channelMask[0]), Integer.toHexString(channelMask[1]),
            //            Integer.toHexString(channelMask[2]), Integer.toHexString(channelMask[3]) });

            ZB_WRITE_CONFIGURATION_RSP response = (ZB_WRITE_CONFIGURATION_RSP)SendSynchronous(
                    new ZB_WRITE_CONFIGURATION(ZB_WRITE_CONFIGURATION.CONFIG_ID.ZCD_NV_CHANLIST, channelMask));

            return response != null && response.Status == 0;
        }

        private bool DongleSetChannel()
        {
            byte[] channelMask = BuildChannelMask(_channel);

            return DongleSetChannel(channelMask);
        }

        private bool DongleSetNetworkMode()
        {
            ZB_WRITE_CONFIGURATION_RSP response = (ZB_WRITE_CONFIGURATION_RSP)SendSynchronous(new ZB_WRITE_CONFIGURATION(
                    ZB_WRITE_CONFIGURATION.CONFIG_ID.ZCD_NV_LOGICAL_TYPE, new byte[] { (byte)_mode }));

            return response != null && response.Status == 0;
        }

        private bool DongleSetPanId()
        {
            _currentPanId = ushort.MaxValue;

            ZB_WRITE_CONFIGURATION_RSP response = (ZB_WRITE_CONFIGURATION_RSP)SendSynchronous(
                    new ZB_WRITE_CONFIGURATION(ZB_WRITE_CONFIGURATION.CONFIG_ID.ZCD_NV_PANID, BitConverter.GetBytes(_pan)));

            return response != null && response.Status == 0;
        }

        private bool DongleSetExtendedPanId()
        {
            ZB_WRITE_CONFIGURATION_RSP response = (ZB_WRITE_CONFIGURATION_RSP)SendSynchronous(
                    new ZB_WRITE_CONFIGURATION(ZB_WRITE_CONFIGURATION.CONFIG_ID.ZCD_NV_EXTPANID, _extendedPanId.PanId));

            return response != null && response.Status == 0;
        }

        private bool DongleSetNetworkKey()
        {
            ZB_WRITE_CONFIGURATION_RSP response = (ZB_WRITE_CONFIGURATION_RSP)SendSynchronous(new ZB_WRITE_CONFIGURATION(
                    ZB_WRITE_CONFIGURATION.CONFIG_ID.ZCD_NV_PRECFGKEY, _networkKey));

            return response != null && response.Status == 0;
        }

        private bool dongleSetDistributeNetworkKey()
        {
            ZB_WRITE_CONFIGURATION_RSP response = (ZB_WRITE_CONFIGURATION_RSP)SendSynchronous(new ZB_WRITE_CONFIGURATION(
                    ZB_WRITE_CONFIGURATION.CONFIG_ID.ZCD_NV_PRECFGKEYS_ENABLE, new byte[] { _distributeNetworkKey ? (byte)0x00 : (byte)0x01 }));

            return response != null && response.Status == 0;
        }

        private bool dongleSetSecurityMode()
        {
            ZB_WRITE_CONFIGURATION_RSP response = (ZB_WRITE_CONFIGURATION_RSP)SendSynchronous(new ZB_WRITE_CONFIGURATION(
                    ZB_WRITE_CONFIGURATION.CONFIG_ID.ZCD_NV_SECURITY_MODE, new byte[] { (byte)_securityMode }));

            return response != null && response.Status == 0;
        }

        /**
         * Sends a command without waiting for the response
         *
         * @param request {@link ZToolPacket}
         */
        public void SendCommand(SerialPacket request)
        {
            SendSynchronous(request);
        }

        private SerialPacket SendSynchronous(SerialPacket request)
        {
            return SendSynchronous(request, RESEND_TIMEOUT);
        }

        private SerialPacket SendSynchronous(SerialPacket request, int timeout)
        {
            SerialPacket[] response = new SerialPacket[] { null };
            // final int RESEND_MAX_RETRY = 3;
            int sending = 1;

            _logger.Trace("{} sending as synchronous command.", request.GetType().Name);

            SynchronousCommandListener listener = new SynchronousCommandListener();

            listener.OnResponseReceived += (object sender, SerialPacket packet) =>
            {
                response[0] = packet;
            };

            //    public void receivedCommandResponse(ZToolPacket packet)
            //    {
            //        _logger.Trace(" {} received as synchronous command.", packet.GetType().Name);
            //        synchronized(response) {
            //            // Do not set response[0] again.
            //            response[0] = packet;
            //            response.notify();
            //        }
            //    }
            //};

            while (sending <= RESEND_MAX_RETRY)
            {
                try
                {
                    try
                    {
                        _commandInterface.SendSynchronousCommand(request, listener, timeout);
                    }
                    catch (IOException e)
                    {
                        _logger.Error("Synchronous command send failed due to IO exception. ", e);
                        break;
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Synchronous command send failed due to unexpected exception.", ex);
                    }
                    _logger.Trace("{} sent (synchronous command, attempt {}).", request.GetType().Name, sending);
                    lock (response)
                    {
                        long wakeUpTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + timeout;
                        while (response[0] == null && wakeUpTime > DateTimeOffset.UtcNow.ToUnixTimeMilliseconds())
                        {
                            long sleeping = wakeUpTime - DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                            _logger.Trace("Waiting for synchronous command up to {}ms till {} Unixtime", sleeping,
                                    wakeUpTime);
                            if (sleeping <= 0)
                            {
                                break;
                            }
                            try
                            {
                                Monitor.Wait(sleeping);
                            }
                            catch (Exception ignored)
                            {
                            }
                        }
                    }
                    if (response[0] != null)
                    {
                        _logger.Trace("{} -> {}", request.GetType().Name,
                                response[0].GetType().Name);
                        break; // Break out as we have response.
                    }
                    else
                    {
                        _logger.Debug("{} executed and timed out while waiting for response.",
                                request.GetType().Name);
                    }
                    if (ResendOnlyException)
                    {
                        break;
                    }
                    else
                    {
                        _logger.Debug("Failed to send {} [attempt {}]", request.GetType().Name, sending);
                        sending++;
                    }
                }
                catch (Exception ignored)
                {
                    _logger.Debug("Failed to send {} [attempt {}]", request.GetType().Name, sending);
                    _logger.Trace("Sending operation failed due to ", ignored);
                    sending++;
                }
            }

            return response[0];
        }

        public AF_REGISTER_SRSP SendAFRegister(AF_REGISTER request)
        {
            if (!WaitForNetwork())
            {
                return null;
            }

            AF_REGISTER_SRSP response = (AF_REGISTER_SRSP)SendSynchronous(request);
            return response;
        }

        public AF_DATA_CONFIRM SendAFDataRequest(AF_DATA_REQUEST request)
        {
            if (!WaitForNetwork())
            {
                return null;
            }
            AF_DATA_CONFIRM result = null;

            WaitAndLock3WayConversation(request);
            BlockingCommandReceiver waiter = new BlockingCommandReceiver(CommandType.AF_DATA_CONFIRM, _commandInterface);

            AF_DATA_SRSP response = (AF_DATA_SRSP)SendSynchronous(request);
            if (response == null || response.Status != 0)
            {
                waiter.Cleanup();
            }
            else
            {
                result = (AF_DATA_CONFIRM)waiter.GetCommand(Timeout);
            }
            UnLock3WayConversation(request);

            return result;
        }

        /**
         * Sends an Application Framework data request and waits for the response.
         *
         * @param request {@link AF_DATA_REQUEST}
         */
        public AF_DATA_SRSP_EXT sendAFDataRequestExt(AF_DATA_REQUEST_EXT request)
        {
            if (!WaitForNetwork())
            {
                return null;
            }
            AF_DATA_SRSP_EXT response = (AF_DATA_SRSP_EXT)SendSynchronous(request);
            return response;
        }

        /**
         * Removes an Application Framework message listener that was previously added with the addAFMessageListener method
         *
         * @param listener a class that implements the {@link ApplicationFrameworkMessageListener} interface
         * @return true if the listener was added
         */
        public bool removeAFMessageListener(IApplicationFrameworkMessageListener listener)
        {
            bool result;
            lock (_messageListeners)
            {
                result = _messageListeners.Remove(listener);
            }

            if (_messageListeners.Count == 0 && IsHardwareReady())
            {
                if (_commandInterface.RemoveAsynchronousCommandListener(_afMessageListenerFilter))
                {
                    _logger.Trace("Removed AsynchrounsCommandListener {} to ZigBeeSerialInterface",
                            _afMessageListenerFilter.GetType().Name);
                }
                else
                {
                    _logger.Warn("Could not remove AsynchrounsCommandListener {} to ZigBeeSerialInterface",
                            _afMessageListenerFilter.GetType().Name);
                }
            }
            if (result)
            {
                _logger.Trace("Removed ApplicationFrameworkMessageListener {}:{}", listener,
                        listener.GetType().Name);
                return true;
            }
            else
            {
                _logger.Warn("Could not remove ApplicationFrameworkMessageListener {}:{}", listener,
                        listener.GetType().Name);
                return false;
            }
        }

        /**
         * Adds an Application Framework message listener
         *
         * @param listener a class that implements the {@link ApplicationFrameworkMessageListener} interface
         * @return true if the listener was added
         */
        public bool AddAFMessageListener(IApplicationFrameworkMessageListener listener)
        {
            lock (_messageListeners)
            {
                if (_messageListeners.Contains(listener))
                {
                    return true;
                }
            }
            if (_messageListeners.Count == 0 && IsHardwareReady())
            {
                if (_commandInterface.AddAsynchronousCommandListener(_afMessageListenerFilter))
                {
                    _logger.Trace("Added AsynchrounsCommandListener {} to ZigBeeSerialInterface",
                            _afMessageListenerFilter.GetType().Name);
                }
                else
                {
                    _logger.Trace("Could not add AsynchrounsCommandListener {} to ZigBeeSerialInterface",
                            _afMessageListenerFilter.GetType().Name);
                }
            }
            lock (_messageListeners)
            {
                _messageListeners.Add(listener);
            }

            _logger.Trace("Added ApplicationFrameworkMessageListener {}:{}", listener, listener.GetType().Name);

            return true;
        }

        private bool IsNetworkReady()
        {
            return _state == DriverStatus.NETWORK_READY;
        }

        private bool IsHardwareReady()
        {
            return _state == DriverStatus.HARDWARE_READY || _state == DriverStatus.NETWORK_INITIALIZING || _state == DriverStatus.NETWORK_READY;
        }

        /**
         * Gets the extended PAN ID
         *
         * @return the PAN ID or -1 on failure
         */
        //public ExtendedPanId GetCurrentExtendedPanId()
        //{
        //    if (!WaitForHardware())
        //    {
        //        _logger.Info("Failed to reach the {} level: getExtendedPanId() failed", DriverStatus.HARDWARE_READY);
        //        return new ExtendedPanId();
        //    }

        //    byte[] result = GetDeviceInfo(ZB_GET_DEVICE_INFO.DEV_INFO_TYPE.EXT_PAN_ID);

        //    if (result == null)
        //    {
        //        // luckily -1 (aka 0xffffffffffffffffL) is not a valid extended PAN ID value
        //        return new ExtendedPanId();
        //    }
        //    else
        //    {
        //        return new ExtendedPanId(result);
        //    }
        //}

        /**
         * Gets the IEEE address of our node on the network
         *
         * @return the IEEE address as a long or -1 on failure
         */
        public ulong GetIeeeAddress()
        {
            if (_ieeeAddress != ulong.MaxValue)
            {
                return _ieeeAddress;
            }

            if (!WaitForHardware())
            {
                _logger.Info("Failed to reach the {} level: getIeeeAddress() failed", DriverStatus.HARDWARE_READY);
                return ulong.MaxValue;
            }

            byte[] result = GetDeviceInfo(ZB_GET_DEVICE_INFO.DEV_INFO_TYPE.IEEE_ADDR);

            if (result == null)
            {
                return ulong.MaxValue;
            }
            else
            {
                _ieeeAddress = new IeeeAddress(result).Value;
                return _ieeeAddress;
            }
        }

        /**
         * Gets the current PAN ID
         *
         * @return current PAN ID as an int or -1 on failure
         */
        public ushort GetCurrentPanId()
        {
            if (!WaitForHardware())
            {
                _logger.Info("Failed to reach the {} level: getCurrentPanId() failed", DriverStatus.NETWORK_READY);
                return ushort.MaxValue;
            }

            if (_currentPanId != ushort.MaxValue)
            {
                return _currentPanId;
            }

            byte[] result = GetDeviceInfo(ZB_GET_DEVICE_INFO.DEV_INFO_TYPE.PAN_ID);
            if (result == null)
            {
                return ushort.MaxValue;
            }
            else
            {
                _currentPanId = new ZigBeeAddress16(result).Value;
                return _currentPanId;
            }
        }

        /**
         * Gets the current ZigBee channe number
         *
         * @return the current channel as an int, or -1 on failure
         */
        public byte GetCurrentChannel()
        {
            if (!WaitForHardware())
            {
                _logger.Info("Failed to reach the {} level: getCurrentChannel() failed", DriverStatus.HARDWARE_READY);
                return byte.MaxValue;
            }

            byte[] result = GetDeviceInfo(ZB_GET_DEVICE_INFO.DEV_INFO_TYPE.CHANNEL);
            if (result == null)
            {
                return byte.MaxValue;
            }
            else
            {
                return result[0];
            }
        }

        private byte[] GetDeviceInfo(ZB_GET_DEVICE_INFO.DEV_INFO_TYPE type)
        {
            ZB_GET_DEVICE_INFO_RSP response = (ZB_GET_DEVICE_INFO_RSP)SendSynchronous(new ZB_GET_DEVICE_INFO(type));

            if (response == null)
            {
                _logger.Warn("Failed GetDeviceInfo for {} due to null value", type);
                return null;
            }
            else if (response.Param != type)
            {
                _logger.Warn("Failed GetDeviceInfo for {} non matching response returned {}", type, response.Param);
                return null;
            }
            else
            {
                _logger.Trace("GetDeviceInfo for {} done", type);
                return response.Value;
            }
        }

        public int GetZigBeeNodeMode()
        {
            ZB_READ_CONFIGURATION_RSP response = (ZB_READ_CONFIGURATION_RSP)SendSynchronous(
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

        public byte[] GetZigBeeNetworkKey()
        {
            ZB_READ_CONFIGURATION_RSP response = (ZB_READ_CONFIGURATION_RSP)SendSynchronous(
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
                _logger.Error("Error reading zigbee network key: " + response.Status);
                return null;
            }
        }

        public DriverStatus GetDriverStatus()
        {
            return _state;
        }

        private void CreateCustomDevicesOnDongle()
        {
            DoubleByte[] input;
            DoubleByte[] output;

            if (_ep != null)
            {
                for (int i = 0; i < _ep.Length; i++)
                {
                    // input
                    int size = 0;
                    for (int j = 0; j < _inp[i].Length; j++)
                    {

                        if (_inp[i][j] != 0 && _inp[i][j] != ushort.MaxValue)
                        {
                            size++;
                        }
                    }

                    input = new DoubleByte[size];
                    for (int j = 0; j < _inp[i].Length; j++)
                    {
                        if (_inp[i][j] != 0 && _inp[i][j] != ushort.MaxValue)
                        {
                            input[j] = new DoubleByte(_inp[i][j]);
                        }
                    }

                    // output
                    size = 0;
                    for (int j = 0; j < _out[i].Length; j++)
                    {

                        if (_out[i][j] != 0 && _out[i][j] != ushort.MaxValue)
                        {
                            size++;
                        }
                    }

                    output = new DoubleByte[size];

                    for (int j = 0; j < _out[i].Length; j++)
                    {
                        if (_out[i][j] != 0 && _out[i][j] != ushort.MaxValue)
                        {
                            output[j] = new DoubleByte(_out[i][j]);
                        }
                    }

                    if (newDevice(new AF_REGISTER(_ep[i], new DoubleByte(_prof[i]), new DoubleByte(_dev[i]), _ver[i], input, output)))
                    {
                        _logger.Debug("Custom device {} registered at endpoint {}", _dev[i], _ep[i]);
                    }
                    else
                    {
                        _logger.Debug("Custom device {} registration failed at endpoint {}", _dev[i], _ep[i]);
                    }
                }
            }
        }

        private bool newDevice(AF_REGISTER request)
        {
            try
            {
                AF_REGISTER_SRSP response = (AF_REGISTER_SRSP)SendSynchronous(request);
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
