// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.OTAUpgrade;


namespace ZigBeeNet.ZCL.Clusters.OTAUpgrade
{
    /// <summary>
    /// Query Next Image Response value object class.
    /// <para>
    /// Cluster: OTA Upgrade. Command is sent FROM the server.
    /// This command is a specific command used for the OTA Upgrade cluster.
    ///
    /// The upgrade server sends a Query Next Image Response with one of the following status: SUCCESS,
    /// NO_IMAGE_AVAILABLE or NOT_AUTHORIZED. When a SUCCESS status is sent, it is
    /// considered to be the explicit authorization to a device by the upgrade server that the device may
    /// upgrade to a specific software image.
    /// <br>
    /// A status of NO_IMAGE_AVAILABLE indicates that the server is authorized to upgrade the client but
    /// it currently does not have the (new) OTA upgrade image available for the client. For all clients (both
    /// ZR and ZED)9 , they shall continue sending Query Next Image Requests to the server periodically until
    /// an image becomes available.
    /// <br>
    /// A status of NOT_AUTHORIZED indicates the server is not authorized to upgrade the client. In this
    /// case, the client may perform discovery again to find another upgrade server. The client may implement
    /// an intelligence to avoid querying the same unauthorized server.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class QueryNextImageResponse : ZclCommand
    {
        /// <summary>
        /// Status command message field.
        /// </summary>
        public ZclStatus Status { get; set; }

        /// <summary>
        /// Manufacturer code command message field.
        /// </summary>
        public ushort ManufacturerCode { get; set; }

        /// <summary>
        /// Image type command message field.
        /// </summary>
        public ushort ImageType { get; set; }

        /// <summary>
        /// File Version command message field.
        /// </summary>
        public uint FileVersion { get; set; }

        /// <summary>
        /// Image Size command message field.
        /// </summary>
        public uint ImageSize { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public QueryNextImageResponse()
        {
            GenericCommand = false;
            ClusterId = 25;
            CommandId = 2;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Status, ZclDataType.Get(DataType.ZCL_STATUS));

            if (Status == ZclStatus.SUCCESS)
            {
                serializer.Serialize(ManufacturerCode, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            }

            if (Status == ZclStatus.SUCCESS)
            {
                serializer.Serialize(ImageType, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            }

            if (Status == ZclStatus.SUCCESS)
            {
                serializer.Serialize(FileVersion, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            }

            if (Status == ZclStatus.SUCCESS)
            {
                serializer.Serialize(ImageSize, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            }
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Status = deserializer.Deserialize<ZclStatus>(ZclDataType.Get(DataType.ZCL_STATUS));

            if (Status == ZclStatus.SUCCESS)
            {
                ManufacturerCode = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            }

            if (Status == ZclStatus.SUCCESS)
            {
                ImageType = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            }

            if (Status == ZclStatus.SUCCESS)
            {
                FileVersion = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            }

            if (Status == ZclStatus.SUCCESS)
            {
                ImageSize = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("QueryNextImageResponse [");
            builder.Append(base.ToString());
            builder.Append(", Status=");
            builder.Append(Status);
            builder.Append(", ManufacturerCode=");
            builder.Append(ManufacturerCode);
            builder.Append(", ImageType=");
            builder.Append(ImageType);
            builder.Append(", FileVersion=");
            builder.Append(FileVersion);
            builder.Append(", ImageSize=");
            builder.Append(ImageSize);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
