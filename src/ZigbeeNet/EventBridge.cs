using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.CC;

namespace ZigbeeNet
{
    public class EventBridge
    {
        private ZigbeeController _controller;

        public EventBridge(ZigbeeController controller)
        {
            _controller = controller;
            _controller.NewPacket += _controller_NewPacket;
        }

        private void _controller_NewPacket(object sender, ZpiObject e)
        {
            switch(e.SubSystem)
            {
                case CC.SubSystem.SYS:
                    switch((CC.SYS)e.CommandId)
                    {
                        case CC.SYS.resetInd:

                            break;
                    }
                    break;
                case CC.SubSystem.ZDO:
                    switch ((ZDO)e.CommandId)
                    {
                        case ZDO.tcDeviceInd:

                            break;
                        case ZDO.stateChangeInd:                            
                            if ((DeviceState)e.RequestArguments["state"] == DeviceState.Started_as_ZigBee_Coordinator)
                            {
                                _controller.Service.Ready();
                            }
                            break;
                        case ZDO.matchDescRspSent:

                            break;
                        case ZDO.statusErrorRsp:

                            break;
                        case ZDO.srcRtgInd:

                            break;
                        case ZDO.beacon_notify_ind:

                            break;
                        case ZDO.leaveInd:

                            break;
                        case ZDO.msgCbIncoming:

                            break;
                        case ZDO.serverDiscRsp:

                            break;
                    }
                    break;
            }
        }
    }
}
