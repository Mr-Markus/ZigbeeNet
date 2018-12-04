using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet
{
    public class ZigbeeCommandResult
    {
        public ZigBeeCommand Response { get; set; }

        public ZigbeeCommandResult(ZigBeeCommand response)
        {
            Response = response;
        }
    }
}
