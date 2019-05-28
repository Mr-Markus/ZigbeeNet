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
        /// <param name="request">the request <see cref="ZigBeeCommand"></param>
        /// <param name="response">the response <see cref="ZigBeeCommand"></param>
        /// <returns>true if request matches response</returns>
        /// </summary>
        bool IsTransactionMatch(ZigBeeCommand request, ZigBeeCommand response);
    }
}
