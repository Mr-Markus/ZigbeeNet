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
    
    
    /// <summary>
    /// Class to implement the Ember EZSP command " mfglibGetChannel ".
    /// Returns the current radio channel, as previously set via mfglibSetChannel().
    /// This class provides methods for processing EZSP commands.
    /// </summary>
    public class EzspMfglibGetChannelRequest : EzspFrameRequest
    {
        
        public const int FRAME_ID = 139;
        
        private EzspSerializer _serializer;
        
        public EzspMfglibGetChannelRequest()
        {
            _frameId = FRAME_ID;
            _serializer = new EzspSerializer();
        }
        
        /// <summary>
        /// Method for serializing the command fields </summary>
        public override int[] Serialize()
        {
            SerializeHeader(_serializer);
            return _serializer.GetPayload();
        }
        
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
