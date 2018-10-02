using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet
{
    public class ZigbeeController
    {
        private Options _options;

        public ZigbeeService Service { get; }
        public CCZnp Znp { get; set; }

        public event EventHandler Started;
        public event EventHandler Stoped;

        public ZigbeeController(ZigbeeService service, Options options)
        {
            _options = options;

            Service = service;

            Znp = new CCZnp();
            Znp.Ready += Znp_Ready;
        }

        private void Znp_Ready(object sender, EventArgs e)
        {
            //TODO: SetupCoord

        }

        public void Init()
        {
            
        }

        public void Start()
        {
            //TODO. Init ZNP --> Execute Start Command
            Znp.Init(_options.Port, _options.Baudrate);
        }

        private void StartupCoord()
        {
            ArgumentCollection args = new ArgumentCollection();
            args.Add("startdelay", DataType.UInt16, 100);

            //TODO: Add stateChangedInd event

            Request(SubSystem.ZDO, 0x40, args);
        }

        public void PermitJoin(int time, bool onCoordOnly, Action callback = null)
        {
            if (time > 255 || time < 0)
            {
                throw new ArgumentOutOfRangeException("time", "Given value for 'time' have to be greater than 0 and less than 255");
            }            

            ArgumentCollection valObj = new ArgumentCollection();
            valObj.Add("addrmode", DataType.UInt8, 0x02);
            valObj.Add("dstaddr", DataType.UInt16, 0);
            valObj.Add("duration", DataType.UInt16, 0);
            valObj.Add("tcsignificance", DataType.UInt16, 0);

            this.Request(SubSystem.ZDO, 54, valObj, callback);
        }

        public void Request(SubSystem subSystem, byte commandId, ArgumentCollection valObj, Action callback = null)
        {
            if(subSystem == SubSystem.ZDO)
            {
                ZDO zdo = new ZDO(this);

                zdo.Request(commandId, valObj, callback);
            }
            else
            {
                Znp.Request(subSystem, commandId, valObj);
            }
        }
    }
}
