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
    /// Move To Level Command value object class.
    ///
    /// Cluster: Level Control. Command ID 0x00 is sent TO the server.
    /// This command is a specific command used for the Level Control cluster.
    ///
    /// On receipt of this command, a device shall move from its current level to the value given
    /// in the Level field. The meaning of ‘level’ is device dependent –e.g., for a light it may
    /// mean brightness level.The movement shall be as continuous as technically practical,
    /// i.e., not a step function, and the time taken to move to the new level shall be equal to the
    /// value of the Transition time field, in tenths of a second, or as close to this as the device
    /// is able.If the Transition time field takes the value 0xffff then the time taken to move to
    /// the new level shall instead be determined by the OnOffTransitionTimeattribute. If
    /// OnOffTransitionTime, which is an optional attribute, is not present, the device shall
    /// move to its new level as fast as it is able.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class MoveToLevelCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0008;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x00;

        /// <summary>
        /// Level command message field.
        /// </summary>
        public byte Level { get; set; }

        /// <summary>
        /// Transition Time command message field.
        /// </summary>
        public ushort TransitionTime { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MoveToLevelCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Level, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(TransitionTime, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Level = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            TransitionTime = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("MoveToLevelCommand [");
            builder.Append(base.ToString());
            builder.Append(", Level=");
            builder.Append(Level);
            builder.Append(", TransitionTime=");
            builder.Append(TransitionTime);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
