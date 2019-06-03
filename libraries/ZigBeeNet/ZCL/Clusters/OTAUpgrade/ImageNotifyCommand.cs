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
    /// Image Notify Command value object class.
    /// <para>
    /// Cluster: OTA Upgrade. Command is sent TO the server.
    /// This command is a specific command used for the OTA Upgrade cluster.
    ///
    /// The purpose of sending Image Notify command is so the server has a way to notify client devices of
    /// when the OTA upgrade images are available for them. It eliminates the need for ZR client devices
    /// having to check with the server periodically of when the new images are available. However, all client
    /// devices still need to send in Query Next Image Request command in order to officially start the OTA
    /// upgrade process.
    /// <br>
    /// For ZR client devices, the upgrade server may send out a unicast, broadcast, or multicast indicating it
    /// has the next upgrade image, via an Image Notify command. Since the command may not have APS
    /// security (if it is broadcast or multicast), it is considered purely informational and not authoritative.
    /// Even in the case of a unicast, ZR shall continue to perform the query process described in later section.
    /// <br>
    /// When the command is sent with payload type value of zero, it generally means the server wishes to
    /// notify all clients disregard of their manufacturers, image types or file versions. Query jitter is needed
    /// to protect the server from being flooded with clients’ queries for next image.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ImageNotifyCommand : ZclCommand
    {
        /// <summary>
        /// Payload type command message field.
        /// </summary>
        public byte PayloadType { get; set; }

        /// <summary>
        /// Query jitter command message field.
        /// </summary>
        public byte QueryJitter { get; set; }

        /// <summary>
        /// Manufacturer code command message field.
        /// </summary>
        public ushort ManufacturerCode { get; set; }

        /// <summary>
        /// Image type command message field.
        /// </summary>
        public ushort ImageType { get; set; }

        /// <summary>
        /// New File Version command message field.
        /// </summary>
        public uint NewFileVersion { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public ImageNotifyCommand()
        {
            GenericCommand = false;
            ClusterId = 25;
            CommandId = 0;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(PayloadType, ZclDataType.Get(DataType.ENUMERATION_8_BIT));

            if (PayloadType >= 0)
            {
                serializer.Serialize(QueryJitter, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            }

            if (PayloadType >= 1)
            {
                serializer.Serialize(ManufacturerCode, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            }

            if (PayloadType >= 2)
            {
                serializer.Serialize(ImageType, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            }

            if (PayloadType >= 3)
            {
                serializer.Serialize(NewFileVersion, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            }
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            PayloadType = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));

            if (PayloadType >= 0)
            {
                QueryJitter = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            }

            if (PayloadType >= 1)
            {
                ManufacturerCode = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            }

            if (PayloadType >= 2)
            {
                ImageType = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            }

            if (PayloadType >= 3)
            {
                NewFileVersion = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ImageNotifyCommand [");
            builder.Append(base.ToString());
            builder.Append(", PayloadType=");
            builder.Append(PayloadType);
            builder.Append(", QueryJitter=");
            builder.Append(QueryJitter);
            builder.Append(", ManufacturerCode=");
            builder.Append(ManufacturerCode);
            builder.Append(", ImageType=");
            builder.Append(ImageType);
            builder.Append(", NewFileVersion=");
            builder.Append(NewFileVersion);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
