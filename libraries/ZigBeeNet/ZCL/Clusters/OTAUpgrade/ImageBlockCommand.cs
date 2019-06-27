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
    /// Image Block Command value object class.
    /// <para>
    /// Cluster: OTA Upgrade. Command is sent TO the server.
    /// This command is a specific command used for the OTA Upgrade cluster.
    ///
    /// The client device requests the image data at its leisure by sending Image Block Request command to
    /// the upgrade server. The client knows the total number of request commands it needs to send from the
    /// image size value received in Query Next Image Response command.
    /// <br>
    /// The client repeats Image Block Requests until it has successfully obtained all data. Manufacturer code,
    /// image type and file version are included in all further queries regarding that image. The information
    /// eliminates the need for the server to remember which OTA Upgrade Image is being used for each
    /// download process.
    /// <br>
    /// If the client supports the BlockRequestDelay attribute it shall include the value of the attribute as the
    /// BlockRequestDelay field of the Image Block Request message. The client shall ensure that it delays at
    /// least BlockRequestDelay milliseconds after the previous Image Block Request was sent before sending
    /// the next Image Block Request message. A client may delay its next Image Block Requests longer than
    /// its BlockRequestDelay attribute.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ImageBlockCommand : ZclCommand
    {
        /// <summary>
        /// Field control command message field.
        /// </summary>
        public byte FieldControl { get; set; }

        /// <summary>
        /// Manufacturer code command message field.
        /// </summary>
        public ushort ManufacturerCode { get; set; }

        /// <summary>
        /// Image type command message field.
        /// </summary>
        public ushort ImageType { get; set; }

        /// <summary>
        /// File version command message field.
        /// </summary>
        public uint FileVersion { get; set; }

        /// <summary>
        /// File offset command message field.
        /// </summary>
        public uint FileOffset { get; set; }

        /// <summary>
        /// Maximum data size command message field.
        /// </summary>
        public byte MaximumDataSize { get; set; }

        /// <summary>
        /// Request node address command message field.
        /// </summary>
        public IeeeAddress RequestNodeAddress { get; set; }

        /// <summary>
        /// BlockRequestDelay command message field.
        /// </summary>
        public ushort BlockRequestDelay { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public ImageBlockCommand()
        {
            GenericCommand = false;
            ClusterId = 25;
            CommandId = 3;
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
