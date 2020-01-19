using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.IASACE;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.IASACE
{
    /// <summary>
    /// Zone Status Changed Command value object class.
    ///
    /// Cluster: IAS ACE. Command ID 0x03 is sent FROM the server.
    /// This command is a specific command used for the IAS ACE cluster.
    ///
    /// This command updates ACE clients in the system of changes to zone status recorded by the
    /// ACE server (e.g., IAS CIE device). An IAS ACE server should send a Zone Status Changed
    /// command upon a change to an IAS Zone device’s ZoneStatus that it manages (i.e., IAS ACE
    /// server should send a Zone Status Changed command upon receipt of a Zone Status Change
    /// Notification command).
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZoneStatusChangedCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0501;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x03;

        /// <summary>
        /// Zone ID command message field.
        /// 
        /// The index of the Zone in the CIE’s zone table. If none is programmed, the ZoneID
        /// attribute default value shall be indicated in this field.
        /// </summary>
        public byte ZoneId { get; set; }

        /// <summary>
        /// Zone Status command message field.
        /// </summary>
        public ushort ZoneStatus { get; set; }

        /// <summary>
        /// Audible Notification command message field.
        /// </summary>
        public byte AudibleNotification { get; set; }

        /// <summary>
        /// Zone Label command message field.
        /// 
        /// Provides the ZoneLabel stored in the IAS CIE. If none is programmed, the IAS ACE
        /// server shall transmit a string with a length of zero. There is no minimum or maximum
        /// length to the Zone Label field; however, the Zone Label should be between 16 to 24
        /// alphanumeric characters in length.
        /// </summary>
        public string ZoneLabel { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ZoneStatusChangedCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(ZoneId, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(ZoneStatus, ZclDataType.Get(DataType.ENUMERATION_16_BIT));
            serializer.Serialize(AudibleNotification, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(ZoneLabel, ZclDataType.Get(DataType.CHARACTER_STRING));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            ZoneId = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            ZoneStatus = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.ENUMERATION_16_BIT));
            AudibleNotification = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            ZoneLabel = deserializer.Deserialize<string>(ZclDataType.Get(DataType.CHARACTER_STRING));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ZoneStatusChangedCommand [");
            builder.Append(base.ToString());
            builder.Append(", ZoneId=");
            builder.Append(ZoneId);
            builder.Append(", ZoneStatus=");
            builder.Append(ZoneStatus);
            builder.Append(", AudibleNotification=");
            builder.Append(AudibleNotification);
            builder.Append(", ZoneLabel=");
            builder.Append(ZoneLabel);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
