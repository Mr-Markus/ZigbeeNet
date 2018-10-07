using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Commands
{
    public class StartupFromAppRequest : ZpiObject
    {
        public StartupFromAppRequest(ushort startDelay = 0)
            : base(SubSystem.ZDO, (byte)ZDO.startupFromApp)
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
