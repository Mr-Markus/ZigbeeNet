using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Commands
{
    public class StartRequest : ZpiObject
    {
        public StartRequest()
            : base(SubSystem.SAPI, (byte)SAPI.startRequest)
        {
        }
    }
}
