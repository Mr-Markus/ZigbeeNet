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
    /// Get Zone Status Command value object class.
    /// <para>
    /// Cluster: IAS ACE. Command is sent TO the server.
    /// This command is a specific command used for the IAS ACE cluster.
    ///
    /// This command is used by ACE clients to request an update of the status of the IAS Zone devices managed by the ACE server
    /// (i.e., the IAS CIE). In particular, this command is useful for battery-powered ACE clients with polling rates longer than
    /// the ZigBee standard check-in rate. The command is similar to the Get Attributes Supported command in that it specifies a
    /// starting Zone ID and a number of Zone IDs for which information is requested. Depending on the number of IAS Zone devices
    /// managed by the IAS ACE server, sending the Zone Status of all zones MAY not fit into a single Get ZoneStatus Response command.
    /// IAS ACE clients MAY need to send multiple Get Zone Status commands in order to get the information they seek.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetZoneStatusCommand : ZclCommand
    {
        /// <summary>
        /// Starting Zone ID command message field.
        ///
        /// Specifies the starting Zone ID at which the IAS Client would like to obtain zone status information.
        /// </summary>
        public byte StartingZoneID { get; set; }

        /// <summary>
        /// Max Zone IDs command message field.
        ///
        /// Specifies the maximum number of Zone IDs and corresponding Zone Statuses that are to be returned by the IAS ACE server
        /// when it responds with a Get Zone Status Response command
        /// </summary>
        public byte MaxZoneIDs { get; set; }

        /// <summary>
        /// Zone Status Mask Flag command message field.
        ///
        /// Functions as a query operand with the Zone Status Mask field. If set to zero (i.e., FALSE), the IAS ACE server SHALL include all Zone
        /// IDs and their status, regardless of their Zone Status when it responds with a Get Zone Status Response command. If set to one (i.e., TRUE),
        /// the IAS ACE server SHALL include only those Zone IDs whose Zone Status attribute is equal to one or more of the Zone Statuses requested
        /// in the Zone Status Mask field of the Get Zone Status command.
        /// <p>
        /// Use of Zone Status Mask Flag and Zone Status Mask fields allow a client to obtain updated information for the subset of Zone IDs
        /// they’re interested in, which is beneficial when the number of IAS Zone devices in a system is large.
        /// </summary>
        public bool ZoneStatusMaskFlag { get; set; }

        /// <summary>
        /// Zone Status Mask command message field.
        ///
        /// Coupled with the Zone Status Mask Flag field, functions as a mask to enable IAS ACE clients to get information about the Zone IDs whose
        /// ZoneStatus attribute is equal to any of the bits indicated by the IAS ACE client in the Zone Status Mask field. The format of this field
        /// is the same as the ZoneStatus attribute in the IAS Zone cluster. Per the Zone Status Mask Flag field, IAS ACE servers SHALL respond with
        /// only the Zone IDs whose ZoneStatus attributes are equal to at least one of the Zone Status bits set in the Zone Status Mask field requested
        /// by the IAS ACE client.For example, if the Zone Status Mask field set to “0x0003” would match IAS Zones whose ZoneStatus attributes are
        /// 0x0001, 0x0002, and 0x0003.
        /// <p>
        /// In other words, if a logical 'AND' between the Zone Status Mask field and the IAS Zone’s ZoneStatus attribute yields a non-zero result,
        /// the IAS ACE server SHALL include that IAS Zone in the Get Zone Status Response command
        /// </summary>
        public ushort ZoneStatusMask { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetZoneStatusCommand()
        {
            GenericCommand = false;
            ClusterId = 1281;
            CommandId = 9;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(StartingZoneID, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(MaxZoneIDs, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(ZoneStatusMaskFlag, ZclDataType.Get(DataType.BOOLEAN));
            serializer.Serialize(ZoneStatusMask, ZclDataType.Get(DataType.BITMAP_16_BIT));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            StartingZoneID = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            MaxZoneIDs = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            ZoneStatusMaskFlag = deserializer.Deserialize<bool>(ZclDataType.Get(DataType.BOOLEAN));
            ZoneStatusMask = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetZoneStatusCommand [");
            builder.Append(base.ToString());
            builder.Append(", StartingZoneID=");
            builder.Append(StartingZoneID);
            builder.Append(", MaxZoneIDs=");
            builder.Append(MaxZoneIDs);
            builder.Append(", ZoneStatusMaskFlag=");
            builder.Append(ZoneStatusMaskFlag);
            builder.Append(", ZoneStatusMask=");
            builder.Append(ZoneStatusMask);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
