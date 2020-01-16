//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:3.0.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZigBeeNet.Hardware.Ember.Ezsp.Command
{
    using ZigBeeNet.Hardware.Ember.Internal.Serializer;
    using ZigBeeNet.Hardware.Ember.Ezsp.Structure;
    
    
    /// <summary>
    /// Class to implement the Ember EZSP command " sendUnicast ".
    /// Sends a unicast message as per the ZigBee specification. The message will arrive at its
    /// destination only if there is a known route to the destination node. Setting the
    /// ENABLE_ROUTE_DISCOVERY option will cause a route to be discovered if none is known. Setting
    /// the FORCE_ROUTE_DISCOVERY option will force route discovery. Routes to end-device
    /// children of the local node are always known. Setting the APS_RETRY option will cause the
    /// message to be retransmitted until either a matching acknowledgement is received or three
    /// transmissions have been made.
    /// * <p>
    /// * <b>Note:</b> Using the FORCE_ROUTE_DISCOVERY option will cause the first
    /// transmission to be consumed by a route request as part of discovery, so the application
    /// payload of this packet will not reach its destination on the first attempt. If you want the
    /// packet to reach its destination, the APS_RETRY option must be set so that another attempt is
    /// made to transmit the message with its application payload after the route has been
    /// constructed.
    /// * <p>
    /// * <b>Note:</b> When sending fragmented messages, the stack will only assign a new APS
    /// sequence number for the first fragment of the message (i.e., EMBER_APS_OPTION_FRAGMENT is
    /// set and the low-order byte of the groupId field in the APS frame is zero). For all subsequent
    /// fragments of the same message, the application must set the sequence number field in the APS
    /// frame to the sequence number assigned by the stack to the first fragment.
    /// This class provides methods for processing EZSP commands.
    /// </summary>
    public class EzspSendUnicastRequest : EzspFrameRequest
    {
        
        public const int FRAME_ID = 52;
        
        /// <summary>
        ///  Specifies the outgoing message type. Must be one of EMBER_OUTGOING_DIRECT,
        /// EMBER_OUTGOING_VIA_ADDRESS_TABLE, or EMBER_OUTGOING_VIA_BINDING.
        /// </summary>
        private EmberOutgoingMessageType _type;
        
        /// <summary>
        ///  Depending on the type of addressing used, this is either the EmberNodeId of the destination,
        /// an index into the address table, or an index into the binding table.
        /// </summary>
        private int _indexOrDestination;
        
        /// <summary>
        ///  The APS frame which is to be added to the message.
        /// </summary>
        private EmberApsFrame _apsFrame;
        
        /// <summary>
        ///  A value chosen by the Host. This value is used in the ezspMessageSentHandler response to
        /// refer to this message.
        /// </summary>
        private int _messageTag;
        
        /// <summary>
        ///  Content of the message.
        /// </summary>
        private int[] _messageContents;
        
        private EzspSerializer _serializer;
        
        public EzspSendUnicastRequest()
        {
            _frameId = FRAME_ID;
            _serializer = new EzspSerializer();
        }
        
        /// <summary>
        /// The type to set as <see cref="EmberOutgoingMessageType"/> </summary>
        public void SetType(EmberOutgoingMessageType type)
        {
            _type = type;
        }
        
        /// <summary>
        /// The indexOrDestination to set as <see cref="EmberNodeId"/> </summary>
        public void SetIndexOrDestination(int indexOrDestination)
        {
            _indexOrDestination = indexOrDestination;
        }
        
        /// <summary>
        /// The apsFrame to set as <see cref="EmberApsFrame"/> </summary>
        public void SetApsFrame(EmberApsFrame apsFrame)
        {
            _apsFrame = apsFrame;
        }
        
        /// <summary>
        /// The messageTag to set as <see cref="uint8_t"/> </summary>
        public void SetMessageTag(int messageTag)
        {
            _messageTag = messageTag;
        }
        
        /// <summary>
        /// The messageContents to set as <see cref="uint8_t[]"/> </summary>
        public void SetMessageContents(int[] messageContents)
        {
            _messageContents = messageContents;
        }
        
        /// <summary>
        ///  Specifies the outgoing message type. Must be one of EMBER_OUTGOING_DIRECT,
        /// EMBER_OUTGOING_VIA_ADDRESS_TABLE, or EMBER_OUTGOING_VIA_BINDING.
        /// Return the type as <see cref="EmberOutgoingMessageType"/>
        /// </summary>
        public EmberOutgoingMessageType GetType2()
        {
            return _type;
        }
        
        /// <summary>
        ///  Depending on the type of addressing used, this is either the EmberNodeId of the destination,
        /// an index into the address table, or an index into the binding table.
        /// Return the indexOrDestination as <see cref="System.Int32"/>
        /// </summary>
        public int GetIndexOrDestination()
        {
            return _indexOrDestination;
        }
        
        /// <summary>
        ///  The APS frame which is to be added to the message.
        /// Return the apsFrame as <see cref="EmberApsFrame"/>
        /// </summary>
        public EmberApsFrame GetApsFrame()
        {
            return _apsFrame;
        }
        
        /// <summary>
        ///  A value chosen by the Host. This value is used in the ezspMessageSentHandler response to
        /// refer to this message.
        /// Return the messageTag as <see cref="System.Int32"/>
        /// </summary>
        public int GetMessageTag()
        {
            return _messageTag;
        }
        
        /// <summary>
        ///  Content of the message.
        /// Return the messageContents as <see cref="System.Int32"/>
        /// </summary>
        public int[] GetMessageContents()
        {
            return _messageContents;
        }
        
        /// <summary>
        /// Method for serializing the command fields </summary>
        public override int[] Serialize()
        {
            SerializeHeader(_serializer);
            _serializer.SerializeEmberOutgoingMessageType(_type);
            _serializer.SerializeUInt16(_indexOrDestination);
            _serializer.SerializeEmberApsFrame(_apsFrame);
            _serializer.SerializeUInt8(_messageTag);
            _serializer.SerializeUInt8(_messageContents.Length);
            _serializer.SerializeUInt8Array(_messageContents);
            return _serializer.GetPayload();
        }
        
        public override string ToString()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("EzspSendUnicastRequest [type=");
            builder.Append(_type);
            builder.Append(", indexOrDestination=");
            builder.Append(string.Format("0x{0:X04}", _indexOrDestination));
            builder.Append(", apsFrame=");
            builder.Append(_apsFrame);
            builder.Append(", messageTag=");
            builder.Append(string.Format("0x{0:X02}", _messageTag));
            builder.Append(", messageContents=");
            if (_messageContents == null)
            {
                builder.Append("null");
            }
            else
            {
                for (int cnt = 0
                ; cnt < _messageContents.Length; cnt++
                )
                {
                    if (cnt > 0)
                    {
                        builder.Append(' ');
                    }
                    builder.Append(string.Format("0x{0:X02}", _messageContents[cnt]));
                }
            }
            builder.Append(']');
            return builder.ToString();
        }
    }
}
