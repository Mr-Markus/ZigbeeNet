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
    /// Window Covering Go To Tilt Percentage value object class.
    ///
    /// Cluster: Window Covering. Command ID 0x08 is sent TO the server.
    /// This command is a specific command used for the Window Covering cluster.
    ///
    /// Goto the specified tilt percentage
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class WindowCoveringGoToTiltPercentage : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0102;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x08;

        /// <summary>
        /// Percentage Tilt Value command message field.
        /// </summary>
        public byte PercentageTiltValue { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public WindowCoveringGoToTiltPercentage()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(PercentageTiltValue, DataType.UNSIGNED_8_BIT_INTEGER);
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            PercentageTiltValue = deserializer.Deserialize<byte>(DataType.UNSIGNED_8_BIT_INTEGER);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("WindowCoveringGoToTiltPercentage [");
            builder.Append(base.ToString());
            builder.Append(", PercentageTiltValue=");
            builder.Append(PercentageTiltValue);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
