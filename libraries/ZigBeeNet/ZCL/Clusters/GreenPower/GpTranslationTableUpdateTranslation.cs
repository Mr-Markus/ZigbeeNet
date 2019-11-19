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
    /// Gp Translation Table Update Translation structure implementation.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GpTranslationTableUpdateTranslation : IZigBeeSerializable
    {
        /// <summary>
        /// Index structure field.
        /// </summary>
        public byte Index { get; set; }

        /// <summary>
        /// Gpd Command ID structure field.
        /// </summary>
        public byte GpdCommandId { get; set; }

        /// <summary>
        /// Endpoint structure field.
        /// </summary>
        public byte Endpoint { get; set; }

        /// <summary>
        /// Profile structure field.
        /// </summary>
        public ushort Profile { get; set; }

        /// <summary>
        /// Cluster structure field.
        /// </summary>
        public ushort Cluster { get; set; }

        /// <summary>
        /// Zigbee Command ID structure field.
        /// </summary>
        public byte ZigbeeCommandId { get; set; }

        /// <summary>
        /// Zigbee Command Payload structure field.
        /// </summary>
        public ByteArray ZigbeeCommandPayload { get; set; }



        public void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Index, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(GpdCommandId, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(Endpoint, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(Profile, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(Cluster, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(ZigbeeCommandId, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(ZigbeeCommandPayload, ZclDataType.Get(DataType.OCTET_STRING));
        }

        public void Deserialize(ZclFieldDeserializer deserializer)
        {
            Index = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            GpdCommandId = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            Endpoint = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            Profile = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            Cluster = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            ZigbeeCommandId = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            ZigbeeCommandPayload = deserializer.Deserialize<ByteArray>(ZclDataType.Get(DataType.OCTET_STRING));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GpTranslationTableUpdateTranslation [");
            builder.Append(base.ToString());
            builder.Append(", Index=");
            builder.Append(Index);
            builder.Append(", GpdCommandId=");
            builder.Append(GpdCommandId);
            builder.Append(", Endpoint=");
            builder.Append(Endpoint);
            builder.Append(", Profile=");
            builder.Append(Profile);
            builder.Append(", Cluster=");
            builder.Append(Cluster);
            builder.Append(", ZigbeeCommandId=");
            builder.Append(ZigbeeCommandId);
            builder.Append(", ZigbeeCommandPayload=");
            builder.Append(ZigbeeCommandPayload);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
