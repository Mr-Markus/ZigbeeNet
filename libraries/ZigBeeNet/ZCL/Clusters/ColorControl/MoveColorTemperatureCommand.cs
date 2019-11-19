using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.ColorControl;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.ColorControl
{
    /// <summary>
    /// Move Color Temperature Command value object class.
    ///
    /// Cluster: Color Control. Command ID 0x4B is sent TO the server.
    /// This command is a specific command used for the Color Control cluster.
    ///
    /// The Move Color Temperature command allows the color temperature of a lamp to be moved at a
    /// specified rate.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class MoveColorTemperatureCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0300;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x4B;

        /// <summary>
        /// Move Mode command message field.
        /// </summary>
        public byte MoveMode { get; set; }

        /// <summary>
        /// Rate command message field.
        /// </summary>
        public ushort Rate { get; set; }

        /// <summary>
        /// Color Temperature Minimum command message field.
        /// </summary>
        public ushort ColorTemperatureMinimum { get; set; }

        /// <summary>
        /// Color Temperature Maximum command message field.
        /// </summary>
        public ushort ColorTemperatureMaximum { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MoveColorTemperatureCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(MoveMode, ZclDataType.Get(DataType.BITMAP_8_BIT));
            serializer.Serialize(Rate, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(ColorTemperatureMinimum, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(ColorTemperatureMaximum, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            MoveMode = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.BITMAP_8_BIT));
            Rate = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            ColorTemperatureMinimum = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            ColorTemperatureMaximum = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("MoveColorTemperatureCommand [");
            builder.Append(base.ToString());
            builder.Append(", MoveMode=");
            builder.Append(MoveMode);
            builder.Append(", Rate=");
            builder.Append(Rate);
            builder.Append(", ColorTemperatureMinimum=");
            builder.Append(ColorTemperatureMinimum);
            builder.Append(", ColorTemperatureMaximum=");
            builder.Append(ColorTemperatureMaximum);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
