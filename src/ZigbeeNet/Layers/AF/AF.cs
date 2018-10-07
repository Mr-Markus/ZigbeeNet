using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet
{
    public class AF
    {
        public event EventHandler AF_DataConfirm;
        public event EventHandler AF_ReflectError;
        public event EventHandler AF_IncomingMsg;
        public event EventHandler AF_IncomingMsgExt;
        public event EventHandler ZCL_IncomingMsg;

        public AF(ZigbeeController controller)
        {
            Controller = controller;

            
        }

        public ZigbeeController Controller { get; private set; }
    }
}
