using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.WindowCovering;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.WindowCovering
{
    /// <summary>
    /// Window Covering Go To Lift Value value object class.
    ///
    /// Cluster: Window Covering. Command ID 0x04 is sent TO the server.
    /// This command is a specific command used for the Window Covering cluster.
    ///
    /// Goto the specified lift value
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class WindowCoveringGoToLiftValue : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0102;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x04;

        /// <summary>
        /// Lift Value command message field.
        /// </summary>
        public ushort LiftValue { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public WindowCoveringGoToLiftValue()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(LiftValue, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            LiftValue = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("WindowCoveringGoToLiftValue [");
            builder.Append(base.ToString());
            builder.Append(", LiftValue=");
            builder.Append(LiftValue);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
