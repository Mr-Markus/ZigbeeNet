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
    /// Bind Response value object class.
    ///
    ///
    /// The Bind_rsp is generated in response to a Bind_req. If the Bind_req is processed and the
    /// Binding Table entry committed on the Remote Device, a Status of SUCCESS is returned. If
    /// the Remote Device is not a Primary binding table cache or the SrcAddress, a Status of
    /// NOT_SUPPORTED is returned. The supplied endpoint shall be checked to determine
    /// whether it falls within the specified range. If it does not, a Status of INVALID_EP shall
    /// be returned. If the Remote Device is the Primary binding table cache or SrcAddress but
    /// does not have Binding Table resources for the request, a Status of TABLE_FULL is
    /// returned.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class BindResponse : ZdoResponse
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x8021;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BindResponse()
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

            builder.Append("BindResponse [");
            builder.Append(base.ToString());
            builder.Append(", Status=");
            builder.Append(Status);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
