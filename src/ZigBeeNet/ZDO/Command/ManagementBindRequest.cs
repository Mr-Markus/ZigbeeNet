using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZDO;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
     /// Management Bind Request value object class.
     /// 
     /// The Mgmt_Bind_req is generated from a Local Device wishing to retrieve the
     /// contents of the Binding Table from the Remote Device. The destination
     /// addressing on this command shall be unicast only and the destination address
     /// must be that of a Primary binding table cache or source device holding its own
     /// binding table.
     /// </summary>
    public class ManagementBindRequest : ZdoRequest, IZigBeeTransactionMatcher
    {
        /// <summary>
         /// StartIndex command message field.
         /// </summary>
        public byte StartIndex { get; set; }

        /// <summary>
         /// Default constructor.
         /// </summary>
        public ManagementBindRequest()
        {
            ClusterId = 0x0033;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(StartIndex, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            StartIndex = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public bool IsTransactionMatch(ZigBeeCommand request, ZigBeeCommand response)
        {
            if (!(response is ManagementBindResponse)) {
                return false;
            }

            return ((ZdoRequest)request).DestinationAddress.Equals(((ManagementBindResponse)response).SourceAddress);
        }

        public override string ToString()
        {
             StringBuilder builder = new StringBuilder();

            builder.Append("ManagementBindRequest [")
                   .Append(base.ToString())
                   .Append(", startIndex=")
                   .Append(StartIndex)
                   .Append(']');

            return builder.ToString();
        }

    }
}
