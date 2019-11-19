using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Metering;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.Metering
{
    /// <summary>
    /// Mirror Report Attribute Response value object class.
    ///
    /// Cluster: Metering. Command ID 0x09 is sent TO the server.
    /// This command is a specific command used for the Metering cluster.
    ///
    /// FIXME: This command is sent in response to the ReportAttribute command when the
    /// MirrorReporting attribute is set.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class MirrorReportAttributeResponse : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0702;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x09;

        /// <summary>
        /// Notification Scheme command message field.
        /// 
        /// An unsigned 8-bit integer that allows for the pre-loading of the Notification
        /// Flags bit mapping to ZCL or Smart Energy Standard commands.
        /// </summary>
        public byte NotificationScheme { get; set; }

        /// <summary>
        /// Notification Flags command message field.
        /// </summary>
        public int NotificationFlags { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MirrorReportAttributeResponse()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(NotificationScheme, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(NotificationFlags, ZclDataType.Get(DataType.BITMAP_32_BIT));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            NotificationScheme = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            NotificationFlags = deserializer.Deserialize<int>(ZclDataType.Get(DataType.BITMAP_32_BIT));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("MirrorReportAttributeResponse [");
            builder.Append(base.ToString());
            builder.Append(", NotificationScheme=");
            builder.Append(NotificationScheme);
            builder.Append(", NotificationFlags=");
            builder.Append(NotificationFlags);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
