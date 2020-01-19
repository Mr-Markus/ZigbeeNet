using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.LevelControl;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.LevelControl
{
    /// <summary>
    /// Move (with On/Off) Command value object class.
    ///
    /// Cluster: Level Control. Command ID 0x05 is sent TO the server.
    /// This command is a specific command used for the Level Control cluster.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class MoveWithOnOffCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0008;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x05;

        /// <summary>
        /// Move Mode command message field.
        /// </summary>
        public byte MoveMode { get; set; }

        /// <summary>
        /// Rate command message field.
        /// </summary>
        public byte Rate { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MoveWithOnOffCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(MoveMode, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(Rate, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            MoveMode = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            Rate = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("MoveWithOnOffCommand [");
            builder.Append(base.ToString());
            builder.Append(", MoveMode=");
            builder.Append(MoveMode);
            builder.Append(", Rate=");
            builder.Append(Rate);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
