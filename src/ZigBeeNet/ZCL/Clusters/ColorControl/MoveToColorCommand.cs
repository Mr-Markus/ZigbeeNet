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
    /// Move to Color Command value object class.
    /// <para>
    /// Cluster: Color Control. Command is sent TO the server.
    /// This command is a specific command used for the Color Control cluster.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class MoveToColorCommand : ZclCommand
    {
        /// <summary>
        /// ColorX command message field.
        /// </summary>
        public ushort ColorX { get; set; }

        /// <summary>
        /// ColorY command message field.
        /// </summary>
        public ushort ColorY { get; set; }

        /// <summary>
        /// Transition time command message field.
        /// </summary>
        public ushort TransitionTime { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public MoveToColorCommand()
        {
            GenericCommand = false;
            ClusterId = 768;
            CommandId = 7;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(ColorX, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(ColorY, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(TransitionTime, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            ColorX = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            ColorY = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            TransitionTime = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("MoveToColorCommand [");
            builder.Append(base.ToString());
            builder.Append(", ColorX=");
            builder.Append(ColorX);
            builder.Append(", ColorY=");
            builder.Append(ColorY);
            builder.Append(", TransitionTime=");
            builder.Append(TransitionTime);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
