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
            : this(zpiObject.SubSystem, zpiObject.CommandId)
        {
            Type = zpiObject.Type;
            RequestArguments = zpiObject.RequestArguments;
        }

        public ZpiSREQ()
            : base()
        {
            ResponseArguments = new ArgumentCollection();
        }

        public ZpiSREQ(SubSystem subSystem, MessageType type, byte commandId)
            : this(subSystem, (byte)commandId)
        {
            Type = type;
        }

        public ZpiSREQ(SYS.SysCommand sysCmd)
            : this(SubSystem.SYS, (byte)sysCmd)
        {

        }

        public ZpiSREQ(ZDO.ZdoCommand zdoCmd)
            : this(SubSystem.ZDO, (byte)zdoCmd)
        {

        }

        public ZpiSREQ(AF.AfCommand afCmd)
            : this(SubSystem.AF, (byte)afCmd)
        {

        }

        public ZpiSREQ(APP.AppCommand appCmd)
            : this(SubSystem.APP, (byte)appCmd)
        {

        }

        public ZpiSREQ(MAC.MacCommand macCmd)
            : this(SubSystem.MAC, (byte)macCmd)
        {

        }

        public ZpiSREQ(SAPI.SapiCommand sapiCmd)
            : this(SubSystem.SAPI, (byte)sapiCmd)
        {

        }

        public ZpiSREQ(UTIL.UtilCommand utilCmd)
            : this(SubSystem.UTIL, (byte)utilCmd)
        {

        }

        public ZpiSREQ(DBG.DbgCommand dbgCmd)
            : this(SubSystem.DBG, (byte)dbgCmd)
        {

        }

        public ZpiSREQ(DEBUG.DebugCommand debugCmd)
            : this(SubSystem.DEBUG, (byte)debugCmd)
        {

        }

        public ZpiSREQ(SubSystem subSystem, byte cmdId)
        {
            SubSystem = subSystem;
            CommandId = cmdId;

            ZpiObject zpi = ZpiMeta.GetCommand(subSystem, cmdId);

            if (zpi != null)
            {
                this.Type = zpi.Type;
                this.Name = zpi.Name;
                this.RequestArguments = zpi.RequestArguments;
                if (zpi is ZpiSREQ)
                {
                    this.ResponseArguments = ((ZpiSREQ)zpi).ResponseArguments;
                }
            }
        }

        public async override void RequestAsync(IHardwareChannel znp)
        {
            byte[] data = await this.ToSerialPacket().ToFrame().ConfigureAwait(false);
            await znp.SendAsync(data).ConfigureAwait(false);
        }

        public override void Parse(MessageType type, int length, byte[] buffer)
        {
            this.Type = (MessageType)type;

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

                Parsed(this);
            }
            else
            {
                base.ParseArguments(RequestArguments, length, buffer);

                Parsed(this);
            }
        }
    }
}
