using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.Commands
{
    public abstract class BaseCommand
    {
        public byte Identifier
        {
            get
            {
                return Header.CommandIdentifierField;
            }
            set
            {
                Header.CommandIdentifierField = value;
            }
        }

        public FrameHeader Header { get; set; }

        public BaseCommand(FrameType frameType, Direction direction, byte commandIdentifier)
        {
            Header = new FrameHeader(frameType, direction, commandIdentifier);
        }
    }
}
