using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Unbind Response value object class.
    /// 
    /// The Unbind_rsp is generated in response to an Unbind_req. If the Unbind_req is
    /// processed and the corresponding Binding Table entry is removed from the Remote
    /// Device, a Status of SUCCESS is returned. If the Remote Device is not the ZigBee
    /// Coordinator or the SrcAddress, a Status of NOT_SUPPORTED is returned. The
    /// supplied endpoint shall be checked to determine whether it falls within the
    /// specified range. If it does not, a Status of INVALID_EP shall be returned If the
    /// Remote Device is the ZigBee Coordinator or SrcAddress but does not have a
    /// Binding Table entry corresponding to the parameters received in the request, a
    /// Status of NO_ENTRY is returned.
    /// 
/// </summary>

    public class UnbindResponse : ZdoResponse
    {
        /// <summary>
        /// Default constructor.
/// </summary>
        public UnbindResponse()
        {
            ClusterId = 0x8022;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(Status, ZclDataType.Get(DataType.ZDO_STATUS));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            Status = (ZdoStatus)deserializer.Deserialize(ZclDataType.Get(DataType.ZDO_STATUS));
            if (Status != ZdoStatus.SUCCESS)
            {
                // Don't read the full response if we have an error
                return;
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("UnbindResponse [")
                   .Append(base.ToString())
                   .Append(", status=")
                   .Append(Status)
                   .Append(']');

        return builder.ToString();
        }
    }
}
