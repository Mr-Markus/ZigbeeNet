using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using ZigBeeNet.Hardware.Digi.XBee.Internal;
using ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol;
using ZigBeeNet.Security;
using ZigBeeNet.Transport;

namespace ZigBeeNet.Hardware.Digi.XBee
{
    public class ZigBeeDongleXBee : IZigBeeTransportTransmit, IXBeeEventListener
    {
        #region private fields

        private readonly IZigBeePort _serialPort;
        private IXBeeFrameHandler _frameHandler;
        private IZigBeeTransportReceive _zigBeeTransportReceive;
        private readonly ZigBeeChannel _radioChannel;
        private readonly ExtendedPanId _extendedPanId;
        private bool _coordinatorStarted;
        private bool _initialisationComplete;

        private readonly IeeeAddress _groupIeeeAddress = new IeeeAddress(BigInteger.Parse("000000000000FFFE", NumberStyles.HexNumber));
        private readonly IeeeAddress _broadcastIeeeAddress = new IeeeAddress(BigInteger.Parse("000000000000FFFF", NumberStyles.HexNumber));

        private const int MAX_RESET_RETRIES = 3;

        #endregion private fields

        #region constructor

        public ZigBeeDongleXBee(IZigBeePort serialPort)
        {
            _serialPort = serialPort;
        }

        #endregion constructor

        #region properties

        public string VersionString { get; set; } = "Unknown";
        public IeeeAddress IeeeAddress { get; set; }
        public ushort NwkAddress { get; set; }
        public ZigBeeKey LinkKey { get; private set; } = new ZigBeeKey(new byte[] { 0x5A, 0x69, 0x67, 0x42, 0x65, 0x65, 0x41, 0x6C, 0x6C, 0x69, 0x61, 0x6E, 0x63, 0x65, 0x30, 0x39 });
        public ZigBeeKey NetworkKey { get; private set; } = new ZigBeeKey();

        public ZigBeeChannel ZigBeeChannel
        {
            get
            {
                if (_frameHandler == null)
                {
                    return ZigBeeChannel.UNKNOWN;
                }
                XBeeGetOperatingChannelCommand request = new XBeeGetOperatingChannelCommand();
                XBeeOperatingChannelResponse response = (XBeeOperatingChannelResponse)_frameHandler.SendRequest(request);

                return (ZigBeeChannel)response.GetChannel();
            }
        }

        public ushort PanID { get; private set; }

        public ExtendedPanId ExtendedPanId { get; private set; }

        public ZigBeeKey ZigBeeNetworkKey { get; private set; }

        public ZigBeeKey TcLinkKey { get; private set; }

        #endregion properties

        #region methods

