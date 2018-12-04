using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet
{
    public interface IZigBeeAnnounceListener
    {
        void DeviceStatusUpdate(ZigBeeNodeStatus deviceStatus, ZigbeeAddress16 networkAddress, ZigBeeAddress64 ieeeAddress);
    }
}
