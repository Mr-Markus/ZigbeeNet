using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.CC;

namespace ZigbeeNet
{
    public class ZDO
    {
        public ZDO(ZigbeeController controller)
        {
            Controller = controller;
        }

        public ZigbeeController Controller { get; }

        public void Request(byte commandId, ArgumentCollection valObj, Action callback = null)
        {
            //TODO: Get RequestType
            RequestType requestType = RequestType.Special;

            switch(requestType)
            {
                case RequestType.Responseless:
                    ResponselessRequest(commandId, valObj, callback);
                    break;
                case RequestType.Generic:
                    GenericRequest(commandId, valObj, callback);
                    break;
                case RequestType.Concat:

                    break;
                case RequestType.Special:
                    SpecialRequest(commandId, valObj, callback);
                    break;
                default:

                    break;
            }
        }

        private void SendZdoRequestViaZnp(byte commandId, ArgumentCollection valObj, Action callback = null)
        {
            Controller.Znp.Request(SubSystem.ZDO, commandId, valObj, callback);
        }

        private void ResponselessRequest(byte commandId, ArgumentCollection valObj, Action callback = null)
        {
            SendZdoRequestViaZnp(commandId, valObj, callback);
        }

        private void SpecialRequest(byte commandId, ArgumentCollection valObj, Action callback = null)
        {
            if(commandId == 54)
            {

            }
        }

        private void GenericRequest(byte commandId, ArgumentCollection valObj, Action callback = null)
        {

        }
    }
}