        public ZigBeeStatus Initialize()
        {
            Log.Debug("XBee device initialize.");

            if (!_serialPort.Open())
            {
                Log.Error("Unable to open XBee serial port");
                return ZigBeeStatus.COMMUNICATION_ERROR;
            }

            // Create and start the frame handler
            _frameHandler = new XBeeFrameHandler();
            _frameHandler.Start(_serialPort);
            _frameHandler.AddEventListener(this);

            // Reset to a known state
            // Device sends WATCHDOG_TIMER_RESET event
            // A retry mechanism is used as sometimes the reset response is not received.
            // This appears to happen if there are other events queued in the stick.
            int resetCount = 0;
            do
            {
                if (resetCount >= MAX_RESET_RETRIES)
                {
                    Log.Information($"XBee device reset failed after {++resetCount}");
                    return ZigBeeStatus.NO_RESPONSE;
                }
                Log.Debug($"XBee device reset {++resetCount}");
                XBeeSetSoftwareResetCommand resetCommand = new XBeeSetSoftwareResetCommand();
                _frameHandler.SendRequest(resetCommand);
            } while (_frameHandler.EventWait(typeof(XBeeModemStatusEvent)) == null);
            

            // Enable the API with escaping
            XBeeSetApiEnableCommand apiEnableCommand = new XBeeSetApiEnableCommand();
            apiEnableCommand.SetMode(2);
            _frameHandler.SendRequest(apiEnableCommand);

            // Set the API mode so we receive detailed data including ZDO
            XBeeSetApiModeCommand apiModeCommand = new XBeeSetApiModeCommand();
            apiModeCommand.SetMode(3);
            _frameHandler.SendRequest(apiModeCommand);

            // Get the product information
            XBeeGetHardwareVersionCommand hwVersionCommand = new XBeeGetHardwareVersionCommand();
            _frameHandler.SendRequest(hwVersionCommand);

            XBeeGetFirmwareVersionCommand fwVersionCommand = new XBeeGetFirmwareVersionCommand();
            _frameHandler.SendRequest(fwVersionCommand);

            XBeeGetDetailedVersionCommand versionCommand = new XBeeGetDetailedVersionCommand();
            _frameHandler.SendRequest(versionCommand);

            // Get Ieee Address
            XBeeGetIeeeAddressHighCommand ieeeHighCommand = new XBeeGetIeeeAddressHighCommand();
            XBeeIeeeAddressHighResponse ieeeHighResponse = (XBeeIeeeAddressHighResponse)_frameHandler.SendRequest(ieeeHighCommand);

            XBeeGetIeeeAddressLowCommand ieeeLowCommand = new XBeeGetIeeeAddressLowCommand();
            XBeeIeeeAddressLowResponse ieeeLowResponse = (XBeeIeeeAddressLowResponse)_frameHandler.SendRequest(ieeeLowCommand);

            if (ieeeHighResponse == null || ieeeLowCommand == null)
            {
                Log.Error("Unable to get XBee IEEE address");
                return ZigBeeStatus.BAD_RESPONSE;
            }

            byte[] tmpAddress = new byte[8];
            tmpAddress[0] = (byte)ieeeLowResponse.GetIeeeAddress()[3];
            tmpAddress[1] = (byte)ieeeLowResponse.GetIeeeAddress()[2];
            tmpAddress[2] = (byte)ieeeLowResponse.GetIeeeAddress()[1];
            tmpAddress[3] = (byte)ieeeLowResponse.GetIeeeAddress()[0];
            tmpAddress[4] = (byte)ieeeHighResponse.GetIeeeAddress()[3];
            tmpAddress[5] = (byte)ieeeHighResponse.GetIeeeAddress()[2];
            tmpAddress[6] = (byte)ieeeHighResponse.GetIeeeAddress()[1];
            tmpAddress[7] = (byte)ieeeHighResponse.GetIeeeAddress()[0];
            IeeeAddress = new IeeeAddress(tmpAddress);

            Log.Debug($"XBee IeeeAddress={IeeeAddress}");

            // Set the ZigBee stack profile
            XBeeSetZigbeeStackProfileCommand stackProfile = new XBeeSetZigbeeStackProfileCommand();
            stackProfile.SetStackProfile(2);
            _frameHandler.SendRequest(stackProfile);

            // Enable Security
            XBeeSetEncryptionEnableCommand enableEncryption = new XBeeSetEncryptionEnableCommand();
            enableEncryption.SetEnableEncryption(true);
            _frameHandler.SendRequest(enableEncryption);

            XBeeSetEncryptionOptionsCommand encryptionOptions = new XBeeSetEncryptionOptionsCommand();
            encryptionOptions.AddEncryptionOptions(EncryptionOptions.ENABLE_TRUST_CENTRE);
            _frameHandler.SendRequest(encryptionOptions);

            // Enable coordinator mode
            XBeeSetCoordinatorEnableCommand coordinatorEnable = new XBeeSetCoordinatorEnableCommand();
            coordinatorEnable.SetEnable(true);
            _frameHandler.SendRequest(coordinatorEnable);

            // Set the network key
            XBeeSetNetworkKeyCommand networkKey = new XBeeSetNetworkKeyCommand();
            networkKey.SetNetworkKey(new ZigBeeKey());
            _frameHandler.SendRequest(networkKey);

            // Set the link key
            XBeeSetLinkKeyCommand setLinkKey = new XBeeSetLinkKeyCommand();
            setLinkKey.SetLinkKey(LinkKey);
            _frameHandler.SendRequest(setLinkKey);

            // Save the configuration in the XBee
            XBeeSetSaveDataCommand saveData = new XBeeSetSaveDataCommand();
            _frameHandler.SendRequest(saveData);

            // Get network information
            XBeeGetPanIdCommand getPanId = new XBeeGetPanIdCommand();
            XBeePanIdResponse panIdResponse = (XBeePanIdResponse)_frameHandler.SendRequest(getPanId);
            // TODO: Check if cast is appropriate. Possible loss of data.
            PanID = (ushort)panIdResponse.GetPanId();

            XBeeGetExtendedPanIdCommand getEPanId = new XBeeGetExtendedPanIdCommand();
            XBeeExtendedPanIdResponse epanIdResponse = (XBeeExtendedPanIdResponse)_frameHandler.SendRequest(getEPanId);
            ExtendedPanId = epanIdResponse.GetExtendedPanId();

            return ZigBeeStatus.SUCCESS;
        }

