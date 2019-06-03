// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.Thermostat;


namespace ZigBeeNet.ZCL.Clusters.Thermostat
{
    /// <summary>
    /// Get Weekly Schedule value object class.
    /// <para>
    /// Cluster: Thermostat. Command is sent TO the server.
    /// This command is a specific command used for the Thermostat cluster.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetWeeklySchedule : ZclCommand
    {
        /// <summary>
        /// Days To Return command message field.
        /// </summary>
        public byte DaysToReturn { get; set; }

        /// <summary>
        /// Mode To Return command message field.
        /// </summary>
        public byte ModeToReturn { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetWeeklySchedule()
        {
            GenericCommand = false;
            ClusterId = 513;
            CommandId = 2;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(DaysToReturn, ZclDataType.Get(DataType.BITMAP_8_BIT));
            serializer.Serialize(ModeToReturn, ZclDataType.Get(DataType.BITMAP_8_BIT));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            DaysToReturn = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.BITMAP_8_BIT));
            ModeToReturn = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.BITMAP_8_BIT));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetWeeklySchedule [");
            builder.Append(base.ToString());
            builder.Append(", DaysToReturn=");
            builder.Append(DaysToReturn);
            builder.Append(", ModeToReturn=");
            builder.Append(ModeToReturn);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
