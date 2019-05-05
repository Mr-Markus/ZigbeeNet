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

        public ushort ProfileId { get; set; }

        /// <summary>
         /// The application device identifier field of the simple descriptor is sixteen bits in length and specifies the
         /// device description supported on this endpoint. Device description identifiers shall be obtained from the ZigBee
         /// Alliance.
         /// </summary>
        public ushort DeviceId { get; set; }

        public byte DeviceVersion { get; set; }

        public List<ushort> InputClusterList;

        public List<ushort> OutputClusterList;

        /// <summary>
         /// Deserialise the contents of the structure.
         ///
         /// <param name="deserializer">the <see cref="ZigBeeDeserializer"> used to deserialize</param>
         /// </summary>
        public void Deserialize(IZigBeeDeserializer deserializer)
        {
            // Deserialize the fields
            Endpoint = deserializer.ReadZigBeeType<byte>(DataType.UNSIGNED_8_BIT_INTEGER);
            ProfileId = deserializer.ReadZigBeeType<ushort>(DataType.UNSIGNED_16_BIT_INTEGER);
            DeviceId = deserializer.ReadZigBeeType<ushort>(DataType.UNSIGNED_16_BIT_INTEGER);
            DeviceVersion = deserializer.ReadZigBeeType<byte>(DataType.UNSIGNED_8_BIT_INTEGER);
            InputClusterList = deserializer.ReadZigBeeType<List<ushort>>(DataType.N_X_UNSIGNED_16_BIT_INTEGER);
            OutputClusterList = deserializer.ReadZigBeeType<List<ushort>>(DataType.N_X_UNSIGNED_16_BIT_INTEGER);
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
            string input = InputClusterList != null ? string.Join(", ", InputClusterList) : "";
            string output = OutputClusterList != null ? string.Join(", ", OutputClusterList) : "";

            return "SimpleDescriptor [endpoint=" + Endpoint + ", profileId=" + ProfileId.ToString("X4")
                    + ", deviceId=" + DeviceId + ", deviceVersion=" + DeviceVersion + ", inputClusterList="
                    + input + ", outputClusterList=" + output + "]";
        }

    }
}
