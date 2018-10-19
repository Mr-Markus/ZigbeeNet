using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC
{
    public class Coordinator : Device
    {
        public Coordinator(Network network)
            //:base(0x0000)
        {
            Network = network;

            Status = DeviceStatus.Online;
        }

        public Network Network { get; private set; }
    }
}
