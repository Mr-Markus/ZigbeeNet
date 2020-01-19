using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Metering;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.Metering
{
    /// <summary>
    /// Start Sampling Response value object class.
    ///
    /// Cluster: Metering. Command ID 0x0D is sent FROM the server.
    /// This command is a specific command used for the Metering cluster.
    ///
    /// This command is transmitted by a Metering Device in response to a StartSampling
    /// command.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class StartSamplingResponse : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0702;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x0D;

        /// <summary>
        /// Sample ID command message field.
        /// 
        /// 16 Bit Unsigned Integer indicating the ID allocated by the Metering Device for the
        /// requested Sampling session. If the Metering Device is unable to support a further
        /// Sampling session, Sample ID shall be returned as 0xFFFF. If valid, the Sample ID
        /// shall be used for all further communication regarding this Sampling session.
        /// NOTE that the Metering Device may reserve a Sample ID of 0x0000 in order to provide an
        /// alternative mechanism for retrieving Profile data. This mechanism will allow an
        /// increased number of samples to be returned than is available via the existing
        /// (automatically started) Profile mechanism.
        /// </summary>
        public ushort SampleId { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public StartSamplingResponse()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(SampleId, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            SampleId = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("StartSamplingResponse [");
            builder.Append(base.ToString());
            builder.Append(", SampleId=");
            builder.Append(SampleId);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
