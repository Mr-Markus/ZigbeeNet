using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.ZCL;

namespace ZigbeeNet
{
    public class ZigBeeCommand
    {
        public ZigbeeAddress SourceAddress { get; set; }

        public ZigbeeAddress DestinationAddress { get; set; }

        public ushort ClusterId { get; set; }

        public byte TransactionId { get; set; }

        public bool ApsSecurity { get; set; }

        public virtual void Serialize(ZclFieldSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public virtual void Deserialize(ZclFieldDeserializer serializer)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(SourceAddress);
            sb.Append(" -> ");
            sb.Append(DestinationAddress);
            sb.Append($", cluster={ClusterId}, transId={TransactionId}");

            return sb.ToString();
        }
    }
}
