using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ZigBeeNet.Hardware.TI.CC2531.Frame;
using ZigBeeNet.Hardware.TI.CC2531.Implementation;
using ZigBeeNet.Hardware.TI.CC2531.Network;
using ZigBeeNet.Hardware.TI.CC2531.Packet;
using ZigBeeNet.Hardware.TI.CC2531.Packet.AF;
using ZigBeeNet.Hardware.TI.CC2531.Util;
using Serilog;
using ZigBeeNet.Security;
using ZigBeeNet.Transport;
using ZigBeeNet.ZCL;
using static ZigBeeNet.ZigBeeNetworkManager;

namespace ZigBeeNet.Hardware.TI.CC2531
{
    public class ZigBeeDongleTiCc2531 : IZigBeeTransportTransmit, IApplicationFrameworkMessageListener, IAsynchronousCommandListener
    {
        private NetworkManager _networkManager;
        private IZigBeeTransportReceive _zigBeeNetworkReceive;

        private readonly Dictionary<ushort, byte> _sender2Endpoint = new Dictionary<ushort, byte>();
        private readonly Dictionary<byte, ushort> _Endpoint2Profile = new Dictionary<byte, ushort>();

        private List<ushort> _supportedInputClusters = new List<ushort>();
        private List<ushort> _supportedOutputClusters = new List<ushort>();

        public string VersionString { get; set; }

        public IeeeAddress IeeeAddress { get; set; }

        public ushort NwkAddress { get; set; }


        public ZigBeeChannel ZigBeeChannel
        {
            get
            {
                return (ZigBeeChannel)_networkManager.GetCurrentChannel();
            }
        }

        public ushort PanID
        {
            get
            {
                return _networkManager.GetCurrentPanId();
            }
        }

        public ExtendedPanId ExtendedPanId
        {
            get
            {
                return _networkManager.GetCurrentExtendedPanId();
            }
        }

        public ZigBeeKey ZigBeeNetworkKey
        {
            get
            {
                return _networkManager.GetZigBeeNetworkKey();
            }
        }

        public ZigBeeKey TcLinkKey
        {
            get
            {
                return new ZigBeeKey();
            }
        }

        public ZigBeeDongleTiCc2531(IZigBeePort serialPort)
        {
            _networkManager = new NetworkManager(new CommandInterfaceImpl(serialPort), NetworkMode.Coordinator, 2500);
        }

        public void SetMagicNumber(byte magicNumber)
        {
            _networkManager.SetMagicNumber(magicNumber);
        }

        public ZigBeeStatus Initialize()
        {
            Log.Debug("CC2531 transport initialize");

            // This basically just initialises the hardware so we can communicate with the 2531
            VersionString = _networkManager.Startup();
            if (VersionString == null)
            {
                return ZigBeeStatus.COMMUNICATION_ERROR;
            }

            IeeeAddress = new IeeeAddress(_networkManager.GetIeeeAddress());

            return ZigBeeStatus.SUCCESS;
        }

        public bool Notify(AF_INCOMING_MSG msg)
        {
            ZigBeeApsFrame apsFrame = new ZigBeeApsFrame();
            apsFrame.Cluster = msg.ClusterId.Value;
            apsFrame.DestinationEndpoint = msg.DstEndpoint;
            apsFrame.SourceEndpoint = msg.SrcEndpoint;
            apsFrame.Profile = GetEndpointProfile(msg.DstEndpoint);
            apsFrame.SourceAddress = msg.SrcAddr.Value;
            apsFrame.Payload = msg.Data;

            _zigBeeNetworkReceive.ReceiveCommand(apsFrame);

            return true;
        }

