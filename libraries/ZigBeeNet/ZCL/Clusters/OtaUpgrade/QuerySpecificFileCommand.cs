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
    /// Query Specific File Command value object class.
    ///
    /// Cluster: Ota Upgrade. Command ID 0x08 is sent TO the server.
    /// This command is a specific command used for the Ota Upgrade cluster.
    ///
    /// Client devices shall send a Query Specific File Request command to the server to request
    /// for a file that is specific and unique to it. Such file could contain non-firmware data
    /// such as security credential (needed for upgrading from Smart Energy 1.1 to Smart Energy
    /// 2.0), configuration or log. When the device decides to send the Query Specific File
    /// Request command is manufacturer specific. However, one example is during upgrading
    /// from SE 1.1 to 2.0 where the client may have already obtained new SE 2.0 image and now needs
    /// new SE 2.0 security credential data.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class QuerySpecificFileCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0019;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x08;

        /// <summary>
        /// Request Node Address command message field.
        /// </summary>
        public IeeeAddress RequestNodeAddress { get; set; }

        /// <summary>
        /// Image Type command message field.
        /// </summary>
        public ushort ImageType { get; set; }

        /// <summary>
        /// File Version command message field.
        /// </summary>
        public uint FileVersion { get; set; }

        /// <summary>
        /// Zigbee Stack Version command message field.
        /// </summary>
        public ushort ZigbeeStackVersion { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public QuerySpecificFileCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(RequestNodeAddress, ZclDataType.Get(DataType.IEEE_ADDRESS));
            serializer.Serialize(ManufacturerCode, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(ImageType, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(FileVersion, ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            serializer.Serialize(ZigbeeStackVersion, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            RequestNodeAddress = deserializer.Deserialize<IeeeAddress>(ZclDataType.Get(DataType.IEEE_ADDRESS));
            ManufacturerCode = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            ImageType = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            FileVersion = deserializer.Deserialize<uint>(ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER));
            ZigbeeStackVersion = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("QuerySpecificFileCommand [");
            builder.Append(base.ToString());
            builder.Append(", RequestNodeAddress=");
            builder.Append(RequestNodeAddress);
            builder.Append(", ManufacturerCode=");
            builder.Append(ManufacturerCode);
            builder.Append(", ImageType=");
            builder.Append(ImageType);
            builder.Append(", FileVersion=");
            builder.Append(FileVersion);
            builder.Append(", ZigbeeStackVersion=");
            builder.Append(ZigbeeStackVersion);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
