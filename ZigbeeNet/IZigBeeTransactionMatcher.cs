using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet
{
    public interface IZigBeeTransactionMatcher
    {
        bool IsTransactionMatch(ZigBeeCommand request, ZigBeeCommand response);
    }
}