        public void ReceivedAsynchronousCommand(ZToolPacket packet)
        {
            switch (packet.Subsystem)
            {
                case ZToolPacket.CommandSubsystem.AF:
                    return;
                case ZToolPacket.CommandSubsystem.ZDO:
                    break;
                default:
                    break;
            }

            ZigBeeApsFrame apsFrame = null;
            switch ((ZToolCMD)packet.CMD.Value)
            {
                case ZToolCMD.ZDO_MSG_CB_INCOMING:
                    apsFrame = ZdoCallbackIncoming.Create(packet);
                    break;
                case ZToolCMD.ZDO_IEEE_ADDR_RSP:
                    apsFrame = ZdoIeeeAddress.Create(packet);
                    break;
                case ZToolCMD.ZDO_END_DEVICE_ANNCE_IND:
                    apsFrame = ZdoEndDeviceAnnounce.Create(packet);
                    break;
                case ZToolCMD.ZDO_NODE_DESC_RSP:
                    apsFrame = ZdoNodeDescriptor.Create(packet);
                    break;
                case ZToolCMD.ZDO_POWER_DESC_RSP:
                    apsFrame = ZdoPowerDescriptor.Create(packet);
                    break;
                case ZToolCMD.ZDO_ACTIVE_EP_RSP:
                    apsFrame = ZdoActiveEndpoint.Create(packet);
                    break;
                case ZToolCMD.ZDO_SIMPLE_DESC_RSP:
                    apsFrame = ZdoSimpleDescriptor.Create(packet);
                    break;
                case ZToolCMD.ZDO_MGMT_LQI_RSP:
                    apsFrame = ZdoManagementLqi.Create(packet);
                    break;
                case ZToolCMD.ZDO_MGMT_RTG_RSP:
                    apsFrame = ZdoManagementRouting.Create(packet);
                    break;
                case ZToolCMD.ZDO_MGMT_LEAVE_RSP:
                    apsFrame = ZdoManagementLeave.Create(packet);
                    break;
                default:
                    //Log.Debug($"Unhandled SerialPacket type {packet.CMD}");
                    break;
            }

            if (apsFrame != null)
            {
                _zigBeeNetworkReceive.ReceiveCommand(apsFrame);
                return;
            }
        }

        public void ReceivedUnclaimedSynchronousCommandResponse(ZToolPacket packet)
        {
            if (packet is ZToolPacket srsp)
            {
                throw new NotImplementedException();
            }
        }

        public void SendCommand(ZigBeeApsFrame apsFrame)
        {
            lock (_networkManager)
            {
                byte sender;
                if (apsFrame.Profile == 0)
                {
                    sender = 0;
                }
                else
                {
                    sender = (byte)GetSendingEndpoint(apsFrame.Profile);
                }

                // TODO: How to differentiate group and device addressing?????
                bool groupCommand = false;
                if (!groupCommand)
                {
                    _networkManager.SendCommand(new AF_DATA_REQUEST(apsFrame.DestinationAddress,
                            (byte)apsFrame.DestinationEndpoint, sender, apsFrame.Cluster,
                            apsFrame.ApsCounter, (byte)0x30, (byte)apsFrame.Radius, apsFrame.Payload));
                }
                else
                {
                    _networkManager.SendCommand(new AF_DATA_REQUEST_EXT(apsFrame.DestinationAddress, sender,
                            apsFrame.Cluster, apsFrame.ApsCounter, (byte)(0), (byte)0, apsFrame.Payload));
                }
            }
        }

        public void Shutdown()
        {
            _networkManager.Shutdown();
        }

        public ZigBeeStatus Startup(bool reinitialize)
        {
            Log.Debug("CC2531 transport startup");

            // Add listeners for ZCL and ZDO received messages
            _networkManager.AddAFMessageListener(this);
            _networkManager.AddAsynchronousCommandListener(this);

            if (!_networkManager.InitializeZigBeeNetwork(reinitialize))
            {
                return ZigBeeStatus.INVALID_STATE;
            }

            // TODO: ugly ugly ugly
            // See: https://github.com/zigbeenet/ZigbeeNet/issues/46
            while (true)
            {
                if (_networkManager.GetDriverStatus() == DriverStatus.NETWORK_READY)
                {
                    break;
                }
                if (_networkManager.GetDriverStatus() == DriverStatus.CLOSED)
                {
                    return ZigBeeStatus.BAD_RESPONSE;
                }

                Thread.Sleep(50);
            }

            CreateEndpoint(1, 0x104);

            return ZigBeeStatus.SUCCESS;
        }

        private byte GetSendingEndpoint(ushort profileId)
        {
            lock (_sender2Endpoint)
            {
                if (_sender2Endpoint.ContainsKey(profileId))
                {
                    return _sender2Endpoint[profileId];
                }
                else
                {
                    Log.Information($"No endpoint registered for profileId={profileId}");
                    return byte.MaxValue;
                }
            }
        }

