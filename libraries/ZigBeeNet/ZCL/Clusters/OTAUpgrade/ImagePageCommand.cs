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
    /// Image Page Command value object class.
    /// <para>
    /// Cluster: OTA Upgrade. Command is sent TO the server.
    /// This command is a specific command used for the OTA Upgrade cluster.
    ///
    /// The support for the command is optional. The client device may choose to request OTA upgrade data
    /// in one page size at a time from upgrade server. Using Image Page Request reduces the numbers of
    /// requests sent from the client to the upgrade server, compared to using Image Block Request command.
    /// In order to conserve battery life a device may use the Image Page Request command. Using the Image
    /// Page Request command eliminates the need for the client device to send Image Block Request
    /// command for every data block it needs; possibly saving the transmission of hundreds or thousands of
    /// messages depending on the image size.
    /// <br>
    /// The client keeps track of how much data it has received by keeping a cumulative count of each data
    /// size it has received in each Image Block Response. Once the count has reach the value of the page size
    /// requested, it shall repeat Image Page Requests until it has successfully obtained all pages. Note that the
    /// client may choose to switch between using Image Block Request and Image Page Request during the
    /// upgrade process. For example, if the client does not receive all data requested in one Image Page
    /// Request, the client may choose to request the missing block of data using Image Block Request
    /// command, instead of requesting the whole page again.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ImagePageCommand : ZclCommand
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
        /// Page size command message field.
        /// </summary>
        public ushort PageSize { get; set; }

        /// <summary>
        /// Response spacing command message field.
        /// </summary>
        public ushort ResponseSpacing { get; set; }

        /// <summary>
        /// Request node address command message field.
        /// </summary>
        public IeeeAddress RequestNodeAddress { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public ImagePageCommand()
        {
            GenericCommand = false;
            ClusterId = 25;
            CommandId = 4;
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
            serializer.Serialize(PageSize, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(ResponseSpacing, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));

            if ((FieldControl & 0x01) != 0)
            {
                serializer.Serialize(RequestNodeAddress, ZclDataType.Get(DataType.IEEE_ADDRESS));
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
            PageSize = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            ResponseSpacing = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));

            if ((FieldControl & 0x01) != 0)
            {
                RequestNodeAddress = deserializer.Deserialize<IeeeAddress>(ZclDataType.Get(DataType.IEEE_ADDRESS));
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ImagePageCommand [");
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
            builder.Append(", PageSize=");
            builder.Append(PageSize);
            builder.Append(", ResponseSpacing=");
            builder.Append(ResponseSpacing);
            builder.Append(", RequestNodeAddress=");
            builder.Append(RequestNodeAddress);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
