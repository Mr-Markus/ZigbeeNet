using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Scenes;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.Scenes
{
    /// <summary>
    /// Copy Scene Command value object class.
    ///
    /// Cluster: Scenes. Command ID 0x42 is sent TO the server.
    /// This command is a specific command used for the Scenes cluster.
    ///
    /// The Copy Scene command allows a device to efficiently copy scenes from one group/scene
    /// identifier pair to another group/scene identifier pair.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class CopySceneCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0005;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x42;

        /// <summary>
        /// Mode command message field.
        /// </summary>
        public byte Mode { get; set; }

        /// <summary>
        /// Group ID From command message field.
        /// </summary>
        public ushort GroupIdFrom { get; set; }

        /// <summary>
        /// Scene ID From command message field.
        /// </summary>
        public byte SceneIdFrom { get; set; }

        /// <summary>
        /// Group ID To command message field.
        /// </summary>
        public ushort GroupIdTo { get; set; }

        /// <summary>
        /// Scene ID To command message field.
        /// </summary>
        public byte SceneIdTo { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CopySceneCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Mode, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(GroupIdFrom, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(SceneIdFrom, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(GroupIdTo, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(SceneIdTo, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Mode = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            GroupIdFrom = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            SceneIdFrom = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            GroupIdTo = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            SceneIdTo = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("CopySceneCommand [");
            builder.Append(base.ToString());
            builder.Append(", Mode=");
            builder.Append(Mode);
            builder.Append(", GroupIdFrom=");
            builder.Append(GroupIdFrom);
            builder.Append(", SceneIdFrom=");
            builder.Append(SceneIdFrom);
            builder.Append(", GroupIdTo=");
            builder.Append(GroupIdTo);
            builder.Append(", SceneIdTo=");
            builder.Append(SceneIdTo);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
