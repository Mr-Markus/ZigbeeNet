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
    /// Zone Enroll Request Command value object class.
    ///
    /// Cluster: IAS Zone. Command ID 0x01 is sent FROM the server.
    /// This command is a specific command used for the IAS Zone cluster.
    ///
    /// The Zone Enroll Request command is generated when a device embodying the Zone server
    /// cluster wishes to be enrolled as an active alarm device. It must do this immediately it
    /// has joined the network (during commissioning).
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZoneEnrollRequestCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0500;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x01;

        /// <summary>
        /// Zone Type command message field.
        /// </summary>
        public ushort ZoneType { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ZoneEnrollRequestCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(ZoneType, ZclDataType.Get(DataType.ENUMERATION_16_BIT));
            serializer.Serialize(ManufacturerCode, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            ZoneType = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.ENUMERATION_16_BIT));
            ManufacturerCode = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ZoneEnrollRequestCommand [");
            builder.Append(base.ToString());
            builder.Append(", ZoneType=");
            builder.Append(ZoneType);
            builder.Append(", ManufacturerCode=");
            builder.Append(ManufacturerCode);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
