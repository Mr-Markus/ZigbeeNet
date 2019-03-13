using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Serialization;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;
using static ZigBeeNet.ZDO.Field.NodeDescriptor;

namespace ZigBeeNet.ZDO.Field
{
    /// <summary>
     /// Class representing the ZigBee neighbor table
     /// </summary>
    public class NeighborTable
    {
        public ExtendedPanId ExtendedPanId { get; private set; }

        public IeeeAddress ExtendedAddress { get; private set; }

        public ushort NetworkAddress { get; private set; }

        public LogicalType DeviceType { get; private set; }

        public NeighborTableRxState RxOnWhenIdle { get; private set; }

        public NeighborTableRelationship Relationship { get; private set; }

        public NeighborTableJoining PermitJoining { get; private set; }

        public byte Depth;

        public byte Lqi = 0;

        public enum NeighborTableRelationship
        {
            PARENT,
            CHILD,
            SIBLING,
            UNKNOWN,
            PREVIOUS_CHILD
        }

        public enum NeighborTableJoining
        {
            ENABLED,
            DISABLED,
            UNKNOWN
        }

        public enum NeighborTableRxState
        {
            RX_OFF,
            RX_ON,
            UNKNOWN
        }

        /// <summary>
         /// Deserialise the contents of the structure.
         ///
         /// <param name="deserializer">the <see cref="ZigBeeDeserializer"> used to deserialize</param>
         /// </summary>
        public void Deserialize(IZigBeeDeserializer deserializer)
        {
            // Deserialize the fields
            ExtendedPanId = deserializer.ReadZigBeeType<ExtendedPanId>(DataType.EXTENDED_PANID);
            ExtendedAddress = deserializer.ReadZigBeeType<IeeeAddress>(DataType.IEEE_ADDRESS);
            NetworkAddress = deserializer.ReadZigBeeType<ushort>(DataType.UNSIGNED_16_BIT_INTEGER);

            byte temp = deserializer.ReadZigBeeType<byte>(DataType.UNSIGNED_8_BIT_INTEGER);
            SetDeviceType(temp & 0x03);
            SetRxOnWhenIdle((temp & 0x0c) >> 2);
            SetRelationship((temp & 0x70) >> 4);

            temp = deserializer.ReadZigBeeType<byte>(DataType.UNSIGNED_8_BIT_INTEGER);
            SetPermitJoining(temp & 0x03);
            Depth = deserializer.ReadZigBeeType<byte>(DataType.UNSIGNED_8_BIT_INTEGER);
            Lqi = deserializer.ReadZigBeeType<byte>(DataType.UNSIGNED_8_BIT_INTEGER);
        }

        private void SetDeviceType(int deviceType)
        {
            switch (deviceType)
            {
                case 0:
                    this.DeviceType = LogicalType.COORDINATOR;
                    break;
                case 1:
                    this.DeviceType = LogicalType.ROUTER;
                    break;
                case 2:
                    this.DeviceType = LogicalType.END_DEVICE;
                    break;
                default:
                    this.DeviceType = LogicalType.UNKNOWN;
                    break;

            }
        }

        public void SetRxOnWhenIdle(int rxOnWhenIdle)
        {
            switch (rxOnWhenIdle)
            {
                case 0:
                    this.RxOnWhenIdle = NeighborTableRxState.RX_OFF;
                    break;
                case 1:
                    this.RxOnWhenIdle = NeighborTableRxState.RX_ON;
                    break;
                default:
                    this.RxOnWhenIdle = NeighborTableRxState.UNKNOWN;
                    break;

            }
        }

