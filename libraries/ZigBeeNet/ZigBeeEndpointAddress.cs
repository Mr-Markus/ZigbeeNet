using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Extensions;
using ZigBeeNet.Util;

namespace ZigBeeNet
{
    public class ZigBeeEndpointAddress : IZigBeeAddress
    {
        public byte Endpoint { get; private set; }

        public ushort Address { get; set; }

        public bool IsGroup
        {
            get { return false; }
        }

        /// <summary>
         /// Constructor for ZDO ZigBee devices where only the address is defined
         ///
         /// @param address
         ///            the network address
         ///
         /// </summary>
        public ZigBeeEndpointAddress(ushort address)
        {
            this.Address = address;
            this.Endpoint = 0;
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
            this.Address = address;
            this.Endpoint = endpoint;
        }

        public ZigBeeEndpointAddress(string address)
        {
            if (address.Contains("/"))
            {
                var splits = address.Split('/');
                if (splits.Length > 2)
                {
                    throw new ArgumentException(nameof(address));
                }
                this.Address = ushort.Parse(splits[0]);
                this.Endpoint = byte.Parse(splits[1]);
            }
            else
            {
                this.Address = ushort.Parse(address);
                this.Endpoint = 0;
            }
        }

        public override int GetHashCode()
        {
            byte[] hash = new byte[3];

            hash[0] = Address.GetByte(0);
            hash[1] = Address.GetByte(1);
            hash[2] = Endpoint;

            return Hash.CalcHashCode(hash);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!typeof(ZigBeeEndpointAddress).IsAssignableFrom(obj.GetType()))
            {
                return false;
            }

            ZigBeeEndpointAddress other = (ZigBeeEndpointAddress)obj;

            return (other.Address == Address && other.Endpoint == Endpoint);
        }

        public int CompareTo(IZigBeeAddress that)
        {
            if (this == that)
            {
                return 0;
            }

            ZigBeeEndpointAddress thatAddr = (ZigBeeEndpointAddress)that;

            if (thatAddr.Address == Address && thatAddr.Endpoint == Endpoint)
            {
                return 0;
            }

            if (thatAddr.Address == Address)
            {
                return Endpoint - thatAddr.Endpoint;
            }

            return Address - thatAddr.Address;
        }

        public override string ToString()
        {
            return Address + "/" + Endpoint;
        }

    }
}
