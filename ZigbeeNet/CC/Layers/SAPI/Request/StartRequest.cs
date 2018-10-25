using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.SAPI
{
    public class StartRequest : ZpiObject
    {
        public StartRequest()
            : base(CommandType.ZB_START_REQUEST)
        {
        }
    }
}
