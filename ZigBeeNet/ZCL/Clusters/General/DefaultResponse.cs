using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.General
{
    public class DefaultResponse : ZclCommand
    {
    /**
     * Command identifier command message field.
     */
    public byte CommandIdentifier { get; set; }

    /**
     * Status code command message field.
     */
    public ZclStatus StatusCode { get; set; }

    /**
     * Default constructor.
     */
    public DefaultResponse()
    {
        GenericCommand = true;
        CommandId = 11;
        CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
    }


    public override void Serialize(ZclFieldSerializer serializer)
    {
        serializer.Serialize(CommandIdentifier, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        serializer.Serialize(StatusCode, ZclDataType.Get(DataType.ZCL_STATUS));
    }

    public override void Deserialize(ZclFieldDeserializer deserializer)
    {
        CommandIdentifier = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        StatusCode = (ZclStatus)deserializer.Deserialize(ZclDataType.Get(DataType.ZCL_STATUS));
    }

    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("DefaultResponse [")
               .Append(base.ToString())
               .Append(", commandIdentifier=")
               .Append(CommandIdentifier)
               .Append(", statusCode=")
               .Append(StatusCode)
               .Append(']');

        return builder.ToString();
    }

}
}
