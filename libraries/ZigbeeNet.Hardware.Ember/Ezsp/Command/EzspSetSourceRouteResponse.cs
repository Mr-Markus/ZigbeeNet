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
    /// Class to implement the Ember EZSP command " setSourceRoute ".
    /// Supply a source route for the next outgoing message.
    /// This class provides methods for processing EZSP commands.
    /// </summary>
    public class EzspSetSourceRouteResponse : EzspFrameResponse
    {
        
        public const int FRAME_ID = 90;
        
        /// <summary>
        ///  EMBER_SUCCESS if the source route was successfully stored, and EMBER_NO_BUFFERS
        /// otherwise.
        /// </summary>
        private EmberStatus _status;
        
        public EzspSetSourceRouteResponse(int[] inputBuffer) : 
                base(inputBuffer)
        {
            _status = deserializer.DeserializeEmberStatus();
        }
        
        /// <summary>
        /// The status to set as <see cref="EmberStatus"/> </summary>
        public void SetStatus(EmberStatus status)
        {
            _status = status;
        }
        
        /// <summary>
        ///  EMBER_SUCCESS if the source route was successfully stored, and EMBER_NO_BUFFERS
        /// otherwise.
        /// Return the status as <see cref="EmberStatus"/>
        /// </summary>
        public EmberStatus GetStatus()
        {
            return _status;
        }
        
        public override string ToString()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("EzspSetSourceRouteResponse [status=");
            builder.Append(_status);
            builder.Append(']');
            return builder.ToString();
        }
    }
}
