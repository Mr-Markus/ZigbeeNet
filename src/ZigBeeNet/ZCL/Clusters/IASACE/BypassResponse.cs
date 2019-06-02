// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.IASACE;


namespace ZigBeeNet.ZCL.Clusters.IASACE
{
    /// <summary>
    /// Bypass Response value object class.
    /// <para>
    /// Cluster: IAS ACE. Command is sent FROM the server.
    /// This command is a specific command used for the IAS ACE cluster.
    ///
    /// Provides the response of the security panel to the request from the IAS ACE client to bypass zones via a Bypass command.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class BypassResponse : ZclCommand
    {
        /// <summary>
        /// Bypass Result command message field.
        ///
        /// An array of Zone IDs for each zone requested to be bypassed via the Bypass command where X is equal to the value of
        /// the Number of Zones field. The order of results for Zone IDs SHALL be the same as the order of Zone IDs sent in
        /// the Bypass command by the IAS ACE client.
        /// </summary>
        public List<byte> BypassResult { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public BypassResponse()
        {
            GenericCommand = false;
            ClusterId = 1281;
            CommandId = 7;
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
            builder.Append(BypassResult);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
