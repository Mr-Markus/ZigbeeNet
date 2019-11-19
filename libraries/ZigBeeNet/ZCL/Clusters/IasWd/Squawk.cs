using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.IASWD;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.IASWD
{
    /// <summary>
    /// Squawk value object class.
    ///
    /// Cluster: IAS WD. Command ID 0x01 is sent TO the server.
    /// This command is a specific command used for the IAS WD cluster.
    ///
    /// This command uses the WD capabilities to emit a quick audible/visible pulse called a
    /// "squawk". The squawk command has no effect if the WD is currently active (warning in
    /// progress).
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class Squawk : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0502;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x01;

        /// <summary>
        /// Squawk Info command message field.
        /// </summary>
        public byte SquawkInfo { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Squawk()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(SquawkInfo, ZclDataType.Get(DataType.BITMAP_8_BIT));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            SquawkInfo = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.BITMAP_8_BIT));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("Squawk [");
            builder.Append(base.ToString());
            builder.Append(", SquawkInfo=");
            builder.Append(SquawkInfo);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
