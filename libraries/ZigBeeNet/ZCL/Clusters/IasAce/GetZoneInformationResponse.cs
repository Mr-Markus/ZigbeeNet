using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.IasAce;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.IasAce
{
    /// <summary>
    /// Get Zone Information Response value object class.
    ///
    /// Cluster: IAS ACE. Command ID 0x02 is sent FROM the server.
    /// This command is a specific command used for the IAS ACE cluster.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetZoneInformationResponse : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0501;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x02;

        /// <summary>
        /// Zone ID command message field.
        /// </summary>
        public byte ZoneId { get; set; }

        /// <summary>
        /// Zone Type command message field.
        /// </summary>
        public ushort ZoneType { get; set; }

        /// <summary>
        /// IEEE Address command message field.
        /// </summary>
        public IeeeAddress IeeeAddress { get; set; }

        /// <summary>
        /// Zone Label command message field.
        /// 
        /// Provides the ZoneLabel stored in the IAS CIE. If none is programmed, the IAS ACE
        /// server shall transmit a string with a length of zero.There is no minimum or maximum
        /// length to the Zone Label field; however, the Zone Label should be between 16 to 24
        /// alphanumeric characters in length.
        /// The string encoding shall be UTF-8.
        /// </summary>
        public string ZoneLabel { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetZoneInformationResponse()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(ZoneId, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(ZoneType, ZclDataType.Get(DataType.ENUMERATION_16_BIT));
            serializer.Serialize(IeeeAddress, ZclDataType.Get(DataType.IEEE_ADDRESS));
            serializer.Serialize(ZoneLabel, ZclDataType.Get(DataType.CHARACTER_STRING));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            ZoneId = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            ZoneType = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.ENUMERATION_16_BIT));
            IeeeAddress = deserializer.Deserialize<IeeeAddress>(ZclDataType.Get(DataType.IEEE_ADDRESS));
            ZoneLabel = deserializer.Deserialize<string>(ZclDataType.Get(DataType.CHARACTER_STRING));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetZoneInformationResponse [");
            builder.Append(base.ToString());
            builder.Append(", ZoneId=");
            builder.Append(ZoneId);
            builder.Append(", ZoneType=");
            builder.Append(ZoneType);
            builder.Append(", IeeeAddress=");
            builder.Append(IeeeAddress);
            builder.Append(", ZoneLabel=");
            builder.Append(ZoneLabel);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
