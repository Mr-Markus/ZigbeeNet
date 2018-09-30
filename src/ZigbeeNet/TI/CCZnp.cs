using System;
using System.Collections.Generic;
using System.IO;
using ZigbeeNet;
using ZigbeeNet.TI.Components;
using UnpiNet;

namespace ZigbeeNet.TI
{
    public class CCZnp : IZnp
    {
        //TODO: get and set to data destination e.g. database or textfile
        public Dictionary<ulong, Device> Devices { get; set; }

        public bool Enabled { get; set; }
        public Unpi Unpi { get; set; }

        public CCZnp()
        {
            
        }

        public void Init(string port, int baudrate = 115200, Action callback = null)
        {
            Unpi = new Unpi(port, baudrate, 1);
            Unpi.DataReceived += Unpi_DataReceived;
            Unpi.Opened += Unpi_Opened;
            Unpi.Closed += Unpi_Closed;

            Unpi.Open();
        }

        private void Unpi_Closed(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Unpi_Opened(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Unpi_DataReceived(object sender, Packet e)
        {
            ParseIncomingData(e);
        }

        public void Start()
        {
            Enabled = true;

            //TODO: Fire ready event
        }

        public void Stop()
        {
            Enabled = false;
        }

        public void Reset()
        {

        }

        public void RegisterDevice(Device device)
        {

        }

        public void UnregisterDevice(Device device)
        {

        }

        public void PermitJoin(int time, bool onCoord)
        {
            if(Enabled == false)
            {
                throw new Exception("ZNP is not enabled");
            }
            
            byte addrmode = 0x02;       //Coordinator
            ushort dstaddr = 0x0000;    //Coordinator adress;

            if (onCoord == false)
            {
                addrmode = 0x0F;
                dstaddr = 0xFFFC;   // all coord and routers
            }

            if (time > 255 || time < 0)
                throw new Exception("Jointime can only range from  0 to 255.");

            byte cmd = 54; //mgmtPermitJoinReq

            ArgumentCollection valObj = new ArgumentCollection();
            valObj.Add("addrmode", DataType.UInt8, addrmode);
            valObj.Add("dstaddr", DataType.UInt16, dstaddr);
            valObj.Add("duration", DataType.UInt16, 0);
            valObj.Add("tcsignificance", DataType.UInt16, 0);

            this.Request(SubSystem.ZDO, cmd, valObj);
        }

        public void Request(SubSystem subSystem, byte commandId, ArgumentCollection valObject, Action callback = null)
        {
            if(Unpi == null)
            {
                throw new NullReferenceException("CCZnp has not been initialized yet");
            }

            ZpiObject zpiObject = new ZpiObject(subSystem, commandId, valObject);

            if(zpiObject.Type == CommandType.SREQ)
            {
                SendSREQ(zpiObject, callback);
            }
            else if (zpiObject.Type == CommandType.AREQ)
            {

            }
        }

        private void SendSREQ(ZpiObject zpiObject, Action callback)
        {
            Unpi.Send((int)zpiObject.Type, (int)zpiObject.SubSystem, zpiObject.CommandId, zpiObject.Frame);
        }

        private ZpiObject ParseIncomingData(Packet data)
        {
            ZpiObject zpiObject = new ZpiObject((SubSystem)data.SubSystem, data.Cmd1);

            zpiObject.Parse((CommandType)data.Type, data.Length, data.Payload, (string error, string result) =>
            {

            });

            return zpiObject;            
        }
    }
}
