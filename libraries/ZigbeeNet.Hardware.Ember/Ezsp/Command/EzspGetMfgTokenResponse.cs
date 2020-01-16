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
    /// Class to implement the Ember EZSP command " getMfgToken ".
    /// Retrieves a manufacturing token from the Flash Information Area of the NCP (except for
    /// EZSP_STACK_CAL_DATA which is managed by the stack).
    /// This class provides methods for processing EZSP commands.
    /// </summary>
    public class EzspGetMfgTokenResponse : EzspFrameResponse
    {
        
        public const int FRAME_ID = 11;
        
        /// <summary>
        ///  The manufacturing token data.
        /// </summary>
        private int[] _tokenData;
        
        public EzspGetMfgTokenResponse(int[] inputBuffer) : 
                base(inputBuffer)
        {
            int tokenDataLength = deserializer.DeserializeUInt8();
            _tokenData = deserializer.DeserializeUInt8Array(tokenDataLength);
        }
        
        /// <summary>
        /// The tokenData to set as <see cref="uint8_t[]"/> </summary>
        public void SetTokenData(int[] tokenData)
        {
            _tokenData = tokenData;
        }
        
        /// <summary>
        ///  The manufacturing token data.
        /// Return the tokenData as <see cref="System.Int32"/>
        /// </summary>
        public int[] GetTokenData()
        {
            return _tokenData;
        }
        
        public override string ToString()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("EzspGetMfgTokenResponse [tokenData=");
            if (_tokenData == null)
            {
                builder.Append("null");
            }
            else
            {
                for (int cnt = 0
                ; cnt < _tokenData.Length; cnt++
                )
                {
                    if (cnt > 0)
                    {
                        builder.Append(' ');
                    }
                    builder.Append(string.Format("0x{0:X02}", _tokenData[cnt]));
                }
            }
            builder.Append(']');
            return builder.ToString();
        }
    }
}
