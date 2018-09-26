using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.Commands.General
{
    public class WriteAttributesNoResponseCommand : GeneralCommand
    {
        public List<WriteAttributeRecord> WriteAttributeRecords { get; set; }

        public WriteAttributesNoResponseCommand()
            : base(FrameType.Global, Direction.ClientToServer, (byte)GlobalCommands.WriteAttributesNoResponse)
        {
            WriteAttributeRecords = new List<WriteAttributeRecord>();
        }
        
        public void AddAttribute(ushort identifier, DataType dataType, object value)
        {
            WriteAttributeRecord attribute = new WriteAttributeRecord(identifier, dataType, value);

            WriteAttributeRecords.Add(attribute);
        }
    }
}
