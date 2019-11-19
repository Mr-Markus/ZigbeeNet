using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.IASZone;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.IASZone
{
    /// <summary>
    /// Zone Status Change Notification Command value object class.
    ///
    /// Cluster: IAS Zone. Command ID 0x00 is sent FROM the server.
    /// This command is a specific command used for the IAS Zone cluster.
    ///
    /// The Zone Status Change Notification command is generated when a change takes place in
    /// one or more bits of the ZoneStatus attribute.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZoneStatusChangeNotificationCommand : ZclCommand
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
        /// Zone Status command message field.
        /// 
        /// The Zone Status field shall be the current value of the ZoneStatus attribute.
        /// </summary>
        public ushort ZoneStatus { get; set; }

        /// <summary>
        /// Extended Status command message field.
        /// 
        /// The Extended Status field is reserved for additional status information and shall
        /// be set to zero.
        /// </summary>
        public byte ExtendedStatus { get; set; }

        /// <summary>
        /// Zone ID command message field.
        /// 
        /// Zone ID is the index of the Zone in the CIE's zone table.
        /// </summary>
        public byte ZoneId { get; set; }

        /// <summary>
        /// Delay command message field.
        /// 
        /// The Delay field is defined as the amount of time, in quarter-seconds, from the
        /// moment when a change takes place in one or more bits of the Zone Status attribute and
        /// the successful transmission of the Zone Status Change Notification. This is
        /// designed to help congested networks or offline servers quantify the amount of time
        /// from when an event was detected and when it could be reported to the client.
        /// </summary>
        public ushort Delay { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ZoneStatusChangeNotificationCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(ZoneStatus, ZclDataType.Get(DataType.ENUMERATION_16_BIT));
            serializer.Serialize(ExtendedStatus, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(ZoneId, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(Delay, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            ZoneStatus = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.ENUMERATION_16_BIT));
            ExtendedStatus = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            ZoneId = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            Delay = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
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
            builder.Append(", ZoneId=");
            builder.Append(ZoneId);
            builder.Append(", Delay=");
            builder.Append(Delay);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
