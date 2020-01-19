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
    /// Replace Device Response value object class.
    ///
    ///
    /// The Replace_Device_rsp is generated from a primary binding table cache device in
    /// response to a Replace_Device_req and contains the status of the request. This command
    /// shall be unicast to the requesting device. If the device receiving the
    /// Replace_Device_req is not a primary binding table cache, a Status of NOT_SUPPORTED is
    /// returned. The primary binding table cache shall search its binding table for entries
    /// whose source address and source endpoint, or whose destination address and
    /// destination endpoint match OldAddress and OldEndpoint, as described in the text for
    /// Replace_Device_req. It shall change these entries to have NewAddress and possibly
    /// NewEndpoint. It shall then return a response of SUCCESS.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ReplaceDeviceResponse : ZdoResponse
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x8024;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ReplaceDeviceResponse()
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

            builder.Append("ReplaceDeviceResponse [");
            builder.Append(base.ToString());
            builder.Append(", Status=");
            builder.Append(Status);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
