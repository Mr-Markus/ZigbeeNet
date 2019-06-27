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
    /// Image Block Response value object class.
    /// <para>
    /// Cluster: OTA Upgrade. Command is sent FROM the server.
    /// This command is a specific command used for the OTA Upgrade cluster.
    ///
    /// Upon receipt of an Image Block Request command the server shall generate an Image Block Response.
    /// If the server is able to retrieve the data for the client and does not wish to change the image download
    /// rate, it will respond with a status of SUCCESS and it will include all the fields in the payload. The use
    /// of file offset allows the server to send packets with variable data size during the upgrade process. This
    /// allows the server to support a case when the network topology of a client may change during the
    /// upgrade process, for example, mobile client may move around during the upgrade process. If the client
    /// has moved a few hops away, the data size shall be smaller. Moreover, using file offset eliminates the
    /// need for data padding since each Image Block Response command may contain different data size. A
    /// simple server implementation may choose to only support largest possible data size for the worst-case
    /// scenario in order to avoid supporting sending packets with variable data size.
    /// <br>
    /// The server shall respect the maximum data size value requested by the client and shall not send the data
    /// with length greater than that value. The server may send the data with length smaller than the value
    /// depending on the network topology of the client. For example, the client may be able to receive 100
    /// bytes of data at once so it sends the request with 100 as maximum data size. But after considering all
    /// the security headers (perhaps from both APS and network levels) and source routing overhead (for
    /// example, the client is five hops away), the largest possible data size that the server can send to the
    /// client shall be smaller than 100 bytes.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ImageBlockResponse : ZclCommand
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
        /// File offset command message field.
        /// </summary>
        public uint FileOffset { get; set; }

        /// <summary>
        /// Image Data command message field.
        /// </summary>
        public ByteArray ImageData { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public ImageBlockResponse()
        {
            GenericCommand = false;
            ClusterId = 25;
            CommandId = 5;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Status, ZclDataType.Get(DataType.ZCL_STATUS));
            serializer.Serialize(ManufacturerCode, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(ImageType, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(FileVersion, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            serializer.Serialize(FileOffset, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            serializer.Serialize(ImageData, ZclDataType.Get(DataType.BYTE_ARRAY));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Status = deserializer.Deserialize<ZclStatus>(ZclDataType.Get(DataType.ZCL_STATUS));
            ManufacturerCode = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            ImageType = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            FileVersion = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            FileOffset = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            ImageData = deserializer.Deserialize<ByteArray>(ZclDataType.Get(DataType.BYTE_ARRAY));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ImageBlockResponse [");
            builder.Append(base.ToString());
            builder.Append(", Status=");
            builder.Append(Status);
            builder.Append(", ManufacturerCode=");
            builder.Append(ManufacturerCode);
            builder.Append(", ImageType=");
            builder.Append(ImageType);
            builder.Append(", FileVersion=");
            builder.Append(FileVersion);
            builder.Append(", FileOffset=");
            builder.Append(FileOffset);
            builder.Append(", ImageData=");
            builder.Append(ImageData);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
