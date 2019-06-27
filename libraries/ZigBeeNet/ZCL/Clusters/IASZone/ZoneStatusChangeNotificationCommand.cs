// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.IASZone;


namespace ZigBeeNet.ZCL.Clusters.IASZone
{
    /// <summary>
    /// Zone Status Change Notification Command value object class.
    /// <para>
    /// Cluster: IAS Zone. Command is sent FROM the server.
    /// This command is a specific command used for the IAS Zone cluster.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZoneStatusChangeNotificationCommand : ZclCommand
    {
        /// <summary>
        /// Zone Status command message field.
        /// </summary>
        public ushort ZoneStatus { get; set; }

        /// <summary>
        /// Extended Status command message field.
        /// </summary>
        public byte ExtendedStatus { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public ZoneStatusChangeNotificationCommand()
        {
            GenericCommand = false;
            ClusterId = 1280;
            CommandId = 0;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(ZoneStatus, ZclDataType.Get(DataType.ENUMERATION_16_BIT));
            serializer.Serialize(ExtendedStatus, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            ZoneStatus = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.ENUMERATION_16_BIT));
            ExtendedStatus = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ZoneStatusChangeNotificationCommand [");
            builder.Append(base.ToString());
            builder.Append(", ZoneStatus=");
            builder.Append(ZoneStatus);
            builder.Append(", ExtendedStatus=");
            builder.Append(ExtendedStatus);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
