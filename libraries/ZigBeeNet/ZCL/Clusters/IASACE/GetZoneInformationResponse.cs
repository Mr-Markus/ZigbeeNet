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
    /// Get Zone Information Response value object class.
    /// <para>
    /// Cluster: IAS ACE. Command is sent FROM the server.
    /// This command is a specific command used for the IAS ACE cluster.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetZoneInformationResponse : ZclCommand
    {
        /// <summary>
        /// Zone ID command message field.
        /// </summary>
        public byte ZoneID { get; set; }

        /// <summary>
        /// Zone Type command message field.
        /// </summary>
        public ushort ZoneType { get; set; }

        /// <summary>
        /// IEEE address command message field.
        /// </summary>
        public IeeeAddress IEEEAddress { get; set; }

        /// <summary>
        /// Zone Label command message field.
        ///
        /// Provides the ZoneLabel stored in the IAS CIE. If none is programmed, the IAS ACE server SHALL transmit a string with a length
        /// of zero.There is no minimum or maximum length to the Zone Label field; however, the Zone Label SHOULD be between 16 to 24
        /// alphanumeric characters in length.
        /// <p>
        /// The string encoding SHALL be UTF-8.
        /// </summary>
        public string ZoneLabel { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetZoneInformationResponse()
        {
            GenericCommand = false;
            ClusterId = 1281;
            CommandId = 2;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(ZoneID, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(ZoneType, ZclDataType.Get(DataType.ENUMERATION_16_BIT));
            serializer.Serialize(IEEEAddress, ZclDataType.Get(DataType.IEEE_ADDRESS));
            serializer.Serialize(ZoneLabel, ZclDataType.Get(DataType.CHARACTER_STRING));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            ZoneID = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            ZoneType = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.ENUMERATION_16_BIT));
            IEEEAddress = deserializer.Deserialize<IeeeAddress>(ZclDataType.Get(DataType.IEEE_ADDRESS));
            ZoneLabel = deserializer.Deserialize<string>(ZclDataType.Get(DataType.CHARACTER_STRING));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetZoneInformationResponse [");
            builder.Append(base.ToString());
            builder.Append(", ZoneID=");
            builder.Append(ZoneID);
            builder.Append(", ZoneType=");
            builder.Append(ZoneType);
            builder.Append(", IEEEAddress=");
            builder.Append(IEEEAddress);
            builder.Append(", ZoneLabel=");
            builder.Append(ZoneLabel);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
