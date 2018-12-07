using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Serialization;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Field
{
    /**
     * Binding Table field.
     */
    public class BindingTable
    {
        /**
         * The source IEEE address for the binding entry.
         */
        public IeeeAddress SrcAddr { get; private set; }

        /**
         * The source endpoint for the binding entry.
         */
        public int SrcEndpoint { get; private set; }

        /**
         * The identifier of the cluster on the source device that is bound to the destination device.
         */
        public int ClusterId { get; private set; }


        /**
         * Destination address mode
         * <p>
         * <ul>
         * <li>0x01 - Group address
         * <li>0x03 - IEEE address
         * </ul>
         */
        public int DstAddrMode { get; private set; }

        /**
         * Destination address if the address mode is group addressing
         */
        public int DstGroupAddr { get; private set; }

        /**
         * Destination address if the address mode is a node address
         */
        public IeeeAddress DstAddr { get; private set; }

        /**
         * Destination endpoint if the address mode is a node address
         */
        public int DstNodeEndpoint { get; private set; }


        public void Serialize(IZigBeeSerializer serializer)
        {
            serializer.AppendZigBeeType(SrcAddr, ZclDataType.Get(DataType.IEEE_ADDRESS));
            serializer.AppendZigBeeType(SrcEndpoint, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.AppendZigBeeType(ClusterId, ZclDataType.Get(DataType.CLUSTERID));
            serializer.AppendZigBeeType(DstAddrMode, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));

            if (DstAddrMode == 1)
            {
                serializer.AppendZigBeeType(DstGroupAddr, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            }
            else if (DstAddrMode == 3)
            {
                serializer.AppendZigBeeType(DstAddr, ZclDataType.Get(DataType.IEEE_ADDRESS));
                serializer.AppendZigBeeType(DstNodeEndpoint, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            }
        }

        public void Deserialize(IZigBeeDeserializer deserializer)
        {
            SrcAddr = (IeeeAddress)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.IEEE_ADDRESS));
            SrcEndpoint = (int)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            ClusterId = (int)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.CLUSTERID));
            DstAddrMode = (int)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            if (DstAddrMode == 1)
            {
                DstGroupAddr = (int)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            }
            else if (DstAddrMode == 3)
            {
                DstAddr = (IeeeAddress)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.IEEE_ADDRESS));
                DstNodeEndpoint = (int)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            }
        }

        public override int GetHashCode()
        {
            int prime = 31;
            int result = 1;
            result = prime * result + ClusterId;
            result = prime * result + DstAddrMode;
            result = prime * result + DstGroupAddr;
            result = prime * result + ((DstAddr == null) ? 0 : DstAddr.GetHashCode());
            result = prime * result + DstNodeEndpoint;
            result = prime * result + ((SrcAddr == null) ? 0 : SrcAddr.GetHashCode());
            result = prime * result + SrcEndpoint;
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
            BindingTable other = (BindingTable)obj;
            if (ClusterId != other.ClusterId)
            {
                return false;
            }
            if (DstAddrMode != other.DstAddrMode)
            {
                return false;
            }
            if (DstGroupAddr != other.DstGroupAddr)
            {
                return false;
            }
            if (DstAddr == null)
            {
                if (other.DstAddr != null)
                {
                    return false;
                }
            }
            else if (!DstAddr.Equals(other.DstAddr))
            {
                return false;
            }
            if (DstNodeEndpoint != other.DstNodeEndpoint)
            {
                return false;
            }
            if (SrcAddr == null)
            {
                if (other.SrcAddr != null)
                {
                    return false;
                }
            }
            else if (!SrcAddr.Equals(other.SrcAddr))
            {
                return false;
            }
            if (SrcEndpoint != other.SrcEndpoint)
            {
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(120);
            builder.Append("BindingTable [srcAddr=");
            builder.Append(SrcAddr);
            builder.Append('/');
            builder.Append(SrcEndpoint);
            builder.Append(", dstAddr=");
            switch (DstAddrMode)
            {
                case 1:
                    builder.Append(DstGroupAddr);
                    break;
                case 3:
                    builder.Append(DstAddr);
                    builder.Append('/');
                    builder.Append(DstNodeEndpoint);
                    break;
                default:
                    builder.Append(", Unknown destination mode");
                    break;
            }
            builder.Append(", clusterId=");
            builder.Append(ClusterId);
            builder.Append(']');
            return builder.ToString();
        }
    }
}
