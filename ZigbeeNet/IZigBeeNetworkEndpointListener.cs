using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet
{
    public interface IZigBeeNetworkEndpointListener
    {
        /// <summary>
        /// Device was added to network.
        /// </summary>
        /// <param name="endpoint"></param>
        void DeviceAdded(ZigBeeEndpoint endpoint);

        /// <summary>
        /// Device was updated.
        /// </summary>
        /// <param name="endpoint"></param>
        void DeviceUpdated(ZigBeeEndpoint endpoint);

        /// <summary>
        /// Device was removed from network.
        /// </summary>
        /// <param name="endpoint"></param>
        void DeviceRemoved(ZigBeeEndpoint endpoint);
    }
}
