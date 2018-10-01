using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.Commands.General
{
    public class WriteAttributeRecord
    {
        public WriteAttributeRecord(ushort identifier, DataType dataType, object value)
        {
            AttributeIdentifier = identifier;
            AttributeDataType = dataType;
            AttributeValue = value;
        }
        /// <summary>
        /// The attribute identifier field is 16 bits in length and SHALL contain the identifier of the attribute that is to be written. 
        /// </summary>
        public ushort AttributeIdentifier { get; set; }

        /// <summary>
        /// The attribute data type field SHALL contain the data type of the attribute that is to be written. 
        /// </summary>
        public DataType AttributeDataType { get; set; }

        /// <summary>
        /// The attribute data field is variable in length and SHALL contain the actual value of the attribute that is to be written. 
        /// </summary>
        public object AttributeValue { get; set; }
    }

    /// <summary>
    /// The Write Attributes command is generated when a device wishes to change the values of one or more attributes located on another device. 
    /// Each write attribute record SHALL contain the identifier and the actual value of the attribute to be written
    /// </summary>
    public class WriteAttributesCommand : GeneralCommand
    {
        public List<WriteAttributeRecord> WriteAttributes { get; set; }

        public WriteAttributesCommand()
            : base(FrameType.Global, Direction.ClientToServer, (byte)GlobalCommands.WriteAttributes)
        {
            WriteAttributes = new List<WriteAttributeRecord>();
        }

        public void AddAttribute(ushort identifier, DataType dataType, object value)
        {
            WriteAttributeRecord attribute = new WriteAttributeRecord(identifier, dataType, value);

            WriteAttributes.Add(attribute);
        }
    }
}
