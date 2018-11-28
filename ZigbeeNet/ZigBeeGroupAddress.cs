using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet
{
    public class ZigBeeGroupAddress : ZigbeeAddress
    {
        public ushort GroupId { get; set; }

        public string Label { get; set; }

        public ZigBeeGroupAddress(byte groupId, string label)
        {
            GroupId = groupId;
            Label = label;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if(obj is ZigBeeGroupAddress addr)
            {
                return GroupId == addr.GroupId;
            }
            return false;
        }

        public override byte[] ToByteArray()
        {
            return BitConverter.GetBytes(GroupId);
        }

        public override int GetHashCode()
        {
            return -1221475543 + GroupId.GetHashCode();
        }
    }
}
