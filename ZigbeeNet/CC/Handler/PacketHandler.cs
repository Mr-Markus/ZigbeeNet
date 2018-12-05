using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZigBeeNet.CC.Packet;
using ZigBeeNet.CC.Packet.SimpleAPI;
using ZigBeeNet.CC.Packet.SYS;
using ZigBeeNet.CC.Packet.ZDO;
using ZigBeeNet.Logging;
using ZigBeeNet.ZCL;

namespace ZigBeeNet.CC.Handler
{
    public class PacketHandler : IPacketHandler
    {
        private readonly ILog _logger = LogProvider.For<PacketHandler>();
        private CCZnp _znp;

        public PacketHandler(CCZnp znp)
        {
            _znp = znp;
        }
        public async Task Handle(AsynchronousRequest asynchronousRequest)
        {
            if (asynchronousRequest is SYS_RESET_RESPONSE res)
            {
                //TODO: Maybe it is not correct at this point
                //==> Starts the hardware CC2531
                ZB_START_REQUEST start = new ZB_START_REQUEST();
                await _znp.SendAsync<ZB_START_REQUEST_RSP>(start).ConfigureAwait(false);
            }
            
            if (asynchronousRequest is ZDO_STATE_CHANGE_IND stateInd)
            {
                _logger.Info("State changed: {state}", stateInd.Status);
                
                if (stateInd.Status == DeviceState.Started_as_ZigBee_Coordinator)
                {
                    _znp.OnStarted();
                }
            }
        }
    }
}
