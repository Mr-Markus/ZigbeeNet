using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.Commands
{
    public class BaseCommand : ZpiObject
    {
        public BaseCommand(SubSystem subSystem, byte commandId, ArgumentCollection valObj = null) 
            : base(subSystem, commandId, valObj)
        {
        }
    }
}
