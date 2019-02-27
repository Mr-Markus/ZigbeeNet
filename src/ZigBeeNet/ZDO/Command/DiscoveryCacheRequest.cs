using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
   /// Discovery Cache Request value object class.
   /// 
   /// The Discovery_Cache_req is provided to enable devices on the network to locate
   /// a Primary Discovery Cache device on the network. The destination addressing on
   /// this primitive shall be broadcast to all devices for which macRxOnWhenIdle =
   /// TRUE.
   /// </summary>
    public class DiscoveryCacheRequest : ZdoRequest
    {
        /// <summary>
         /// Default constructor.
         /// </summary>
        public DiscoveryCacheRequest()
        {
            ClusterId = 0x0012;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("DiscoveryCacheRequest [")
                   .Append(base.ToString())
                   .Append(']');

            return builder.ToString();
        }

    }
}