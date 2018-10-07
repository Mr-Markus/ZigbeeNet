using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.CC;
using ZigbeeNet.ZCL;

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
            Znp.AsyncResponse += Znp_AsyncResponse;
            Znp.SyncResponse += Znp_SyncResponse;
        }

        private void Znp_SyncResponse(object sender, ZpiObject e)
        {
            throw new NotImplementedException();
        }

        private void Znp_AsyncResponse(object sender, ZpiObject e)
        {
            throw new NotImplementedException();
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
            args.AddOrUpdate("startdelay", ParamType.uint16, 100);

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
            valObj.AddOrUpdate("addrmode", ParamType.uint8, 0x02);
            valObj.AddOrUpdate("dstaddr", ParamType.uint16, 0);
            valObj.AddOrUpdate("duration", ParamType.uint16, 0);
            valObj.AddOrUpdate("tcsignificance", ParamType.uint16, 0);

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
