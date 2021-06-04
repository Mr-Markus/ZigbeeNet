using System;
using System.Numerics;
using System.Globalization;
using ZigBeeNet.Util;

namespace ZigBeeNet
{
    public class IeeeAddress : IComparable<IeeeAddress>
    {
        public ulong Value {get;private set; }
        /// <summary>
        /// Default constructor. Creates an address 0
        /// </summary>
        public IeeeAddress()
        {
        }

        /// <summary>
        /// Create an <see cref="IeeeAddress"> from a <see cref="BigInteger">
        ///
        /// <param name="address">the address as a <see cref="BigInteger"></param>
        /// </summary>
        public IeeeAddress(ulong address)
        {
            Value = address;
        }

        /// <summary>
        /// Create an <see cref="IeeeAddress"> from a <see cref="String">
        ///
        /// <param name="address">the address as a <see cref="String"></param>
        /// </summary>
        public IeeeAddress(string address)
        {
            if (!UInt64.TryParse(address,NumberStyles.HexNumber,CultureInfo.InvariantCulture,out ulong _address))
                throw new ArgumentException("IeeeAddress string must contain valid hexadecimal value");
            Value=_address;
        }

        /// <summary>
        /// Create an <see cref="IeeeAddress"> from an int array
        ///
        /// <param name="address">the address as an int array. Array length must be 8.</param>
        /// @throws ArgumentOutOfRangeException
        /// </summary>
        public IeeeAddress(byte[] address)
        {
            if (address.Length != 8)
                throw new ArgumentOutOfRangeException("IeeeAddress array length must be 8");
            Value = address.ToUInt64();
        }

        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <returns></returns>
        public byte[] GetAddress()
        {   
            return ByteHelper.FromUInt64(Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return !(obj is null)
                && (obj is IeeeAddress other)
                && Value==other.Value;
        }

        public override string ToString()
        {
            return Value.ToString("X16");
        }

        public int CompareTo(IeeeAddress other) => Value.CompareTo(other.Value);

    }
}
