using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO
{
    public class ZdoCommand : ZigBeeCommand
    {
        internal override void Serialize(ZclFieldSerializer serializer)
        {
            if(TransactionId == null) {
                TransactionId = 0;
            }
            serializer.Serialize(TransactionId, DataType.UNSIGNED_8_BIT_INTEGER);
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            TransactionId = (byte?)deserializer.Deserialize(DataType.UNSIGNED_8_BIT_INTEGER);
        }
    }
}
