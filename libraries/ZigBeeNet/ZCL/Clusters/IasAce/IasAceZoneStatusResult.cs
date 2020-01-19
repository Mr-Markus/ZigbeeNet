using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Serialization;
using ZigBeeNet.ZCL.Clusters.IASACE;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.IASACE
{
    /// <summary>
    /// IAS ACE Zone Status Result structure implementation.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class IasAceZoneStatusResult : IZigBeeSerializable
    {
        /// <summary>
        /// Zone ID structure field.
        /// </summary>
        public byte ZoneId { get; set; }

        /// <summary>
        /// Zone Status structure field.
        /// </summary>
        public ushort ZoneStatus { get; set; }



        public void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(ZoneId, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(ZoneStatus, ZclDataType.Get(DataType.BITMAP_16_BIT));
        }

        public void Deserialize(ZclFieldDeserializer deserializer)
        {
            ZoneId = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            ZoneStatus = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("IasAceZoneStatusResult [");
            builder.Append(base.ToString());
            builder.Append(", ZoneId=");
            builder.Append(ZoneId);
            builder.Append(", ZoneStatus=");
            builder.Append(ZoneStatus);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
