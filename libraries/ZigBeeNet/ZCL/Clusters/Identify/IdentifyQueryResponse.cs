// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.Identify;


namespace ZigBeeNet.ZCL.Clusters.Identify
{
    /// <summary>
    /// Identify Query Response value object class.
    /// <para>
    /// Cluster: Identify. Command is sent FROM the server.
    /// This command is a specific command used for the Identify cluster.
    ///
    /// The identify query response command is generated in response to receiving an
    /// Identify Query command in the case that the device is currently identifying itself.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class IdentifyQueryResponse : ZclCommand
    {
        /// <summary>
        /// Identify Time command message field.
        /// </summary>
        public ushort IdentifyTime { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public IdentifyQueryResponse()
        {
            GenericCommand = false;
            ClusterId = 3;
            CommandId = 0;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(IdentifyTime, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            IdentifyTime = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("IdentifyQueryResponse [");
            builder.Append(base.ToString());
            builder.Append(", IdentifyTime=");
            builder.Append(IdentifyTime);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
