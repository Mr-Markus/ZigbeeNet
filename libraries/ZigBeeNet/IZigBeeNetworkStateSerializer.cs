using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet
{
    public interface IZigBeeNetworkStateSerializer
    {
        /// <summary>
        /// Serializes the network state from the ZigBeeNetworkManager.
        /// </summary>
        /// <param name="networkManager"></param>
        void Serialize(ZigBeeNetworkManager networkManager);

        /// <summary>
        /// Deserializes the network state into the ZigBeeNetworkManager.
        /// </summary>
        /// <param name="networkManager"></param>
        void Deserialize(ZigBeeNetworkManager networkManager);
    }
}
