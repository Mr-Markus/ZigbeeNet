using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.Commands
{
    public class GeneralCommand : BaseCommand
    {
        public GeneralCommand(FrameType frameType, Direction direction, byte commandIdentifier)
            : base(frameType, direction, commandIdentifier)
        {

        }
    }
}
