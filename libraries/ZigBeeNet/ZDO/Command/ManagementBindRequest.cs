using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZDO.Field;


namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Management Bind Request value object class.
    ///
    ///
    /// The Mgmt_Bind_req is generated from a Local Device wishing to retrieve the contents of
    /// the Binding Table from the Remote Device. The destination addressing on this command
    /// shall be unicast only and the destination address must be that of a Primary binding table
    /// cache or source device holding its own binding table.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ManagementBindRequest : ZdoRequest, IZigBeeTransactionMatcher
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0033;

        /// <summary>
        /// Start Index command message field.
        /// </summary>
        public byte StartIndex { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ManagementBindRequest()
        {
            ClusterId = CLUSTER_ID;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(StartIndex, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            StartIndex = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public bool IsTransactionMatch(ZigBeeCommand request, ZigBeeCommand response)
        {
            return (response is ManagementBindResponse) && ((ZdoRequest) request).DestinationAddress.Equals(((ManagementBindResponse) response).SourceAddress);
         }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ManagementBindRequest [");
            builder.Append(base.ToString());
            builder.Append(", StartIndex=");
            builder.Append(StartIndex);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
