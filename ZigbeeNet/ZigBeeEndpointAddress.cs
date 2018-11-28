using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet
{
    public class ZigBeeEndpointAddress : ZigbeeAddress
    {
        private ushort _address;
        /// <summary>
        /// The network address
        /// </summary>
        public ushort Address
        {
            get { return _address; }
            set { _address = value; }
        }

        private byte _endpoint;
        /// <summary>
        /// The endpoint number
        /// </summary>
        public byte Endpoint
        {
            get { return _endpoint; }
            set { _endpoint = value; }
        }

        public ZigBeeEndpointAddress(ushort address)
        {
            _address = address;
            _endpoint = 0;
        }

        public ZigBeeEndpointAddress(ushort address, byte endpoint)
        {
            _address = address;
            _endpoint = endpoint;
        }

        public override byte[] ToByteArray()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if(obj is ZigBeeEndpointAddress epAddr)
            {
                return epAddr.Address == Address && epAddr.Endpoint == Endpoint;
            }

            return false;
        }

        public override string ToString()
        {
            return $"{Address}/{Endpoint}";
        }
    }
}
