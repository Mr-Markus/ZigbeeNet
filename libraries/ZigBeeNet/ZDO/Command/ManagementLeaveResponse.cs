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
    /// Management Leave Response value object class.
    ///
    ///
    /// The Mgmt_Leave_rsp is generated in response to a Mgmt_Leave_req. If this management
    /// command is not supported, a status of NOT_SUPPORTED shall be returned. Otherwise, the
    /// Remote Device shall implement the following processing. <br> Upon receipt of and after
    /// support for the Mgmt_Leave_req has been verified, the Remote Device shall execute the
    /// NLME-LEAVE.request to disassociate from the currently associated network. The
    /// Mgmt_Leave_rsp shall contain the same status that was contained in the
    /// NLME-LEAVE.confirm primitive. <br> Once a device has disassociated, it may execute
    /// pre-programmed logic to perform NLME-NETWORK-DISCOVERY and NLME-JOIN to
    /// join/re-join a network.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ManagementLeaveResponse : ZdoResponse
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x8034;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ManagementLeaveResponse()
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

            builder.Append("ManagementLeaveResponse [");
            builder.Append(base.ToString());
            builder.Append(", Status=");
            builder.Append(Status);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
