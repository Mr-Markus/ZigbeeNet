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
    /// Window Covering Go To Tilt Value value object class.
    ///
    /// Cluster: Window Covering. Command ID 0x07 is sent TO the server.
    /// This command is a specific command used for the Window Covering cluster.
    ///
    /// Goto the specified tilt value
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class WindowCoveringGoToTiltValue : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0102;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x07;

        /// <summary>
        /// Tilt Value command message field.
        /// </summary>
        public ushort TiltValue { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public WindowCoveringGoToTiltValue()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(TiltValue, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            TiltValue = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("WindowCoveringGoToTiltValue [");
            builder.Append(base.ToString());
            builder.Append(", TiltValue=");
            builder.Append(TiltValue);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
