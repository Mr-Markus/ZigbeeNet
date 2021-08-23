using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet
{
    public struct ZigBeeGroupAddress : IZigBeeAddress, IComparable<ZigBeeGroupAddress>
    {

        /// <summary>
         /// The group ID.
         /// </summary>
        public ushort GroupId => Address;

        public ushort Address { get; }

        // /// <summary>
        //  /// Default constructor.
        //  /// </summary>
        // public ZigBeeGroupAddress()
        // {
        // }

        /// <summary>
         /// Constructor which sets group ID.
         ///
         /// @param groupId
         ///            the group ID
         /// </summary>
        public ZigBeeGroupAddress(ushort groupId)
        {
            Address = groupId;
        }


        public bool IsGroup => true;

        public int CompareTo(ZigBeeGroupAddress other)  => (int)Address - (int)other.Address;

        public static bool operator >(ZigBeeGroupAddress operand1, ZigBeeGroupAddress operand2)
        {
            return operand1.CompareTo(operand2) == 1;
        }

        // Define the is less than operator.
        public static bool operator <(ZigBeeGroupAddress operand1, ZigBeeGroupAddress operand2)
        {
            return operand1.CompareTo(operand2) == -1;
        }

        // Define the is greater than or equal to operator.
        public static bool operator >=(ZigBeeGroupAddress operand1, ZigBeeGroupAddress operand2)
        {
            return operand1.CompareTo(operand2) >= 0;
        }

        // Define the is less than or equal to operator.
        public static bool operator <=(ZigBeeGroupAddress operand1, ZigBeeGroupAddress operand2)
        {
            return operand1.CompareTo(operand2) <= 0;
        }

        public override int GetHashCode()
        {
            return Address;
        }

        public override bool Equals(object obj)
        {
            return !(obj is null) && (obj is ZigBeeGroupAddress other) && CompareTo(other)==0;
        }


    }
}
