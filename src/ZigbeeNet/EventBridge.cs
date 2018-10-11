using System;
using System.Collections.Generic;
using System.Text;

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

        private void _controller_NewPacket(object sender, CC.ZpiObject e)
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
                    switch ((CC.ZDO)e.CommandId)
                    {
                        case CC.ZDO.tcDeviceInd:

                            break;
                        case CC.ZDO.stateChangeInd:

                            break;
                        case CC.ZDO.matchDescRspSent:

                            break;
                        case CC.ZDO.statusErrorRsp:

                            break;
                        case CC.ZDO.srcRtgInd:

                            break;
                        case CC.ZDO.beacon_notify_ind:

                            break;
                        case CC.ZDO.leaveInd:

                            break;
                        case CC.ZDO.msgCbIncoming:

                            break;
                        case CC.ZDO.serverDiscRsp:

                            break;
                    }
                    break;
            }
        }

        //public void AttachEventHandlers()
        //{
        //    controller.on('SYS:resetInd', hdls.resetInd);

        //    controller.on('ZDO:devIncoming', hdls.devIncoming);

        //    controller.on('ZDO:tcDeviceInd', hdls.tcDeviceInd);

        //    controller.on('ZDO:stateChangeInd', hdls.stateChangeInd);

        //    controller.on('ZDO:matchDescRspSent', hdls.matchDescRspSent);

        //    controller.on('ZDO:statusErrorRsp', hdls.statusErrorRsp);

        //    controller.on('ZDO:srcRtgInd', hdls.srcRtgInd);

        //    controller.on('ZDO:beacon_notify_ind', hdls.beacon_notify_ind);

        //    controller.on('ZDO:leaveInd', hdls.leaveInd);

        //    controller.on('ZDO:msgCbIncoming', hdls.msgCbIncoming);

        //    controller.on('ZDO:serverDiscRsp', hdls.serverDiscRsp);

        //    // controller.on('ZDO:permitJoinInd',     hdls.permitJoinInd);
        //}
    }
}
