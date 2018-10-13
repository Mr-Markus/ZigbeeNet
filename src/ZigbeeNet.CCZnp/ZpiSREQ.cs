using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC
{
    public class ZpiSREQ : ZpiObject
    {
        public ZpiStatus Status { get; set; }

        public ArgumentCollection ResponseArguments { get; set; }

        public ZpiSREQ(ZpiObject zpiObject)
            :base(zpiObject.SubSystem, zpiObject.CommandId)
        {
            RequestArguments = zpiObject.RequestArguments;
        }

        public ZpiSREQ()
            :base()
        {
            ResponseArguments = new ArgumentCollection();
        }

        public ZpiSREQ(SubSystem subSystem, MessageType type, byte commandId)
            : base(subSystem, (byte)commandId)
        {
            Type = type;
        }

        public ZpiSREQ(SYS sysCmd)
            : base(SubSystem.SYS, (byte)sysCmd)
        {

        }

        public ZpiSREQ(ZDO zdoCmd)
            : base(SubSystem.ZDO, (byte)zdoCmd)
        {

        }

        public ZpiSREQ(AF afCmd)
            : base(SubSystem.AF, (byte)afCmd)
        {

        }

        public ZpiSREQ(APP appCmd)
            : base(SubSystem.APP, (byte)appCmd)
        {

        }

        public ZpiSREQ(MAC macCmd)
            : base(SubSystem.MAC, (byte)macCmd)
        {

        }

        public ZpiSREQ(SAPI sapiCmd)
            : base(SubSystem.SAPI, (byte)sapiCmd)
        {

        }

        public ZpiSREQ(UTIL utilCmd)
            : base(SubSystem.UTIL, (byte)utilCmd)
        {

        }

        public ZpiSREQ(DBG dbgCmd)
            : base(SubSystem.DBG, (byte)dbgCmd)
        {

        }

        public ZpiSREQ(DEBUG debugCmd)
            : base(SubSystem.DEBUG, (byte)debugCmd)
        {

        }

        public override void Parse(MessageType type, int length, byte[] buffer)
        {
            if (type == MessageType.SRSP)
            {
                if(ResponseArguments == null)
                {
                    ResponseArguments = new ArgumentCollection();
                }
                base.ParseArguments(ResponseArguments, length, buffer);

                if (ResponseArguments.Arguments.Exists(a => a.Name == "status")) {
                    Status = (ZpiStatus)ResponseArguments["status"];
                }

                IsParsed = true;

                Parsed(this);
            }
            else
            {
                base.ParseArguments(RequestArguments, length, buffer);

                IsParsed = true;

                Parsed(this);
            }
        }
    }
}
