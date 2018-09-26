using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.Commands.General
{
    /// <summary>
    /// The Discover Attributes command is generated when a remote device wishes to discover the identifiers and types of the attributes on a device which 
    /// are supported within the cluster to which this command is directed
    /// </summary>
    public class DiscoverAttributesCommand : GeneralCommand
    {
        /// <summary>
        /// The start attribute identifier field is 16 bits in length and specifies the value of the identifier at which to begin the attribute discovery
        /// </summary>
        public ushort StartAttributeIdentifier { get; set; }

        /// <summary>
        /// The maximum attribute identifiers field is 8 bits in length and specifies the maximum number of attribute identifiers 
        /// that are to be returned in the resulting Discover Attributes Response command
        /// </summary>
        public byte MaximumAttributeIdentifiers { get; set; }

        public DiscoverAttributesCommand() 
            : base(FrameType.Global, Direction.ClientToServer, (byte)GlobalCommands.DiscoverAttributes)
        {

        }
    }
}
