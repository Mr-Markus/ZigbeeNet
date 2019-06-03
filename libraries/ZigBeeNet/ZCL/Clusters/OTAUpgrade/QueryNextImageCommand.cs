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
    /// Query Next Image Command value object class.
    /// <para>
    /// Cluster: OTA Upgrade. Command is sent TO the server.
    /// This command is a specific command used for the OTA Upgrade cluster.
    ///
    /// Client devices shall send a Query Next Image Request command to the server to see if there is new
    /// OTA upgrade image available. ZR devices may send the command after receiving Image Notify
    /// command. ZED device shall periodically wake up and send the command to the upgrade server. Client
    /// devices query what the next image is, based on their own information.
    /// <br>
    /// The server takes the client’s information in the command and determines whether it has a suitable
    /// image for the particular client. The decision should be based on specific policy that is specific to the
    /// upgrade server and outside the scope of this document.. However, a recommended default policy is for
    /// the server to send back a response that indicates the availability of an image that matches the
    /// manufacturer code, image type, and the highest available file version of that image on the
    /// server. However, the server may choose to upgrade, downgrade, or reinstall clients’ image, as its
    /// policy dictates. If client’s hardware version is included in the command, the server shall examine the
    /// value against the minimum and maximum hardware versions included in the OTA file header.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class QueryNextImageCommand : ZclCommand
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
        /// Hardware version command message field.
        /// </summary>
        public ushort HardwareVersion { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public QueryNextImageCommand()
        {
            GenericCommand = false;
            ClusterId = 25;
            CommandId = 1;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(FieldControl, ZclDataType.Get(DataType.BITMAP_8_BIT));
            serializer.Serialize(ManufacturerCode, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(ImageType, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(FileVersion, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));

            if ((FieldControl & 0x01) != 0)
            {
                serializer.Serialize(HardwareVersion, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            }
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            FieldControl = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.BITMAP_8_BIT));
            ManufacturerCode = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            ImageType = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            FileVersion = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));

            if ((FieldControl & 0x01) != 0)
            {
                HardwareVersion = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("QueryNextImageCommand [");
            builder.Append(base.ToString());
            builder.Append(", FieldControl=");
            builder.Append(FieldControl);
            builder.Append(", ManufacturerCode=");
            builder.Append(ManufacturerCode);
            builder.Append(", ImageType=");
            builder.Append(ImageType);
            builder.Append(", FileVersion=");
            builder.Append(FileVersion);
            builder.Append(", HardwareVersion=");
            builder.Append(HardwareVersion);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
