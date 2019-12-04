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
    /// Unbind Response value object class.
    ///
    ///
    /// The Unbind_rsp is generated in response to an Unbind_req. If the Unbind_req is
    /// processed and the corresponding Binding Table entry is removed from the Remote Device,
    /// a Status of SUCCESS is returned. If the Remote Device is not the ZigBee Coordinator or the
    /// SrcAddress, a Status of NOT_SUPPORTED is returned. The supplied endpoint shall be
    /// checked to determine whether it falls within the specified range. If it does not, a
    /// Status of INVALID_EP shall be returned If the Remote Device is the ZigBee Coordinator or
    /// SrcAddress but does not have a Binding Table entry corresponding to the parameters
    /// received in the request, a Status of NO_ENTRY is returned.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class UnbindResponse : ZdoResponse
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x8022;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public UnbindResponse()
        {
            ClusterId = CLUSTER_ID;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(Status, ZclDataType.Get(DataType.ZDO_STATUS));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            Status = deserializer.Deserialize<ZdoStatus>(ZclDataType.Get(DataType.ZDO_STATUS));
            if (Status != ZdoStatus.SUCCESS)
            {
                // Don't read the full response if we have an error
                return;
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("UnbindResponse [");
            builder.Append(base.ToString());
            builder.Append(", Status=");
            builder.Append(Status);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
