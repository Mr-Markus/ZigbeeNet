using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.General;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.General
{
    /// <summary>
    /// Write Attributes Command value object class.
    ///
    /// Cluster: General. Command ID 0x02 is sent TO the server.
    /// This command is a generic command used across the profile.
    ///
    /// The write attributes command is generated when a device wishes to change the values of
    /// one or more attributes located on another device. Each write attribute record shall
    /// contain the identifier and the actual value of the attribute to be written.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class WriteAttributesCommand : ZclCommand
    {
        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x02;

        /// <summary>
        /// Records command message field.
        /// </summary>
        public List<WriteAttributeRecord> Records { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public WriteAttributesCommand()
        {
            CommandId = COMMAND_ID;
            GenericCommand = true;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Records, ZclDataType.Get(DataType.N_X_WRITE_ATTRIBUTE_RECORD));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Records = deserializer.Deserialize<List<WriteAttributeRecord>>(ZclDataType.Get(DataType.N_X_WRITE_ATTRIBUTE_RECORD));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("WriteAttributesCommand [");
            builder.Append(base.ToString());
            builder.Append(", Records=");
            builder.Append(Records);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
