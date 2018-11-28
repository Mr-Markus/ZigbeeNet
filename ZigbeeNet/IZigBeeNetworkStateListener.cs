using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.Transport;

namespace ZigbeeNet
{
    public interface IZigBeeNetworkStateListener
    {
        void NetworkStateUpdated(ZigBeeTransportState state);
    }
}
