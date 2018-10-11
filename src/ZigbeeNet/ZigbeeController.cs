using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.CC;
using ZigbeeNet.CC.Commands;
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
        public event EventHandler<ZpiObject> NewPacket;
        public event EventHandler<ZpiObject> PermitJoining;

        public ZigbeeController(ZigbeeService service, Options options)
        {
            _options = options;

            Service = service;

            Znp = new CCZnp();
            Znp.Ready += Znp_Ready;
            Znp.AsyncResponse += Znp_AsyncResponse;
        }

        private void Znp_SyncResponse(object sender, ZpiObject e)
        {
            NewPacket?.Invoke(this, e);

            if (e.SubSystem == SubSystem.ZDO && e.CommandId == (byte)CC.ZDO.endDeviceAnnceInd)
            {
                endDeviceAnnceHdlr(e);
            }
        }

        private void Znp_AsyncResponse(object sender, ZpiObject e)
        {
            NewPacket?.Invoke(this, e);

            if (e.SubSystem == SubSystem.ZDO && e.CommandId == (byte)CC.ZDO.endDeviceAnnceInd)
            {
                endDeviceAnnceHdlr(e);
            }
            if(e.SubSystem == SubSystem.ZDO && e.CommandId == (byte)CC.ZDO.mgmtPermitJoinRsp)
            {
                PermitJoining?.Invoke(this, e);
            }
        }

        private void Znp_Ready(object sender, EventArgs e)
        {
            StartupFromAppRequest startupFromAppRequest = new StartupFromAppRequest();

            //TODO: Add stateChangedInd event
            this.Request(startupFromAppRequest);
        }

        public void Init()
        {
            
        }

        public void Start()
        {
            //TODO. Init ZNP --> Execute Start Command
            Znp.Init(_options.Port, _options.Baudrate);
        }

        private void endDeviceAnnceHdlr(ZpiObject zpiObject)
        {
            EndDeviceAnnouncedInd ind = new EndDeviceAnnouncedInd(zpiObject);

            //TODO: Fill devInfo

            ZpiObject epReq = new ZpiObject(SubSystem.ZDO, (byte)CC.ZDO.activeEpReq);

            epReq.RequestArguments["dstaddr"] = ind.NetworkAddress;
            epReq.RequestArguments["nwkaddrofinterest"] = ind.NetworkAddress;

            Request(epReq);
        }

        public void PermitJoin(int time, Action<ZpiObject> callback = null)
        {
            if (time > 255 || time < 0)
            {
                throw new ArgumentOutOfRangeException("time", "Given value for 'time' have to be greater than 0 and less than 255");
            }

            PermitJoinRequest permitJoinRequest = new PermitJoinRequest(Convert.ToByte(time));

            this.Request(permitJoinRequest, callback);
        }

        public void Request(ZpiObject zpiObject, Action<ZpiObject> callback = null)
        {
            Znp.Request(zpiObject, callback);
        }

        public void Request(SubSystem subSystem, byte cmdId, ArgumentCollection valObj, Action<ZpiObject> callback = null)
        {
            ZpiObject zpiObject = new ZpiObject(subSystem, cmdId)
            {
                RequestArguments = valObj
            };

            Request(zpiObject, callback);
        }
    }
}
