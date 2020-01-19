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
    /// Replace Device Request value object class.
    ///
    ///
    /// The Replace_Device_req is intended for use by a special device such as a Commissioning
    /// tool and is sent to a primary binding table cache device to change all binding table
    /// entries which match OldAddress and OldEndpoint as specified. Note that OldEndpoint = 0
    /// has special meaning and signifies that only the address needs to be matched. The
    /// endpoint in the binding table will not be changed in this case and so NewEndpoint is
    /// ignored. The processing changes all binding table entries for which the source address
    /// is the same as OldAddress and, if OldEndpoint is non-zero, for which the source endpoint
    /// is the same as OldEndpoint. It shall also change all binding table entries which have the
    /// destination address the same as OldAddress and, if OldEndpoint is non-zero, the
    /// destination endpoint the same as OldEndpoint. The destination addressing mode for
    /// this request is unicast.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ReplaceDeviceRequest : ZdoRequest
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0024;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ReplaceDeviceRequest()
        {
            ClusterId = CLUSTER_ID;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ReplaceDeviceRequest [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
