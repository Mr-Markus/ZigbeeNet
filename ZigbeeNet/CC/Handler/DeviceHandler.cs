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
        private ConcurrentBag<ZigbeeNode> _devices;
        private ConcurrentQueue<ZDO_END_DEVICE_ANNCE_IND> _joinQueue;

        public DeviceHandler(CCZnp znp)
        {
            _znp = znp;
            _devices = new ConcurrentBag<ZigbeeNode>();
            _joinQueue = new ConcurrentQueue<ZDO_END_DEVICE_ANNCE_IND>();
        }

        public async Task Handle(AsynchronousRequest asynchronousRequest)
        {
            if (asynchronousRequest is ZDO_END_DEVICE_ANNCE_IND endDevInd)
            {
                endDeviceAnnceHdlr(endDevInd);
                _logger.Info("New Device! NwkAddr: {NwkAddr}, IeeeAddr: {ieeeAddr}", endDevInd.NwkAddr, endDevInd.IEEEAddr);
            }
            if (asynchronousRequest is ZDO_NODE_DESC_RSP nodeDesc)
            {
                ZigbeeNode device = _devices.SingleOrDefault(d => d.NwkAdress.Value == nodeDesc.NwkAddr.Value);

                if (device != null)
                {
                    device.ManufacturerId = nodeDesc.ManufacturerCode;

                    _znp.OnDeviceInfoChanged(device);

                    ZDO_ACTIVE_EP_REQ epReq = new ZDO_ACTIVE_EP_REQ(device.NwkAdress, device.NwkAdress);
                    ZDO_ACTIVE_EP_REQ_SRSP epRsp = await _znp.SendAsync<ZDO_ACTIVE_EP_REQ_SRSP>(epReq).ConfigureAwait(false);
                }
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
                ZigbeeNode device = _devices.SingleOrDefault(d => d.NwkAdress.Value == simpRsp.NwkAddr.Value);

                if (device != null)
                {
                    ZigbeeEndpoint ep = new ZigbeeEndpoint(device)
                    {
                        Id = simpRsp.Endpoint,
                        ProfileId = simpRsp.ProfileId
                    };

                    ep.InClusters.AddRange(simpRsp.InClusterList.Select(c => (ZclCluster)c.Value));
                    ep.OutClusters.AddRange(simpRsp.OutClusterList.Select(c => (ZclCluster)c.Value));

                    device.Endpoints.Add(ep);

                    _znp.OnDeviceInfoChanged(device);                }
            }
        }

        private async void endDeviceAnnceHdlr(ZDO_END_DEVICE_ANNCE_IND deviceInd)
        {
            //TODO: Try to get device from device db and check status if it is online. If true continue with next ind
            ZigbeeNode device = _devices.SingleOrDefault(d => d.IeeeAddress.Value == deviceInd.IEEEAddr.Value);
            if (device != null && device.Status == ZigbeeNodeStatus.Online)
            {
                _logger.Info("Device {Device} already in Network", device.IeeeAddress);

                ZDO_END_DEVICE_ANNCE_IND removed = null;
                if (_joinQueue.TryDequeue(out removed))
                {
                    ZDO_END_DEVICE_ANNCE_IND next = null;

                    if (_joinQueue.TryDequeue(out next))
                    {
                        endDeviceAnnceHdlr(next);
                    }
                }

                return;
            }

            //TODO: Timeout

            device = new ZigbeeNode();
            device.NwkAdress = deviceInd.NwkAddr;
            device.IeeeAddress = deviceInd.IEEEAddr;

            _devices.Add(device);

            _znp.OnNewDevice(device);

            ZDO_NODE_DESC_REQ nodeReq = new ZDO_NODE_DESC_REQ(deviceInd.NwkAddr, deviceInd.NwkAddr);
            await _znp.SendAsync<ZDO_NODE_DESC_REQ_SRSP>(nodeReq).ConfigureAwait(false);
        }
    }
}
