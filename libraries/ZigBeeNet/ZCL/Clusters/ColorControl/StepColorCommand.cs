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
    /// Step Color Command value object class.
    ///
    /// Cluster: Color Control. Command ID 0x09 is sent TO the server.
    /// This command is a specific command used for the Color Control cluster.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class StepColorCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0300;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x09;

        /// <summary>
        /// Step X command message field.
        /// </summary>
        public short StepX { get; set; }

        /// <summary>
        /// Step Y command message field.
        /// </summary>
        public short StepY { get; set; }

        /// <summary>
        /// Transition Time command message field.
        /// </summary>
        public ushort TransitionTime { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public StepColorCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(StepX, ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            serializer.Serialize(StepY, ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            serializer.Serialize(TransitionTime, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            StepX = deserializer.Deserialize<short>(ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            StepY = deserializer.Deserialize<short>(ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            TransitionTime = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("StepColorCommand [");
            builder.Append(base.ToString());
            builder.Append(", StepX=");
            builder.Append(StepX);
            builder.Append(", StepY=");
            builder.Append(StepY);
            builder.Append(", TransitionTime=");
            builder.Append(TransitionTime);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
