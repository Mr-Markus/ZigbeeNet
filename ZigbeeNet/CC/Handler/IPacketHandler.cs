using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZigBeeNet.CC.Packet;

namespace ZigBeeNet.CC.Handler
{
    public interface IPacketHandler
    {
        Task Handle(AsynchronousRequest asynchronousRequest);
    }
}
