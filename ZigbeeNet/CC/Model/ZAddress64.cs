using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC
{
    public class ZAddress64 : ZAddress
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

        public ZAddress64(ulong ieee)
        {
            _address = BitConverter.GetBytes(ieee);
        }

        public ZAddress64(byte[] address)
        {
            _address = address;
        }

        public override byte[] ToByteArray()
        {
            return BitConverter.GetBytes(Value);
        }
    }
}
