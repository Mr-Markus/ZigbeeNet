using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet;
using ZigbeeNet.ZCL;

namespace ZigbeeNet
{
    /// <summary>
    /// Fill with values from basic cluster
    /// 
    /// Maybe it could be implemented in Zigbee library as common object???
    /// </summary>
    public class ZigBeeNode
    {
        public ushort Id { get; set; }

        public ZigbeeDeviceType Type { get; set; }

        public ZigbeeAddress64 IeeeAddress { get; set; }

        public ZigbeeAddress16 NwkAdress { get; set; }

        public DoubleByte ManufacturerId { get; set; }

        public string ManufacturerName { get; set; }

        public PowerSource PowerSource { get; set; }

        public string ModelId { get; set; }

        public ZigBeeNodeStatus Status { get; set; }

        public ZigbeeNodeState DeviceEnabled { get; set; }

        public DateTime JoinTime { get; set; }

        public List<ZigbeeEndpoint> Endpoints { get; set; }

        public ZigBeeNode()
        {
            Endpoints = new List<ZigbeeEndpoint>();

            Status = ZigBeeNodeStatus.Offline;
        }

        public void Shutdown()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            if(obj is ZigBeeNode node)
            {
                if(NwkAdress != null && node.NwkAdress != null)
                {
                    return NwkAdress.Value == node.NwkAdress.Value;
                }
            }
            return false;
        }
    }
}
