using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet
{
    public class Endpoint
    {
        public Device Device { get; set; }

        public Endpoint(Device device)
        {
            Device = device;
        }
    }
}
