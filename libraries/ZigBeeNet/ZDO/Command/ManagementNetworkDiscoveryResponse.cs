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
    /// Management Network Discovery Response value object class.
    ///
    ///
    /// The Mgmt_NWK_Disc_rsp is generated in response to an Mgmt_NWK_Disc_req. If this
    /// management command is not supported, a status of NOT_SUPPORTED shall be returned and
    /// all parameter fields after the Status field shall be omitted. Otherwise, the Remote
    /// Device shall implement the following process. <br> Upon receipt of and after support
    /// for the Mgmt_NWK_Disc_req has been verified, the Remote Device shall issue an
    /// NLME-NETWORKDISCOVERY.request primitive using the ScanChannels and ScanDuration
    /// parameters, supplied in the Mgmt_NWK_Disc_req command. Upon receipt of the
    /// NLME-NETWORK-DISCOVERY.confirm primitive, the Remote Device shall report the
    /// results, starting with the StartIndex element, via the Mgmt_NWK_Disc_rsp command.
    /// The NetworkList field shall contain whole NetworkList records, until the limit on MSDU
    /// size, i.e., aMaxMACFrameSize, is reached. The number of results reported shall be set
    /// in the NetworkListCount.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ManagementNetworkDiscoveryResponse : ZdoResponse
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x8030;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ManagementNetworkDiscoveryResponse()
        {
            ClusterId = CLUSTER_ID;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ManagementNetworkDiscoveryResponse [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
