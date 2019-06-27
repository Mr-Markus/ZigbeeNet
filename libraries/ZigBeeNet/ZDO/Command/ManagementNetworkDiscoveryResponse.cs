using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Management Network Discovery Response value object class.
    /// 
    /// The Mgmt_NWK_Disc_rsp is generated in response to an
    /// Mgmt_NWK_Disc_req. If this management command is not supported, a Status
    /// of NOT_SUPPORTED shall be returned and all parameter fields after the Status
    /// field shall be omitted. Otherwise, the Remote Device shall implement the
    /// following process.
    /// 
    /// Upon receipt of and after support for the Mgmt_NWK_Disc_req has been
    /// verified, the Remote Device shall issue an NLME-NETWORKDISCOVERY.request
    /// primitive using the ScanChannels and ScanDuration
    /// parameters, supplied in the Mgmt_NWK_Disc_req command. Upon receipt of the
    /// NLME-NETWORK-DISCOVERY.confirm primitive, the Remote Device shall
    /// report the results, starting with the StartIndex element, via the
    /// Mgmt_NWK_Disc_rsp command. The NetworkList field shall contain whole
    /// NetworkList records, until the limit on
    /// MSDU size, i.e., aMaxMACFrameSize, is reached. The number of
    /// results reported shall be set in the NetworkListCount.
    /// 
    /// </summary>
    public class ManagementNetworkDiscoveryResponse : ZdoResponse
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ManagementNetworkDiscoveryResponse()
        {
            ClusterId = 0x8030;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("ManagementNetworkDiscoveryResponse [")
                   .Append(base.ToString())
                   .Append(']');

            return builder.ToString();
        }

    }
}
