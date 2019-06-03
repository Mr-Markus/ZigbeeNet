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
    /// RSSI Ping Command value object class.
    /// <para>
    /// Cluster: RSSI Location. Command is sent FROM the server.
    /// This command is a specific command used for the RSSI Location cluster.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class RSSIPingCommand : ZclCommand
    {
        /// <summary>
        /// Location Type command message field.
        /// </summary>
        public byte LocationType { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public RSSIPingCommand()
        {
            GenericCommand = false;
            ClusterId = 11;
            CommandId = 4;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(LocationType, ZclDataType.Get(DataType.DATA_8_BIT));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            LocationType = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.DATA_8_BIT));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("RSSIPingCommand [");
            builder.Append(base.ToString());
            builder.Append(", LocationType=");
            builder.Append(LocationType);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
