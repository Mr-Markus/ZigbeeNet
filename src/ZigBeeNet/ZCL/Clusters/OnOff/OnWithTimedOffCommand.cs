// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.OnOff;


namespace ZigBeeNet.ZCL.Clusters.OnOff
{
    /// <summary>
    /// On With Timed Off Command value object class.
    /// <para>
    /// Cluster: On/Off. Command is sent TO the server.
    /// This command is a specific command used for the On/Off cluster.
    ///
    /// The On With Timed Off command allows devices to be turned on for a specific duration
    /// with a guarded off duration so that SHOULD the device be subsequently switched off,
    /// further On With Timed Off commands, received during this time, are prevented from
    /// turning the devices back on. Note that the device can be periodically re-kicked by
    /// subsequent On With Timed Off commands, e.g., from an on/off sensor.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class OnWithTimedOffCommand : ZclCommand
    {
        /// <summary>
        /// On Off Control command message field.
        /// </summary>
        public byte OnOffControl { get; set; }

        /// <summary>
        /// On Time command message field.
        /// </summary>
        public ushort OnTime { get; set; }

        /// <summary>
        /// Off Wait Time command message field.
        /// </summary>
        public ushort OffWaitTime { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public OnWithTimedOffCommand()
        {
            GenericCommand = false;
            ClusterId = 6;
            CommandId = 66;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(OnOffControl, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(OnTime, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(OffWaitTime, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            OnOffControl = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            OnTime = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            OffWaitTime = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("OnWithTimedOffCommand [");
            builder.Append(base.ToString());
            builder.Append(", OnOffControl=");
            builder.Append(OnOffControl);
            builder.Append(", OnTime=");
            builder.Append(OnTime);
            builder.Append(", OffWaitTime=");
            builder.Append(OffWaitTime);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
