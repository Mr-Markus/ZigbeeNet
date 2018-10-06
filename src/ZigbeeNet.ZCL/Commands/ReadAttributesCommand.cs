using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.ZCL.Commands
{
    public class ReadAttributesCommand : ZclCommand
    {
        public ReadAttributesCommand(params byte[] attrToRead)
        {
            if(attrToRead != null)
            {
                foreach (byte attr in attrToRead)
                {
                    ZclCommandParam param = new ZclCommandParam()
                    {
                        DataType = DataType.UInt16,
                        Value = attr
                    };
                    this.Params.Add(param);
                }
            }
        }
    }
}
