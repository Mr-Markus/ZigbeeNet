using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Simple Descriptor Store Response value object class.
    /// 
    /// The Simple_Desc_store_rsp is provided to notify a Local Device of the request
    /// Status from a Primary Discovery Cache device. Included in the response is a Status
    /// code to notify the Local Device whether the request is successful (the Primary
    /// Cache Device has space to store the discovery cache data for the Local Device),
    /// the request is not supported (meaning the Remote Device is not a Primary
    /// Discovery Cache device), or insufficient space exists.
    /// 
    /// </summary>

    public class SimpleDescriptorStoreResponse : ZdoResponse
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public SimpleDescriptorStoreResponse()
        {
            ClusterId = 0x801A;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("SimpleDescriptorStoreResponse [")
                   .Append(base.ToString())
                   .Append(']');

            return builder.ToString();
        }
    }
}
