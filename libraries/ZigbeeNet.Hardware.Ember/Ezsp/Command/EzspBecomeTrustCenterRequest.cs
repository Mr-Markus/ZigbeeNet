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
    /// Class to implement the Ember EZSP command " becomeTrustCenter ".
    /// This function causes a coordinator to become the Trust Center when it is operating in a
    /// network that is not using one. It will send out an updated Network Key to all devices that will
    /// indicate a transition of the network to now use a Trust Center. The Trust Center should also
    /// switch all devices to using this new network key with the appropriate API.
    /// This class provides methods for processing EZSP commands.
    /// </summary>
    public class EzspBecomeTrustCenterRequest : EzspFrameRequest
    {
        
        public const int FRAME_ID = 119;
        
        /// <summary>
        ///  The key data for the Updated Network Key.
        /// </summary>
        private EmberKeyData _newNetworkKey;
        
        private EzspSerializer _serializer;
        
        public EzspBecomeTrustCenterRequest()
        {
            _frameId = FRAME_ID;
            _serializer = new EzspSerializer();
        }
        
        /// <summary>
        /// The newNetworkKey to set as <see cref="EmberKeyData"/> </summary>
        public void SetNewNetworkKey(EmberKeyData newNetworkKey)
        {
            _newNetworkKey = newNetworkKey;
        }
        
        /// <summary>
        ///  The key data for the Updated Network Key.
        /// Return the newNetworkKey as <see cref="EmberKeyData"/>
        /// </summary>
        public EmberKeyData GetNewNetworkKey()
        {
            return _newNetworkKey;
        }
        
        /// <summary>
        /// Method for serializing the command fields </summary>
        public override int[] Serialize()
        {
            SerializeHeader(_serializer);
            _serializer.SerializeEmberKeyData(_newNetworkKey);
            return _serializer.GetPayload();
        }
        
        public override string ToString()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("EzspBecomeTrustCenterRequest [newNetworkKey=");
            builder.Append(_newNetworkKey);
            builder.Append(']');
            return builder.ToString();
        }
    }
}
