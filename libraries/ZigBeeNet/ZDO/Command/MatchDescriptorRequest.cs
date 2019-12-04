using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZDO.Field;


namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Match Descriptor Request value object class.
    ///
    ///
    /// The Match_Desc_req command is generated from a local device wishing to find remote
    /// devices supporting a specific simple descriptor match criterion. This command shall
    /// either be broadcast to all devices for which macRxOnWhenIdle = TRUE, or unicast. If the
    /// command is unicast, it shall be directed either to the remote device itself or to an
    /// alternative device that contains the discovery information of the remote device.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class MatchDescriptorRequest : ZdoRequest
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0006;

        /// <summary>
        /// NWK Addr Of Interest command message field.
        /// </summary>
        public ushort NwkAddrOfInterest { get; set; }

        /// <summary>
        /// Profile ID command message field.
        /// </summary>
        public ushort ProfileId { get; set; }

        /// <summary>
        /// In Cluster List command message field.
        /// </summary>
        public List<ushort> InClusterList { get; set; }

        /// <summary>
        /// Out Cluster List command message field.
        /// </summary>
        public List<ushort> OutClusterList { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MatchDescriptorRequest()
        {
            ClusterId = CLUSTER_ID;
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

            NwkAddrOfInterest = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.NWK_ADDRESS));
            ProfileId = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            byte? inClusterCount = (byte?) deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            if (inClusterCount != null)
            {
                for (int cnt = 0; cnt < inClusterCount; cnt++)
                {
                    InClusterList.Add((ushort) deserializer.Deserialize(ZclDataType.Get(DataType.CLUSTERID)));
                }
            }
            byte? outClusterCount = (byte?) deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            if (outClusterCount != null)
            {
                for (int cnt = 0; cnt < outClusterCount; cnt++)
                {
                    OutClusterList.Add((ushort) deserializer.Deserialize(ZclDataType.Get(DataType.CLUSTERID)));
                }
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("MatchDescriptorRequest [");
            builder.Append(base.ToString());
            builder.Append(", NwkAddrOfInterest=");
            builder.Append(NwkAddrOfInterest);
            builder.Append(", ProfileId=");
            builder.Append(ProfileId);
            builder.Append(", InClusterList=");
            builder.Append(string.Join(", ", InClusterList));
            builder.Append(", OutClusterList=");
            builder.Append(string.Join(", ", OutClusterList));
            builder.Append(']');

            return builder.ToString();
        }
    }
}
