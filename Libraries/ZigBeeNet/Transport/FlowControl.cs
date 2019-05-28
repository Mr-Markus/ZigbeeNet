using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Transport
{
    public enum FlowControl
    {
        /// <summary>
        /// No flow control
        /// </summary>
        FLOWCONTROL_OUT_NONE,
        /// <summary>
        /// XOn / XOff (software) flow control
        /// </summary>
        FLOWCONTROL_OUT_XONOFF,
        /// <summary>
        /// RTS / CTS (hardware) flow control
        /// </summary>
        FLOWCONTROL_OUT_RTSCTS
    }
}
