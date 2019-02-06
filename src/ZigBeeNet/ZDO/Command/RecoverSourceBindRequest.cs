using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /**
    * Recover Source Bind Request value object class.
    * 
    * The Recover_Source_Bind_req is generated from a local primary binding table
    * cache and sent to the remote backup binding table cache device when it wants a
    * complete restore of the source binding table. The destination addressing mode for
    * this request is unicast.
    * 
    */
    public class RecoverSourceBindRequest : ZdoRequest
    {
        /**
        * StartIndex command message field.
*/
        public byte StartIndex { get; set; }

        /**
        * Default constructor.
*/
        public RecoverSourceBindRequest()
        {
            ClusterId = 0x002A;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(StartIndex, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            StartIndex = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("RecoverSourceBindRequest [")
                   .Append(base.ToString())
                   .Append(", startIndex=")
                   .Append(StartIndex)
                   .Append(']');

            return builder.ToString();
        }
    }
}
