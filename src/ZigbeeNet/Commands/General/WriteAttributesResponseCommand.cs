using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.Commands.General
{
    public class WriteAttributeStatusRecord
    {
        public WriteAttributeStatusRecord(ushort identifier, Status status)
        {
            AttributeIdentifier = identifier;
            Status = status;
        }
        /// <summary>
        /// The status field is 8 bits in length and specifies the status of the write operation attempted on this attribute, as detailed in 2.5.3.3.  
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// The attribute identifier field is 16 bits in length and SHALL contain the identifier of the attribute on which the write operation was attempted
        /// </summary>
        public ushort AttributeIdentifier { get; set; }
    }

    /// <summary>
    /// The Write Attributes Response command is generated in response to a Write Attributes command
    /// 
    /// Note that write attribute status records are not included for successfully written attributes, in order to save bandwidth. 
    /// In the case of successful writing of all attributes, only a single write attribute status record SHALL be included in the command, 
    /// with the status field set to SUCCESS and the attribute identifier field omitted.
    /// </summary>
    public class WriteAttributesResponseCommand : GeneralCommand
    {
        public List<WriteAttributeStatusRecord> WriteAttributeStatuses { get; set; }
       
        public WriteAttributesResponseCommand()
            : base(FrameType.Global, Direction.ServerToClient, (byte)GlobalCommands.WriteAttributesResponse)
        {
            WriteAttributeStatuses = new List<WriteAttributeStatusRecord>();
        }

        public void AddAttributeStatus(ushort identifier, Status status)
        {
            WriteAttributeStatusRecord writeAttribute = new WriteAttributeStatusRecord(identifier, status);

            WriteAttributeStatuses.Add(writeAttribute);
        }
    }
}
