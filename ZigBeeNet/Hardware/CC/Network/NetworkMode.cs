using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.CC.Network
{
    public enum NetworkMode : byte
    {
        Coordinator = 0x00,
        Router = 0x01,
        EndDevice = 0x02
    }
}
