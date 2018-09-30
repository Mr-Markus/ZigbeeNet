using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet
{
    public class ZigbeeController
    {
        public ZigbeeService Service { get; }
        public IZnp Znp { get; set; }

        private ConcurrentDictionary<ulong, Device> _deviceInfoList = new ConcurrentDictionary<ulong, Device>();

        public ZigbeeController(ZigbeeService service, IZnp znp)
        {
            Service = service;
            Znp = znp;
        }

        public void Init()
        {
            Service.Ready += Service_Ready;
        }

        public void Start(Action callback)
        {

        }

        private void Service_Ready(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void PermitJoin(int time, bool onCoordOnly)
        {
            if (time > 255 || time < 0)
            {
                throw new ArgumentOutOfRangeException("time", "Given value for 'time' have to be greater than 0 and less than 255");
            }

            object[] valObj = new object[] { 0x02, 0x00, time, 0 };

            //this.Request(SubSystem.ZDO, 54, valObj);
        }

        public void Request(SubSystem subSystem, byte commandId, Dictionary<string, object> valObj, Action callback = null)
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
