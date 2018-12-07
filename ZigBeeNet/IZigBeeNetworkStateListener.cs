using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Transport;

namespace ZigBeeNet
{
    public interface IZigBeeNetworkStateListener
    {
        void NetworkStateUpdated(ZigBeeTransportState state);
    }
}
