using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.CC;
using ZigbeeNet.ZCL;
using ZigbeeNet.CC.ZDO;
using ZigbeeNet.CC.SAPI;

namespace ZigbeeNet
{
    public class ZigbeeController
    {
        private Options _options;

        public ZigbeeService Service { get; }
        public CCZnp Znp { get; set; }

        public event EventHandler Started;
        public event EventHandler Stoped;
        public event EventHandler<ZpiObject> NewPacket;
        public event EventHandler<ZpiObject> PermitJoining;
        public event EventHandler<Device> NewDevice;

        private ConcurrentQueue<EndDeviceAnnouncedInd> _joinQueue;
        private bool _spinLock;
        private byte _transId;

        public ZigbeeController(ZigbeeService service, Options options)
        {
            _options = options;
            _joinQueue = new ConcurrentQueue<EndDeviceAnnouncedInd>();

            Service = service;

            Znp = new CCZnp();
            Znp.Ready += Znp_Ready;
            Znp.AsyncResponse += Znp_AsyncResponse;
        }

        private void Znp_SyncResponse(object sender, ZpiObject e)
        {
            NewPacket?.Invoke(this, e);
        }

        private void Znp_AsyncResponse(object sender, ZpiObject e)
        {
            NewPacket?.Invoke(this, e);

            if (e.SubSystem == SubSystem.ZDO && e.CommandId == (byte)ZdoCommand.endDeviceAnnceInd)
            {
                EndDeviceAnnouncedInd ind = e.ToSpecificObject<EndDeviceAnnouncedInd>();
                if (_spinLock)
                {
                    if(_joinQueue.Where(zpi => zpi.IeeeAddress == ind.IeeeAddress).Count() > 0)
                    {
                        return;
                    }

                    _joinQueue.Enqueue(ind);
                }
                else
                {
                    _spinLock = true;
                    endDeviceAnnceHdlr(ind);
                }
            }
            if(e.SubSystem == SubSystem.ZDO && e.CommandId == (byte)ZdoCommand.mgmtPermitJoinRsp)
            {
                PermitJoining?.Invoke(this, e);
            }
        }

        private void Znp_Ready(object sender, EventArgs e)
        {
            


            StartRequest startRequest = new StartRequest();
            startRequest.OnResponse += StartRequest_OnResponse;
            startRequest.Request(Znp);
        }

        private void StartRequest_OnResponse(object sender, ZpiObject e)
        {
            Started?.Invoke(this, EventArgs.Empty);
        }

        //init.setupCoord = function(controller, callback)
        //{

        //    return controller.checkNvParams().then(function() {

        //        return init._bootCoordFromApp(controller);

        //    }).then(function(netInfo) {

        //        return init._registerDelegators(controller, netInfo);

        //    }).nodeify(callback);

        //};

        private void BootCoordFromApp()
        {

        }

        public void Init()
        {
            
        }

        public void Start()
        {
            Znp.Init(_options.Port, _options.Baudrate);
        }

        private void endDeviceAnnceHdlr(EndDeviceAnnouncedInd deviceInd)
        {
            //TODO: Try to get device from device db and check status if it is online. If true continue with next ind
            Device device = null;
            if (device != null && device.Status == DeviceStatus.Online)
            {
                Console.WriteLine("Device already in Network");

                EndDeviceAnnouncedInd removed = null;
                if (_joinQueue.TryDequeue(out removed))
                {
                    EndDeviceAnnouncedInd next = null;

                    if (_joinQueue.TryDequeue(out next))
                    {
                        endDeviceAnnceHdlr(next);
                    } else
                    {
                        _spinLock = false;
                    }
                }

                return;
            }

            //TODO: Timeout

            Query query = new Query(Znp);

            query.GetDevice(deviceInd.NetworkAddress, deviceInd.IeeeAddress, (dev) =>
            {
                NewDevice?.Invoke(this, dev);
            });
        }

        public void PermitJoin(int time)
        {
            if (time > 255 || time < 0)
            {
                throw new ArgumentOutOfRangeException("time", "Given value for 'time' have to be greater than 0 and less than 255");
            }

            PermitJoinRequest permitJoinRequest = new PermitJoinRequest(Convert.ToByte(time));

            this.Request(permitJoinRequest);
        }

        public void Request(ZpiObject zpiObject)
        {
            Znp.Request(zpiObject);
        }

        public void Request(SubSystem subSystem, byte cmdId, ArgumentCollection valObj)
        {
            ZpiObject zpiObject = new ZpiObject(subSystem, cmdId)
            {
                RequestArguments = valObj
            };

            Request(zpiObject);
        }

        internal byte NextTransId()
        {  // zigbee transection id

            if (++_transId > 255)
                _transId = 1;

            return _transId;
        }
    }
}
