using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /**
    * End Device Bind Response value object class.
    * 
    * The End_Device_Bind_rsp is generated by the ZigBee Coordinator in response to
    * an End_Device_Bind_req and contains the status of the request. This command
    * shall be unicast to each device involved in the bind attempt, using the
    * acknowledged data service.
    * 
    */
    public class EndDeviceBindResponse : ZdoResponse
    {
        /**
        * Default constructor.
        */
        public EndDeviceBindResponse()
        {
            ClusterId = 0x8020;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(Status, ZclDataType.Get(DataType.ZDO_STATUS));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
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

            builder.Append("EndDeviceBindResponse [")
                   .Append(base.ToString())
                   .Append(", status=")
                   .Append(Status)
                   .Append(']');

            return builder.ToString();
        }

    }
}