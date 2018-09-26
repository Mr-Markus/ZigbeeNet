using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.TI.Components
{
    public class Controller
    {
        public Controller(Gateway gateway)
        {
            _gateway = gateway;
        }

        public Gateway _gateway { get; private set; }

        /// <summary>
        /// Starts time frame in which devices can join the network
        /// </summary>
        /// <param name="time">Time in seconds</param>
        /// <param name="onCoord">Flag if joining is only possible on coordinator or via all devices</param>
        public void PermitJoin(int time, bool onCoord)
        {
            byte addrmode = 0x02;       //Coordinator
            ushort dstaddr = 0x0000;    //Coordinator adress;

            if (onCoord == false)
            {
                addrmode = 0x0F;
                dstaddr = 0xFFFC;   // all coord and routers
            }

            if (time > 255 || time < 0)
                throw new Exception("Jointime can only range from  0 to 255.");

            this.Request("ZDO", "mgmtPermitJoinReq", new { addrmode = addrmode, dstaddr = dstaddr, duration = time, tcsignificance = 0 });
        }

        public void Request(string subSystem, string cmdId, object valObj)
        {
            if(subSystem == "ZDO")
            {
                //Zdo zdo = new Zdo(this);
               // zdo.Request(cmdId, valObj);
                
                //TODO: removed callback as log?
            }
            else
            {
                //znp.request
            }
        }
    }
}
