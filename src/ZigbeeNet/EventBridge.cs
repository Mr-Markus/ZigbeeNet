using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigbeeNet.CC;
using ZigbeeNet.CC.SYS;
using ZigbeeNet.CC.ZDO;
using ZigbeeNet.Logging;
using ZigbeeNet.ZCL;
using ZigbeeNet.ZCL.Commands;

namespace ZigbeeNet
{
    public class EventBridge
    {
        private readonly ILog _logger = LogProvider.For<CCZnp>();

        private ZigbeeController _controller;
        private ZclBridge _zclBridge;

        public EventBridge(ZigbeeController controller)
        {
            _controller = controller;
            _controller.NewPacket += _controller_NewPacket;
            _controller.NewDevice += _controller_NewDevice;

            _zclBridge = new ZclBridge(_controller);
        }

        private void _controller_NewDevice(object sender, Device e)
        {
            e.Status = DeviceStatus.Online;
            e.JoinTime = DateTime.Now;

            try
            {
                Endpoint basicEp = e.Endpoints.SingleOrDefault(ep => ep.ClusterList.Contains(Cluster.genBasic));

                if(basicEp != null)
                {
                    ReadAttributesCommand readAttributes = new ReadAttributesCommand(4, 5, 7);
                    
                    readAttributes.OnResponse += ReadAttributes_OnResponse;

                    //ZCL.ZclMeta.Clusters
                    _zclBridge.ZclGlobal(basicEp, basicEp, Cluster.genBasic, readAttributes);
                }
            } catch (Exception ex)
            {
                _logger.Error(ex, "Error 0x0002");
            }
        }

        private void ReadAttributes_OnResponse(object sender, ZclCommand e)
        {
            throw new NotImplementedException();
        }

        private void _controller_NewPacket(object sender, ZpiObject e)
        {
            switch(e.SubSystem)
            {
                case SubSystem.SYS:
                    switch((SysCommand)e.CommandId)
                    {
                        case SysCommand.resetInd:

                            break;
                    }
                    break;
                case CC.SubSystem.ZDO:
                    switch ((ZdoCommand)e.CommandId)
                    {
                        case ZdoCommand.tcDeviceInd:

                            break;
                        case ZdoCommand.stateChangeInd:                            
                            if ((DeviceState)e.RequestArguments["state"] == DeviceState.Started_as_ZigBee_Coordinator)
                            {
                                _controller.Service.Ready();
                            }
                            break;
                        case ZdoCommand.matchDescRspSent:

                            break;
                        case ZdoCommand.statusErrorRsp:

                            break;
                        case ZdoCommand.srcRtgInd:

                            break;
                        case ZdoCommand.beacon_notify_ind:

                            break;
                        case ZdoCommand.leaveInd:

                            break;
                        case ZdoCommand.msgCbIncoming:

                            break;
                        case ZdoCommand.serverDiscRsp:

                            break;
                    }
                    break;
            }
        }
    }
}
