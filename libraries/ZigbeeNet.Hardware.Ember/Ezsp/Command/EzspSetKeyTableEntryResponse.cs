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
    /// Class to implement the Ember EZSP command " setKeyTableEntry ".
    /// Sets the key table entry at the specified index.
    /// This class provides methods for processing EZSP commands.
    /// </summary>
    public class EzspSetKeyTableEntryResponse : EzspFrameResponse
    {
        
        public const int FRAME_ID = 114;
        
        /// <summary>
        ///  EMBER_KEY_INVALID if the passed key data is using one of the reserved key values.
        /// EMBER_INDEX_OUT_OF_RANGE if passed index is not valid. EMBER_SUCCESS on success.
        /// </summary>
        private EmberStatus _status;
        
        public EzspSetKeyTableEntryResponse(int[] inputBuffer) : 
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
        ///  EMBER_KEY_INVALID if the passed key data is using one of the reserved key values.
        /// EMBER_INDEX_OUT_OF_RANGE if passed index is not valid. EMBER_SUCCESS on success.
        /// Return the status as <see cref="EmberStatus"/>
        /// </summary>
        public EmberStatus GetStatus()
        {
            return _status;
        }
        
        public override string ToString()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("EzspSetKeyTableEntryResponse [status=");
            builder.Append(_status);
            builder.Append(']');
            return builder.ToString();
        }
    }
}
