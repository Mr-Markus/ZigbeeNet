using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Thermostat;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.Thermostat
{
    /// <summary>
    /// Set Weekly Schedule value object class.
    ///
    /// Cluster: Thermostat. Command ID 0x01 is sent TO the server.
    /// This command is a specific command used for the Thermostat cluster.
    ///
    /// The set weekly schedule command is used to update the thermostat weekly set point
    /// schedule from a management system. If the thermostat already has a weekly set point
    /// schedule programmed then it should replace each daily set point set as it receives the
    /// updates from the management system. For example if the thermostat has 4 set points for
    /// every day of the week and is sent a Set Weekly Schedule command with one set point for
    /// Saturday then the thermostat should remove all 4 set points for Saturday and replace
    /// those with the updated set point but leave all other days unchanged. <br> If the schedule
    /// is larger than what fits in one ZigBee frame or contains more than 10 transitions, the
    /// schedule shall then be sent using multipleSet Weekly Schedule Commands.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class SetWeeklySchedule : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0201;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x01;

        /// <summary>
        /// Number Of Transitions command message field.
        /// </summary>
        public byte NumberOfTransitions { get; set; }

        /// <summary>
        /// Day Of Week command message field.
        /// </summary>
        public byte DayOfWeek { get; set; }

        /// <summary>
        /// Mode command message field.
        /// </summary>
        public byte Mode { get; set; }

        /// <summary>
        /// Transition command message field.
        /// </summary>
        public ushort Transition { get; set; }

        /// <summary>
        /// Heat Set command message field.
        /// </summary>
        public short HeatSet { get; set; }

        /// <summary>
        /// Cool Set command message field.
        /// </summary>
        public short CoolSet { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SetWeeklySchedule()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(NumberOfTransitions, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(DayOfWeek, ZclDataType.Get(DataType.BITMAP_8_BIT));
            serializer.Serialize(Mode, ZclDataType.Get(DataType.BITMAP_8_BIT));
            serializer.Serialize(Transition, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(HeatSet, ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            serializer.Serialize(CoolSet, ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            NumberOfTransitions = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            DayOfWeek = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.BITMAP_8_BIT));
            Mode = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.BITMAP_8_BIT));
            Transition = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            HeatSet = deserializer.Deserialize<short>(ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            CoolSet = deserializer.Deserialize<short>(ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("SetWeeklySchedule [");
            builder.Append(base.ToString());
            builder.Append(", NumberOfTransitions=");
            builder.Append(NumberOfTransitions);
            builder.Append(", DayOfWeek=");
            builder.Append(DayOfWeek);
            builder.Append(", Mode=");
            builder.Append(Mode);
            builder.Append(", Transition=");
            builder.Append(Transition);
            builder.Append(", HeatSet=");
            builder.Append(HeatSet);
            builder.Append(", CoolSet=");
            builder.Append(CoolSet);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
