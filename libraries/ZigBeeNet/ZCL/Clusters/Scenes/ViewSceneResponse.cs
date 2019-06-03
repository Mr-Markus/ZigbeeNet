// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.Scenes;


namespace ZigBeeNet.ZCL.Clusters.Scenes
{
    /// <summary>
    /// View Scene Response value object class.
    /// <para>
    /// Cluster: Scenes. Command is sent FROM the server.
    /// This command is a specific command used for the Scenes cluster.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ViewSceneResponse : ZclCommand
    {
        /// <summary>
        /// Status command message field.
        /// </summary>
        public byte Status { get; set; }

        /// <summary>
        /// Group ID command message field.
        /// </summary>
        public ushort GroupID { get; set; }

        /// <summary>
        /// Scene ID command message field.
        /// </summary>
        public byte SceneID { get; set; }

        /// <summary>
        /// Transition time command message field.
        /// </summary>
        public ushort TransitionTime { get; set; }

        /// <summary>
        /// Scene Name command message field.
        /// </summary>
        public string SceneName { get; set; }

        /// <summary>
        /// Extension field sets command message field.
        /// </summary>
        public List<ExtensionFieldSet> ExtensionFieldSets { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public ViewSceneResponse()
        {
            GenericCommand = false;
            ClusterId = 5;
            CommandId = 1;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Status, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(GroupID, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(SceneID, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(TransitionTime, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(SceneName, ZclDataType.Get(DataType.CHARACTER_STRING));
            serializer.Serialize(ExtensionFieldSets, ZclDataType.Get(DataType.N_X_EXTENSION_FIELD_SET));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Status = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            GroupID = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            SceneID = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            TransitionTime = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            SceneName = deserializer.Deserialize<string>(ZclDataType.Get(DataType.CHARACTER_STRING));
            ExtensionFieldSets = deserializer.Deserialize<List<ExtensionFieldSet>>(ZclDataType.Get(DataType.N_X_EXTENSION_FIELD_SET));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ViewSceneResponse [");
            builder.Append(base.ToString());
            builder.Append(", Status=");
            builder.Append(Status);
            builder.Append(", GroupID=");
            builder.Append(GroupID);
            builder.Append(", SceneID=");
            builder.Append(SceneID);
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
