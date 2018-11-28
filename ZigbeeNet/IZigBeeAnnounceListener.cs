using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet
{
    public interface IZigBeeAnnounceListener
    {
        void DeviceStatusUpdate(ZigbeeNodeStatus deviceStatus, ZigbeeAddress16 networkAddress, ZigbeeAddress64 ieeeAddress);
    }
}
