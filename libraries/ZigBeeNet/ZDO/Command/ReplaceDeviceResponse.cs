using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Replace Device Response value object class.
    /// 
    /// The Replace_Device_rsp is generated from a primary binding table cache device
    /// in response to a Replace_Device_req and contains the Status of the request. This
    /// command shall be unicast to the requesting device. If the device receiving the
    /// Replace_Device_req is not a primary binding table cache, a Status of
    /// NOT_SUPPORTED is returned. The primary binding table cache shall search its
    /// binding table for entries whose source address and source endpoint, or whose
    /// destination address and destination endpoint match OldAddress and OldEndpoint,
    /// as described in the text for Replace_Device_req. It shall change these entries to
    /// have NewAddress and possibly NewEndpoint. It shall then return a response of
    /// SUCCESS.
    /// 
    /// </summary>
    public class ReplaceDeviceResponse : ZdoResponse
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ReplaceDeviceResponse()
        {
            ClusterId = 0x8024;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(Status, ZclDataType.Get(DataType.ZDO_STATUS));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            Status = (ZdoStatus)deserializer.Deserialize(ZclDataType.Get(DataType.ZDO_STATUS));
            if (Status != ZdoStatus.SUCCESS)
            {
                // Don't read the full response if we have an error
                return;
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("ReplaceDeviceResponse [")
                   .Append(base.ToString())
                   .Append(", Status=")
                   .Append(Status)
                   .Append(']');

            return builder.ToString();
        }
    }
}
