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
    /// Remove Scene Command value object class.
    /// <para>
    /// Cluster: Scenes. Command is sent TO the server.
    /// This command is a specific command used for the Scenes cluster.
    ///
    /// The Remove All Scenes may be addressed to a single device or to a group.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class RemoveSceneCommand : ZclCommand
    {
        /// <summary>
        /// Group ID command message field.
        /// </summary>
        public ushort GroupID { get; set; }

        /// <summary>
        /// Scene ID command message field.
        /// </summary>
        public byte SceneID { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public RemoveSceneCommand()
        {
            GenericCommand = false;
            ClusterId = 5;
            CommandId = 2;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(GroupID, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(SceneID, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            GroupID = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            SceneID = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("RemoveSceneCommand [");
            builder.Append(base.ToString());
            builder.Append(", GroupID=");
            builder.Append(GroupID);
            builder.Append(", SceneID=");
            builder.Append(SceneID);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
