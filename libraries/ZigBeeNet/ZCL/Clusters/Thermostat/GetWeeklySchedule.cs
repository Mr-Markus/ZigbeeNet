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
    /// Get Weekly Schedule value object class.
    ///
    /// Cluster: Thermostat. Command ID 0x02 is sent TO the server.
    /// This command is a specific command used for the Thermostat cluster.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetWeeklySchedule : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0201;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x02;

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
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
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
