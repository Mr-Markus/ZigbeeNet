using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZigbeeNet.CC.Packet;
using ZigbeeNet.CC.Packet.SimpleAPI;
using ZigbeeNet.CC.Packet.SYS;
using ZigbeeNet.CC.Packet.ZDO;
using ZigbeeNet.Logging;

namespace ZigbeeNet.CC.Handler
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
                await _znp.SendAsync<ZB_START_REQUEST_RSP>(start).ContinueWith((t) =>
                {
                    _znp.OnStarted();
                });
            }
            
            if (asynchronousRequest is ZDO_STATE_CHANGE_IND stateInd)
            {
                _logger.Info("State changed: {state}", stateInd.Status);
                
                if (stateInd.Status == DeviceState.Started_as_ZigBee_Coordinator)
                {
                    ZigbeeNetwork network = new ZigbeeNetwork()
                    {
                        IeeeAddress = await GetIeeeAddress(),
                        PanId = await GetCurrentPanId(),
                        Channel = await GetCurrentChannel(),
                        NetworkAddress = await GetShortAddress()
                    };

                    _znp.Network = network;

                    _logger.Info("Network started: {@Network}", network);

                    //ZDO_SIMPLE_DESC_REQ simpleReq = new ZDO_SIMPLE_DESC_REQ(network.NetworkAddress, 0);
                    //ZDO_SIMPLE_DESC_REQ_SRSP simpleRsp = await _znp.SendAsync<ZDO_SIMPLE_DESC_REQ_SRSP>(simpleReq).ConfigureAwait(false);

                    await _znp.PermitJoinAsync(255);
                }
            }
        }

        private async Task<byte[]> GetDeviceInfo(DEV_INFO_TYPE info)
        {
            ZB_GET_DEVICE_INFO infoReq = new ZB_GET_DEVICE_INFO(info);
            ZB_GET_DEVICE_INFO_RSP infoRsp = await _znp.SendAsync<ZB_GET_DEVICE_INFO_RSP>(infoReq).ConfigureAwait(false);

            return infoRsp.Value;
        }

        internal async Task<ZigbeeAddress64> GetIeeeAddress()
        {
            byte[] result = await GetDeviceInfo(DEV_INFO_TYPE.IEEE_ADDR);

            ZigbeeAddress64 ieeeAddr = new ZigbeeAddress64(result);

            return ieeeAddr;
        }

        internal async Task<ZigbeeAddress16> GetShortAddress()
        {
            byte[] result = await GetDeviceInfo(DEV_INFO_TYPE.SHORT_ADDR);

            ZigbeeAddress16 addr = new ZigbeeAddress16(ByteHelper.ShortFromBytes(result, 1, 0));

            return addr;
        }

        internal async Task<ZigbeeAddress16> GetCurrentPanId()
        {
            byte[] result = await GetDeviceInfo(DEV_INFO_TYPE.PAN_ID);

            ushort relevantValue = ByteHelper.ShortFromBytes(result, 1, 0);

            ZigbeeAddress16 panId = new ZigbeeAddress16(relevantValue);

            return panId;
        }

        internal async Task<byte> GetCurrentChannel()
        {
            byte[] result = await GetDeviceInfo(DEV_INFO_TYPE.CHANNEL);

            return result[0];
        }
    }
}
