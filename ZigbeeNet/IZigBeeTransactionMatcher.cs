using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet
{
    public interface IZigBeeTransactionMatcher
    {
        bool IsTransactionMatch(ZigbeeCommand request, ZigbeeCommand response);
    }
}
