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
        public Dictionary<string, object>  ValObj { get; set; }

        public ZpiObject(SubSystem subSystem, byte commandId, Dictionary<string, object> valObj = null)
        {
            //TODO: Get CommandType by mapping
            SubSystem = subSystem;
            CommandId = commandId;
            ValObj = valObj;
        }

        public void Parse(CommandType type, int length, byte[] payload, Action<string, string> result = null)
        {
            throw new NotImplementedException();
        }
    }
}
