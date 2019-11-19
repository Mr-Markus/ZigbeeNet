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
    /// Request Mirror Response value object class.
    ///
    /// Cluster: Metering. Command ID 0x01 is sent TO the server.
    /// This command is a specific command used for the Metering cluster.
    ///
    /// The Request Mirror Response Command allows the ESI to inform a sleepy Metering Device it
    /// has the ability to store and mirror its data.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class RequestMirrorResponse : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0702;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x01;

        /// <summary>
        /// Endpoint ID command message field.
        /// 
        /// 16 Bit Unsigned Integer indicating the End Point ID to contain the Metering Devices
        /// meter data. Valid End Point ID values are 0x0001 to 0x00F0. If the ESI is able to
        /// mirror the Metering Device data, the low byte of the unsigned 16 bit integer shall be
        /// used to contain the eight bit EndPoint ID. If the ESI is unable to mirror the Metering
        /// Device data, EndPoint ID shall be returned as 0xFFFF. All other EndPoint ID values
        /// are reserved. If valid, the Metering device shall use the EndPoint ID to forward its
        /// metered data.
        /// </summary>
        public ushort EndpointId { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public RequestMirrorResponse()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(EndpointId, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            EndpointId = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("RequestMirrorResponse [");
            builder.Append(base.ToString());
            builder.Append(", EndpointId=");
            builder.Append(EndpointId);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
