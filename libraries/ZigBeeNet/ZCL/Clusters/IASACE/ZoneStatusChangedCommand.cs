// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.IASACE;


namespace ZigBeeNet.ZCL.Clusters.IASACE
{
    /// <summary>
    /// Zone Status Changed Command value object class.
    /// <para>
    /// Cluster: IAS ACE. Command is sent FROM the server.
    /// This command is a specific command used for the IAS ACE cluster.
    ///
    /// This command updates ACE clients in the system of changes to zone status recorded by the ACE server (e.g., IAS CIE device).
    /// An IAS ACE server SHOULD send a Zone Status Changed command upon a change to an IAS Zone device’s ZoneStatus that it manages (i.e.,
    /// IAS ACE server SHOULD send a Zone Status Changed command upon receipt of a Zone Status Change Notification command).
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZoneStatusChangedCommand : ZclCommand
    {
        /// <summary>
        /// Zone ID command message field.
        ///
        /// The index of the Zone in the CIE’s zone table (Table 8-11). If none  is programmed, the  ZoneID  attribute default
        /// value SHALL be indicated in this field.
        /// </summary>
        public byte ZoneID { get; set; }

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
        /// Provides the ZoneLabel stored in the IAS CIE. If none is programmed, the IAS ACE server SHALL transmit a string with a length
        /// of zero. There is no minimum or maximum length to the Zone Label field; however, the Zone Label SHOULD be between 16 to 24
        /// alphanumeric characters in length.
        /// </summary>
        public string ZoneLabel { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public ZoneStatusChangedCommand()
        {
            GenericCommand = false;
            ClusterId = 1281;
            CommandId = 3;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(ZoneID, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(ZoneStatus, ZclDataType.Get(DataType.ENUMERATION_16_BIT));
            serializer.Serialize(AudibleNotification, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(ZoneLabel, ZclDataType.Get(DataType.CHARACTER_STRING));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            ZoneID = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            ZoneStatus = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.ENUMERATION_16_BIT));
            AudibleNotification = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            ZoneLabel = deserializer.Deserialize<string>(ZclDataType.Get(DataType.CHARACTER_STRING));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ZoneStatusChangedCommand [");
            builder.Append(base.ToString());
            builder.Append(", ZoneID=");
            builder.Append(ZoneID);
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
