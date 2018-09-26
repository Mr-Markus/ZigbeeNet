using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.Commands.General
{
    public class DefaultResponseCommand : GeneralCommand
    {
        public ushort CommandIdentifier { get; set; }
        public Status Status { get; set; }

        public DefaultResponseCommand()
            :base(FrameType.Global, Direction.ServerToClient, (byte)GlobalCommands.DefaultResponse)
        {

        }
    }
}
