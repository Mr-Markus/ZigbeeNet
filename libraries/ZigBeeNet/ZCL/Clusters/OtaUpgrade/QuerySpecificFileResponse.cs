using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.OtaUpgrade;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.OtaUpgrade
{
    /// <summary>
    /// Query Specific File Response value object class.
    ///
    /// Cluster: Ota Upgrade. Command ID 0x09 is sent FROM the server.
    /// This command is a specific command used for the Ota Upgrade cluster.
    ///
    /// The server sends Query Specific File Response after receiving Query Specific File
    /// Request from a client. The server shall determine whether it first supports the Query
    /// Specific File Request command. Then it shall determine whether it has the specific file
    /// being requested by the client using all the information included in the request. The
    /// upgrade server sends a Query Specific File Response with one of the following status:
    /// SUCCESS, NO_IMAGE_AVAILABLE or NOT_AUTHORIZED. <br> A status of NO_IMAGE_AVAILABLE
    /// indicates that the server currently does not have the device specific file available
    /// for the client. A status of NOT_AUTHORIZED indicates the server is not authorized to
    /// send the file to the client.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class QuerySpecificFileResponse : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0019;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x09;

        /// <summary>
        /// Status command message field.
        /// </summary>
        public ZclStatus Status { get; set; }

        /// <summary>
        /// Image Type command message field.
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
        public QuerySpecificFileResponse()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
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

            builder.Append("QuerySpecificFileResponse [");
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
