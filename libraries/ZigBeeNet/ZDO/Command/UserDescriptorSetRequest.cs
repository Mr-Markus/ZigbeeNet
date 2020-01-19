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
    /// User Descriptor Set Request value object class.
    ///
    ///
    /// The User_Desc_set command is generated from a local device wishing to configure the
    /// user descriptor on a remote device. This command shall be unicast either to the remote
    /// device itself or to an alternative device that contains the discovery information of
    /// the remote device. <br> The local device shall generate the User_Desc_set command
    /// using the format illustrated in Table 2.55. The NWKAddrOfInterest field shall contain
    /// the network address of the remote device for which the user descriptor is to be
    /// configured and the UserDescription field shall contain the ASCII character string
    /// that is to be configured in the user descriptor. Characters with ASCII codes numbered
    /// 0x00 through 0x1f are not permitted to be included in this string.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class UserDescriptorSetRequest : ZdoRequest
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0014;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public UserDescriptorSetRequest()
        {
            ClusterId = CLUSTER_ID;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("UserDescriptorSetRequest [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
