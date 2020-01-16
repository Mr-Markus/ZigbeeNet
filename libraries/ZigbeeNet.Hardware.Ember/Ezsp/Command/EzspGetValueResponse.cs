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
    public class EzspGetValueResponse : EzspFrameResponse
    {
        
        public const int FRAME_ID = 170;
        
        /// <summary>
        ///  EZSP_SUCCESS if the value was read successfully, EZSP_ERROR_INVALID_ID if the NCP does not
        /// recognize valueId.
        /// </summary>
        private EzspStatus _status;
        
        /// <summary>
        ///  The value.
        /// </summary>
        private int[] _value;
        
        public EzspGetValueResponse(int[] inputBuffer) : 
                base(inputBuffer)
        {
            _status = deserializer.DeserializeEzspStatus();
            int valueLength = deserializer.DeserializeUInt8();
            _value = deserializer.DeserializeUInt8Array(valueLength);
        }
        
        /// <summary>
        /// The status to set as <see cref="EzspStatus"/> </summary>
        public void SetStatus(EzspStatus status)
        {
            _status = status;
        }
        
        /// <summary>
        /// The value to set as <see cref="uint8_t[]"/> </summary>
        public void SetValue(int[] value)
        {
            _value = value;
        }
        
        /// <summary>
        ///  EZSP_SUCCESS if the value was read successfully, EZSP_ERROR_INVALID_ID if the NCP does not
        /// recognize valueId.
        /// Return the status as <see cref="EzspStatus"/>
        /// </summary>
        public EzspStatus GetStatus()
        {
            return _status;
        }
        
        /// <summary>
        ///  The value.
        /// Return the value as <see cref="System.Int32"/>
        /// </summary>
        public int[] GetValue()
        {
            return _value;
        }
        
        public override string ToString()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("EzspGetValueResponse [status=");
            builder.Append(_status);
            builder.Append(", value=");
            if (_value == null)
            {
                builder.Append("null");
            }
            else
            {
                for (int cnt = 0
                ; cnt < _value.Length; cnt++
                )
                {
                    if (cnt > 0)
                    {
                        builder.Append(' ');
                    }
                    builder.Append(string.Format("0x{0:X02}", _value[cnt]));
                }
            }
            builder.Append(']');
            return builder.ToString();
        }
    }
}
