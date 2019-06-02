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
    /// Zone Enroll Request Command value object class.
    /// <para>
    /// Cluster: IAS Zone. Command is sent FROM the server.
    /// This command is a specific command used for the IAS Zone cluster.
    ///
    /// The Zone Enroll Request command is generated when a device embodying the Zone server cluster wishes
    /// to be  enrolled as an active  alarm device. It  must do this immediately it has joined the network
    /// (during commissioning).
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZoneEnrollRequestCommand : ZclCommand
    {
        /// <summary>
        /// Zone Type command message field.
        /// </summary>
        public ushort ZoneType { get; set; }

        /// <summary>
        /// Manufacturer Code command message field.
        /// </summary>
        public ushort ManufacturerCode { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public ZoneEnrollRequestCommand()
        {
            GenericCommand = false;
            ClusterId = 1280;
            CommandId = 1;
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
