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
    /// Class to implement the Ember Structure " EmberNeighborTableEntry ".
    /// A neighbor table entry stores information about the reliability of RF links to and from
    /// neighboring nodes.
    /// </summary>
    public class EmberNeighborTableEntry
    {
        
        /// <summary>
        ///  The neighbor's two byte network id.
        /// </summary>
        private int _shortId;
        
        /// <summary>
        ///  An exponentially weighted moving average of the link quality values of incoming packets
        /// from this neighbor as reported by the PHY.
        /// </summary>
        private int _averageLqi;
        
        /// <summary>
        ///  The incoming cost for this neighbor, computed from the average LQI. Values range from 1 for a
        /// good link to 7 for a bad link.
        /// </summary>
        private int _inCost;
        
        /// <summary>
        ///  The outgoing cost for this neighbor, obtained from the most recently received neighbor
        /// exchange message from the neighbor. A value of zero means that a neighbor exchange message
        /// from the neighbor has not been received recently enough, or that our id was not present in the
        /// most recently received one.
        /// </summary>
        private int _outCost;
        
        /// <summary>
        ///  The number of aging periods elapsed since a link status message was last received from this
        /// neighbor. The aging period is 16 seconds.
        /// </summary>
        private int _age;
        
        /// <summary>
        ///  The 8 byte EUI64 of the neighbor.
        /// </summary>
        private IeeeAddress _longId;
        
        public EmberNeighborTableEntry()
        {
        }
        
        public EmberNeighborTableEntry(EzspDeserializer deserializer)
        {
            Deserialize(deserializer);
        }
        
        /// <summary>
        /// The shortId to set as <see cref="uint16_t"/> </summary>
        public void SetShortId(int shortId)
        {
            _shortId = shortId;
        }
        
        /// <summary>
        /// The averageLqi to set as <see cref="uint8_t"/> </summary>
        public void SetAverageLqi(int averageLqi)
        {
            _averageLqi = averageLqi;
        }
        
        /// <summary>
        /// The inCost to set as <see cref="uint8_t"/> </summary>
        public void SetInCost(int inCost)
        {
            _inCost = inCost;
        }
        
        /// <summary>
        /// The outCost to set as <see cref="uint8_t"/> </summary>
        public void SetOutCost(int outCost)
        {
            _outCost = outCost;
        }
        
        /// <summary>
        /// The age to set as <see cref="uint8_t"/> </summary>
        public void SetAge(int age)
        {
            _age = age;
        }
        
        /// <summary>
        /// The longId to set as <see cref="EmberEUI64"/> </summary>
        public void SetLongId(IeeeAddress longId)
        {
            _longId = longId;
        }
        
        /// <summary>
        ///  The neighbor's two byte network id.
        /// Return the shortId as <see cref="System.Int32"/>
        /// </summary>
        public int GetShortId()
        {
            return _shortId;
        }
        
        /// <summary>
        ///  An exponentially weighted moving average of the link quality values of incoming packets
        /// from this neighbor as reported by the PHY.
        /// Return the averageLqi as <see cref="System.Int32"/>
        /// </summary>
        public int GetAverageLqi()
        {
            return _averageLqi;
        }
        
        /// <summary>
        ///  The incoming cost for this neighbor, computed from the average LQI. Values range from 1 for a
        /// good link to 7 for a bad link.
        /// Return the inCost as <see cref="System.Int32"/>
        /// </summary>
        public int GetInCost()
        {
            return _inCost;
        }
        
        /// <summary>
        ///  The outgoing cost for this neighbor, obtained from the most recently received neighbor
        /// exchange message from the neighbor. A value of zero means that a neighbor exchange message
        /// from the neighbor has not been received recently enough, or that our id was not present in the
        /// most recently received one.
        /// Return the outCost as <see cref="System.Int32"/>
        /// </summary>
        public int GetOutCost()
        {
            return _outCost;
        }
        
        /// <summary>
        ///  The number of aging periods elapsed since a link status message was last received from this
        /// neighbor. The aging period is 16 seconds.
        /// Return the age as <see cref="System.Int32"/>
        /// </summary>
        public int GetAge()
        {
            return _age;
        }
        
        /// <summary>
        ///  The 8 byte EUI64 of the neighbor.
        /// Return the longId as <see cref="IeeeAddress"/>
        /// </summary>
        public IeeeAddress GetLongId()
        {
            return _longId;
        }
        
        /// <summary>
        /// Serialise the contents of the EZSP structure. </summary>
        public int[] Serialize(EzspSerializer serializer)
        {
            serializer.SerializeUInt16(_shortId);
            serializer.SerializeUInt8(_averageLqi);
            serializer.SerializeUInt8(_inCost);
            serializer.SerializeUInt8(_outCost);
            serializer.SerializeUInt8(_age);
            serializer.SerializeEmberEui64(_longId);
            return serializer.GetPayload();
        }
        
        /// <summary>
        /// Deserialise the contents of the EZSP structure. </summary>
        public void Deserialize(EzspDeserializer deserializer)
        {
            _shortId = deserializer.DeserializeUInt16();
            _averageLqi = deserializer.DeserializeUInt8();
            _inCost = deserializer.DeserializeUInt8();
            _outCost = deserializer.DeserializeUInt8();
            _age = deserializer.DeserializeUInt8();
            _longId = deserializer.DeserializeEmberEui64();
        }
        
        public override string ToString()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("EmberNeighborTableEntry [shortId=");
            builder.Append(_shortId);
            builder.Append(", averageLqi=");
            builder.Append(_averageLqi);
            builder.Append(", inCost=");
            builder.Append(_inCost);
            builder.Append(", outCost=");
            builder.Append(_outCost);
            builder.Append(", age=");
            builder.Append(_age);
            builder.Append(", longId=");
            builder.Append(_longId);
            builder.Append(']');
            return builder.ToString();
        }
    }
}