        public ZigBeeStatus Startup(bool reinitialize)
        {
            Log.Debug("XBee dongle startup.");

            // If frameHandler is null then the serial port didn't initialise
            if (_frameHandler == null)
            {
                Log.Error("Initialising XBee Dongle but low level handler is not initialised.");
                return ZigBeeStatus.INVALID_STATE;
            }

            // If we want to reinitialize the network, then go...
            if (reinitialize)
            {
                Log.Debug("Reinitialising XBee dongle and forming network.");
                InitialiseNetwork();
            }

            // Check if the network is now up

            // radioChannel = networkInfo.getChannel();
            // panId = networkInfo.getPanId();
            // extendedPanId = networkInfo.getEpanId();

            _initialisationComplete = true;

            return _coordinatorStarted ? ZigBeeStatus.SUCCESS : ZigBeeStatus.BAD_RESPONSE;
        }

        public void Shutdown()
        {
            if (_frameHandler == null)
            {
                return;
            }
            _frameHandler.SetClosing();
            _zigBeeTransportReceive.SetNetworkState(ZigBeeTransportState.OFFLINE);

            _serialPort.Close();
            _frameHandler.Close();
            Log.Debug("XBee dongle shutdown.");
        }

        private void InitialiseNetwork()
        {
            // TODO
        }

        public void SendCommand(ZigBeeApsFrame apsFrame)
        {
            if (_frameHandler == null)
            {
                Log.Debug("XBee frame handler not set for send.");
                return;
            }

            XBeeTransmitRequestExplicitCommand command = new XBeeTransmitRequestExplicitCommand();
            command.SetNetworkAddress(apsFrame.DestinationAddress);
            command.SetDestinationEndpoint(apsFrame.DestinationEndpoint);
            command.SetSourceEndpoint(apsFrame.SourceEndpoint);
            command.SetProfileId(apsFrame.Profile);
            command.SetCluster(apsFrame.Cluster);
            command.SetBroadcastRadius(0);

            if (apsFrame.DestinationAddress > 0xFFF8)
            {
                command.SetIeeeAddress(_broadcastIeeeAddress);
            }
            else if (apsFrame.DestinationIeeeAddress == null)
            {
                if (apsFrame.AddressMode == ZigBeeNwkAddressMode.Group)
                {
                    command.SetIeeeAddress(_groupIeeeAddress);
                }
                command.SetIeeeAddress(new IeeeAddress(BigInteger.Parse("FFFFFFFFFFFFFFFF", NumberStyles.HexNumber)));
            }
            else
            {
                command.SetIeeeAddress(apsFrame.DestinationIeeeAddress);
            }

            if (apsFrame.SecurityEnabled)
            {
                command.AddOptions(TransmitOptions.ENABLE_APS_ENCRYPTION);
            }
            command.SetData(apsFrame.Payload.Select(item => (int)item).ToArray());

            Log.Debug($"XBee send: {{{command.ToString()}}}");
            _frameHandler.SendRequestAsync(command);
        }

        public void SetZigBeeTransportReceive(IZigBeeTransportReceive zigBeeTransportReceive)
        {
            _zigBeeTransportReceive = zigBeeTransportReceive;
        }

