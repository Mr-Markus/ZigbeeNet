using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.OnOff;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.OnOff
{
    /// <summary>
    /// On With Timed Off Command value object class.
    ///
    /// Cluster: On/Off. Command ID 0x42 is sent TO the server.
    /// This command is a specific command used for the On/Off cluster.
    ///
    /// The On With Timed Off command allows devices to be turned on for a specific duration with a
    /// guarded off duration so that should the device be subsequently switched off, further On
    /// With Timed Off commands, received during this time, are prevented from turning the
    /// devices back on. Note that the device can be periodically re-kicked by subsequent On
    /// With Timed Off commands, e.g., from an on/off sensor.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class OnWithTimedOffCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0006;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x42;

        /// <summary>
        /// On Off Control command message field.
        /// 
        /// The On/Off Control field is 8-bits in length and contains information on how the
        /// device is to be operated.
        /// </summary>
        public byte OnOffControl { get; set; }

        /// <summary>
        /// On Time command message field.
        /// 
        /// The On Time field is 16 bits in length and specifies the length of time (in 1/10ths
        /// second) that the device is to remain “on”, i.e., with its OnOffattribute equal to
        /// 0x01, before automatically turning “off”. This field shall be specified in the
        /// range 0x0000–0xfffe. The Off Wait Time field is 16 bits in length and specifies the
        /// length of time (in 1/10ths second) that the device shall remain “off”, i.e., with
        /// its OnOffattribute equal to 0x00, and guarded to prevent an on command turning the
        /// device back “on”. This field shall be specified in the range 0x0000–0xfffe.
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
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
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
