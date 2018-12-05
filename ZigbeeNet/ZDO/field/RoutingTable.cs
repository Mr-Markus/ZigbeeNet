﻿using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Serialization;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Field
{
    public class RoutingTable
    {

        public int DestinationAddress;

        public DiscoveryState Status;

        public bool IsMemoryConstrained;

        public bool IsManyToOne;

        public bool IsRouteRecordRequired;

        public int IsNextHopAddress;

        public enum DiscoveryState
        {
            UNKNOWN,
            ACTIVE,
            DISCOVERY_UNDERWAY,
            DISCOVERY_FAILED,
            INACTIVE,
            VALIDATION_UNDERWAY
        }

        /**
         * Deserialise the contents of the structure.
         *
         * @param deserializer the {@link ZigBeeDeserializer} used to deserialize
         */
        public void Deserialize(IZigBeeDeserializer deserializer)
        {
            // Deserialize the fields
            DestinationAddress = (int)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            int temp = (int)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            switch (temp & 0x07)
            {
                case 0:
                    Status = DiscoveryState.ACTIVE;
                    break;
                case 1:
                    Status = DiscoveryState.DISCOVERY_UNDERWAY;
                    break;
                case 2:
                    Status = DiscoveryState.DISCOVERY_FAILED;
                    break;
                case 3:
                    Status = DiscoveryState.INACTIVE;
                    break;
                case 4:
                    Status = DiscoveryState.VALIDATION_UNDERWAY;
                    break;
                default:
                    Status = DiscoveryState.UNKNOWN;
                    break;
            }

            IsMemoryConstrained = (temp & 0x08) != 0;
            IsManyToOne = (temp & 0x10) != 0;
            IsRouteRecordRequired = (temp & 0x20) != 0;
            IsNextHopAddress = (int)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }



        public override int GetHashCode()
        {
            int prime = 31;
            int result = 1;
            result = prime * result + /*((DestinationAddress == null) ? 0 :*/ DestinationAddress.GetHashCode();
            result = prime * result + (IsManyToOne ? 1231 : 1237);
            result = prime * result + (IsMemoryConstrained ? 1231 : 1237);
            result = prime * result + /*((IsNextHopAddress == null) ? 0 : */ IsNextHopAddress.GetHashCode();
            result = prime * result + (IsRouteRecordRequired ? 1231 : 1237);
            result = prime * result + /*((Status == null) ? 0 :*/ Status.GetHashCode();
            return result;
        }

        public override bool Equals(Object obj)
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
            RoutingTable other = (RoutingTable)obj;
            //if (DestinationAddress == null)
            //{
            //    if (other.DestinationAddress != null)
            //    {
            //        return false;
            //    }
            //}
            /*else*/
            if (!DestinationAddress.Equals(other.DestinationAddress))
            {
                return false;
            }
            if (IsManyToOne != other.IsManyToOne)
            {
                return false;
            }
            if (IsMemoryConstrained != other.IsMemoryConstrained)
            {
                return false;
            }
            //if (IsNextHopAddress == null)
            //{
            //    if (other.IsNextHopAddress != null)
            //    {
            //        return false;
            //    }
            //}
            else if (!IsNextHopAddress.Equals(other.IsNextHopAddress))
            {
                return false;
            }
            if (IsRouteRecordRequired != other.IsRouteRecordRequired)
            {
                return false;
            }
            if (Status != other.Status)
            {
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            return "RoutingTable [destinationAddress=" + DestinationAddress + ", status=" + Status + ", memoryConstrained="
                    + IsMemoryConstrained + ", manyToOne=" + IsManyToOne + ", routeRecordRequired=" + IsRouteRecordRequired
                    + ", nextHopAddress=" + IsNextHopAddress + "]";
        }

    }
}
