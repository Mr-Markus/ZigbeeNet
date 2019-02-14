using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using ZigBeeNet.Util;

namespace ZigBeeNet
{
    public class IeeeAddress : IComparable<IeeeAddress>
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
                SetAddress(value);
            }
        }

        /**
         * Default constructor. Creates an address 0
         */
        public IeeeAddress()
        {
            this._address = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        }

        /**
         * Create an {@link IeeeAddress} from a {@link BigInteger}
         *
         * @param address the address as a {@link BigInteger}
         */
        public IeeeAddress(BigInteger address) : this()
        {
            SetAddress((ulong)address);
        }

        /**
         * Create an {@link IeeeAddress} from a {@link String}
         *
         * @param address the address as a {@link String}
         */
        public IeeeAddress(string address) : this()
        {
            try
            {
                SetAddress(ulong.Parse(address));
            }
            catch (FormatException e)
            {
                throw new ArgumentException("IeeeAddress string must contain valid hexadecimal value");
            }
        }

        /**
         * Create an {@link IeeeAddress} from an int array
         *
         * @param address the address as an int array. Array length must be 8.
         * @throws IllegalArgumentException
         */
        public IeeeAddress(byte[] address)
        {
            if (address.Length != 8)
            {
                throw new ArgumentNullException("IeeeAddress array length must be 8");
            }
            _address = address;//Arrays.copyOf(address, 8);
        }

        public override int GetHashCode()
        {
            return Hash.CalcHashCode(_address);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!typeof(IeeeAddress).IsAssignableFrom(obj.GetType()))
            {
                return false;
            }

            IeeeAddress other = (IeeeAddress)obj;
            for (int cnt = 0; cnt < 8; cnt++)
            {
                if (other._address[cnt] != _address[cnt])
                {
                    return false;
                }
            }
            return true;
        }

        public override string ToString()
        {             
            return Value.ToString();
        }

        public int CompareTo(IeeeAddress other)
        {
            if (other == null)
            {
                return -1;
            }

            if (!typeof(IeeeAddress).IsAssignableFrom(other.GetType()))
            {
                return -1;
            }

            for (int cnt = 0; cnt < 8; cnt++)
            {
                if (other._address[cnt] == _address[cnt])
                {
                    continue;
                }

                return other._address[cnt] < _address[cnt] ? 1 : -1;
            }
            return 0;
        }

        private void SetAddress(ulong longVal)
        {
            this._address[0] = (byte)(longVal & 0xff);
            this._address[1] = (byte)((longVal >> 8) & 0xff);
            this._address[2] = (byte)((longVal >> 16) & 0xff);
            this._address[3] = (byte)((longVal >> 24) & 0xff);
            this._address[4] = (byte)((longVal >> 32) & 0xff);
            this._address[5] = (byte)((longVal >> 40) & 0xff);
            this._address[6] = (byte)((longVal >> 48) & 0xff);
            this._address[7] = (byte)((longVal >> 56) & 0xff);

        }

    }
}
