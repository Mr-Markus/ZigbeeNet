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
    /// Unlock With Timeout value object class.
    ///
    /// Cluster: Door Lock. Command ID 0x03 is sent TO the server.
    /// This command is a specific command used for the Door Lock cluster.
    ///
    /// This command causes the lock device to unlock the door with a timeout parameter. After
    /// the time in seconds specified in the timeout field, the lock device will relock itself
    /// automatically. This timeout parameter is only temporary for this message transition
    /// only and overrides the default relock time as specified in the [Auto Relock Time
    /// attribute] attribute. If the door lock device is not capable of or does not want to
    /// support temporary Relock Timeout, it should not support this optional command.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class UnlockWithTimeout : ZclCommand
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
        /// Timeout In Seconds command message field.
        /// </summary>
        public ushort TimeoutInSeconds { get; set; }

        /// <summary>
        /// PIN command message field.
        /// </summary>
        public string Pin { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public UnlockWithTimeout()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(TimeoutInSeconds, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(Pin, ZclDataType.Get(DataType.CHARACTER_STRING));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            TimeoutInSeconds = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            Pin = deserializer.Deserialize<string>(ZclDataType.Get(DataType.CHARACTER_STRING));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("UnlockWithTimeout [");
            builder.Append(base.ToString());
            builder.Append(", TimeoutInSeconds=");
            builder.Append(TimeoutInSeconds);
            builder.Append(", Pin=");
            builder.Append(Pin);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
