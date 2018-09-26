using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.Commands.General
{
    public class DiscoverCommandsReceivedResponseCommand : GeneralCommand
    {
        public bool DiscoveryComplete { get; set; }

        public List<ushort> CommandIdentifiers{ get; set; }

        public DiscoverCommandsReceivedResponseCommand()
            : base(FrameType.Global, Direction.ServerToClient, (byte)GlobalCommands.DiscoverCommandsReceivedResponse)
        {
            CommandIdentifiers = new List<ushort>();
        }

        public void AddCommand(ushort identifier)
        {
            CommandIdentifiers.Add(identifier);
        }
    }
}
