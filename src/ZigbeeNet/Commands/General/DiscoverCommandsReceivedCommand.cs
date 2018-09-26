using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.Commands.General
{
    public class DiscoverCommandsReceivedCommand : GeneralCommand
    {
        /// <summary>
        /// The start command identifier field is 8-bits in length and specifies the value of the identifier at which to begin the command discovery
        /// </summary>
        public ushort StartAttributeIdentifier { get; set; }

        /// <summary>
        /// The maximum command identifiers field is 8-bits in length and specifies the maximum number of command identifiers that are to be returned 
        /// in the resulting Discover Commands Received Response
        /// </summary>
        public byte MaximumAttributeIdentifiers { get; set; }

        public DiscoverCommandsReceivedCommand()
            :base(FrameType.Global, Direction.ServerToClient, (byte)GlobalCommands.DiscoverCommandsReceived)
        {

        }
    }
}
