using System;
using System.Collections.Generic;
using ZigbeeNet;
using ZigbeeNet.TI.Components;

namespace ZigbeeNet.TI
{
    public class Gateway
    {
        //TODO: get and set to data destination e.g. database or textfile
        public Dictionary<ulong, Device> Devices { get; set; }

        public bool Enabled { get; set; }
        public Controller Controller { get; private set; }

        public Gateway()
        {
            Controller = new Controller(this);
        }

        public void Init()
        {
            //TODO: permitJoin
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

        public void PermitJoin(int time)
        {
            if(time < 0)
            {
                throw new ArgumentOutOfRangeException("Time", time, "Value should be greater or equal 0");
            }
            if(Enabled == false)
            {
                throw new Exception("Gateway is not enabled");
            }

            Controller.PermitJoin(time, true);
        }
    }
}
