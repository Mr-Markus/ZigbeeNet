using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL;

namespace ZigBeeNet
{
    public class ZigBeeCommand
    {
        public IZigBeeAddress SourceAddress { get; set; }

        public IZigBeeAddress DestinationAddress { get; set; }

        public ushort ClusterId { get; set; }

        public byte? TransactionId { get; set; }

        public bool ApsSecurity { get; set; }

        public virtual void Serialize(ZclFieldSerializer serializer)
        {
            // Default implementation does nothing - overridden by each class
        }

        public virtual void Deserialize(ZclFieldDeserializer deserializer)
        {
            // Default implementation does nothing - overridden by each class
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