        private void SetRelationship(int relationship)
        {
            switch (relationship)
            {
                case 0:
                    this.Relationship = NeighborTableRelationship.PARENT;
                    break;
                case 1:
                    this.Relationship = NeighborTableRelationship.CHILD;
                    break;
                case 2:
                    this.Relationship = NeighborTableRelationship.SIBLING;
                    break;
                case 3:
                    this.Relationship = NeighborTableRelationship.UNKNOWN;
                    break;
                case 4:
                    this.Relationship = NeighborTableRelationship.PREVIOUS_CHILD;
                    break;
                default:
                    this.Relationship = NeighborTableRelationship.UNKNOWN;
                    break;
            }
        }

        private void SetPermitJoining(int permitJoining)
        {
            switch (permitJoining)
            {
                case 0:
                    this.PermitJoining = NeighborTableJoining.DISABLED;
                    break;
                case 1:
                    this.PermitJoining = NeighborTableJoining.ENABLED;
                    break;
                default:
                    this.PermitJoining = NeighborTableJoining.UNKNOWN;
                    break;
            }
        }


        public override int GetHashCode()
        {
            int prime = 31;
            int result = 1;
            result = prime * result + /*((depth == null) ? 0 :*/ Depth.GetHashCode();
            result = prime * result + /*((DeviceType == null) ? 0 :*/ DeviceType.GetHashCode();
            result = prime * result + /*((ExtendedAddress == null) ? 0 :*/ ExtendedAddress.GetHashCode();
            result = prime * result + ((ExtendedPanId == null) ? 0 : ExtendedPanId.GetHashCode());
            result = prime * result + /*((lqi == null) ? 0 : */ Lqi.GetHashCode();
            result = prime * result + /*((networkAddress == null) ? 0 :*/ NetworkAddress.GetHashCode();
            result = prime * result + /*((PermitJoining == null) ? 0 :*/ PermitJoining.GetHashCode();
            result = prime * result + /*((Relationship == null) ? 0 :*/ Relationship.GetHashCode();
            result = prime * result + /*((RxOnWhenIdle == null) ? 0 :*/ RxOnWhenIdle.GetHashCode();
            return result;
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }
            if (obj == null)
            {
                return false;
            }
            if (this.GetType() != obj.GetType())
            {
                return false;
            }
            NeighborTable other = (NeighborTable)obj;

            //if (Depth == null)
            //{
            //    if (other.Depth != null)
            //    {
            //        return false;
            //    }
            //}
            if (!Depth.Equals(other.Depth))
            {
                return false;
            }
            if (DeviceType != other.DeviceType)
            {
                return false;
            }
            if (ExtendedAddress == null)
            {
                if (other.ExtendedAddress != null)
                {
                    return false;
                }
            }
            else if (!ExtendedAddress.Equals(other.ExtendedAddress))
            {
                return false;
            }
            if (ExtendedPanId == null)
            {
                if (other.ExtendedPanId != null)
                {
                    return false;
                }
            }
            else if (!ExtendedPanId.Equals(other.ExtendedPanId))
            {
                return false;
            }
            //if (lqi == null)
            //{
            //    if (other.lqi != null)
            //    {
            //        return false;
            //    }
            //}
            else if (!Lqi.Equals(other.Lqi))
            {
                return false;
            }
            //if (networkAddress == null)
            //{
            //    if (other.networkAddress != null)
            //    {
            //        return false;
            //    }
            //}
            else if (!NetworkAddress.Equals(other.NetworkAddress))
            {
                return false;
            }
            if (PermitJoining != other.PermitJoining)
            {
                return false;
            }
            if (Relationship != other.Relationship)
            {
                return false;
            }
            if (RxOnWhenIdle != other.RxOnWhenIdle)
            {
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            return "NeighborTable [extendedPanId=" + ExtendedPanId + ", extendedAddress=" + ExtendedAddress
                    + ", networkAddress=" + NetworkAddress + ", deviceType=" + DeviceType + ", rxOnWhenIdle=" + RxOnWhenIdle
                    + ", relationship=" + Relationship + ", permitJoining=" + PermitJoining + ", depth=" + Depth + ", lqi="
                    + Lqi + "]";
        }

    }
}
