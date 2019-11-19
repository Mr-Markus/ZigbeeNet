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
    /// Image Block Command value object class.
    ///
    /// Cluster: Ota Upgrade. Command ID 0x03 is sent TO the server.
    /// This command is a specific command used for the Ota Upgrade cluster.
    ///
    /// The client device requests the image data at its leisure by sending Image Block Request
    /// command to the upgrade server. The client knows the total number of request commands it
    /// needs to send from the image size value received in Query Next Image Response command.
    /// <br> The client repeats Image Block Requests until it has successfully obtained all
    /// data. Manufacturer code, image type and file version are included in all further
    /// queries regarding that image. The information eliminates the need for the server to
    /// remember which OTA Upgrade Image is being used for each download process. <br> If the
    /// client supports the BlockRequestDelay attribute it shall include the value of the
    /// attribute as the BlockRequestDelay field of the Image Block Request message. The
    /// client shall ensure that it delays at least BlockRequestDelay milliseconds after the
    /// previous Image Block Request was sent before sending the next Image Block Request
    /// message. A client may delay its next Image Block Requests longer than its
    /// BlockRequestDelay attribute.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ImageBlockCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0019;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x03;

        /// <summary>
        /// Field Control command message field.
        /// </summary>
        public byte FieldControl { get; set; }

        /// <summary>
        /// Image Type command message field.
        /// </summary>
        public ushort ImageType { get; set; }

        /// <summary>
        /// File Version command message field.
        /// </summary>
        public uint FileVersion { get; set; }

        /// <summary>
        /// File Offset command message field.
        /// </summary>
        public uint FileOffset { get; set; }

        /// <summary>
        /// Maximum Data Size command message field.
        /// </summary>
        public byte MaximumDataSize { get; set; }

        /// <summary>
        /// Request Node Address command message field.
        /// </summary>
        public IeeeAddress RequestNodeAddress { get; set; }

        /// <summary>
        /// Block Request Delay command message field.
        /// </summary>
        public ushort BlockRequestDelay { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ImageBlockCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(FieldControl, ZclDataType.Get(DataType.BITMAP_8_BIT));
            serializer.Serialize(ManufacturerCode, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(ImageType, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(FileVersion, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            serializer.Serialize(FileOffset, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            serializer.Serialize(MaximumDataSize, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            if ((FieldControl & 0x01) != 0)
            {
                serializer.Serialize(RequestNodeAddress, ZclDataType.Get(DataType.IEEE_ADDRESS));
            }
            if ((FieldControl & 0x02) != 0)
            {
                serializer.Serialize(BlockRequestDelay, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            }
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            FieldControl = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.BITMAP_8_BIT));
            ManufacturerCode = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            ImageType = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            FileVersion = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            FileOffset = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            MaximumDataSize = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            if ((FieldControl & 0x01) != 0)
            {
                RequestNodeAddress = deserializer.Deserialize<IeeeAddress>(ZclDataType.Get(DataType.IEEE_ADDRESS));
            }
            if ((FieldControl & 0x02) != 0)
            {
                BlockRequestDelay = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ImageBlockCommand [");
            builder.Append(base.ToString());
            builder.Append(", FieldControl=");
            builder.Append(FieldControl);
            builder.Append(", ManufacturerCode=");
            builder.Append(ManufacturerCode);
            builder.Append(", ImageType=");
            builder.Append(ImageType);
            builder.Append(", FileVersion=");
            builder.Append(FileVersion);
            builder.Append(", FileOffset=");
            builder.Append(FileOffset);
            builder.Append(", MaximumDataSize=");
            builder.Append(MaximumDataSize);
            builder.Append(", RequestNodeAddress=");
            builder.Append(RequestNodeAddress);
            builder.Append(", BlockRequestDelay=");
            builder.Append(BlockRequestDelay);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
