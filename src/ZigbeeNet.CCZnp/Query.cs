using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.ZCL;

namespace ZigbeeNet.CC
{
    public static class Query
    {
        public static Dictionary<ushort, Device> Devices = new Dictionary<ushort, Device>;

        public static Device GetDeviceInfo(CCZnp controller, long ieeeAddr, ushort nwkAddr, Action<Device> callback)
        {
            Device device = new Device()
            {
                NwkAdress = nwkAddr
            };

            if(Devices.ContainsKey(nwkAddr) == false)
            {
                Devices.Add(nwkAddr, device);
            }

            ZpiObject zpiObject = new ZpiObject(ZDO.nodeDescReq);

            zpiObject.RequestArguments["dstaddr"] = nwkAddr;
            zpiObject.RequestArguments["nwkaddrofinterest"] = nwkAddr;

            controller.Request(zpiObject, (result) =>
            {
                return;

                device.Type = (Devices)((byte)result.RequestArguments["logicaltype_cmplxdescavai_userdescavai"] & 0x07);
                device.ManufacturerId = (ushort)result.RequestArguments["manufacturercode"];

                ZpiObject activeEpReq = new ZpiObject(ZDO.activeEpReq);

                activeEpReq.RequestArguments["dstaddr"] = nwkAddr;
                activeEpReq.RequestArguments["nwkaddrofinterest"] = nwkAddr;

                controller.Request(activeEpReq);




                if (result.SubSystem == SubSystem.ZDO && result.CommandId == (byte)ZDO.activeEpRsp)
                {
                    ZpiObject activeEpRsp = result;

                    foreach (byte ep in (byte[])result.RequestArguments["activeeplist"])
                    {
                        Endpoint endpoint = new Endpoint(device)
                        {
                            Id = ep
                        };
                        device.Endpoints.Add(endpoint);
                    }

                    callback(device);
                }
            });

            return device;
        }
    }
}
