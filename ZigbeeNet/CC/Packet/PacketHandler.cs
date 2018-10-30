using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZigbeeNet.CC.Packet.SimpleAPI;
using ZigbeeNet.CC.Packet.SYS;
using ZigbeeNet.CC.Packet.ZDO;
using ZigbeeNet.Logging;

namespace ZigbeeNet.CC.Packet
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
                await _znp.SendAsync<ZB_START_REQUEST_RSP>(start, msg => { return msg is SynchronousResponse && msg.SubSystem == start.SubSystem; }).ContinueWith((t) =>
                {
                    _znp.OnStarted();
                });
            }
            //TODO: HandleIncommingMessage
            if (asynchronousRequest is ZDO_STATE_CHANGE_IND stateInd)
            {
                _logger.Info("State changed: {state}", stateInd.Status);

                if (stateInd.Status == DeviceState.Started_as_ZigBee_Coordinator)
                {
                    ZAddress64 ieeeAddr = await GetIeeeAddress();
                    //ZAddress16 panId = await GetCurrentPanId();

                    await _znp.PermitJoinAsync(255);
                }
            }            
        }
        private async Task<byte[]> GetDeviceInfo(DEV_INFO_TYPE info)
        {
            ZB_GET_DEVICE_INFO infoReq = new ZB_GET_DEVICE_INFO(info);
            ZB_GET_DEVICE_INFO_RSP infoRsp = await _znp.SendAsync<ZB_GET_DEVICE_INFO_RSP>(infoReq, msg => msg.SubSystem == infoReq.SubSystem && msg.Cmd1 == infoReq.Cmd1).ConfigureAwait(false);

            return infoRsp.Value;
        }

        private async Task<ZAddress64> GetIeeeAddress()
        {
            byte[] result = await GetDeviceInfo(DEV_INFO_TYPE.IEEE_ADDR);

            ZAddress64 ieeeAddr = new ZAddress64(result);

            return ieeeAddr;
        }

        private async Task<ZAddress16> GetCurrentPanId()
        {
            byte[] result = await GetDeviceInfo(DEV_INFO_TYPE.PAN_ID);

            ZAddress16 panId = new ZAddress16(result);

            return panId;
        }
    }
}
