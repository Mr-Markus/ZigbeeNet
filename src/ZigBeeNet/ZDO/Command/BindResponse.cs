using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Bind Response value object class.
    /// 
    /// The Bind_rsp is generated in response to a Bind_req. If the Bind_req is processed
    /// and the Binding Table entry committed on the Remote Device, a Status of
    /// SUCCESS is returned. If the Remote Device is not a Primary binding table cache
    /// or the SrcAddress, a Status of NOT_SUPPORTED is returned. The supplied
    /// endpoint shall be checked to determine whether it falls within the specified range.
    /// If it does not, a Status of INVALID_EP shall be returned. If the Remote Device is
    /// the Primary binding table cache or SrcAddress but does not have Binding Table
    /// resources for the request, a Status of TABLE_FULL is returned.
    /// 
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class BindResponse : ZdoResponse
    {
        /// <summary>
         /// Default constructor.
         /// </summary>
        public BindResponse()
        {
            ClusterId = 0x8021;
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

            builder.Append("BindResponse [")
                   .Append(base.ToString())
                   .Append(", status=")
                   .Append(Status)
                   .Append(']');

            return builder.ToString();
        }

    }
}