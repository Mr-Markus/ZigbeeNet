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
    /// Enhanced Add Scene Command value object class.
    ///
    /// Cluster: Scenes. Command ID 0x40 is sent TO the server.
    /// This command is a specific command used for the Scenes cluster.
    ///
    /// The Enhanced Add Scene command allows a scene to be added using a finer scene transition
    /// time than the Add Scene command.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class EnhancedAddSceneCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0005;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x40;

        /// <summary>
        /// Group ID command message field.
        /// </summary>
        public ushort GroupId { get; set; }

        /// <summary>
        /// Scene ID command message field.
        /// </summary>
        public byte SceneId { get; set; }

        /// <summary>
        /// Transition Time command message field.
        /// </summary>
        public ushort TransitionTime { get; set; }

        /// <summary>
        /// Scene Name command message field.
        /// </summary>
        public string SceneName { get; set; }

        /// <summary>
        /// Extension Field Sets command message field.
        /// </summary>
        public List<ExtensionFieldSet> ExtensionFieldSets { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public EnhancedAddSceneCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(GroupId, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(SceneId, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(TransitionTime, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(SceneName, ZclDataType.Get(DataType.CHARACTER_STRING));
            serializer.Serialize(ExtensionFieldSets, ZclDataType.Get(DataType.N_X_EXTENSION_FIELD_SET));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            GroupId = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            SceneId = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            TransitionTime = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            SceneName = deserializer.Deserialize<string>(ZclDataType.Get(DataType.CHARACTER_STRING));
            ExtensionFieldSets = deserializer.Deserialize<List<ExtensionFieldSet>>(ZclDataType.Get(DataType.N_X_EXTENSION_FIELD_SET));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("EnhancedAddSceneCommand [");
            builder.Append(base.ToString());
            builder.Append(", GroupId=");
            builder.Append(GroupId);
            builder.Append(", SceneId=");
            builder.Append(SceneId);
            builder.Append(", TransitionTime=");
            builder.Append(TransitionTime);
            builder.Append(", SceneName=");
            builder.Append(SceneName);
            builder.Append(", ExtensionFieldSets=");
            builder.Append(ExtensionFieldSets);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
