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
    /// Class to implement the Ember EZSP command " getCurrentSecurityState ".
    /// Gets the current security state that is being used by a device that is joined in the network.
    /// This class provides methods for processing EZSP commands.
    /// </summary>
    public class EzspGetCurrentSecurityStateResponse : EzspFrameResponse
    {
        
        public const int FRAME_ID = 105;
        
        /// <summary>
        ///  The success or failure code of the operation.
        /// </summary>
        private EmberStatus _status;
        
        /// <summary>
        ///  The security configuration in use by the stack.
        /// </summary>
        private EmberCurrentSecurityState _state;
        
        public EzspGetCurrentSecurityStateResponse(int[] inputBuffer) : 
                base(inputBuffer)
        {
            _status = deserializer.DeserializeEmberStatus();
            _state = deserializer.DeserializeEmberCurrentSecurityState();
        }
        
        /// <summary>
        /// The status to set as <see cref="EmberStatus"/> </summary>
        public void SetStatus(EmberStatus status)
        {
            _status = status;
        }
        
        /// <summary>
        /// The state to set as <see cref="EmberCurrentSecurityState"/> </summary>
        public void SetState(EmberCurrentSecurityState state)
        {
            _state = state;
        }
        
        /// <summary>
        ///  The success or failure code of the operation.
        /// Return the status as <see cref="EmberStatus"/>
        /// </summary>
        public EmberStatus GetStatus()
        {
            return _status;
        }
        
        /// <summary>
        ///  The security configuration in use by the stack.
        /// Return the state as <see cref="EmberCurrentSecurityState"/>
        /// </summary>
        public EmberCurrentSecurityState GetState()
        {
            return _state;
        }
        
        public override string ToString()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("EzspGetCurrentSecurityStateResponse [status=");
            builder.Append(_status);
            builder.Append(", state=");
            builder.Append(_state);
            builder.Append(']');
            return builder.ToString();
        }
    }
}
