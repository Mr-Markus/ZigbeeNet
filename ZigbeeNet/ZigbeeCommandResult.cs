using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet
{
    public class ZigbeeCommandResult
    {
        public ZigbeeCommand Response { get; set; }

        public ZigbeeCommandResult(ZigbeeCommand response)
        {
            Response = response;
        }
    }
}
