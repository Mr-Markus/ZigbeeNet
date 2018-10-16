using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.ZDO
{
    public class StartupFromAppRequest : ZpiObject
    {
        public StartupFromAppRequest(ushort startDelay = 0)
            : base(ZdoCommand.startupFromApp)
        {
            StartDelay = startDelay;
        }

        public ushort StartDelay
        {
            get
            {
                return (ushort)RequestArguments["startdelay"];
            }
            set
            {
                RequestArguments["startdelay"] = value;
            }
        }
    }
}
