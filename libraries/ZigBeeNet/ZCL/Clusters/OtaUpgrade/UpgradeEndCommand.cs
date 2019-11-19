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
    /// Upgrade End Command value object class.
    ///
    /// Cluster: Ota Upgrade. Command ID 0x06 is sent TO the server.
    /// This command is a specific command used for the Ota Upgrade cluster.
    ///
    /// Upon reception all the image data, the client should verify the image to ensure its
    /// integrity and validity. If the device requires signed images it shall examine the image
    /// and verify the signature. Clients may perform additional manufacturer specific
    /// integrity checks to validate the image, for example, CRC check on the actual file data.
    /// <br> If the image fails any integrity checks, the client shall send an Upgrade End
    /// Request command to the upgrade server with a status of INVALID_IMAGE. In this case, the
    /// client may reinitiate the upgrade process in order to obtain a valid OTA upgrade image.
    /// The client shall not upgrade to the bad image and shall discard the downloaded image
    /// data. <br> If the image passes all integrity checks and the client does not require
    /// additional OTA upgrade image file, it shall send back an Upgrade End Request with a
    /// status of SUCCESS. However, if the client requires multiple OTA upgrade image files
    /// before performing an upgrade, it shall send an Upgrade End Request command with status
    /// REQUIRE_MORE_IMAGE. This shall indicate to the server that it cannot yet upgrade the
    /// image it received. <br> If the client decides to cancel the download process for any
    /// other reasons, it has the option of sending Upgrade End Request with status of ABORT at
    /// anytime during the download process. The client shall then try to reinitiate the
    /// download process again at a later time.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class UpgradeEndCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0019;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x06;

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
        /// Default constructor.
        /// </summary>
        public UpgradeEndCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Status, ZclDataType.Get(DataType.ZCL_STATUS));
            serializer.Serialize(ManufacturerCode, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(ImageType, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(FileVersion, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Status = deserializer.Deserialize<ZclStatus>(ZclDataType.Get(DataType.ZCL_STATUS));
            ManufacturerCode = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            ImageType = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            FileVersion = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("UpgradeEndCommand [");
            builder.Append(base.ToString());
            builder.Append(", Status=");
            builder.Append(Status);
            builder.Append(", ManufacturerCode=");
            builder.Append(ManufacturerCode);
            builder.Append(", ImageType=");
            builder.Append(ImageType);
            builder.Append(", FileVersion=");
            builder.Append(FileVersion);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
