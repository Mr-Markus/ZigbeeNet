using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC
{
    public class Endpoint
    {
        public byte Id { get; set; }

        public Device Device { get; set; }

        public Endpoint(Device device)
        {
            Device = device;
        }
    }
}
