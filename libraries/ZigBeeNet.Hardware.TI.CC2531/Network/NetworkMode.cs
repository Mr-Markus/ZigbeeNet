using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Network
{
    public enum NetworkMode : byte
    {
        Coordinator = 0x00,
        Router = 0x01,
        EndDevice = 0x02
    }
}
