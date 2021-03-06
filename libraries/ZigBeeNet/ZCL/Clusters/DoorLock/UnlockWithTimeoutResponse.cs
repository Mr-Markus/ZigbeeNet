using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.DoorLock;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.DoorLock
{
    /// <summary>
    /// Unlock With Timeout Response value object class.
    ///
    /// Cluster: Door Lock. Command ID 0x03 is sent FROM the server.
    /// This command is a specific command used for the Door Lock cluster.
    ///
    /// This command is sent in response to an Unlock with Timeout command with one status byte
    /// payload. The Status field shall be set to SUCCESS or FAILURE.
    /// The status byte only indicates if the message has received successfully. To determine
    /// status, the client should query to [Lock State attribute] and [Door State attribute].
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class UnlockWithTimeoutResponse : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0101;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x03;

        /// <summary>
        /// Status command message field.
        /// </summary>
        public byte Status { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public UnlockWithTimeoutResponse()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Status, DataType.UNSIGNED_8_BIT_INTEGER);
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Status = deserializer.Deserialize<byte>(DataType.UNSIGNED_8_BIT_INTEGER);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("UnlockWithTimeoutResponse [");
            builder.Append(base.ToString());
            builder.Append(", Status=");
            builder.Append(Status);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
