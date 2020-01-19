using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZDO.Field;


namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Management Permit Joining Response value object class.
    ///
    ///
    /// The Mgmt_Permit_Joining_rsp is generated in response to a unicast
    /// Mgmt_Permit_Joining_req. In the description which follows, note that no response
    /// shall be sent if the Mgmt_Permit_Joining_req was received as a broadcast to all
    /// routers. If this management command is not permitted by the requesting device, a status
    /// of INVALID_REQUEST shall be returned. Upon receipt and after support for
    /// Mgmt_Permit_Joining_req has been verified, the Remote Device shall execute the
    /// NLME-PERMIT-JOINING.request. The Mgmt_Permit-Joining_rsp shall contain the same
    /// status that was contained in the NLME-PERMIT-JOINING.confirm primitive.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ManagementPermitJoiningResponse : ZdoResponse
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x8036;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ManagementPermitJoiningResponse()
        {
            ClusterId = CLUSTER_ID;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(Status, ZclDataType.Get(DataType.ZDO_STATUS));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            Status = deserializer.Deserialize<ZdoStatus>(ZclDataType.Get(DataType.ZDO_STATUS));
            if (Status != ZdoStatus.SUCCESS)
            {
                // Don't read the full response if we have an error
                return;
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ManagementPermitJoiningResponse [");
            builder.Append(base.ToString());
            builder.Append(", Status=");
            builder.Append(Status);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
