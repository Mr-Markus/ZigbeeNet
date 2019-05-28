//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol
{
    
    
    /// <summary>
    /// Class to implement the XBee command " Transmit Status ".
    /// When a Transmit Request (0x10, 0x11) completes, the device sends a Transmit Status message
    /// out of the serial interface. This message indicates if the Transmit Request was successful
    /// or if it failed. 
    /// This class provides methods for processing XBee API commands.
    /// </summary>
    public class XBeeTransmitStatusResponse : XBeeFrame, IXBeeResponse 
    {
        
        /// <summary>
        /// Response field
        /// The frame Id 
        /// </summary>
        private int _frameId;
        
        /// <summary>
        /// Response field
        /// The 16-bit Network Address where the packet was delivered (if successful). If not
        /// successful, this address is 0xFFFD (destination address 6 unknown). 
        /// </summary>
        private int _networkAddress;
        
        /// <summary>
        /// Response field
        /// The number of application transmission retries that occur. 
        /// </summary>
        private int _transmitRetryCount;
        
        /// <summary>
        /// Response field
        /// </summary>
        private DeliveryStatus _deliveryStatus;
        
        /// <summary>
        /// Response field
        /// </summary>
        private DiscoveryStatus _discoveryStatus;
        
        /// <summary>
        ///  The frame Id 
        /// Return the frameId as <see cref="System.Int32"/>
        /// </summary>
        public int GetFrameId()
        {
            return _frameId;
        }
        
        /// <summary>
        ///  The 16-bit Network Address where the packet was delivered (if successful). If not
        /// successful, this address is 0xFFFD (destination address 6 unknown). 
        /// Return the networkAddress as <see cref="System.Int32"/>
        /// </summary>
        public int GetNetworkAddress()
        {
            return _networkAddress;
        }
        
        /// <summary>
        ///  The number of application transmission retries that occur. 
        /// Return the transmitRetryCount as <see cref="System.Int32"/>
        /// </summary>
        public int GetTransmitRetryCount()
        {
            return _transmitRetryCount;
        }
        
        /// <summary>
        ///  Return the deliveryStatus as <see cref="DeliveryStatus"/>
        /// </summary>
        public DeliveryStatus GetDeliveryStatus()
        {
            return _deliveryStatus;
        }
        
        /// <summary>
        ///  Return the discoveryStatus as <see cref="DiscoveryStatus"/>
        /// </summary>
        public DiscoveryStatus GetDiscoveryStatus()
        {
            return _discoveryStatus;
        }
        
        /// <summary>
        /// Method for deserializing the fields for the response </summary>
        public void Deserialize(int[] incomingData)
        {
            InitializeDeserializer(incomingData);
            _frameId = DeserializeInt8();
            _networkAddress = DeserializeInt16();
            _transmitRetryCount = DeserializeInt8();
            _deliveryStatus = DeserializeDeliveryStatus();
            _discoveryStatus = DeserializeDiscoveryStatus();
        }
        
        public override string ToString()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder(566);
            builder.Append("XBeeTransmitStatusResponse [frameId=");
            builder.Append(_frameId);
            builder.Append(", networkAddress=");
            builder.Append(_networkAddress);
            builder.Append(", transmitRetryCount=");
            builder.Append(_transmitRetryCount);
            builder.Append(", deliveryStatus=");
            builder.Append(_deliveryStatus);
            builder.Append(", discoveryStatus=");
            builder.Append(_discoveryStatus);
            builder.Append(']');
            return builder.ToString();
        }
    }
}