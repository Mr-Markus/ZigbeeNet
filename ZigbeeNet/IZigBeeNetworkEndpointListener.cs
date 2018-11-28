using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet
{
    public interface IZigBeeNetworkEndpointListener
    {
        /// <summary>
        /// Device was added to network.
        /// </summary>
        /// <param name="endpoint"></param>
        void DeviceAdded(ZigbeeEndpoint endpoint);

        /// <summary>
        /// Device was updated.
        /// </summary>
        /// <param name="endpoint"></param>
        void DeviceUpdated(ZigbeeEndpoint endpoint);

        /// <summary>
        /// Device was removed from network.
        /// </summary>
        /// <param name="endpoint"></param>
        void DeviceRemoved(ZigbeeEndpoint endpoint);
    }
}
