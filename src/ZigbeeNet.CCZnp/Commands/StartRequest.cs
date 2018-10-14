using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Commands
{
    public class StartRequest : ZpiSREQ
    {
        public StartRequest()
            : base(SAPI.startRequest)
        {
        }
    }
}
