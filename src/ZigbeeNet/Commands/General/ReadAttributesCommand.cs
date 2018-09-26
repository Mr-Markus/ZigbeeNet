using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.Commands.General
{
    /// <summary>
    /// The Read Attributes command is generated when a device wishes to determine the values of one or more attributes located on another device. 
    /// Each attribute identifier field SHALL contain the identifier of the attribute to be read
    /// </summary>
    public class ReadAttributesCommand : GeneralCommand
    {
        public List<ushort> AttributeIdentifiers { get; set; }

        public ReadAttributesCommand()
            : base(FrameType.Global, Direction.ClientToServer, (byte)GlobalCommands.ReadAttributes)
        {
            AttributeIdentifiers = new List<ushort>();
        }
        
        public void AddAttribute(ushort identifier)
        {
            AttributeIdentifiers.Add(identifier);
        }
    }
}
