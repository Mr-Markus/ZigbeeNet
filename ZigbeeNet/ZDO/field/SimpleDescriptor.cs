using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Serialization;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Field
{
    public class SimpleDescriptor
    {

        public byte Endpoint { get; set; }

        private ushort ProfileId { get; set; }

        /**
         * The application device identifier field of the simple descriptor is sixteen bits in length and specifies the
         * device description supported on this endpoint. Device description identifiers shall be obtained from the ZigBee
         * Alliance.
         */
        public ushort DeviceId { get; set; }

        public byte DeviceVersion { get; set; }

        public List<ushort> InputClusterList;

        public List<ushort> OutputClusterList;

        /**
         * Deserialise the contents of the structure.
         *
         * @param deserializer the {@link ZigBeeDeserializer} used to deserialize
         */
        public void deserialize(IZigBeeDeserializer deserializer)
        {
            // Deserialize the fields
            Endpoint = (byte)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            ProfileId = (ushort)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            DeviceId = (ushort)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            DeviceVersion = (byte)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            InputClusterList = (List<ushort>)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.N_X_UNSIGNED_16_BIT_INTEGER));
            OutputClusterList = (List<ushort>)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.N_X_UNSIGNED_16_BIT_INTEGER));
        }

        public override int GetHashCode()
        {
            int prime = 31;
            int result = 1;
            result = prime * result + DeviceId;
            result = prime * result + DeviceVersion;
            result = prime * result + Endpoint;
            result = prime * result + ((InputClusterList == null) ? 0 : InputClusterList.GetHashCode());
            result = prime * result + ((OutputClusterList == null) ? 0 : OutputClusterList.GetHashCode());
            result = prime * result + ProfileId;
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
            SimpleDescriptor other = (SimpleDescriptor)obj;
            if (DeviceId != other.DeviceId)
            {
                return false;
            }
            if (DeviceVersion != other.DeviceVersion)
            {
                return false;
            }
            if (Endpoint != other.Endpoint)
            {
                return false;
            }
            if (InputClusterList == null)
            {
                if (other.InputClusterList != null)
                {
                    return false;
                }
            }
            else if (!InputClusterList.Equals(other.InputClusterList))
            {
                return false;
            }
            if (OutputClusterList == null)
            {
                if (other.OutputClusterList != null)
                {
                    return false;
                }
            }
            else if (!OutputClusterList.Equals(other.OutputClusterList))
            {
                return false;
            }
            if (ProfileId != other.ProfileId)
            {
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            return "SimpleDescriptor [endpoint=" + Endpoint + ", profileId=" + string.Format("%04X", ProfileId)
                    + ", deviceId=" + DeviceId + ", deviceVersion=" + DeviceVersion + ", inputClusterList="
                    + InputClusterList + ", outputClusterList=" + OutputClusterList + "]";
        }

    }
}
