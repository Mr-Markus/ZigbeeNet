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
    /// Step Color Temperature Command value object class.
    ///
    /// Cluster: Color Control. Command ID 0x4C is sent TO the server.
    /// This command is a specific command used for the Color Control cluster.
    ///
    /// The Step Color Temperature command allows the color temperature of a lamp to be stepped
    /// with a specified step size.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class StepColorTemperatureCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0300;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x4C;

        /// <summary>
        /// Step Mode command message field.
        /// </summary>
        public byte StepMode { get; set; }

        /// <summary>
        /// Step Size command message field.
        /// </summary>
        public ushort StepSize { get; set; }

        /// <summary>
        /// Transition Time command message field.
        /// </summary>
        public ushort TransitionTime { get; set; }

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
        public StepColorTemperatureCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(StepMode, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(StepSize, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(TransitionTime, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(ColorTemperatureMinimum, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(ColorTemperatureMaximum, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            StepMode = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            StepSize = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            TransitionTime = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            ColorTemperatureMinimum = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            ColorTemperatureMaximum = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("StepColorTemperatureCommand [");
            builder.Append(base.ToString());
            builder.Append(", StepMode=");
            builder.Append(StepMode);
            builder.Append(", StepSize=");
            builder.Append(StepSize);
            builder.Append(", TransitionTime=");
            builder.Append(TransitionTime);
            builder.Append(", ColorTemperatureMinimum=");
            builder.Append(ColorTemperatureMinimum);
            builder.Append(", ColorTemperatureMaximum=");
            builder.Append(ColorTemperatureMaximum);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
