using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.App;
using ZigbeeNet.Internal;
using ZigbeeNet.Logging;
using ZigbeeNet.Serialization;
using ZigbeeNet.Transport;
using ZigbeeNet.ZCL;
using ZigbeeNet.ZDO;

namespace ZigbeeNet
{
    public class ZigbeeNetworkManager : IZigbeeNetwork, IZigBeeTransportReceive
    {
        private readonly ILog _logger = LogProvider.For<ZigbeeNetworkManager>();
        
        /// <summary>
        /// The nodes in the ZigBee network
        /// </summary>
        private Dictionary<ZigbeeAddress64, ZigbeeNode> _networkNodes = new Dictionary<ZigbeeAddress64, ZigbeeNode>();

        /// <summary>
        /// The groups in the ZigBee network.
        /// </summary>
        private Dictionary<ushort, ZigBeeGroupAddress> _networkGroups = new Dictionary<ushort, ZigBeeGroupAddress>();

        /// <summary>
        ///  The announce listeners are notified whenever a new device is discovered.
        ///  This can be called from the transport layer, or internally by methods watching the network state.
        /// </summary>
        private List<IZigBeeAnnounceListener> _announceListeners = new List<IZigBeeAnnounceListener>();

        private int _sequenceNumber = 0;

        private int _apsCounter = 0;
                
        private IZigBeeNetworkStateSerializer _networkStateSerializer;
        /// <summary>
        /// The network state serializer
        /// 
        /// This will allow saving and restoring the network. The network manager will call ZigBeeNetworkStateSerializer.deserialize 
        /// during the startup and ZigBeeNetworkStateSerializer.serialize during shutdown.
        /// </summary>
        public IZigBeeNetworkStateSerializer NetworkStateSerializer
        {
            get { return _networkStateSerializer; }
            set { _networkStateSerializer = value; }
        }

        /// <summary>
        /// The ZigBeeTransportTransmit implementation. This provides the interface
        /// for sending data to the network which is an implementation of a ZigBee
        /// interface (eg a Dongle).
        /// </summary>
        private IZigBeeTransportTransmit _transport;

        /// <summary>
        /// The ZigBeeCommandNotifier. This is used for sending notifications asynchronously to listeners.
        /// </summary>
        private ZigBeeCommandNotifier _commandNotifier = new ZigBeeCommandNotifier();

        private IZigBeeSerializer _serializer;
        /// <summary>
        /// The serializer class used to serialize commands to data packets
        /// 
        /// Set the serializer class to be used to convert commands and fields into data to be sent to the dongle.
        /// The system instantiates a new serializer for each command
        /// </summary>
        public IZigBeeSerializer Serializer
        {
            get { return _serializer; }
            set { _serializer = value; }
        }

        private IZigBeeDeserializer _deserializer;
        /// <summary>
        /// The serializer class used to serialize commands to data packets
        /// 
        /// Set the serializer class to be used to convert commands and fields into data to be sent to the dongle.
        /// The system instantiates a new serializer for each command
        /// </summary>
        public IZigBeeDeserializer Deserializer
        {
            get { return _deserializer; }
            set { _deserializer = value; }
        }

        /// <summary>
        /// List of {@link ZigBeeNetworkExtension}s that are available to this network. Extensions are added
        /// with the ZigBeeNetworkExtension extension method.
        /// </summary>
        private List<IZigBeeNetworkExtension> _extensions = new List<IZigBeeNetworkExtension>();

        /// <summary>
        /// A ClusterMatcher used to respond to the MatchDescriptorRequest command.
        /// </summary>
        private ClusterMatcher _clusterMatcher = null;

        /// <summary>
        /// The current ZigBeeTransportState
        /// </summary>
        private ZigBeeTransportState _networkState;

        /// <summary>
        /// The listeners of the ZigBee network state.
        /// </summary>
        private List<IZigBeeNetworkStateListener> _stateListeners;

        /// <summary>
        ///  Our local IeeeAddress
        /// </summary>
        public ulong LocalIeeeAddress { get; private set; }

        public ushort LocalNwkAddress { get; private set; }

        /// <summary>
        /// Sets the ZigBee RF channel. The allowable channel range is 11 to 26 for 2.4GHz, however the transport
        /// implementation may allow any value it supports.
        /// </summary>
        public ZigbeeChannel Channel
        {
            get
            {
                return _transport.ZigbeeChannel;
            }
            set
            {
                _transport.ZigbeeChannel = value;
            }
        }

