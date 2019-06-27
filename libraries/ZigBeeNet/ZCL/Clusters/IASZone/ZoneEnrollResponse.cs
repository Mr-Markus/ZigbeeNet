// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.IASZone;


namespace ZigBeeNet.ZCL.Clusters.IASZone
{
    /// <summary>
    /// Zone Enroll Response value object class.
    /// <para>
    /// Cluster: IAS Zone. Command is sent TO the server.
    /// This command is a specific command used for the IAS Zone cluster.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZoneEnrollResponse : ZclCommand
    {
        /// <summary>
        /// Enroll response code command message field.
        ///
        /// Specifies the duration, in seconds, for which the IAS Zone server SHALL operate in its test mode.
        /// Specifies the sensitivity level the IAS Zone server SHALL use for the duration of the Test Mode and with which it must update its
        /// CurrentZoneSensitivityLevel attribute.
        /// <p>
        /// The permitted values of Current Zone Sensitivity Level are shown defined for the CurrentZoneSensitivityLevel Attribute.
        /// </summary>
        public byte EnrollResponseCode { get; set; }

        /// <summary>
        /// Zone ID command message field.
        /// </summary>
        public byte ZoneID { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public ZoneEnrollResponse()
        {
            GenericCommand = false;
            ClusterId = 1280;
            CommandId = 0;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(EnrollResponseCode, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(ZoneID, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            EnrollResponseCode = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            ZoneID = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ZoneEnrollResponse [");
            builder.Append(base.ToString());
            builder.Append(", EnrollResponseCode=");
            builder.Append(EnrollResponseCode);
            builder.Append(", ZoneID=");
            builder.Append(ZoneID);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
