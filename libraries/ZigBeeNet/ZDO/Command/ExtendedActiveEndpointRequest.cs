using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Extended Active Endpoint Request value object class.
    /// 
    /// The Extended_Active_EP_req command is generated from a local device wishing
    /// to acquire the list of endpoints on a remote device with simple descriptors. This
    /// command shall be unicast either to the remote device itself or to an alternative
    /// device that contains the discovery information of the remote device. The
    /// Extended_Active_EP_req is used for devices which support more active
    /// endpoints than can be returned by a single Active_EP_req.
    /// <br>
    /// The NWKAddrOfInterest field shall contain the network address of the remote device for
    /// which the active endpoint list is required. The StartIndex field shall be set in the
    /// request to enable retrieval of lists of active endpoints from devices whose list exceeds
    /// the size of a single ASDU and where fragmentation is not supported.
    /// 
    /// </summary>

    public class ExtendedActiveEndpointRequest : ZdoRequest
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ExtendedActiveEndpointRequest()
        {
            ClusterId = 0x001E;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("ExtendedActiveEndpointRequest [")
                   .Append(base.ToString())
                   .Append(']');

            return builder.ToString();
        }

    }
}
