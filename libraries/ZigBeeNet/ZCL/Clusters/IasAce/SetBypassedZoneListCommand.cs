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
    /// Set Bypassed Zone List Command value object class.
    ///
    /// Cluster: IAS ACE. Command ID 0x06 is sent FROM the server.
    /// This command is a specific command used for the IAS ACE cluster.
    ///
    /// Sets the list of bypassed zones on the IAS ACE client. This command can be sent either as a
    /// response to the GetBypassedZoneList command or unsolicited when the list of bypassed
    /// zones changes on the ACE server.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class SetBypassedZoneListCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0501;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x06;

        /// <summary>
        /// Zone ID command message field.
        /// 
        /// Zone ID is the index of the Zone in the CIE's zone table and is an array of Zone IDs for
        /// each zone that is bypassed where X is equal to the value of the Number of Zones field.
        /// There is no order imposed by the numbering of the Zone ID field in this command
        /// payload. IAS ACE servers should provide the array of Zone IDs in ascending order.
        /// </summary>
        public List<byte> ZoneId { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SetBypassedZoneListCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(ZoneId, ZclDataType.Get(DataType.N_X_UNSIGNED_8_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            ZoneId = deserializer.Deserialize<List<byte>>(ZclDataType.Get(DataType.N_X_UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("SetBypassedZoneListCommand [");
            builder.Append(base.ToString());
            builder.Append(", ZoneId=");
            builder.Append(ZoneId);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
