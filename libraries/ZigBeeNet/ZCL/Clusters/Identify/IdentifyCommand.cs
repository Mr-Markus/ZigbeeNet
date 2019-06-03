// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.Identify;


namespace ZigBeeNet.ZCL.Clusters.Identify
{
    /// <summary>
    /// Identify Command value object class.
    /// <para>
    /// Cluster: Identify. Command is sent TO the server.
    /// This command is a specific command used for the Identify cluster.
    ///
    /// The identify command starts or stops the receiving device identifying itself.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class IdentifyCommand : ZclCommand
    {
        /// <summary>
        /// Identify Time command message field.
        /// </summary>
        public ushort IdentifyTime { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public IdentifyCommand()
        {
            GenericCommand = false;
            ClusterId = 3;
            CommandId = 0;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(IdentifyTime, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            IdentifyTime = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("IdentifyCommand [");
            builder.Append(base.ToString());
            builder.Append(", IdentifyTime=");
            builder.Append(IdentifyTime);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
