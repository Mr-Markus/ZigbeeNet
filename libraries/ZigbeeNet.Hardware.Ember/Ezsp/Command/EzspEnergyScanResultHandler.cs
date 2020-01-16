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
    /// Class to implement the Ember EZSP command " energyScanResultHandler ".
    /// Reports the result of an energy scan for a single channel. The scan is not complete until the
    /// scanCompleteHandler callback is called.
    /// This class provides methods for processing EZSP commands.
    /// </summary>
    public class EzspEnergyScanResultHandler : EzspFrameResponse
    {
        
        public const int FRAME_ID = 72;
        
        /// <summary>
        ///  The 802.15.4 channel number that was scanned
        /// </summary>
        private int _channel;
        
        /// <summary>
        ///  The maximum RSSI value found on the channel.
        /// </summary>
        private int _maxRssiValue;
        
        public EzspEnergyScanResultHandler(int[] inputBuffer) : 
                base(inputBuffer)
        {
            _channel = deserializer.DeserializeUInt8();
            _maxRssiValue = deserializer.DeserializeInt8S();
        }
        
        /// <summary>
        /// The channel to set as <see cref="uint8_t"/> </summary>
        public void SetChannel(int channel)
        {
            _channel = channel;
        }
        
        /// <summary>
        /// The maxRssiValue to set as <see cref="int8s"/> </summary>
        public void SetMaxRssiValue(int maxRssiValue)
        {
            _maxRssiValue = maxRssiValue;
        }
        
        /// <summary>
        ///  The 802.15.4 channel number that was scanned
        /// Return the channel as <see cref="System.Int32"/>
        /// </summary>
        public int GetChannel()
        {
            return _channel;
        }
        
        /// <summary>
        ///  The maximum RSSI value found on the channel.
        /// Return the maxRssiValue as <see cref="System.Int32"/>
        /// </summary>
        public int GetMaxRssiValue()
        {
            return _maxRssiValue;
        }
        
        public override string ToString()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("EzspEnergyScanResultHandler [channel=");
            builder.Append(_channel);
            builder.Append(", maxRssiValue=");
            builder.Append(_maxRssiValue);
            builder.Append(']');
            return builder.ToString();
        }
    }
}
