using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Extensions;
using ZigBeeNet.Util;

namespace ZigBeeNet
{
    public struct ZigBeeEndpointAddress : IZigBeeAddress,IComparable<ZigBeeEndpointAddress>
    {
        public static ZigBeeEndpointAddress Zero {get;} = new ZigBeeEndpointAddress(0,0);
        public static ZigBeeEndpointAddress BROADCAST_RX_ON {get;} = new ZigBeeEndpointAddress(ZigBeeBroadcastDestination.BROADCAST_RX_ON);
        public static ZigBeeEndpointAddress BROADCAST_ALL_DEVICES {get;} = new ZigBeeEndpointAddress(ZigBeeBroadcastDestination.BROADCAST_ALL_DEVICES);
        public static ZigBeeEndpointAddress BROADCAST_LOW_POWER_ROUTERS {get;} = new ZigBeeEndpointAddress(ZigBeeBroadcastDestination.BROADCAST_LOW_POWER_ROUTERS);
        public static ZigBeeEndpointAddress BROADCAST_ROUTERS_AND_COORD {get;} = new ZigBeeEndpointAddress(ZigBeeBroadcastDestination.BROADCAST_ROUTERS_AND_COORD);

        public ushort Address { get; }
        public byte Endpoint { get; }

        public bool IsGroup => false;

        /// <summary>
         /// Constructor for ZDO ZigBee devices where only the address is defined
         ///
         /// @param address
         ///            the network address
         ///
         /// </summary>
        public ZigBeeEndpointAddress(ushort address)
        {
            Address = address;
            Endpoint = 0;
        }

        public ZigBeeEndpointAddress(ZigBeeBroadcastDestination address)
        {
            Address = (ushort)address;
            Endpoint = 0;
        }

        /// <summary>
         /// Constructor for standard ZigBee devices where the address and endpoint are defined
         ///
         /// @param address
         ///            the network address
         /// @param endpoint
         ///            the endpoint number
         /// </summary>
        public ZigBeeEndpointAddress(ushort address, byte endpoint)
        {
            Address = address;
            Endpoint = endpoint;
        }

        public ZigBeeEndpointAddress(string address)
        {
            if (TryParse(address,out (ushort a,byte e) result))
                (Address,Endpoint)=result;
            else
                throw new ArgumentException(nameof(address));
        }

        private static bool TryParse(string address,out (ushort addr,byte endpt) result )
        {
            result=(0,0);
            if (string.IsNullOrWhiteSpace(address))
                return false;
            string[] splits = address.Split('/');
            return (splits.Length==1 && ushort.TryParse(address,out result.addr))
                   || (splits.Length==2 && ushort.TryParse(splits[0],out result.addr) && byte.TryParse(splits[1],out result.endpt));
        }

        public static bool TryParse(string address,out ZigBeeEndpointAddress result)
        {
            if (TryParse(address,out (ushort addr,byte endpt) r))
            {
                result=new ZigBeeEndpointAddress(r.addr,r.endpt);
                return true;
            }
            result=default;
            return false;
        }

        public override int GetHashCode() => Endpoint<<16 | Address;

        public override bool Equals(object obj)
        {
            return  !(obj is null) 
                && (obj is ZigBeeEndpointAddress objAddr) 
                && objAddr.Address == Address && objAddr.Endpoint == Endpoint;
        }

        public int CompareTo(ZigBeeEndpointAddress other) => (Address == other.Address) ? (int)Endpoint - (int)other.Endpoint: (int)Address - (int)other.Address;

        public override string ToString() =>Address + "/" + Endpoint;
    }
}
