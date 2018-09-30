using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.TI
{
    public class ZpiObject
    {
        public SubSystem SubSystem { get; set; }
        public byte CommandId { get; set; }
        public CommandType Type { get; set; }
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
            //TODO: Get CommandType by mapping
            SubSystem = subSystem;
            CommandId = commandId;
            Arguments = valObj;
        }

        public void Parse(CommandType type, int length, byte[] payload, Action<string, string> result = null)
        {
            throw new NotImplementedException();
        }
    }
}
