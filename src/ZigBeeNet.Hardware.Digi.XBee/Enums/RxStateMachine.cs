using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.Digi.XBee.Enums
{
    public enum RxStateMachine
    {
        WAITING,
        RECEIVE_LEN1,
        RECEIVE_LEN2,
        RECEIVE_DATA,
    }
}
