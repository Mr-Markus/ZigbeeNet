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
    /// Upgrade End Response value object class.
    /// <para>
    /// Cluster: OTA Upgrade. Command is sent FROM the server.
    /// This command is a specific command used for the OTA Upgrade cluster.
    ///
    /// When an upgrade server receives an Upgrade End Request command with a status of
    /// INVALID_IMAGE, REQUIRE_MORE_IMAGE, or ABORT, no additional processing shall be done
    /// in its part. If the upgrade server receives an Upgrade End Request command with a status of
    /// SUCCESS, it shall generate an Upgrade End Response with the manufacturer code and image type
    /// received in the Upgrade End Request along with the times indicating when the device should upgrade
    /// to the new image.
    /// <br>
    /// The server may send an unsolicited Upgrade End Response command to the client. This may be used
    /// for example if the server wants to synchronize the upgrade on multiple clients simultaneously. For
    /// client devices, the upgrade server may unicast or broadcast Upgrade End Response command
    /// indicating a single client device or multiple client devices shall switch to using their new images. The
    /// command may not be reliably received by sleepy devices if it is sent unsolicited.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class UpgradeEndResponse : ZclCommand
    {
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
            GenericCommand = false;
            ClusterId = 25;
            CommandId = 7;
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
