using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
   /// Active Endpoint Store Response value object class.
   /// 
   /// The Active_EP_store_rsp is provided to notify a Local Device of the request
   /// status from a Primary Discovery Cache device. Included in the response is a status
   /// code to notify the Local Device whether the request is successful (the Primary
   /// Cache Device has space to store the discovery cache data for the Local Device),
   /// the request is not supported (meaning the Remote Device is not a Primary
   /// Discovery Cache device), or insufficient space exists.
   /// </summary>
    public class ActiveEndpointStoreResponse : ZdoResponse
    {
        /// <summary>
         /// Default constructor.
         /// </summary>
        public ActiveEndpointStoreResponse()
        {
            ClusterId = 0x8019;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("ActiveEndpointStoreResponse [")
                   .Append(base.ToString())
                   .Append(']');

            return builder.ToString();
        }

    }
}