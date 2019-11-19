using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.KeyEstablishment;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.KeyEstablishment
{
    /// <summary>
    /// Ephemeral Data Response value object class.
    ///
    /// Cluster: Key Establishment. Command ID 0x01 is sent FROM the server.
    /// This command is a specific command used for the Key Establishment cluster.
    ///
    /// The Ephemeral Data Response command allows a device to communicate its ephemeral data
    /// to another device that previously requested it.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class EphemeralDataResponse : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0800;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x01;

        /// <summary>
        /// Ephemeral Data command message field.
        /// </summary>
        public ByteArray EphemeralData { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public EphemeralDataResponse()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(EphemeralData, ZclDataType.Get(DataType.RAW_OCTET));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            EphemeralData = deserializer.Deserialize<ByteArray>(ZclDataType.Get(DataType.RAW_OCTET));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("EphemeralDataResponse [");
            builder.Append(base.ToString());
            builder.Append(", EphemeralData=");
            builder.Append(EphemeralData);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
