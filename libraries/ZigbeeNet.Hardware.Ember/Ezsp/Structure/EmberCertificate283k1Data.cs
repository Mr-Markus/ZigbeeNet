//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:3.0.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZigBeeNet.Hardware.Ember.Ezsp.Structure
{
    using ZigBeeNet.Hardware.Ember.Internal.Serializer;
    
    
    /// <summary>
    /// Class to implement the Ember Structure " EmberCertificate283k1Data ".
    /// The implicit certificate used in CBKE.
    /// </summary>
    public class EmberCertificate283k1Data
    {
        
        /// <summary>
        ///  The 283k1 certificate data.
        /// </summary>
        private int[] _contents;
        
        public EmberCertificate283k1Data()
        {
        }
        
        public EmberCertificate283k1Data(EzspDeserializer deserializer)
        {
            Deserialize(deserializer);
        }
        
        /// <summary>
        /// The contents to set as <see cref="uint8_t[74]"/> </summary>
        public void SetContents(int[] contents)
        {
            _contents = contents;
        }
        
        /// <summary>
        ///  The 283k1 certificate data.
        /// Return the contents as <see cref="System.Int32"/>
        /// </summary>
        public int[] GetContents()
        {
            return _contents;
        }
        
        /// <summary>
        /// Serialise the contents of the EZSP structure. </summary>
        public int[] Serialize(EzspSerializer serializer)
        {
            serializer.SerializeUInt8Array(_contents);
            return serializer.GetPayload();
        }
        
        /// <summary>
        /// Deserialise the contents of the EZSP structure. </summary>
        public void Deserialize(EzspDeserializer deserializer)
        {
            _contents = deserializer.DeserializeUInt8Array(74);
        }
        
        public override string ToString()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("EmberCertificate283k1Data [contents=");
            if (_contents == null)
            {
                builder.Append("null");
            }
            else
            {
                for (int cnt = 0
                ; cnt < _contents.Length; cnt++
                )
                {
                    if (cnt > 0)
                    {
                        builder.Append(' ');
                    }
                    builder.Append(string.Format("0x{0:X02}", _contents[cnt]));
                }
            }
            builder.Append(']');
            return builder.ToString();
        }
    }
}
