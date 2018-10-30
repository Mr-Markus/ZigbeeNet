using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZigbeeNet.CC.Packet;

namespace ZigbeeNet.CC.Packet
{
    public interface IPacketHandler
    {
        Task Handle(AsynchronousRequest asynchronousRequest);
    }
}
