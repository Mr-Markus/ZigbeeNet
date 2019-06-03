using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Management Leave Response value object class.
    /// 
    /// The Mgmt_Leave_rsp is generated in response to a Mgmt_Leave_req. If this
    /// management command is not supported, a Status of NOT_SUPPORTED shall be
    /// returned. Otherwise, the Remote Device shall implement the following
    /// processing.
    /// 
    /// Upon receipt of and after support for the Mgmt_Leave_req has been verified, the
    /// Remote Device shall execute the NLME-LEAVE.request to disassociate from the
    /// currently associated network. The Mgmt_Leave_rsp shall contain the same Status
    /// that was contained in the NLME-LEAVE.confirm primitive.
    /// 
    /// Once a device has disassociated, it may execute pre-programmed logic to perform
    /// NLME-NETWORK-DISCOVERY and NLME-JOIN to join/re-join a network.
    /// 
    /// </summary>
    public class ManagementLeaveResponse : ZdoResponse
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ManagementLeaveResponse()
        {
            ClusterId = 0x8034;
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

            builder.Append("ManagementLeaveResponse [")
                   .Append(base.ToString())
                   .Append(", Status=")
                   .Append(Status)
                   .Append(']');

            return builder.ToString();
        }

    }
}
