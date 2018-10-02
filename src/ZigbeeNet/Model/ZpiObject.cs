using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet
{
    public class ZpiObject
    {
        public virtual SubSystem SubSystem { get; set; }
        public virtual byte CommandId { get; set; }
        public virtual MessageType Type { get; set; }
        public ArgumentCollection Arguments { get; set; }

        public byte[] Frame
        {
            get
            {
                if(this.Arguments == null)
                {
                    throw new ArgumentException("ZpiObject has no ValObj", "ValObj");
                }

                List<byte> result = new List<byte>();

                foreach (var item in Arguments.Arguments)
                {
                    switch(item.DataType)
                    {
                        case DataType.UInt8:
                            result.Add(Convert.ToByte(item.Value));
                            break;
                    }
                }

                return result.ToArray();
            }
        }

        public ZpiObject(SubSystem subSystem, byte commandId, ArgumentCollection valObj = null)
        {
            SubSystem = subSystem;
            CommandId = commandId;
            Arguments = valObj;
        }

        public void Parse(MessageType type, int length, byte[] buffer, Action<string, string> result = null)
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
