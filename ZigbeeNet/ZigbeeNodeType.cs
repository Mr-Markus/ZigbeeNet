﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet
{
    public enum ZigBeeNodeType : byte
    {
        ZigBeeCoordinator = 0x00,
        ZigBeeRouter = 0x01,
        ZigBeeEndDevice = 0x02
    }
}
