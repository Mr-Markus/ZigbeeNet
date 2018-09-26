using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet;

namespace ZigbeeNet
{
    /// <summary>
    /// Fill with values from basic cluster
    /// 
    /// Maybe it could be implemented in Zigbee library as common object???
    /// </summary>
    public class Device
    {
        //TODO: Id Property

        public Devices Type { get; set; }

        public ulong IeeeAddress { get; set; }

        public ushort NwkAdress { get; set; }

        public ushort ManufacturerId { get; set; }

        public string ManufacturerName { get; set; }

        public PowerSources PowerSource { get; set; }

        public string ModelId { get; set; }

        public eDeviceStatus Status { get; set; }

        public eDeviceEnabled DeviceEnabled { get; set; }

        public DateTime JoinTime { get; set; }

        public List<Endpoint> Endpoints { get; set; }

        public Device()
        {
            Endpoints = new List<Endpoint>();
        }
    }
}
