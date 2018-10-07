using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.ZCL;

namespace ZigbeeNet.CC
{
    public class ZpiObject
    {
        public virtual MessageType Type { get; set; }

        public virtual SubSystem SubSystem { get; set; }

        public virtual byte CommandId { get; set; }

        public string Name { get; set; }

        public ArgumentCollection RequestArguments { get; set; }

        public ArgumentCollection ResponseArguments { get; set; }

        public ZpiObject()
        {
            RequestArguments = new ArgumentCollection();
            ResponseArguments = new ArgumentCollection();
        }

        public ZpiObject(SubSystem subSystem, byte cmdId)
        {
            SubSystem = subSystem;
            CommandId = cmdId;

            ZpiObject zpi = ZpiMeta.GetCommand(subSystem, cmdId);

            this.Type = zpi.Type;
            this.Name = zpi.Name;
            this.RequestArguments = zpi.RequestArguments;
            this.ResponseArguments = zpi.ResponseArguments;
        }

        public byte[] Frame
        {
            get
            {
                if (this.RequestArguments == null)
                {
                    throw new ArgumentException("ZpiObject has no ValObj", "ValObj");
                }

                List<byte> result = new List<byte>();

                foreach (var item in RequestArguments.Arguments)
                {
                    switch (item.ParamType)
                    {
                        case ParamType.uint8:
                            result.Add(Convert.ToByte(item.Value));
                            break;
                    }
                }

                return result.ToArray();
            }
        }


        public void Parse(MessageType type, int length, byte[] buffer, Action<string, ArgumentCollection> result = null)
        {
            ArgumentCollection arguments = new ArgumentCollection();

            if(type == MessageType.SRSP)
            {
                //TODO: GetRspParams(subsys, cmd)
            } else if (type == MessageType.AREQ)
            {
                //TODO: GetReqParams(subsys, cmd)
            }

            foreach (ZpiArgument argument in arguments.Arguments)
            {

            }
        }
    }
}
