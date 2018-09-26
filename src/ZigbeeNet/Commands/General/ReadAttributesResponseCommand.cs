using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.Commands.General
{
    public class ReadAttributesStatus
    {
        public ReadAttributesStatus(ushort identifier, Status status, DataType dataType, object value)
        {
            AttributeIdentifier = identifier;
            Status = status;
            AttributeDataType = dataType;
            AttributeValue = value;
        }
        /// <summary>
        /// The attribute identifier field is 16 bits in length and SHALL contain the identifier of the attribute 
        /// that has been read (or of which an element has been read). 
        /// 
        /// This field SHALL contain the same value that was included in the corresponding attribute identifier field of the 
        /// original Read Attributes or Read Attributes Structured command. 
        /// </summary>
        public ushort AttributeIdentifier { get; set; }

        /// <summary>
        /// The status field is 8 bits in length and specifies the status of the read operation on this attribute. 
        /// This field SHALL be set to SUCCESS, if the operation was successful, or an error code, if the operation was not successful. 
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// The attribute data type field SHALL contain the data type of the attribute in the same Read Attributes status record (see Table 2-10). 
        /// This field SHALL only be included if the associated status field contains a value of SUCCESS. 
        /// </summary>
        public DataType AttributeDataType { get; set; }

        public object AttributeValue { get; set; }
    }

    /// <summary>
    /// The Read Attributes Response command is generated in response to a Read Attributes or Read Attributes Structured command. 
    /// The command frame SHALL contain a read attribute status record for each attribute identifier specified in the original Read Attributes 
    /// or Read Attributes Structured command. For each read attribute status record, the attribute identifier field SHALL contain the identifier specified 
    /// in the original Read Attributes or Read Attributes Structured command. The status field SHALL contain a suitable status code, as detailed in 2.5.1.3.
    /// 
    /// The attribute data type and attribute value field SHALL only be included in the read attribute status record if the associated status field contains 
    /// a value of SUCCESS and, where present, SHALL contain the data type and current value, respectively, of the attribute, or element thereof, that was read
    /// 
    /// The length of this command may exceed a single frame, and thus fragmentation support may be needed to return the entire response If fragmentation is not supported, 
    /// only as many read attribute status records as will fit in the frame SHALL be returned
    /// </summary>
    public class ReadAttributesResponseCommand : BaseCommand
    {
        public List<ReadAttributesStatus> ReadAttributesStatuses { get; set; }

        public ReadAttributesResponseCommand()
            : base(FrameType.Global, Direction.ServerToClient, (byte)GlobalCommands.ReadAttributesResponse)
        {
            ReadAttributesStatuses = new List<ReadAttributesStatus>();
        }


        public void AddAttribute(ushort identifier, Status status, DataType dataType, object value)
        {
            ReadAttributesStatus attribute = new ReadAttributesStatus(identifier, status, dataType, value);

            ReadAttributesStatuses.Add(attribute);
        }
    }
}
