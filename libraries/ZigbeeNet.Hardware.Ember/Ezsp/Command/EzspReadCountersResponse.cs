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
    /// Class to implement the Ember EZSP command " readCounters ".
    /// Retrieves Ember counters. See the EmberCounterType enumeration for the counter types.
    /// This class provides methods for processing EZSP commands.
    /// </summary>
    public class EzspReadCountersResponse : EzspFrameResponse
    {
        
        public const int FRAME_ID = 241;
        
        /// <summary>
        ///  A list of all counter values ordered according to the EmberCounterType enumeration.
        /// </summary>
        private int[] _values;
        
        public EzspReadCountersResponse(int[] inputBuffer) : 
                base(inputBuffer)
        {
            _values = deserializer.DeserializeUInt16Array(29);
        }
        
        /// <summary>
        /// The values to set as <see cref="uint16_t[29]"/> </summary>
        public void SetValues(int[] values)
        {
            _values = values;
        }
        
        /// <summary>
        ///  A list of all counter values ordered according to the EmberCounterType enumeration.
        /// Return the values as <see cref="System.Int32"/>
        /// </summary>
        public int[] GetValues()
        {
            return _values;
        }
        
        public override string ToString()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("EzspReadCountersResponse [values=");
            if (_values == null)
            {
                builder.Append("null");
            }
            else
            {
                for (int cnt = 0
                ; cnt < _values.Length; cnt++
                )
                {
                    if (cnt > 0)
                    {
                        builder.Append(' ');
                    }
                    builder.Append(string.Format("0x{0:X04}", _values[cnt]));
                }
            }
            builder.Append(']');
            return builder.ToString();
        }
    }
}