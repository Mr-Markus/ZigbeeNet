﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL;

namespace ZigBeeNet.CC
{
    public class Query
    {
        private IHardwareChannel _znp;

        public Query(IHardwareChannel znp)
        {
            _znp = znp;
        }

        public void GetDevice(ZigbeeAddress16 nwkAddr, ZigBeeAddress64 ieeeAddr, Action<ZigBeeNode> callback)
        {
            ZigBeeNode device = new ZigBeeNode()
            {
                NwkAdress = nwkAddr,
                IeeeAddress = ieeeAddr
            };

            //NodeDescRequest nodeDesc = new NodeDescRequest()
            //{
            //    DestinationAddress = nwkAddr,
            //    NetworkAddressOfInteresst = nwkAddr
            //};

            //nodeDesc.OnResponse += (object sender, ZpiObject e) =>
            //{
            //    NodeDescResponse rsp = e.ToSpecificObject<NodeDescResponse>();
            //    device.Type = (Devices)(rsp.LogicaltypeCmplxdescavaiUserdescavai & 0x07);
            //    device.ManufacturerId = rsp.ManufacturerCode;

            //    ActiveEndpointsRequest activeEp = new ActiveEndpointsRequest()
            //    {
            //        DestinationAddress = nwkAddr,
            //        NetworkAddressOfInteresst = nwkAddr
            //    };
            //    activeEp.OnResponse += (object s, ZpiObject ep) =>
            //    {
            //        ActiveEndpointResponse eprsp = ep.ToSpecificObject<ActiveEndpointResponse>();

            //        foreach (byte epByte in eprsp.Endpoints)
            //        {
            //            int count = 0;
            //            GetEndpoint(device, epByte, nwkAddr, (endpoint) =>
            //            {
            //                device.Endpoints.Add(endpoint);
            //                count++;

            //                if(count == eprsp.Endpoints.Count())
            //                {
            //                    callback?.Invoke(device);
            //                }
            //            });
            //        }
            //    };
            //    activeEp.RequestAsync(_znp);
            //};

            //nodeDesc.RequestAsync(_znp);
        }

        public void GetEndpoint(ZigBeeNode device, byte endpointId, ushort nwkAddr, Action<ZigBeeEndpoint> callback)
        {
            //Endpoint endpoint = new Endpoint(device)
            //{
            //    Id = endpointId
            //};

            //SimpleDescRequest simpleDesc = new SimpleDescRequest()
            //{
            //    DestinationAddress = nwkAddr,
            //    NetworkAddressOfInteresst = nwkAddr,
            //    Endpoint = endpointId
            //};

            //simpleDesc.IndObject.OnParsed += (object sDesc, ZpiObject epDef) =>
            //{
            //    SimpleDescResponse descResponse = epDef.ToSpecificObject<SimpleDescResponse>();

            //    endpoint.ProfileId = descResponse.ProfileId;

            //    int chunkSize = 2;

            //    int iIn = 0;
            //    byte[][] cIn = descResponse.ClusterIn.GroupBy(c => iIn++ / chunkSize).Select(g => g.ToArray<byte>()).ToArray();

            //    foreach (var c in cIn)
            //    {
            //        endpoint.InClusters.Add((Cluster)BitConverter.ToUInt16(c, 0));
            //    }

            //    int iOut = 0;
            //    byte[][] cOut = descResponse.ClusterOut.GroupBy(c => iOut++ / chunkSize).Select(g => g.ToArray<byte>()).ToArray();

            //    foreach (var c in cOut)
            //    {
            //        endpoint.OutClusters.Add((Cluster)BitConverter.ToUInt16(c, 0));
            //    }

            //    callback?.Invoke(endpoint);
            //};
            //simpleDesc.RequestAsync(_znp);
        }

        //public void Network()
    }
}
