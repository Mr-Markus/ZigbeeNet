using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Serialization;
using ZigBeeNet.ZCL.Clusters.GreenPower;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.GreenPower
{
    /// <summary>
    /// Gp Pairing Configuration Group List structure implementation.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GpPairingConfigurationGroupList : IZigBeeSerializable
    {
        /// <summary>
        /// Sink Group structure field.
        /// </summary>
        public ushort SinkGroup { get; set; }

        /// <summary>
        /// Alias structure field.
        /// </summary>
        public ushort Alias { get; set; }



        public void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(SinkGroup, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(Alias, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public void Deserialize(ZclFieldDeserializer deserializer)
        {
            SinkGroup = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            Alias = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GpPairingConfigurationGroupList [");
            builder.Append(base.ToString());
            builder.Append(", SinkGroup=");
            builder.Append(SinkGroup);
            builder.Append(", Alias=");
            builder.Append(Alias);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
