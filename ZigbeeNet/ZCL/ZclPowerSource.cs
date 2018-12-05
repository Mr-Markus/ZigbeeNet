﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL
{
    public enum PowerSource
    {
        Unkown = 0,
        MainsSinglePhase = 1,
        Mains3Phase = 2,
        Battery = 3,
        DCSource = 4,
        EmergencyMainsConstantlyPowered = 5,
        EmergencyMainsAndTransferSwith = 6
    }
}
