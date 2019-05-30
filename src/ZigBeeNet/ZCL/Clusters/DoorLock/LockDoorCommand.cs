// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.DoorLock;


namespace ZigBeeNet.ZCL.Clusters.DoorLock
{
    /// <summary>
    /// Lock Door Command value object class.
    /// <para>
    /// Cluster: Door Lock. Command is sent TO the server.
    /// This command is a specific command used for the Door Lock cluster.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class LockDoorCommand : ZclCommand
    {
        /// <summary>
        /// Pin code command message field.
        /// </summary>
        public ByteArray PinCode { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public LockDoorCommand()
        {
            GenericCommand = false;
            ClusterId = 257;
            CommandId = 0;
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

            builder.Append("LockDoorCommand [");
            builder.Append(base.ToString());
            builder.Append(", PinCode=");
            builder.Append(PinCode);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