        private ushort GetEndpointProfile(byte endpointId)
        {
            lock (_Endpoint2Profile)
            {
                if (_Endpoint2Profile.ContainsKey(endpointId))
                {
                    return _Endpoint2Profile[endpointId];
                }
                else
                {
                    Log.Information("No endpoint {Endpoint} registered", endpointId);
                    return ushort.MaxValue;
                }
            }
        }

        private byte CreateEndpoint(byte endpointId, ushort profileId)
        {
            Log.Verbose("Registering a new endpoint {Endpoint} for profile {Profile}", endpointId, profileId);

            AF_REGISTER_SRSP result;
            result = _networkManager.SendAFRegister(new AF_REGISTER(endpointId, profileId, 0, 0,
                    _supportedInputClusters.ToArray(), _supportedOutputClusters.ToArray()));
            // FIX We should retry only when Status != 0xb8 ( Z_APS_DUPLICATE_ENTRY )
            if (result.Status != 0)
            {
                // TODO We should provide a workaround for the maximum number of registered EndPoint
                // For example, with the CC2480 we could reset the dongle
                throw new Exception("Unable create a new Endpoint. AF_REGISTER command failed with " + result.Status);
            }

            _sender2Endpoint[profileId] = endpointId;
            _Endpoint2Profile[endpointId] = profileId;

            Log.Debug("Registered endpoint {Endpoint} with profile: {Profile}", endpointId, profileId);

            return endpointId;
        }

        public ZigBeeStatus SetZigBeeChannel(ZigBeeChannel channel)
        {
            return _networkManager.SetZigBeeChannel((byte)channel) ? ZigBeeStatus.SUCCESS : ZigBeeStatus.FAILURE;
        }

        public ZigBeeStatus SetZigBeePanId(ushort panId)
        {
            return _networkManager.SetZigBeePanId(panId) ? ZigBeeStatus.SUCCESS : ZigBeeStatus.FAILURE;
        }

        public ZigBeeStatus SetZigBeeExtendedPanId(ExtendedPanId panId)
        {
            return _networkManager.SetZigBeeExtendedPanId(panId) ? ZigBeeStatus.SUCCESS : ZigBeeStatus.FAILURE;
        }

        public ZigBeeStatus SetZigBeeNetworkKey(ZigBeeKey key)
        {
            byte[] keyData = new byte[16];
            int cnt = 0;
            foreach (byte keyVal in key.Key)
            {
                keyData[cnt++] = keyVal;
            }
            return _networkManager.SetNetworkKey(keyData) ? ZigBeeStatus.SUCCESS : ZigBeeStatus.FAILURE;
        }

        public ZigBeeStatus SetTcLinkKey(ZigBeeKey key)
        {
            return ZigBeeStatus.FAILURE;
        }

        public void SetZigBeeTransportReceive(IZigBeeTransportReceive zigBeeTransportReceive)
        {
            _zigBeeNetworkReceive = zigBeeTransportReceive;
        }

        public void UpdateTransportConfig(TransportConfig configuration)
        {
            foreach (TransportConfigOption option in configuration.GetOptions())
            {
                try
                {
                    switch (option)
                    {
                        case TransportConfigOption.SUPPORTED_INPUT_CLUSTERS:
                            configuration.SetResult(option, SetSupportedInputClusters((List<ushort>)configuration.GetValue(option)));
                            break;

                        case TransportConfigOption.SUPPORTED_OUTPUT_CLUSTERS:
                            configuration.SetResult(option, SetSupportedOutputClusters((List<ushort>)configuration.GetValue(option)));
                            break;

                        default:
                            configuration.SetResult(option, ZigBeeStatus.UNSUPPORTED);
                            Log.Debug("Unsupported configuration option \"{Option}\" in CC2531 dongle", option);
                            break;
                    }
                }
                catch
                {
                    configuration.SetResult(option, ZigBeeStatus.INVALID_ARGUMENTS);
                }
            }
        }

        private ZigBeeStatus SetSupportedInputClusters(List<ushort> supportedClusters)
        {
            _supportedInputClusters = supportedClusters;
            return ZigBeeStatus.SUCCESS;
        }

        private ZigBeeStatus SetSupportedOutputClusters(List<ushort> supportedClusters)
        {
            _supportedOutputClusters = supportedClusters;
            return ZigBeeStatus.SUCCESS;
        }

        public ZigBeeStatus SetLedMode(byte ledId, bool mode)
        {
            return _networkManager.SetLedMode(ledId, mode);
        }
    }
}
