using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.IASACE;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.IASACE
{
    /// <summary>
    /// Bypass Response value object class.
    ///
    /// Cluster: IAS ACE. Command ID 0x07 is sent FROM the server.
    /// This command is a specific command used for the IAS ACE cluster.
    ///
    /// Provides the response of the security panel to the request from the IAS ACE client to
    /// bypass zones via a Bypass command.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class BypassResponse : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0501;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x07;

        /// <summary>
        /// Bypass Result command message field.
        /// 
        /// An array of Zone IDs for each zone requested to be bypassed via the Bypass command
        /// where X is equal to the value of the Number of Zones field. The order of results for
        /// Zone IDs shall be the same as the order of Zone IDs sent in the Bypass command by the IAS
        /// ACE client.
        /// </summary>
        public List<byte> BypassResult { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BypassResponse()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(BypassResult, ZclDataType.Get(DataType.N_X_UNSIGNED_8_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            BypassResult = deserializer.Deserialize<List<byte>>(ZclDataType.Get(DataType.N_X_UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("BypassResponse [");
            builder.Append(base.ToString());
            builder.Append(", BypassResult=");
            builder.Append(BypassResult == null? "" : string.Join(", ", BypassResult));
            builder.Append(']');

            return builder.ToString();
        }
    }
}
