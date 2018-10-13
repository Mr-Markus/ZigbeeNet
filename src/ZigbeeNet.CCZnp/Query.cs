using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.ZCL;

namespace ZigbeeNet.CC
{
    public class Query
    {
        private CCZnp _znp;

        public Query(CCZnp znp)
        {
            _znp = znp;
        }

        public void DeviceWithEndpoints(ushort nwkAddr, long ieeeAddr)
        {
            List<Endpoint> endpoints = new List<Endpoint>();

            DeviceInfo(nwkAddr, ieeeAddr);
        }

        public void DeviceInfo(ushort nwkAddr, long ieeeAddr)
        {
            Device device = new Device()
            {
                NwkAdress = nwkAddr,
                IeeeAddress = ieeeAddr
            };

            NodeDescRequest nodeDesc = new NodeDescRequest()
            {
                DestinationAddress = nwkAddr,
                NetworkAddressOfInteresst = nwkAddr
            };

            nodeDesc.OnResponse += (object sender, ZpiObject e) =>
            {
                NodeDescResponse rsp = e.ToSpecificObject<NodeDescResponse>();
                device.Type = (Devices)(rsp.LogicaltypeCmplxdescavaiUserdescavai & 0x07);
                device.ManufacturerId = rsp.ManufacturerCode;
            };

            nodeDesc.Request(_znp);
        }

        public static Device GetDeviceInfo(CCZnp controller, long ieeeAddr, ushort nwkAddr, Action<Device> callback)
        {
            Device device = new Device()
            {
                NwkAdress = nwkAddr,
                IeeeAddress = ieeeAddr
            };

            ZpiObject zpiObject = new ZpiObject(ZDO.nodeDescReq);

            zpiObject.RequestArguments["dstaddr"] = nwkAddr;
            zpiObject.RequestArguments["nwkaddrofinterest"] = nwkAddr;

            zpiObject.OnResponse += (object sender, ZpiObject result) =>
            {

                device.Type = (Devices)((byte)result.RequestArguments["logicaltype_cmplxdescavai_userdescavai"] & 0x07);
                device.ManufacturerId = (ushort)result.RequestArguments["manufacturercode"];

                ZpiObject activeEpReq = new ZpiObject(ZDO.activeEpReq);

                activeEpReq.RequestArguments["dstaddr"] = nwkAddr;
                activeEpReq.RequestArguments["nwkaddrofinterest"] = nwkAddr;

                activeEpReq.OnResponse += (object s, ZpiObject e) =>
                {
                    foreach (byte ep in (byte[])result.RequestArguments["activeeplist"])
                    {
                        Endpoint endpoint = new Endpoint(device)
                        {
                            Id = ep
                        };
                        device.Endpoints.Add(endpoint);
                    }
                };

                activeEpReq.Request(controller);
            };

            zpiObject.Request(controller);

            return device;
        }
    }
}
