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
    /// Move Color Command value object class.
    ///
    /// Cluster: Color Control. Command ID 0x08 is sent TO the server.
    /// This command is a specific command used for the Color Control cluster.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class MoveColorCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0300;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x08;

        /// <summary>
        /// Rate X command message field.
        /// </summary>
        public short RateX { get; set; }

        /// <summary>
        /// Rate Y command message field.
        /// </summary>
        public short RateY { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MoveColorCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
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
