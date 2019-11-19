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
    /// Upgrade End Response value object class.
    ///
    /// Cluster: Ota Upgrade. Command ID 0x07 is sent FROM the server.
    /// This command is a specific command used for the Ota Upgrade cluster.
    ///
    /// When an upgrade server receives an Upgrade End Request command with a status of
    /// INVALID_IMAGE, REQUIRE_MORE_IMAGE, or ABORT, no additional processing shall be done
    /// in its part. If the upgrade server receives an Upgrade End Request command with a status
    /// of SUCCESS, it shall generate an Upgrade End Response with the manufacturer code and
    /// image type received in the Upgrade End Request along with the times indicating when the
    /// device should upgrade to the new image. <br> The server may send an unsolicited Upgrade
    /// End Response command to the client. This may be used for example if the server wants to
    /// synchronize the upgrade on multiple clients simultaneously. For client devices, the
    /// upgrade server may unicast or broadcast Upgrade End Response command indicating a
    /// single client device or multiple client devices shall switch to using their new images.
    /// The command may not be reliably received by sleepy devices if it is sent unsolicited.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class UpgradeEndResponse : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0019;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x07;

        /// <summary>
        /// Image Type command message field.
        /// </summary>
        public ushort ImageType { get; set; }

        /// <summary>
        /// File Version command message field.
        /// </summary>
        public uint FileVersion { get; set; }

        /// <summary>
        /// Current Time command message field.
        /// </summary>
        public uint CurrentTime { get; set; }

        /// <summary>
        /// Upgrade Time command message field.
        /// </summary>
        public uint UpgradeTime { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public UpgradeEndResponse()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(ManufacturerCode, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(ImageType, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(FileVersion, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            serializer.Serialize(CurrentTime, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            serializer.Serialize(UpgradeTime, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            ManufacturerCode = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            ImageType = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            FileVersion = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            CurrentTime = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            UpgradeTime = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("UpgradeEndResponse [");
            builder.Append(base.ToString());
            builder.Append(", ManufacturerCode=");
            builder.Append(ManufacturerCode);
            builder.Append(", ImageType=");
            builder.Append(ImageType);
            builder.Append(", FileVersion=");
            builder.Append(FileVersion);
            builder.Append(", CurrentTime=");
            builder.Append(CurrentTime);
            builder.Append(", UpgradeTime=");
            builder.Append(UpgradeTime);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
