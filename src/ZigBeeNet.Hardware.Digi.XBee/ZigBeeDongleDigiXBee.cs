using System;
using ZigBeeNet.Hardware.Digi.XBee.Internal;
using ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol;
using Serilog;
using ZigBeeNet.Security;
using ZigBeeNet.Transport;

namespace ZigBeeNet.Hardware.Digi.XBee
{
    public class ZigBeeDongleDigiXBee : IZigBeeTransportTransmit, IXBeeEventListener
    {
        private readonly IZigBeePort _serialPort;
        private XBeeFrameHandler _frameHandler;
        private readonly IZigBeeTransportReceive _zigBeeTransportReceive;
        private readonly ZigBeeKey _linkKey = new ZigBeeKey(new byte[] { 0x5A, 0x69, 0x67, 0x42, 0x65, 0x65, 0x41, 0x6C, 0x6C, 0x69, 0x61, 0x6E, 0x63, 0x65, 0x30, 0x39 });
        private readonly ZigBeeKey _networkKey = new ZigBeeKey();
        private readonly ZigBeeChannel _radioChannel;
        private readonly ExtendedPanId _extendedPanId;
        private readonly bool _coordinatorStarted;
        private readonly bool _initialisationComplete;

        private readonly IeeeAddress _groupIeeeAddress = new IeeeAddress("000000000000FFFE");
        private readonly IeeeAddress _broadcastIeeeAddress = new IeeeAddress("000000000000FFFF");

        private const int MAX_RESET_RETRIES = 3;

        public ZigBeeDongleDigiXBee(IZigBeePort serialPort)
        {
            _serialPort = serialPort;
        }

        public string VersionString { get; set; } = "Unknown";
        public IeeeAddress IeeeAddress { get; set; }
        public ushort NwkAddress { get; set; }

        public ZigBeeChannel ZigBeeChannel => throw new NotImplementedException();

        public ushort PanID { get; private set; }

        public ExtendedPanId ExtendedPanId { get; private set; }

        public ZigBeeKey ZigBeeNetworkKey => throw new NotImplementedException();

        public ZigBeeKey TcLinkKey => throw new NotImplementedException();

