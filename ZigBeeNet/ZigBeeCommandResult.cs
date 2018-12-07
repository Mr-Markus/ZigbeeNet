using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet
{
    public class ZigBeeCommandResult
    {
        public ZigBeeCommand Response { get; set; }

        public ZigBeeCommandResult(ZigBeeCommand response)
        {
            Response = response;
        }
    }
}
