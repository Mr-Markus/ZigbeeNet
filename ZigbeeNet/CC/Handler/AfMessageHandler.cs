using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZigBeeNet.CC.Packet;

namespace ZigBeeNet.CC.Handler
{
    public class AfMessageHandler : IPacketHandler
    {
        private CCZnp _znp;

        public AfMessageHandler(CCZnp znp)
        {
            _znp = znp;
        }

        public Task Handle(AsynchronousRequest asynchronousRequest)
        {
            throw new NotImplementedException();
        }
    }
}