        /// <summary>
        /// Gets the ZigBee PAN ID currently in use by the transport
        /// </summary>
        public ZigbeeAddress16 PanId
        {
            get
            {
                return _transport.PanID;
            }
            set
            {
                _transport.PanID = value;
            }
        }

        /// <summary>
        /// Get or set the ZigBee Extended PAN ID to the specified value
        /// </summary>
        public ZigbeeAddress64 ExtendedPanId
        {
            get
            {
                return _transport.ExtendedPanId;
            }
            set
            {
                _transport.ExtendedPanId = value;
            }
        }

        /// <summary>
        /// Get or set the current network key in use by the system.
        /// </summary>
        public ZigbeeKey ZigbeeNetworkKey
        {
            get
            {
                return _transport.ZigbeeNetworkKey;
            }
            set
            {
                _transport.ZigbeeNetworkKey = value;
            }
        }

        /// <summary>
        /// Get or set the current link key in use by the system.
        /// </summary>
        public ZigbeeKey ZigbeeLinkKey
        {
            get
            {
                return _transport.TcLinkKey;
            }
            set
            {
                _transport.TcLinkKey = value;
            }
        }

        /// <summary>
        ///  Starts up ZigBee manager components.
        /// </summary>
        /// <param name="reinitialize">true if the provider is to reinitialise the network with the parameters configured since the
        /// initialize} method was called.</param>
        /// <returns>ZigBeeStatus with the status of function</returns>
        public ZigbeeStatus Startup(bool reinitialize)
        {
            ZigbeeStatus status = _transport.Startup(reinitialize);

            if(status != ZigbeeStatus.SUCCESS)
            {
                SetNetworkState(ZigBeeTransportState.OFFLINE);
                return status;
            }
            
            SetNetworkState(ZigBeeTransportState.ONLINE);
            return ZigbeeStatus.SUCCESS;
        }

        public void Shutdown()
        {
            foreach (ZigbeeNode node in _networkNodes.Values)
            {
                node.Shutdown();
            }

            if (_networkStateSerializer != null)
                _networkStateSerializer.Serialize(this);

            foreach (IZigBeeNetworkExtension extension in _extensions)
            {
                extension.ExtensionShutdown();
            }

            _transport.Shutdown();
        }

        public byte SendCommand(ZigbeeCommand command)
        {
            ZigbeeApsFrame apsFrame = new ZigbeeApsFrame();

            if(command.TransactionId == null)
            {
                //TODO: get transId
                //command.TransactionId = _sequenceNumber.getAndIncement() & 0xff;
            }

            command.SourceAddress = new ZigBeeEndpointAddress(LocalNwkAddress);

            _logger.Debug($"TX CMD: {command}");

            apsFrame.Cluster = command.ClusterId;
            //TODO: get aps counter
            //apsFrame.ApsCounter = _apsCounter.getAndIncrement;
            apsFrame.SecurityEnabled = command.ApsSecurity;
            apsFrame.SourceAddress = LocalNwkAddress;
            apsFrame.Radius = 31;

            if(command.DestinationAddress is ZigBeeEndpointAddress epAddr)
            {
                apsFrame.AddressMode = ZigBeeNwkAddressMode.Device;
                apsFrame.DestinationAddress = epAddr.Address;
                apsFrame.DestinationEndpoint = epAddr.Endpoint;

                ZigbeeNode node = GetNode(epAddr.Address);
                if(node != null)
                {
                    apsFrame.DestinationIeeeAddress = node.IeeeAddress;
                }
            } else
            {
                apsFrame.AddressMode = ZigBeeNwkAddressMode.Group;
            }

            ZclFieldSerializer fieldSerializer = new ZclFieldSerializer(Serializer);

            if(command is ZdoCommand zdoCommand)
            {
                apsFrame.Profile = 0;
                apsFrame.SourceEndpoint = 0;
                apsFrame.DestinationEndpoint = 0;

                command.Serialize(fieldSerializer);

                apsFrame.Payload = fieldSerializer.Payload;
            }

            if(command is ZclCommand zclCommand)
            {
                apsFrame.SourceEndpoint = 1;
                apsFrame.Profile = 0x104;

                ZclHeader zclHeader = new ZclHeader();
                zclHeader.FrameType = zclCommand.GenericCommand ? ZclFrameType.ENTIRE_PROFILE_COMMAND : ZclFrameType.CLUSTER_SPECIFIC_COMMAND;
                zclHeader.Direction = zclCommand.Direction;
                zclHeader.CommandId = zclCommand.CommandId;
                zclHeader.SequenceNumber = command.TransactionId;

                command.Serialize(fieldSerializer);

                apsFrame.Payload = zclHeader.Serialize(fieldSerializer, fieldSerializer.Payload);

                _logger.Debug($"TX ZCL: {zclHeader}");
            }

            _logger.Debug($"TX APS: {apsFrame}");

            _transport.SendCommand(apsFrame);

            return command.TransactionId;
        }

