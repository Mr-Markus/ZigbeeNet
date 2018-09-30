using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet
{
    /// <summary>
    /// Defines the interface for the ZNP (Zigbee Network Processor)
    /// </summary>
    public interface IZnp
    {
        void Init(string port, int baudrate = 115200, Action callback = null);
        void Request(SubSystem subSystem, byte commandId, Dictionary<string, object> valObject, Action callback = null);
    }
}
