using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
   /// Cache Request value object class.
   /// 
   /// The Mgmt_Cache_req is provided to enable ZigBee devices on the network to
   /// retrieve a list of ZigBee End Devices registered with a Primary Discovery Cache
   /// device. The destination addressing on this primitive shall be unicast.
   /// </summary>
    public class CacheRequest : ZdoRequest
    {
        /// <summary>
         /// Default constructor.
         /// </summary>
        public CacheRequest()
        {
            ClusterId = 0x0037;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("CacheRequest [")
                   .Append(base.ToString())
                   .Append(']');

            return builder.ToString();
        }

    }
}