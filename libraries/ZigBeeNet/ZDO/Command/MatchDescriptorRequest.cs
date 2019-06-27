using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Match Descriptor Request value object class.
    /// 
    /// The Match_Desc_req command is generated from a local device wishing to find
    /// remote devices supporting a specific simple descriptor match criterion. This
    /// command shall either be broadcast to all devices for which macRxOnWhenIdle =
    /// TRUE, or unicast. If the command is unicast, it shall be directed either to the
    /// remote device itself or to an alternative device that contains the discovery
    /// information of the remote device.
    /// 
    /// </summary>
    public class MatchDescriptorRequest : ZdoRequest
    {
        /// <summary>
        /// NWKAddrOfInterest command message field.
        /// </summary>
        public ushort NwkAddrOfInterest { get; set; }

        /// <summary>
        /// ProfileID command message field.
        /// </summary>
        public ushort ProfileId { get; set; }

        /// <summary>
        /// InClusterList command message field.
        /// </summary>
        public List<ushort> InClusterList { get; set; }

        /// <summary>
        /// OutClusterList command message field.
        /// </summary>
        public List<ushort> OutClusterList { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MatchDescriptorRequest()
        {
            ClusterId = 0x0006;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(NwkAddrOfInterest, ZclDataType.Get(DataType.NWK_ADDRESS));
            serializer.Serialize(ProfileId, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(InClusterList.Count, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            for (int cnt = 0; cnt < InClusterList.Count; cnt++)

            {
                serializer.Serialize(InClusterList[cnt], ZclDataType.Get(DataType.CLUSTERID));
            }

            serializer.Serialize(OutClusterList.Count, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));

            for (int cnt = 0; cnt < OutClusterList.Count; cnt++)
            {
                serializer.Serialize(OutClusterList[cnt], ZclDataType.Get(DataType.CLUSTERID));
            }
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            // Create lists
            InClusterList = new List<ushort>();
            OutClusterList = new List<ushort>();

            NwkAddrOfInterest = (ushort)deserializer.Deserialize(ZclDataType.Get(DataType.NWK_ADDRESS));
            ProfileId = (ushort)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            byte? inClusterCount = (byte?)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));

            if (inClusterCount != null)
            {
                for (int cnt = 0; cnt < inClusterCount; cnt++)
                {
                    InClusterList.Add((ushort)deserializer.Deserialize(ZclDataType.Get(DataType.CLUSTERID)));
                }
            }

            int? outClusterCount = (int?)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));

            if (outClusterCount != null)
            {
                for (int cnt = 0; cnt < outClusterCount; cnt++)
                {
                    OutClusterList.Add((ushort)deserializer.Deserialize(ZclDataType.Get(DataType.CLUSTERID)));
                }
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("MatchDescriptorRequest [")
                   .Append(base.ToString())
                   .Append(", nwkAddrOfInterest=")
                   .Append(NwkAddrOfInterest)
                   .Append(", profileId=")
                   .Append(ProfileId)
                   .Append(", inClusterList=")
                   .Append(InClusterList)
                   .Append(", outClusterList=")
                   .Append(OutClusterList)
                   .Append(']');

            return builder.ToString();
        }

    }
}