        public void AddCommandListener(IZigbeeCommandListener commandListener)
        {
            _commandNotifier.AddCommandListener(commandListener);
        }

        public void RemoveCommandListener(IZigbeeCommandListener commandListener)
        {
            _commandNotifier.RemoveCommandListener(commandListener);
        }

        public void SendTransaction(ZigbeeCommand command)
        {
            throw new NotImplementedException();
        }

        private ZigbeeNode GetNode(ushort networkAddress)
        {
            foreach (ZigbeeNode node in _networkNodes.Values)
            {
                if (node.NwkAdress.Value == networkAddress)
                    return node;
            }

            return null;
        }

        public void ReceiveCommand(ZigbeeApsFrame apsFrame)
        {
            _logger.Debug($"RX APS: {apsFrame}");

            ZclFieldDeserializer fieldDeserializer = new ZclFieldDeserializer(Deserializer);

            ZigbeeCommand command = null;
            switch (apsFrame.Profile)
            {
                case 0x0000:
                    command = ReceiveZdoCommand(fieldDeserializer, apsFrame);
                    break;
                case 0x0104:
                case 0xC05E:
                    command = ReceiveZclCommand(fieldDeserializer, apsFrame);
                    break;
                default:
                    _logger.Debug("Incomming message did not translate to command");
                    break;
            }

            if(command == null)
            {
                _logger.Debug("Incomming message did not translate to command");
                return;
            }

            command.SourceAddress = new ZigBeeEndpointAddress(apsFrame.SourceAddress, apsFrame.SourceEndpoint);
            command.DestinationAddress = new ZigBeeEndpointAddress(apsFrame.DestinationAddress, apsFrame.DestinationEndpoint);
            command.ApsSecurity = apsFrame.SecurityEnabled;

            _logger.Debug($"RX CMD: {command}");

            _commandNotifier.NotifyCommandListeners(command);
        }

        private ZigbeeCommand ReceiveZdoCommand(ZclFieldDeserializer fieldDeserializer, ZigbeeApsFrame apsFrame)
        {
            //TODO: Add CommandType
            //ZdoCommandType commandType = null;

            //if(commandType == null)
            //{
            //    return null;
            //}

            ZigbeeCommand command = null;

            command.Deserialize(fieldDeserializer);

            return command;
        }

        private ZigbeeCommand ReceiveZclCommand(ZclFieldDeserializer fieldDeserializer, ZigbeeApsFrame apsFrame)
        {
            ZclHeader zclHeader = new ZclHeader(fieldDeserializer);

            _logger.Debug($"RX ZCL: {zclHeader}");

            //TODO: Add command Type
            //ZclCommandType commandType = null;

            //if (zclHeader.FrameType == ZclFrameType.ENTIRE_PROFILE_COMMAND)
            //    commandType = null;
            //else
            //    commandType = null;

            //if(commandType == null)
            //{
            //    _logger.Debug($"No command type found for {zclHeader.FrameType}, cluster={apsFrame.Cluster}, command={zclHeader.CommandId}, direction={zclHeader.Direction}");

            //    return null;
            //}

            ZclCommand command = null;
            if(command == null)
            {
                _logger.Debug($"No command type found for {zclHeader.FrameType}, cluster={apsFrame.Cluster}, command={zclHeader.CommandId}, direction={zclHeader.Direction}");

                return null;
            }

            command.Direction = zclHeader.Direction;
            command.Deserialize(fieldDeserializer);
            command.ClusterId = apsFrame.Cluster;
            command.TransactionId = zclHeader.SequenceNumber;

            return command;
        }

        public void SetNetworkState(ZigBeeTransportState state)
        {
            throw new NotImplementedException();
        }

        public void NodeStatusUpdate(ZigbeeNodeStatus deviceStatus, ushort networkAddress, ulong ieeeAddress)
        {
            throw new NotImplementedException();
        }
    }
}
