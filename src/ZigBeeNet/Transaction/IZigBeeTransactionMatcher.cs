using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet;

namespace ZigBeeNet.Transaction
{
    /// <summary>
     /// Defines the interface for transaction matcher
     ///
     /// </summary>
    public interface IZigBeeTransactionMatcher
    {
        /// <summary>
         /// Matches request and response.
         ///
         /// @param request the request {@link ZigBeeCommand}
         /// @param response the response {@link ZigBeeCommand}
         /// @return true if request matches response
         /// </summary>
        bool IsTransactionMatch(ZigBeeCommand request, ZigBeeCommand response);
    }
}
