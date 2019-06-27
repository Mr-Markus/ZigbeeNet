using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
   /// Active Endpoint Store Request value object class.
   /// 
   /// The Active_EP_store_req is provided to enable ZigBee end devices on the
   /// network to request storage of their list of Active Endpoints on a Primary
   /// Discovery Cache device which has previously received a SUCCESS status from a
   /// Discovery_store_req to the same Primary Discovery Cache device. Included in
   /// this request is the count of Active Endpoints the Local Device wishes to cache and
   /// the endpoint list itself.
   /// </summary>

    public class ActiveEndpointStoreRequest : ZdoRequest
    {
        /// <summary>
         /// Default constructor.
         /// </summary>
        public ActiveEndpointStoreRequest()
        {
            ClusterId = 0x0019;
        }

    public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("ActiveEndpointStoreRequest [")
                   .Append(base.ToString())
                   .Append(']');

            return builder.ToString();
        }

    }
}