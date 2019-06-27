// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.ColorControl;


namespace ZigBeeNet.ZCL.Clusters.ColorControl
{
    /// <summary>
    /// Enhanced Move To Hue and Saturation Command value object class.
    /// <para>
    /// Cluster: Color Control. Command is sent TO the server.
    /// This command is a specific command used for the Color Control cluster.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class EnhancedMoveToHueAndSaturationCommand : ZclCommand
    {
        /// <summary>
        /// Hue command message field.
        /// </summary>
        public ushort Hue { get; set; }

        /// <summary>
        /// Saturation command message field.
        /// </summary>
        public byte Saturation { get; set; }

        /// <summary>
        /// Transition time command message field.
        /// </summary>
        public ushort TransitionTime { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public EnhancedMoveToHueAndSaturationCommand()
        {
            GenericCommand = false;
            ClusterId = 768;
            CommandId = 66;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Hue, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(Saturation, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(TransitionTime, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Hue = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            Saturation = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            TransitionTime = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("EnhancedMoveToHueAndSaturationCommand [");
            builder.Append(base.ToString());
            builder.Append(", Hue=");
            builder.Append(Hue);
            builder.Append(", Saturation=");
            builder.Append(Saturation);
            builder.Append(", TransitionTime=");
            builder.Append(TransitionTime);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
