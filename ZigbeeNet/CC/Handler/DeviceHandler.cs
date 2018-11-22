using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZigbeeNet.CC.Packet;
using ZigbeeNet.CC.Packet.ZDO;
using ZigbeeNet.Logging;
using ZigbeeNet.ZCL;

namespace ZigbeeNet.CC.Handler
{
    public class DeviceHandler : IPacketHandler
    {
        private readonly ILog _logger = LogProvider.For<DeviceHandler>();
        private CCZnp _znp;

        public DeviceHandler(CCZnp znp)
        {
            _znp = znp;
        }

        public async Task Handle(AsynchronousRequest asynchronousRequest)
        {
            if (asynchronousRequest is ZDO_END_DEVICE_ANNCE_IND endDevInd)
            {
                endDeviceAnnceHdlr(endDevInd);
            }
            if (asynchronousRequest is ZDO_NODE_DESC_RSP nodeDesc)
            {
                _znp.OnDeviceInfoChanged(nodeDesc);

                ZDO_ACTIVE_EP_REQ epReq = new ZDO_ACTIVE_EP_REQ(nodeDesc.NwkAddr, nodeDesc.NwkAddr);
                ZDO_ACTIVE_EP_REQ_SRSP epRsp = await _znp.SendAsync<ZDO_ACTIVE_EP_REQ_SRSP>(epReq).ConfigureAwait(false);
            }
            if (asynchronousRequest is ZDO_ACTIVE_EP_RSP spRsp)
            {
                foreach (var ep in spRsp.ActiveEpList)
                {
                    ZDO_SIMPLE_DESC_REQ simpleReq = new ZDO_SIMPLE_DESC_REQ(spRsp.NwkAddr, ep);
                    ZDO_SIMPLE_DESC_REQ_SRSP simpleRsp = await _znp.SendAsync<ZDO_SIMPLE_DESC_REQ_SRSP>(simpleReq).ConfigureAwait(false);
                }
            }
            if (asynchronousRequest is ZDO_SIMPLE_DESC_RSP simpRsp)
            {
                ZigbeeEndpoint ep = new ZigbeeEndpoint()
                {
                    Id = simpRsp.Endpoint,
                    ProfileId = simpRsp.ProfileId
                };

                ep.InClusters.AddRange(simpRsp.InClusterList.Select(c => (ZclCluster)c.Value));
                ep.OutClusters.AddRange(simpRsp.OutClusterList.Select(c => (ZclCluster)c.Value));

                _znp.OnNewEndpoint(simpRsp.NwkAddr, ep);
            }
            if (asynchronousRequest is ZDO_BIND_RSP bindRsp)
            {
                ZigbeeAddress16 srcAddr = bindRsp.srcAddr;
            }
        }

        private async void endDeviceAnnceHdlr(ZDO_END_DEVICE_ANNCE_IND deviceInd)
        {
            //TODO: Timeout

            ZigbeeNode device = new ZigbeeNode();
            device.NwkAdress = deviceInd.NwkAddr;
            device.IeeeAddress = deviceInd.IEEEAddr;

            _znp.OnNewDevice(device);

            ZDO_NODE_DESC_REQ nodeReq = new ZDO_NODE_DESC_REQ(deviceInd.NwkAddr, deviceInd.NwkAddr);
            await _znp.SendAsync<ZDO_NODE_DESC_REQ_SRSP>(nodeReq).ConfigureAwait(false);
        }
    }
}
