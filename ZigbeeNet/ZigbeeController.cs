using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.CC;
using ZigbeeNet.ZCL;
using ZigbeeNet.CC.ZDO;
using ZigbeeNet.CC.SAPI;
using System.Threading;

namespace ZigbeeNet
{
    public class ZigbeeController
    {
        private Options _options;

        public ZigbeeService Service { get; }
        public IHardwareChannel Znp { get; set; }

        public event EventHandler Started;
        public event EventHandler Stoped;
        public event EventHandler<ZpiObject> NewPacket;
        public event EventHandler<ZpiObject> PermitJoining;
        public event EventHandler<Device> NewDevice;

        private byte _transId;

        public ZigbeeController(ZigbeeService service, Options options)
        {
            _options = options;

            Service = service;

            Znp = new CCZnp(); //TODO: Get Hardware interface by another way
        }
        
        private void BootCoordFromApp()
        {

        }

        public void Init()
        {
            
        }

        public void Start()
        {
            Znp.Open();
        }

        public void Stop()
        {
            Znp.Close();
        }

        public void PermitJoin(int time)
        {
            if (time > 255 || time < 0)
            {
                throw new ArgumentOutOfRangeException("time", "Given value for 'time' have to be greater than 0 and less than 255");
            }

            Znp.PermitJoinAsync(time);
        }

        internal byte NextTransId()
        {  // zigbee transection id

            if (++_transId > 255)
                _transId = 1;

            return _transId;
        }
    }
}
