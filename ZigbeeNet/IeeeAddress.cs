using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using ZigbeeNet.Util;

namespace ZigbeeNet
{
    public class IeeeAddress : IComparable<IeeeAddress>
    {
        private int[] _address;

        /**
         * Default constructor. Creates an address 0
         */
        public IeeeAddress()
        {
            this._address = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        }

        /**
         * Create an {@link IeeeAddress} from a {@link BigInteger}
         *
         * @param address the address as a {@link BigInteger}
         */
        public IeeeAddress(BigInteger address) : this()
        {
            SetAddress((long)address);
        }

        /**
         * Create an {@link IeeeAddress} from a {@link String}
         *
         * @param address the address as a {@link String}
         * @throws IllegalArgumentException
         */
        public IeeeAddress(string address) : this()
        {
            try
            {
                SetAddress((long)BigInteger.Parse(address, System.Globalization.NumberStyles.HexNumber));
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
        public IeeeAddress(int[] address)
        {
            if (address.Length != 8)
            {
                throw new ArgumentNullException("IeeeAddress array length must be 8");
            }
            this._address = address;//Arrays.copyOf(address, 8);
        }

        /**
         * Gets the IeeeAddress as an integer array with length 8
         *
         * @return int array of address
         */
        public int[] getValue()
        {
            return _address;
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
                if (other.getValue()[cnt] != _address[cnt])
                {
                    return false;
                }
            }
            return true;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            for (int cnt = 7; cnt >= 0; cnt--)
            {
                builder.Append(string.Format("{0}", _address[cnt]));
            }

            return builder.ToString();
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
                if (other.getValue()[cnt] == _address[cnt])
                {
                    continue;
                }

                return other.getValue()[cnt] < _address[cnt] ? 1 : -1;
            }
            return 0;
        }

        private void SetAddress(long longVal)
        {
            this._address[0] = (int)(longVal & 0xff);
            this._address[1] = (int)((longVal >> 8) & 0xff);
            this._address[2] = (int)((longVal >> 16) & 0xff);
            this._address[3] = (int)((longVal >> 24) & 0xff);
            this._address[4] = (int)((longVal >> 32) & 0xff);
            this._address[5] = (int)((longVal >> 40) & 0xff);
            this._address[6] = (int)((longVal >> 48) & 0xff);
            this._address[7] = (int)((longVal >> 56) & 0xff);

        }

    }
}
