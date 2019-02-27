using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet
{
    public class ZigBeeGroupAddress : IZigBeeAddress, IComparable<IZigBeeAddress>
    {

        /// <summary>
         /// The group ID.
         /// </summary>
        public ushort GroupId
        {
            get
            {
                return Address;
            }
            set
            {
                Address = value;
            }
        }

        public ushort Address { get; set; }

        /// <summary>
         /// The group label.
         /// </summary>
        private string Label { get; set; }

        /// <summary>
         /// Default constructor.
         /// </summary>
        public ZigBeeGroupAddress()
        {
        }

        /// <summary>
         /// Constructor which sets group ID.
         ///
         /// @param groupId
         ///            the group ID
         /// </summary>
        public ZigBeeGroupAddress(ushort groupId)
        {
            GroupId = groupId;
        }

        /// <summary>
         /// Constructor which sets group ID and label.
         ///
         /// @param groupId
         ///            the group ID
         /// @param label
         ///            the group label
         /// </summary>
        public ZigBeeGroupAddress(ushort groupId, string label)
        {
            GroupId = groupId;
            Label = Label;
        }

        public bool IsGroup
        {
            get
            {
                return true;
            }
        }

        public int CompareTo(IZigBeeAddress other)
        {
            if (this == other)
            {
                return 0;
            }

            ZigBeeGroupAddress thatAddr = (ZigBeeGroupAddress)other;

            if (thatAddr.GroupId == GroupId)
            {
                return 0;
            }

            return (thatAddr.GroupId < GroupId) ? -1 : 1;
        }

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
            return GroupId.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is ZigBeeGroupAddress other)
                return other.GroupId == GroupId;

            return false;
        }
    }
}
