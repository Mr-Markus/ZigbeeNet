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
    /// Class to implement the Ember EZSP command " getValue ".
    /// Reads a value from the NCP.
    /// This class provides methods for processing EZSP commands.
    /// </summary>
    public class EzspGetValueRequest : EzspFrameRequest
    {
        
        public const int FRAME_ID = 170;
        
        /// <summary>
        ///  Identifies which policy to modify.
        /// </summary>
        private EzspValueId _valueId;
        
        private EzspSerializer _serializer;
        
        public EzspGetValueRequest()
        {
            _frameId = FRAME_ID;
            _serializer = new EzspSerializer();
        }
        
        /// <summary>
        /// The valueId to set as <see cref="EzspValueId"/> </summary>
        public void SetValueId(EzspValueId valueId)
        {
            _valueId = valueId;
        }
        
        /// <summary>
        ///  Identifies which policy to modify.
        /// Return the valueId as <see cref="EzspValueId"/>
        /// </summary>
        public EzspValueId GetValueId()
        {
            return _valueId;
        }
        
        /// <summary>
        /// Method for serializing the command fields </summary>
        public override int[] Serialize()
        {
            SerializeHeader(_serializer);
            _serializer.SerializeEzspValueId(_valueId);
            return _serializer.GetPayload();
        }
        
        public override string ToString()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("EzspGetValueRequest [valueId=");
            builder.Append(_valueId);
            builder.Append(']');
            return builder.ToString();
        }
    }
}
