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
    /// Move To Color Temperature Command value object class.
    ///
    /// Cluster: Color Control. Command ID 0x0A is sent TO the server.
    /// This command is a specific command used for the Color Control cluster.
    ///
    /// On receipt of this command, a device shall set the value of the ColorMode attribute,
    /// where implemented, to 0x02, and shall then move from its current color to the color given
    /// by the Color Temperature Mireds field.
    /// The movement shall be continuous, i.e., not a step function, and the time taken to move to
    /// the new color shall be equal to the Transition Time field, in 1/10ths of a second.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class MoveToColorTemperatureCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0300;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x0A;

        /// <summary>
        /// Color Temperature command message field.
        /// </summary>
        public ushort ColorTemperature { get; set; }

        /// <summary>
        /// Transition Time command message field.
        /// </summary>
        public ushort TransitionTime { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MoveToColorTemperatureCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(ColorTemperature, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(TransitionTime, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            ColorTemperature = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            TransitionTime = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("MoveToColorTemperatureCommand [");
            builder.Append(base.ToString());
            builder.Append(", ColorTemperature=");
            builder.Append(ColorTemperature);
            builder.Append(", TransitionTime=");
            builder.Append(TransitionTime);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