        public void XbeeEventReceived(IXBeeEvent xbeeEvent)
        {
            if (xbeeEvent is XBeeReceivePacketExplicitEvent rxMessage)
            {
                ZigBeeApsFrame apsFrame = new ZigBeeApsFrame
                {
                    Cluster = (ushort)rxMessage.GetClusterId(),
                    DestinationEndpoint = (byte)rxMessage.GetDestinationEndpoint(),
                    Profile = (ushort)rxMessage.GetProfileId(),
                    SourceEndpoint = (byte)rxMessage.GetSourceEndpoint(),

                    SourceAddress = (ushort)rxMessage.GetNetworkAddress(),
                    Payload = rxMessage.GetData().Select(item => (byte)item).ToArray()
                };

                _zigBeeTransportReceive.ReceiveCommand(apsFrame);
                return;
            }

            // Handle dongle status messages
            if (xbeeEvent is XBeeModemStatusEvent modemStatus)
            {
                ModemStatus modemCurrentStatus = modemStatus.GetStatus();
                switch (modemCurrentStatus)
                {
                    case ModemStatus.COORDINATOR_STARTED:
                        {
                            _coordinatorStarted = true;
                            SetNetworkState(ZigBeeTransportState.ONLINE);
                        }
                        break;
                    case ModemStatus.DISASSOCIATED:
                        {
                            SetNetworkState(ZigBeeTransportState.OFFLINE);
                        }
                        break;
                    case ModemStatus.HARDWARE_RESET:
                    case ModemStatus.JOINED_NETWORK:
                    case ModemStatus.NETWORK_SECURITY_KEY_UPDATED:
                    case ModemStatus.WATCHDOG_TIMER_RESET:
                        break;
                    default:
                        break;
                }
                return;
            }

            Log.Debug($"Unhandled XBee Frame: {xbeeEvent.ToString()}");
        }

        private void SetNetworkState(ZigBeeTransportState state)
        {
            if (_initialisationComplete)
            {
                _zigBeeTransportReceive.SetNetworkState(state);
            }
        }

        public ZigBeeStatus SetZigBeeChannel(ZigBeeChannel channel)
        {
            XBeeSetScanChannelsCommand request = new XBeeSetScanChannelsCommand();
            request.SetChannels((1 << ((int)channel - 11)));
            _frameHandler.SendRequest(request);

            return ZigBeeStatus.SUCCESS;
        }

        public ZigBeeStatus SetZigBeePanId(ushort panId)
        {
            PanID = panId;
            return ZigBeeStatus.SUCCESS;
        }

        public ZigBeeStatus SetZigBeeExtendedPanId(ExtendedPanId panId)
        {
            ExtendedPanId = panId;
            return ZigBeeStatus.SUCCESS;
        }

        public ZigBeeStatus SetZigBeeNetworkKey(ZigBeeKey key)
        {
            NetworkKey = key;
            return ZigBeeStatus.SUCCESS;
        }

        public ZigBeeStatus SetTcLinkKey(ZigBeeKey key)
        {
            LinkKey = key;
            return ZigBeeStatus.SUCCESS;
        }

        public void UpdateTransportConfig(TransportConfig configuration)
        {
            IList<TransportConfigOption> configurationOptions = configuration.GetOptions();
            foreach (TransportConfigOption option in configurationOptions)
            {
                try
                {
                    switch (option)
                    {
                        case TransportConfigOption.TRUST_CENTRE_LINK_KEY:
                            {
                                configuration.SetResult(option, SetTcLinkKey((ZigBeeKey)configuration.GetValue(option)));
                            }
                            break;
                        default:
                            {
                                configuration.SetResult(option, ZigBeeStatus.UNSUPPORTED);
                                Log.Debug($"Unsupported configuration option \"{option}\" in XBee dongle");
                            }
                            break;
                    }
                }
                catch (InvalidCastException e)
                {
                    configuration.SetResult(option, ZigBeeStatus.INVALID_ARGUMENTS);
                }
            }
        }

        #endregion methods
    }
}
