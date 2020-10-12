using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet;
using ZigBeeNet.App;
using ZigBeeNet.App.Discovery;
using ZigBeeNet.ZDO.Command;
using Serilog;
using ZigBeeNet.ZCL.Clusters;

namespace ZigBeeNet.App.IasClient
{
    /// <summary>
    /// IAS extension. This provides the top level functionality for the IAS application.
    ///
    /// It listens for new nodes that are discovered on the network and registers the <see cref="ZclIasZoneCluster"/>} if the
    /// device supports IAS Zone control.
    /// </summary>
    public class ZigBeeIasCieExtension : IZigBeeNetworkExtension, IZigBeeNetworkNodeListener
    {
        // private final Map<Integer, IeeeAddress> zoneMap = new ConcurrentHashMap<>();

        private ZigBeeNetworkManager networkManager;

        public ZigBeeStatus ExtensionInitialize(ZigBeeNetworkManager networkManager)
        {
            this.networkManager = networkManager;

            networkManager.AddSupportedClientCluster(ZclIasZoneCluster.CLUSTER_ID);
            networkManager.AddNetworkNodeListener(this);
            return ZigBeeStatus.SUCCESS;
        }

        public ZigBeeStatus ExtensionStartup()
        {
            return ZigBeeStatus.SUCCESS;
        }

        public void ExtensionShutdown()
        {
            networkManager.RemoveNetworkNodeListener(this);
        }

        public void NodeAdded(ZigBeeNode node) 
        {
            foreach(ZigBeeEndpoint endpoint in node.GetEndpoints()) 
            {
                if (endpoint.GetInputCluster(ZclIasZoneCluster.CLUSTER_ID) != null) 
                {
                    endpoint.AddApplication(new ZclIasZoneClient(networkManager, networkManager.LocalIeeeAddress, 0));
                    break;
                }
            }
        }

        public void NodeUpdated(ZigBeeNode node)
        {
        }

        public void NodeRemoved(ZigBeeNode node)
        {
        }
    }
}
