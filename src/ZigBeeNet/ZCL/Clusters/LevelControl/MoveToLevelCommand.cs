// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.LevelControl;


namespace ZigBeeNet.ZCL.Clusters.LevelControl
{
    /// <summary>
    /// Move to Level Command value object class.
    /// <para>
    /// Cluster: Level Control. Command is sent TO the server.
    /// This command is a specific command used for the Level Control cluster.
    ///
    /// On receipt of this command, a device SHALL move from its current level to the
    /// value given in the Level field. The meaning of ‘level’ is device dependent –e.g.,
    /// for a light it MAY mean brightness level.The movement SHALL be as continuous as
    /// technically practical, i.e., not a step function, and the time taken to move to
    /// the new level SHALL be equal to the value of the Transition time field, in tenths
    /// of a second, or as close to this as the device is able.If the Transition time field
    /// takes the value 0xffff then the time taken to move to the new level SHALL instead
    /// be determined by the OnOffTransitionTimeattribute. If OnOffTransitionTime, which is
    /// an optional attribute, is not present, the device SHALL move to its new level as fast
    /// as it is able.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class MoveToLevelCommand : ZclCommand
    {
        /// <summary>
        /// Level command message field.
        /// </summary>
        public byte Level { get; set; }

        /// <summary>
        /// Transition time command message field.
        /// </summary>
        public ushort TransitionTime { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public MoveToLevelCommand()
        {
            GenericCommand = false;
            ClusterId = 8;
            CommandId = 0;
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
