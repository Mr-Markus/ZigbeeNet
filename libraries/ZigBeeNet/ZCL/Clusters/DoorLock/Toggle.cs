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
    /// Toggle value object class.
    ///
    /// Cluster: Door Lock. Command ID 0x02 is sent TO the server.
    /// This command is a specific command used for the Door Lock cluster.
    ///
    /// Request the status of the lock. As of HA 1.2, this command includes an optional code for
    /// the lock. The door lock may require a code depending on the value of the [Require PIN for RF
    /// Operation attribute]
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class Toggle : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0101;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x02;

        /// <summary>
        /// PIN command message field.
        /// </summary>
        public string Pin { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Toggle()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Pin, ZclDataType.Get(DataType.CHARACTER_STRING));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Pin = deserializer.Deserialize<string>(ZclDataType.Get(DataType.CHARACTER_STRING));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("Toggle [");
            builder.Append(base.ToString());
            builder.Append(", Pin=");
            builder.Append(Pin);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
