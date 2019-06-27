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
    /// Get Zone Status Response value object class.
    /// <para>
    /// Cluster: IAS ACE. Command is sent FROM the server.
    /// This command is a specific command used for the IAS ACE cluster.
    ///
    /// This command updates requesting IAS ACE clients in the system of changes to the IAS Zone server statuses recorded
    /// by the ACE server (e.g., IAS CIE device).
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetZoneStatusResponse : ZclCommand
    {
        /// <summary>
        /// Zone Status Complete command message field.
        ///
        /// Indicates whether there are additional Zone IDs managed by the IAS ACE Server with Zone Status information to be obtained.
        /// A value of zero (i.e. FALSE) indicates there are additional Zone IDs for which Zone Status information is available and
        /// that the IAS ACE client SHOULD send another Get Zone Status command.A value of one (i.e. TRUE) indicates there are no
        /// more Zone IDs for the IAS ACE client to query and the IAS ACE client has received all the Zone Status information for all
        /// IAS Zones managed by the IAS ACE server.
        /// <p>
        /// The IAS ACE client SHOULD NOT typically send another Get Zone Status command.
        /// </summary>
        public bool ZoneStatusComplete { get; set; }

        /// <summary>
        /// Number of zones command message field.
        /// </summary>
        public byte NumberOfZones { get; set; }

        /// <summary>
        /// Ias Ace Zone Status command message field.
        /// </summary>
        public byte IasAceZoneStatus { get; set; }

        /// <summary>
        /// Zone Id command message field.
        /// </summary>
        public byte ZoneId { get; set; }

        /// <summary>
        /// Zone Status command message field.
        /// </summary>
        public ushort ZoneStatus { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetZoneStatusResponse()
        {
            GenericCommand = false;
            ClusterId = 1281;
            CommandId = 8;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(ZoneStatusComplete, ZclDataType.Get(DataType.BOOLEAN));
            serializer.Serialize(NumberOfZones, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(IasAceZoneStatus, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(ZoneId, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(ZoneStatus, ZclDataType.Get(DataType.BITMAP_16_BIT));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            ZoneStatusComplete = deserializer.Deserialize<bool>(ZclDataType.Get(DataType.BOOLEAN));
            NumberOfZones = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            IasAceZoneStatus = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            ZoneId = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            ZoneStatus = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetZoneStatusResponse [");
            builder.Append(base.ToString());
            builder.Append(", ZoneStatusComplete=");
            builder.Append(ZoneStatusComplete);
            builder.Append(", NumberOfZones=");
            builder.Append(NumberOfZones);
            builder.Append(", IasAceZoneStatus=");
            builder.Append(IasAceZoneStatus);
            builder.Append(", ZoneId=");
            builder.Append(ZoneId);
            builder.Append(", ZoneStatus=");
            builder.Append(ZoneStatus);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
