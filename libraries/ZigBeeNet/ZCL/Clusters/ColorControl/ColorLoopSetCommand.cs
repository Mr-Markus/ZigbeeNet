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
    /// Color Loop Set Command value object class.
    /// <para>
    /// Cluster: Color Control. Command is sent TO the server.
    /// This command is a specific command used for the Color Control cluster.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ColorLoopSetCommand : ZclCommand
    {
        /// <summary>
        /// Update Flags command message field.
        /// </summary>
        public byte UpdateFlags { get; set; }

        /// <summary>
        /// Action command message field.
        /// </summary>
        public byte Action { get; set; }

        /// <summary>
        /// Direction command message field.
        /// </summary>
        public byte Direction { get; set; }

        /// <summary>
        /// Transition time command message field.
        /// </summary>
        public ushort TransitionTime { get; set; }

        /// <summary>
        /// Start Hue command message field.
        /// </summary>
        public ushort StartHue { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public ColorLoopSetCommand()
        {
            GenericCommand = false;
            ClusterId = 768;
            CommandId = 67;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(UpdateFlags, ZclDataType.Get(DataType.BITMAP_8_BIT));
            serializer.Serialize(Action, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(Direction, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(TransitionTime, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(StartHue, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            UpdateFlags = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.BITMAP_8_BIT));
            Action = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            Direction = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            TransitionTime = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            StartHue = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ColorLoopSetCommand [");
            builder.Append(base.ToString());
            builder.Append(", UpdateFlags=");
            builder.Append(UpdateFlags);
            builder.Append(", Action=");
            builder.Append(Action);
            builder.Append(", Direction=");
            builder.Append(Direction);
            builder.Append(", TransitionTime=");
            builder.Append(TransitionTime);
            builder.Append(", StartHue=");
            builder.Append(StartHue);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