        public ZigBeeStatus Initialize()
        {
            Log.Debug("XBee device initialize.");

            if (!_serialPort.Open())
            {
                Log.Error("Unable to open XBee serial port");
                return ZigBeeStatus.COMMUNICATION_ERROR;
            }

            _frameHandler = new XBeeFrameHandler();
            _frameHandler.Start(_serialPort);
            //_frameHandler.AddEventListener(this);

            int resetCount = 0;
            //do
            //{
            //    if (resetCount >= MAX_RESET_RETRIES)
            //    {
            //        Log.Information($"XBee device reset failed after {++resetCount}");
            //        return ZigBeeStatus.NO_RESPONSE;
            //    }
            //    Log.Debug($"XBee device reset {++resetCount}");
            //    XBeeSetSoftwareResetCommand resetCommand = new XBeeSetSoftwareResetCommand();
            //    _frameHandler.SendRequest(resetCommand);
            //} while (_frameHandler.EventWait(XBeeModemStatusEvent) == null);


            XBeeSetApiEnableCommand apiEnableCommand = new XBeeSetApiEnableCommand();
            //apiEnableCommand.setMode(2);
            //_frameHandler.sendRequest(apiEnableCommand);

            XBeeSetApiModeCommand apiModeCommand = new XBeeSetApiModeCommand();
            //apiModeCommand.SetMode(3);
            //_frameHandler.SendRequest(apiModeCommand);

            XBeeGetHardwareVersionCommand hwVersionCommand = new XBeeGetHardwareVersionCommand();
            //_frameHandler.SendRequest(hwVersionCommand);

            //xBeeGetFirmwareVersionCommand fwVersionCommand = new xBeeGetFirmwareVersionCommand();
            //_frameHandler.SendRequest(fwVersionCommand);

            XBeeGetDetailedVersionCommand versionCommand = new XBeeGetDetailedVersionCommand();
            //_frameHandler.SendRequest(versionCommand);

            XBeeGetIeeeAddressHighCommand ieeeHighCommand = new XBeeGetIeeeAddressHighCommand();
            //XBeeIeeeAddressHighResponse ieeeHighResponse = (XBeeIeeeAddressHighResponse)_frameHandler.SendRequest(ieeeHighCommand);

            XBeeGetIeeeLowCommand ieeeLowCommand = new XBeeGetIeeeLowCommand();
            //XBeeIeeeAddressLowCommand ieeeLowResponse = (XBeeIeeeAddressLowCommand)_frameHandler.SendRequest(ieeeLowCommand);

            //if (ieeeHighResponse == null || ieeeLowCommand == null)
            //{
            //    Log.Error("Unable to get XBee IEEE address");
            //    return ZigBeeStatus.BAD_RESPONSE;
            //}

            //byte[] tmpAddress = new byte[8];
            //tmpAddress[0] = ieeeLowResponse.GetIeeeAddress()[3];
            //tmpAddress[1] = ieeeLowResponse.GetIeeeAddress()[2];
            //tmpAddress[2] = ieeeLowResponse.GetIeeeAddress()[1];
            //tmpAddress[3] = ieeeLowResponse.GetIeeeAddress()[0];
            //tmpAddress[4] = ieeeHighResponse.GetIeeeAddress()[3];
            //tmpAddress[5] = ieeeHighResponse.GetIeeeAddress()[2];
            //tmpAddress[6] = ieeeHighResponse.GetIeeeAddress()[1];
            //tmpAddress[7] = ieeeHighResponse.GetIeeeAddress()[0];
            //IeeeAddress = new IeeeAddress(tmpAddress);

            //Log.Debug($"XBee IeeeAddress={IeeeAddress}");

            //XBeeSetZigbeeStackProfileCommand stackProfile = new XBeeSetZigbeeStackProfileCommand();
            //stackProfile.SetStackProfile(2);
            //_frameHandler.SendRequest(stackProfile);

            //XBeeSetEncryptionEnableCommand enableEncryption = new XBeeSetEncryptionEnableCommand();
            //enableEncryption.SetEnableEncryption(true);
            //_frameHandler.SendRequest(enableEncryption);

            //XBeeSetEncryptionOptionsCommand encryptionOptions = new XBeeSetEncryptionOptionsCommand();
            //encryptionOptions.AddEncryptionOptions(EncryptionOptions.ENABLE_TRUST_CENTRE);
            //_frameHandler.SendRequest(encryptionOptions);

            //XBeeSetCoordinatorEnableCommand coordinatorEnable = new XBeeSetCoordinatorEnableCommand();
            //coordinatorEnable.SetEnable(true);
            //_frameHandler.SendRequest(coordinatorEnable);

            //XBeeSetNetworkKeyCommand networkKey = new XBeeSetNetworkKeyCommand();
            //networkKey.SetNetworkKey(new ZigBeeKey());
            //_frameHandler.SendRequest(networkKey);

            //XBeeSetLinkKeyCommand setLinkKey = new XBeeSetLinkKeyCommand();
            //setLinkKey.SetLinkKey(_linkKey);
            //_frameHandler.SendRequest(setLinkKey);

            //XBeeSetSaveDataCommand saveData = new XBeeSetSaveDataCommand();
            //_frameHandler.SendRequest(saveData);

            //XBeeGetPanIdCommand getPanId = new XBeeGetPanIdCommand();
            //XBeePanIdResponse panIdResponse = (XBeePanIdResponse)_frameHandler.SendRequest(getPanId);
            //PanID = panIdResponse.GetPanId();

            //XBeeGetExtendedPanIdCommand getEPanId = new XBeeGetExtendedPanIdCommand();
            //XBeeExtendedPanIdResponse epanIdResponse = (XBeeExtendedPanIdResponse)_frameHandler.SendRequest(getEPanId);
            //ExtendedPanId = epanIdResponse.GetExtendedPanId();

            return ZigBeeStatus.SUCCESS;


            // Todo af: Hier gehts weiter 
            // https://github.com/zsmartsystems/com.zsmartsystems.zigbee/blob/master/com.zsmartsystems.zigbee.dongle.xbee/src/main/java/com/zsmartsystems/zigbee/dongle/xbee/ZigBeeDongleXBee.java
            throw new NotImplementedException();
        }

        public void SendCommand(ZigBeeApsFrame apsFrame)
        {
            throw new NotImplementedException();
        }

        public ZigBeeStatus SetTcLinkKey(ZigBeeKey key)
        {
            throw new NotImplementedException();
        }

        public ZigBeeStatus SetZigBeeChannel(ZigBeeChannel channel)
        {
            throw new NotImplementedException();
        }

        public ZigBeeStatus SetZigBeeExtendedPanId(ExtendedPanId panId)
        {
            throw new NotImplementedException();
        }

        public ZigBeeStatus SetZigBeeNetworkKey(ZigBeeKey key)
        {
            throw new NotImplementedException();
        }

        public ZigBeeStatus SetZigBeePanId(ushort panId)
        {
            throw new NotImplementedException();
        }

        public void SetZigBeeTransportReceive(IZigBeeTransportReceive zigBeeTransportReceive)
        {
            throw new NotImplementedException();
        }

        public void Shutdown()
        {
            throw new NotImplementedException();
        }

        public ZigBeeStatus Startup(bool reinitialize)
        {
            throw new NotImplementedException();
        }

        public void UpdateTransportConfig(TransportConfig configuration)
        {
            throw new NotImplementedException();
        }

        public void XbeeEventReceived(IXBeeEvent xbeeEvent)
        {
            throw new NotImplementedException();
        }
    }
}
