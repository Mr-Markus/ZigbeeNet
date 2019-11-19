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
    /// Enhanced Move To Hue And Saturation Command value object class.
    ///
    /// Cluster: Color Control. Command ID 0x43 is sent TO the server.
    /// This command is a specific command used for the Color Control cluster.
    ///
    /// The Enhanced Move to Hue and Saturation command allows lamps to be moved in a smooth
    /// continuous transition from their current hue to a target hue and from their current
    /// saturation to a target saturation.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class EnhancedMoveToHueAndSaturationCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0300;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x43;

        /// <summary>
        /// Enhanced Hue command message field.
        /// </summary>
        public ushort EnhancedHue { get; set; }

        /// <summary>
        /// Saturation command message field.
        /// </summary>
        public byte Saturation { get; set; }

        /// <summary>
        /// Transition Time command message field.
        /// </summary>
        public ushort TransitionTime { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public EnhancedMoveToHueAndSaturationCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(EnhancedHue, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(Saturation, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(TransitionTime, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            EnhancedHue = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            Saturation = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            TransitionTime = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("EnhancedMoveToHueAndSaturationCommand [");
            builder.Append(base.ToString());
            builder.Append(", EnhancedHue=");
            builder.Append(EnhancedHue);
            builder.Append(", Saturation=");
            builder.Append(Saturation);
            builder.Append(", TransitionTime=");
            builder.Append(TransitionTime);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
