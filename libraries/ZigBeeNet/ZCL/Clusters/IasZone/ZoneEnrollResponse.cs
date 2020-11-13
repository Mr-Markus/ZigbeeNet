using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.IasZone;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.IasZone
{
    /// <summary>
    /// Zone Enroll Response value object class.
    ///
    /// Cluster: IAS Zone. Command ID 0x00 is sent TO the server.
    /// This command is a specific command used for the IAS Zone cluster.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZoneEnrollResponse : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0500;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x00;

        /// <summary>
        /// Enroll Response Code command message field.
        /// </summary>
        public byte EnrollResponseCode { get; set; }

        /// <summary>
        /// Zone ID command message field.
        /// </summary>
        public byte ZoneId { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ZoneEnrollResponse()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(EnrollResponseCode, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(ZoneId, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            EnrollResponseCode = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            ZoneId = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ZoneEnrollResponse [");
            builder.Append(base.ToString());
            builder.Append(", EnrollResponseCode=");
            builder.Append(EnrollResponseCode);
            builder.Append(", ZoneId=");
            builder.Append(ZoneId);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
