// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.IASWD;


namespace ZigBeeNet.ZCL.Clusters.IASWD
{
    /// <summary>
    /// Squawk Command value object class.
    /// <para>
    /// Cluster: IAS WD. Command is sent TO the server.
    /// This command is a specific command used for the IAS WD cluster.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class SquawkCommand : ZclCommand
    {
        /// <summary>
        /// Header command message field.
        /// </summary>
        public byte Header { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public SquawkCommand()
        {
            GenericCommand = false;
            ClusterId = 1282;
            CommandId = 2;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Header, ZclDataType.Get(DataType.DATA_8_BIT));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Header = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.DATA_8_BIT));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("SquawkCommand [");
            builder.Append(base.ToString());
            builder.Append(", Header=");
            builder.Append(Header);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
