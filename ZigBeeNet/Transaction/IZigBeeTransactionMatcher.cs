using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet;

namespace ZigBeeNet.Transaction
{
    /**
     * Defines the interface for transaction matcher
     *
     */
    public interface IZigBeeTransactionMatcher
    {
        /**
         * Matches request and response.
         *
         * @param request the request {@link ZigBeeCommand}
         * @param response the response {@link ZigBeeCommand}
         * @return true if request matches response
         */
        bool IsTransactionMatch(ZigBeeCommand request, ZigBeeCommand response);
    }
}
