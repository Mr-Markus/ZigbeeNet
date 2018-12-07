using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL;

namespace ZigBeeNet.ZDO
{
    public class ZdoCommand : ZigBeeCommand
    {
        public override void Serialize(ZclFieldSerializer serializer)
        {
            
        }

        public override void Deserialize(ZclFieldDeserializer serializer)
        {
            base.Deserialize(serializer);
        }
    }
}
