using System;
using System.Collections.Generic;
using System.Text;
using ZigbeeNet.ZCL;

namespace ZigbeeNet.ZDO
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
