using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet
{
    public class ZigbeeAddress64 : ZigbeeAddress
    {
        private byte[] _address;

        public ulong Value
        {
            get
            {
                return BitConverter.ToUInt64(_address, 0);
            }
            set
            {
                _address = BitConverter.GetBytes(value);
            }
        }

        public ZigbeeAddress64(ulong ieee)
        {
            _address = BitConverter.GetBytes(ieee);
        }

        public ZigbeeAddress64(byte[] address)
        {
            _address = address;
        }

        public override byte[] ToByteArray()
        {
            return BitConverter.GetBytes(Value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
