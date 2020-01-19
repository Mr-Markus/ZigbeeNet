using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZDO.Field;


namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Recover Source Bind Request value object class.
    ///
    ///
    /// The Recover_Source_Bind_req is generated from a local primary binding table cache and
    /// sent to the remote backup binding table cache device when it wants a complete restore of
    /// the source binding table. The destination addressing mode for this request is unicast.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class RecoverSourceBindRequest : ZdoRequest
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x002A;

        /// <summary>
        /// Start Index command message field.
        /// </summary>
        public byte StartIndex { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public RecoverSourceBindRequest()
        {
            ClusterId = CLUSTER_ID;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(StartIndex, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            StartIndex = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("RecoverSourceBindRequest [");
            builder.Append(base.ToString());
            builder.Append(", StartIndex=");
            builder.Append(StartIndex);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
