using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Serialization;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Field
{
    /// <summary>
     /// Binding Table field.
     /// </summary>
    public class BindingTable
    {
        /// <summary>
         /// The source IEEE address for the binding entry.
         /// </summary>
        public IeeeAddress SrcAddr { get; private set; }

        /// <summary>
         /// The source endpoint for the binding entry.
         /// </summary>
        public byte SrcEndpoint { get; private set; }

        /// <summary>
         /// The identifier of the cluster on the source device that is bound to the destination device.
         /// </summary>
        public ushort ClusterId { get; private set; }


        /// <summary>
         /// Destination address mode
         /// <p>
         /// <ul>
         /// <li>0x01 - Group address
         /// <li>0x03 - IEEE address
         /// </ul>
         /// </summary>
        public byte DstAddrMode { get; private set; }

        /// <summary>
         /// Destination address if the address mode is group addressing
         /// </summary>
        public ushort DstGroupAddr { get; private set; }

        /// <summary>
         /// Destination address if the address mode is a node address
         /// </summary>
        public IeeeAddress DstAddr { get; private set; }

        /// <summary>
         /// Destination endpoint if the address mode is a node address
         /// </summary>
        public byte DstNodeEndpoint { get; private set; }


        public void Serialize(IZigBeeSerializer serializer)
        {
            serializer.AppendZigBeeType(SrcAddr, DataType.IEEE_ADDRESS);
            serializer.AppendZigBeeType(SrcEndpoint, DataType.UNSIGNED_8_BIT_INTEGER);
            serializer.AppendZigBeeType(ClusterId, DataType.CLUSTERID);
            serializer.AppendZigBeeType(DstAddrMode, DataType.UNSIGNED_8_BIT_INTEGER);

            if (DstAddrMode == 1)
            {
                serializer.AppendZigBeeType(DstGroupAddr, DataType.UNSIGNED_16_BIT_INTEGER);
            }
            else if (DstAddrMode == 3)
            {
                serializer.AppendZigBeeType(DstAddr, DataType.IEEE_ADDRESS);
                serializer.AppendZigBeeType(DstNodeEndpoint, DataType.UNSIGNED_8_BIT_INTEGER);
            }
        }

        public void Deserialize(IZigBeeDeserializer deserializer)
        {
            SrcAddr = deserializer.ReadZigBeeType<IeeeAddress>(DataType.IEEE_ADDRESS);
            SrcEndpoint = deserializer.ReadZigBeeType<byte>(DataType.UNSIGNED_8_BIT_INTEGER);
            ClusterId = deserializer.ReadZigBeeType<ushort>(DataType.CLUSTERID);
            DstAddrMode = deserializer.ReadZigBeeType<byte>(DataType.UNSIGNED_8_BIT_INTEGER);
            if (DstAddrMode == 1)
            {
                DstGroupAddr = deserializer.ReadZigBeeType<ushort>(DataType.UNSIGNED_16_BIT_INTEGER);
            }
            else if (DstAddrMode == 3)
            {
                DstAddr = deserializer.ReadZigBeeType<IeeeAddress>(DataType.IEEE_ADDRESS);
                DstNodeEndpoint = deserializer.ReadZigBeeType<byte>(DataType.UNSIGNED_8_BIT_INTEGER);
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
