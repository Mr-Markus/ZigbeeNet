// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.RSSILocation;


namespace ZigBeeNet.ZCL.Clusters.RSSILocation
{
    /// <summary>
    /// Get Location Data Command value object class.
    /// <para>
    /// Cluster: RSSI Location. Command is sent TO the server.
    /// This command is a specific command used for the RSSI Location cluster.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetLocationDataCommand : ZclCommand
    {
        /// <summary>
        /// Header command message field.
        /// </summary>
        public byte Header { get; set; }

        /// <summary>
        /// Number Responses command message field.
        /// </summary>
        public byte NumberResponses { get; set; }

        /// <summary>
        /// Target Address command message field.
        /// </summary>
        public IeeeAddress TargetAddress { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetLocationDataCommand()
        {
            GenericCommand = false;
            ClusterId = 11;
            CommandId = 3;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Header, ZclDataType.Get(DataType.BITMAP_8_BIT));
            serializer.Serialize(NumberResponses, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(TargetAddress, ZclDataType.Get(DataType.IEEE_ADDRESS));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Header = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.BITMAP_8_BIT));
            NumberResponses = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            TargetAddress = deserializer.Deserialize<IeeeAddress>(ZclDataType.Get(DataType.IEEE_ADDRESS));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetLocationDataCommand [");
            builder.Append(base.ToString());
            builder.Append(", Header=");
            builder.Append(Header);
            builder.Append(", NumberResponses=");
            builder.Append(NumberResponses);
            builder.Append(", TargetAddress=");
            builder.Append(TargetAddress);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
