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
    /// Move Color Command value object class.
    /// <para>
    /// Cluster: Color Control. Command is sent TO the server.
    /// This command is a specific command used for the Color Control cluster.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class MoveColorCommand : ZclCommand
    {
        /// <summary>
        /// RateX command message field.
        /// </summary>
        public short RateX { get; set; }

        /// <summary>
        /// RateY command message field.
        /// </summary>
        public short RateY { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public MoveColorCommand()
        {
            GenericCommand = false;
            ClusterId = 768;
            CommandId = 8;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(RateX, ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            serializer.Serialize(RateY, ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            RateX = deserializer.Deserialize<short>(ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            RateY = deserializer.Deserialize<short>(ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("MoveColorCommand [");
            builder.Append(base.ToString());
            builder.Append(", RateX=");
            builder.Append(RateX);
            builder.Append(", RateY=");
            builder.Append(RateY);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
