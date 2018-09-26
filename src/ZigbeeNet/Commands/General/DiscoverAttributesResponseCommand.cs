using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.Commands.General
{
    public class AttributeReport
    {
        public AttributeReport(ushort attributeIdentifier, DataType dataType)
        {
            AttributeIdentifier = attributeIdentifier;
            DataType = dataType;
        }
        /// <summary>
        /// The attribute identifier field SHALL contain the identifier of a discovered attribute. 
        /// 
        /// Attributes SHALL be included in ascending order, starting with the lowest attribute identifier that 
        /// is greater than or equal to the start attribute identifier field of the received Discover Attributes command.
        /// </summary>
        public ushort AttributeIdentifier { get; set; }

        /// <summary>
        /// The attribute data type field SHALL contain the data type of the attribute in the same attribute report field (see Table 2-10). 
        /// </summary>
        public DataType DataType { get; set; }
    }

    public class DiscoverAttributesResponseCommand : GeneralCommand
    {
        public List<AttributeReport> Attributes { get; set; }

        public DiscoverAttributesResponseCommand()
            : base(FrameType.Global, Direction.ServerToClient, (byte)GlobalCommands.DiscoverAttributesResponse)
        {
            Attributes = new List<AttributeReport>();
        }
        
        public void AddAttribute(ushort identifier, DataType dataType)
        {
            AttributeReport attribute = new AttributeReport(identifier, dataType);

            Attributes.Add(attribute);
        }
    }
}
