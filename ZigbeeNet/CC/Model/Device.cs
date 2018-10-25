using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet;
using ZigbeeNet.ZCL;

namespace ZigbeeNet.CC
{
    /// <summary>
    /// Fill with values from basic cluster
    /// 
    /// Maybe it could be implemented in Zigbee library as common object???
    /// </summary>
    public class Device
    {
        public ushort Id { get; set; }

        public Devices Type { get; set; }

        public ZAddress64 IeeeAddress { get; set; }

        public ZAddress16 NwkAdress { get; set; }

        public ushort ManufacturerId { get; set; }

        public string ManufacturerName { get; set; }

        public PowerSource PowerSource { get; set; }

        public string ModelId { get; set; }

        public DeviceStatus Status { get; set; }

        public DeviceEnabled DeviceEnabled { get; set; }

        public DateTime JoinTime { get; set; }

        public List<Endpoint> Endpoints { get; set; }

        public Device()
        {
            Endpoints = new List<Endpoint>();

            Status = DeviceStatus.Offline;
        }
    }
}
