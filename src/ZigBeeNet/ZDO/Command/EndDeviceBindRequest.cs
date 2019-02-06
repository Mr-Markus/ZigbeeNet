using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /**
    * End Device Bind Request value object class.
    * 
    * The End_Device_Bind_req is generated from a Local Device wishing to perform
    * End Device Bind with a Remote Device. The End_Device_Bind_req is generated,
    * typically based on some user action like a button press. The destination addressing
    * on this command shall be unicast, and the destination address shall be that of the
    * ZigBee Coordinator.
    * 
    */
    public class EndDeviceBindRequest : ZdoRequest
    {
        /**
        * BindingTarget command message field.
*/
        public int BindingTarget { get; set; }

        /**
        * SrcAddress command message field.
*/
        public IeeeAddress SrcAddress { get; set; }

        /**
        * SrcEndpoint command message field.
*/
        public int SrcEndpoint { get; set; }

        /**
        * ProfileID command message field.
*/
        public int ProfileId { get; set; }

        /**
        * InClusterList command message field.
*/
        public List<ushort> InClusterList { get; set; }

        /**
        * OutClusterList command message field.
*/
        public List<ushort> OutClusterList { get; set; }

        /**
        * Default constructor.
*/
        public EndDeviceBindRequest()
        {
            ClusterId = 0x0020;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(BindingTarget, ZclDataType.Get(DataType.NWK_ADDRESS));
            serializer.Serialize(SrcAddress, ZclDataType.Get(DataType.IEEE_ADDRESS));
            serializer.Serialize(SrcEndpoint, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(ProfileId, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(InClusterList.Count, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));

            for (int cnt = 0; cnt < InClusterList.Count; cnt++)
            {
                serializer.Serialize(InClusterList[cnt], ZclDataType.Get(DataType.CLUSTERID));
            }

            serializer.Serialize(OutClusterList.Count, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));

            for (int cnt = 0; cnt < OutClusterList.Count; cnt++)
            {
                serializer.Serialize(OutClusterList[cnt], ZclDataType.Get(DataType.CLUSTERID));
            }
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            // Create lists
            InClusterList = new List<ushort>();
            OutClusterList = new List<ushort>();

            BindingTarget = (ushort)deserializer.Deserialize(ZclDataType.Get(DataType.NWK_ADDRESS));
            SrcAddress = (IeeeAddress)deserializer.Deserialize(ZclDataType.Get(DataType.IEEE_ADDRESS));
            SrcEndpoint = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            ProfileId = (ushort)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            byte? inClusterCount = (byte?)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));

            if (inClusterCount != null)
            {
                for (int cnt = 0; cnt < inClusterCount; cnt++)
                {
                    InClusterList.Add((ushort)deserializer.Deserialize(ZclDataType.Get(DataType.CLUSTERID)));
                }
            }

            byte? outClusterCount = (byte?)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));

            if (outClusterCount != null)
            {
                for (int cnt = 0; cnt < outClusterCount; cnt++)
                {
                    OutClusterList.Add((ushort)deserializer.Deserialize(ZclDataType.Get(DataType.CLUSTERID)));
                }
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("EndDeviceBindRequest [")
                   .Append(base.ToString())
                   .Append(", bindingTarget=")
                   .Append(BindingTarget)
                   .Append(", srcAddress=")
                   .Append(SrcAddress)
                   .Append(", srcEndpoint=")
                   .Append(SrcEndpoint)
                   .Append(", profileId=")
                   .Append(ProfileId)
                   .Append(", inClusterList=")
                   .Append(InClusterList)
                   .Append(", outClusterList=")
                   .Append(OutClusterList)
                   .Append(']');

            return builder.ToString();
        }

    }
}
