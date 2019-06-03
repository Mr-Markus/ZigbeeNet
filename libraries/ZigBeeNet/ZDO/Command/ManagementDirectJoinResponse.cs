using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Management Direct Join Response value object class.
    /// 
    /// The Mgmt_Direct_Join_rsp is generated in response to a Mgmt_Direct_Join_req.
    /// If this management command is not supported, a status of NOT_SUPPORTED
    /// shall be returned. Otherwise, the Remote Device shall implement the following
    /// processing.
    /// 
    /// </summary>
    public class ManagementDirectJoinResponse : ZdoResponse
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ManagementDirectJoinResponse()
        {
            ClusterId = 0x8035;
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

            builder.Append("ManagementDirectJoinResponse [")
                   .Append(base.ToString())
                   .Append(", status=")
                   .Append(Status)
                   .Append(']');

            return builder.ToString();
        }

    }
}
