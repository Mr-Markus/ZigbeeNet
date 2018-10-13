using System;
using System.Collections.Generic;
using System.Linq;
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

        public void GetDevice(ushort nwkAddr, ulong ieeeAddr, Action<Device> callback)
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

                ActiveEndpointsRequest activeEp = new ActiveEndpointsRequest()
                {
                    DestinationAddress = nwkAddr,
                    NetworkAddressOfInteresst = nwkAddr
                };
                activeEp.IndObject.OnParsed += (object s, ZpiObject ep) =>
                {
                    ActiveEndpointResponse eprsp = ep.ToSpecificObject<ActiveEndpointResponse>();

                    foreach (byte epByte in eprsp.Endpoints)
                    {
                        int count = 0;
                        GetEndpoint(device, epByte, nwkAddr, (endpoint) =>
                        {
                            device.Endpoints.Add(endpoint);
                            count++;

                            if(count == eprsp.Endpoints.Count())
                            {
                                callback?.Invoke(device);
                            }
                        });
                    }
                };
                activeEp.Request(_znp);
            };

            nodeDesc.Request(_znp);
        }

        public void GetEndpoint(Device device, byte endpointId, ushort nwkAddr, Action<Endpoint> callback)
        {
            Endpoint endpoint = new Endpoint(device)
            {
                Id = endpointId
            };

            device.Endpoints.Add(endpoint);

            SimpleDescRequest simpleDesc = new SimpleDescRequest()
            {
                DestinationAddress = nwkAddr,
                NetworkAddressOfInteresst = nwkAddr,
                Endpoint = endpointId
            };

            simpleDesc.IndObject.OnParsed += (object sDesc, ZpiObject epDef) =>
            {
                SimpleDescResponse descResponse = epDef.ToSpecificObject<SimpleDescResponse>();

                int chunkSize = 2;

                int iIn = 0;
                byte[][] cIn = descResponse.ClusterIn.GroupBy(c => iIn++ / chunkSize).Select(g => g.ToArray<byte>()).ToArray();

                foreach (var c in cIn)
                {
                    endpoint.InClusters.Add((Clusters)BitConverter.ToUInt16(c, 0));
                }

                int iOut = 0;
                byte[][] cOut = descResponse.ClusterOut.GroupBy(c => iOut++ / chunkSize).Select(g => g.ToArray<byte>()).ToArray();

                foreach (var c in cOut)
                {
                    endpoint.OutClusters.Add((Clusters)BitConverter.ToUInt16(c, 0));
                }

                callback?.Invoke(endpoint);
            };
            simpleDesc.Request(_znp);
        }

        public static Device GetDeviceInfo(CCZnp controller, ulong ieeeAddr, ushort nwkAddr, Action<Device> callback)
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
