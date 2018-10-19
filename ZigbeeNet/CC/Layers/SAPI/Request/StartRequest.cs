using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.SAPI
{
    public class StartRequest : ZpiSREQ
    {
        public StartRequest()
            : base(SapiCommand.startRequest)
        {
        }
    }
}
