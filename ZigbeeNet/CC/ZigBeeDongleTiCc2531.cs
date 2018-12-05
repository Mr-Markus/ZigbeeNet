using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.CC.Network;
using ZigbeeNet.CC.Packet;
using ZigbeeNet.CC.Packet.AF;
using ZigbeeNet.Logging;
using ZigbeeNet.Transport;

namespace ZigbeeNet.CC
{
    public class ZigBeeDongleTiCc2531 : IZigBeeTransportTransmit, IApplicationFrameworkMessageListener, IAsynchronousCommandListener
    {
        private readonly ILog _logger = LogProvider.For<ZigBeeDongleTiCc2531>();

        private NetworkManager _networkManager;
        private IZigBeeTransportReceive _zigbeeNetworkReceive;

        private readonly Dictionary<ushort, byte> _sender2Endpoint = new Dictionary<ushort, byte>();
        private readonly Dictionary<byte, ushort> _Endpoint2Profile = new Dictionary<byte, ushort>();

        private List<ushort> _supportedInputClusters = new List<ushort>();
        private List<ushort> _supportedOutputClusters = new List<ushort>();

        public string VersionString { get; set; }
        public ZigbeeAddress64 IeeeAddress { get; set; }
        public ZigBeeChannel ZigBeeChannel { get; set; }
        public ZigbeeAddress16 PanID { get; set; }
        public ZigbeeAddress64 ExtendedPanId { get; set; }
        public ZigBeeKey ZigBeeNetworkKey { get; set; }
        public ZigBeeKey TcLinkKey { get; set; }

        public ZigBeeDongleTiCc2531(IZigbeePort serialPort)
        {
            //TODO: Fill constructor
        }

        public void SetMagicNumber(int magicNumber)
        {
            throw new NotImplementedException();
        }

        public ZigBeeStatus Initialize()
        {
            _logger.Debug("CC2531 transport initialize");

            throw new NotImplementedException();
        }

        public bool Notify(AF_INCOMING_MSG msg)
        {
            ZigBeeApsFrame apsFrame = new ZigBeeApsFrame();
            apsFrame.Cluster = msg.ClusterId.Value;
            apsFrame.DestinationEndpoint = msg.DstEndpoint;
            apsFrame.SourceEndpoint = msg.SrcEndpoint;
            apsFrame.Profile = GetEndpointProfile(msg.DstEndpoint);
            apsFrame.SourceAddress = msg.SrcAddr.Value;
            apsFrame.ApsCounter = msg.TransSeqNumber;
            apsFrame.Payload = msg.Data;

            _zigbeeNetworkReceive.ReceiveCommand(apsFrame);

            return true;
        }

        public void ReceivedAsynchronousCommand(AsynchronousRequest packet)
        {
            switch(packet.SubSystem)
            {
                case SubSystem.AF:
                    return;
                case SubSystem.ZDO:
                    break;
                default:
                    break;
            }

            ZigBeeApsFrame apsFrame = null;
            switch(packet.Cmd)
            {
                case CommandType.ZDO_MSG_CB_INCOMING:
                    throw new NotImplementedException();
                    break;
                case CommandType.ZDO_IEEE_ADDR_RSP:
                    throw new NotImplementedException();
                    break;
                case CommandType.ZDO_END_DEVICE_ANNCE_IND:
                    throw new NotImplementedException();
                    break;
                case CommandType.ZDO_NODE_DESC_RSP:
                    throw new NotImplementedException();
                    break;
                case CommandType.ZDO_POWER_DESC_RSP:
                    throw new NotImplementedException();
                    break;
                case CommandType.ZDO_ACTIVE_EP_RSP:
                    throw new NotImplementedException();
                    break;
                case CommandType.ZDO_SIMPLE_DESC_RSP:
                    throw new NotImplementedException();
                    break;
                case CommandType.ZDO_MGMT_LQI_RSP:
                    throw new NotImplementedException();
                    break;
                case CommandType.ZDO_MGMT_RTG_RSP:
                    throw new NotImplementedException();
                    break;
                case CommandType.ZDO_MGMT_LEAVE_RSP:
                    throw new NotImplementedException();
                    break;
                default:
                    _logger.Debug($"Unhandled SerialPacket type {packet.Cmd}");
                    break;
            }

            if(apsFrame != null)
            {
                _zigbeeNetworkReceive.ReceiveCommand(apsFrame);
                return;
            }
        }

        public void ReceivedUnclaimedSynchronousCommandResponse(SerialPacket packet)
        {
            if(packet is SynchronousResponse srsp)
            {
                throw new NotImplementedException();
            }
        }

        public void SendCommand(ZigBeeApsFrame apsFrame)
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

        private byte GetSendingEndpoint(ushort profileId)
        {
            lock(_sender2Endpoint)
            {
                if(_sender2Endpoint.ContainsKey(profileId))
                {
                    return _sender2Endpoint[profileId];
                } else
                {
                    _logger.Info($"No endpoint registered for profileId={profileId}");
                    return byte.MaxValue;
                }
            }
        }

        private ushort GetEndpointProfile(byte endpointId)
        {
            lock(_Endpoint2Profile)
            {
                if(_Endpoint2Profile.ContainsKey(endpointId))
                {
                    return _Endpoint2Profile[endpointId];
                } else
                {
                    _logger.Info($"No endpoint {endpointId} registered");
                    return ushort.MaxValue;
                }
            }
        }

        private byte CreateEndpoint(byte endpointId, ushort profileId)
        {
            _logger.Trace($"Registering a new endpoint {endpointId} for profile {profileId}");

            throw new NotImplementedException();
        }
    }
}
