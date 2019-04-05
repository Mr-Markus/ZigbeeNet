using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet;
using ZigBeeNet.ZCL;

namespace ZigBeeNet.ZCL
{
    /// <summary>
     /// The ZCL transaction response matcher.
     /// 
     /// Implements <see cref="ZigBeeTransactionMatcher"> to check if a ZCL transaction matches a request.
     /// The matcher will return true if the request and response transaction IDs match and the request destination address,
     /// and response source address match.
     /// </summary>
    public class ZclTransactionMatcher : IZigBeeTransactionMatcher
    {

        public bool IsTransactionMatch(ZigBeeCommand request, ZigBeeCommand response)
        {
            if (!request.DestinationAddress.Equals(response.SourceAddress))
            {
                return false;
            }

            if (response is ZclCommand rsp && ((ZclCommand)request).TransactionId != null)
            {
                int? transactionId = ((ZclCommand)request).TransactionId;

                return transactionId == rsp.TransactionId;
            }
            else
            {
                return false;
            }
        }
    }
}
