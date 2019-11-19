using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.KeyEstablishment;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.KeyEstablishment
{
    /// <summary>
    /// Terminate Key Establishment value object class.
    ///
    /// Cluster: Key Establishment. Command ID 0x03 is sent FROM the server.
    /// This command is a specific command used for the Key Establishment cluster.
    ///
    /// The Terminate Key Establishment command may be sent by either the initiator or
    /// responder to indicate a failure in the key establishment exchange.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class TerminateKeyEstablishment : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0800;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x03;

        /// <summary>
        /// Status Code command message field.
        /// </summary>
        public byte StatusCode { get; set; }

        /// <summary>
        /// Wait Time command message field.
        /// 
        /// This value indicates the minimum amount of time in seconds the initiator device
        /// should wait before trying to initiate key establishment again. The valid range is
        /// 0x00 to 0xFE.
        /// </summary>
        public byte WaitTime { get; set; }

        /// <summary>
        /// Key Establishment Suite command message field.
        /// 
        /// This value will be set the value of the KeyEstablishmentSuite attribute. It
        /// indicates the list of key exchange methods that the device supports.
        /// </summary>
        public ushort KeyEstablishmentSuite { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public TerminateKeyEstablishment()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(StatusCode, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(WaitTime, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(KeyEstablishmentSuite, ZclDataType.Get(DataType.BITMAP_16_BIT));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            StatusCode = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            WaitTime = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            KeyEstablishmentSuite = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("TerminateKeyEstablishment [");
            builder.Append(base.ToString());
            builder.Append(", StatusCode=");
            builder.Append(StatusCode);
            builder.Append(", WaitTime=");
            builder.Append(WaitTime);
            builder.Append(", KeyEstablishmentSuite=");
            builder.Append(KeyEstablishmentSuite);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
