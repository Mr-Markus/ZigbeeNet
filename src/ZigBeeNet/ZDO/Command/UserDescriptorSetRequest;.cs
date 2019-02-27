using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// User Descriptor Set Request value object class.
    /// 
    /// The User_Desc_set command is generated from a local device wishing to
    /// configure the user descriptor on a remote device. This command shall be unicast
    /// either to the remote device itself or to an alternative device that contains the
    /// discovery information of the remote device.
    /// <br>
    /// The local device shall generate the User_Desc_set command using the format
    /// illustrated in Table 2.55. The NWKAddrOfInterest field shall contain the network
    /// address of the remote device for which the user descriptor is to be configured and
    /// the UserDescription field shall contain the ASCII character string that is to be
    /// configured in the user descriptor. Characters with ASCII codes numbered 0x00
    /// through 0x1f are not permitted to be included in this string.
    /// 
    /// </summary>
    public class UserDescriptorSetRequest : ZdoRequest
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public UserDescriptorSetRequest()
        {
            ClusterId = 0x0014;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("UserDescriptorSetRequest [")
                   .Append(base.ToString())
                   .Append(']');

            return builder.ToString();
        }
    }
}
