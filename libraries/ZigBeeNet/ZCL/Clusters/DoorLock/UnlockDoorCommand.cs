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
    /// Unlock Door Command value object class.
    ///
    /// Cluster: Door Lock. Command ID 0x01 is sent TO the server.
    /// This command is a specific command used for the Door Lock cluster.
    ///
    /// This command causes the lock device to unlock the door. As of HA 1.2, this command
    /// includes an optional code for the lock. The door lock may require a code depending on the
    /// value of the [Require PIN for RF Operation attribute].
    ///
    /// Note: If the attribute AutoRelockTime is supported the lock will close when the auto relock
    /// time has expired
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class UnlockDoorCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0101;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x01;

        /// <summary>
        /// PIN Code command message field.
        /// </summary>
        public ByteArray PinCode { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public UnlockDoorCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(PinCode, ZclDataType.Get(DataType.OCTET_STRING));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            PinCode = deserializer.Deserialize<ByteArray>(ZclDataType.Get(DataType.OCTET_STRING));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("UnlockDoorCommand [");
            builder.Append(base.ToString());
            builder.Append(", PinCode=");
            builder.Append(PinCode);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
