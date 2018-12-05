using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet
{
    public interface IZigBeeAnnounceListener
    {
        void DeviceStatusUpdate(ZigBeeNodeStatus deviceStatus, ushort networkAddress, IeeeAddress ieeeAddress);
    }
}
