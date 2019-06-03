using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using ZigBeeNet.ZCL;

[assembly: InternalsVisibleTo("ZigBeeNet.Test"),
    InternalsVisibleTo("ZigBeeNet.Hardware.TI.CC2531.Test"),
    InternalsVisibleTo("ZigBeeNet.Hardware.Digi.XBee.Test")]
namespace ZigBeeNet
{
    public class ZigBeeCommand
    {
        internal IZigBeeAddress SourceAddress { get; set; }

        internal IZigBeeAddress DestinationAddress { get; set; }

        internal ushort ClusterId { get; set; }

        internal byte? TransactionId { get; set; }

        internal bool ApsSecurity { get; set; }

        internal virtual void Serialize(ZclFieldSerializer serializer)
        {
            // Default implementation does nothing - overridden by each class
        }

        internal virtual void Deserialize(ZclFieldDeserializer deserializer)
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
