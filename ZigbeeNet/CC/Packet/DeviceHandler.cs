using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZigbeeNet.CC.Packet.ZDO;
using ZigbeeNet.Logging;

namespace ZigbeeNet.CC.Packet
{
    public class DeviceHandler : IPacketHandler
    {
        private readonly ILog _logger = LogProvider.For<DeviceHandler>();
        private CCZnp _znp;
        private ConcurrentBag<Device> _devices;
        private ConcurrentQueue<ZDO_END_DEVICE_ANNCE_IND> _joinQueue;
        private bool _spinLock;

        public DeviceHandler(CCZnp znp)
        {
            _znp = znp;
            _devices = new ConcurrentBag<Device>();
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
                Device device = _devices.SingleOrDefault(d => d.NwkAdress.Value == nodeDesc.NwkAddr.Value);

                if (device != null)
                {
                    device.ManufacturerId = nodeDesc.ManufacturerCode;

                    ZDO_ACTIVE_EP_REQ epReq = new ZDO_ACTIVE_EP_REQ(device.NwkAdress, device.NwkAdress);
                    ZDO_ACTIVE_EP_REQ_SRSP epRsp = await _znp.SendAsync<ZDO_ACTIVE_EP_REQ_SRSP>(epReq, msg => { return msg is SynchronousResponse && msg.SubSystem == epReq.SubSystem; }).ConfigureAwait(false);
                }
            }
            if (asynchronousRequest is ZDO_ACTIVE_EP_RSP spRsp)
            {
                foreach (var ep in spRsp.ActiveEpList)
                {
                    ZDO_SIMPLE_DESC_REQ simpleReq = new ZDO_SIMPLE_DESC_REQ(spRsp.NwkAddr, ep);
                    ZDO_SIMPLE_DESC_REQ_SRSP simpleRsp = await _znp.SendAsync<ZDO_SIMPLE_DESC_REQ_SRSP>(simpleReq, msg => { return msg is SynchronousResponse && msg.SubSystem == simpleReq.SubSystem; }).ConfigureAwait(false);
                }
            }
            if (asynchronousRequest is ZDO_SIMPLE_DESC_RSP simpRsp)
            {
                Device device = _devices.SingleOrDefault(d => d.NwkAdress.Value == simpRsp.NwkAddr.Value);

                if (device != null)
                {
                    Endpoint ep = new Endpoint(device)
                    {
                        Id = simpRsp.Endpoint,
                        ProfileId = simpRsp.ProfileId
                    };

                    ep.InClusters.AddRange(simpRsp.InClusterList);
                    ep.OutClusters.AddRange(simpRsp.OutClusterList);

                    device.Endpoints.Add(ep);

                    //TODO: Bind Endpoint via ZDO_BIND_REQ
                }
            }
        }

        private async void endDeviceAnnceHdlr(ZDO_END_DEVICE_ANNCE_IND deviceInd)
        {
            //TODO: Try to get device from device db and check status if it is online. If true continue with next ind
            Device device = _devices.SingleOrDefault(d => d.IeeeAddress.Value == deviceInd.IEEEAddr.Value);
            if (device != null && device.Status == DeviceStatus.Online)
            {
                Console.WriteLine("Device already in Network");

                ZDO_END_DEVICE_ANNCE_IND removed = null;
                if (_joinQueue.TryDequeue(out removed))
                {
                    ZDO_END_DEVICE_ANNCE_IND next = null;

                    if (_joinQueue.TryDequeue(out next))
                    {
                        endDeviceAnnceHdlr(next);
                    }
                    else
                    {
                        _spinLock = false;
                    }
                }

                return;
            }

            //TODO: Timeout

            device = new Device();
            device.NwkAdress = deviceInd.NwkAddr;
            device.IeeeAddress = deviceInd.IEEEAddr;

            _devices.Add(device);

            _znp.OnNewDevice(device);

            ZDO_NODE_DESC_REQ nodeReq = new ZDO_NODE_DESC_REQ(deviceInd.NwkAddr, deviceInd.NwkAddr);
            await _znp.SendAsync<ZDO_NODE_DESC_REQ_SRSP>(nodeReq, msg => msg.SubSystem == nodeReq.SubSystem && msg.Cmd1 == nodeReq.Cmd1).ConfigureAwait(false);
        }
    }
}
