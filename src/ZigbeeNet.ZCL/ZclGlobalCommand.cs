using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ZigbeeNet.ZCL
{
    public class ZclGlobalCommand : ZclCommand
    {
        public ZclGlobalCommand()
        {

        }

        public ZclGlobalCommand(byte cmdId)
        {
            this.Id = cmdId;
            this.Params = ZclMeta.GetGlobalCommandParams(cmdId);
        }

        public int KnownBufLen { get; set; }
    }
}
