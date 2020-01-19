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
    /// End Device Bind Request value object class.
    ///
    ///
    /// The End_Device_Bind_req is generated from a Local Device wishing to perform End Device
    /// Bind with a Remote Device. The End_Device_Bind_req is generated, typically based on
    /// some user action like a button press. The destination addressing on this command shall
    /// be unicast, and the destination address shall be that of the ZigBee Coordinator.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class EndDeviceBindRequest : ZdoRequest
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0020;

        /// <summary>
        /// Binding Target command message field.
        /// </summary>
        public ushort BindingTarget { get; set; }

        /// <summary>
        /// Src Address command message field.
        /// </summary>
        public IeeeAddress SrcAddress { get; set; }

        /// <summary>
        /// Src Endpoint command message field.
        /// </summary>
        public byte SrcEndpoint { get; set; }

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
        public EndDeviceBindRequest()
        {
            ClusterId = CLUSTER_ID;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(BindingTarget, ZclDataType.Get(DataType.NWK_ADDRESS));
            serializer.Serialize(SrcAddress, ZclDataType.Get(DataType.IEEE_ADDRESS));
            serializer.Serialize(SrcEndpoint, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
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

            BindingTarget = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.NWK_ADDRESS));
            SrcAddress = deserializer.Deserialize<IeeeAddress>(ZclDataType.Get(DataType.IEEE_ADDRESS));
            SrcEndpoint = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
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

            builder.Append("EndDeviceBindRequest [");
            builder.Append(base.ToString());
            builder.Append(", BindingTarget=");
            builder.Append(BindingTarget);
            builder.Append(", SrcAddress=");
            builder.Append(SrcAddress);
            builder.Append(", SrcEndpoint=");
            builder.Append(SrcEndpoint);
            builder.Append(", ProfileId=");
            builder.Append(ProfileId);
            builder.Append(", InClusterList=");
            builder.Append(InClusterList == null? "" : string.Join(", ", InClusterList));
            builder.Append(", OutClusterList=");
            builder.Append(OutClusterList == null? "" : string.Join(", ", OutClusterList));
            builder.Append(']');

            return builder.ToString();
        }
    }
}
