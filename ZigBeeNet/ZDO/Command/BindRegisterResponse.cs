using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZDO.Field;

namespace ZigBeeNet.ZDO.Command
{
    /**
    * Bind Register Response value object class.
    * 
    * The Bind_Register_rsp is generated from a primary binding table cache device in
    * response to a Bind_Register_req and contains the status of the request. This
    * command shall be unicast to the requesting device.
    */
    public class BindRegisterResponse : ZdoResponse
    {
        /**
         * BindingTableEntries command message field.
         */
        public int BindingTableEntries { get; set; }

        /**
         * BindingTableList command message field.
         */
        public List<List<BindingTable>> BindingTableList { get; set; }

        /**
         * Default constructor.
         */
        public BindRegisterResponse()
        {
            ClusterId = 0x8023;
        }


        public override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(Status, ZclDataType.Get(DataType.ZDO_STATUS));
            serializer.Serialize(BindingTableEntries, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(BindingTableList.Count, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));

            for (int cnt = 0; cnt < BindingTableList.Count; cnt++)
            {
                serializer.Serialize(BindingTableList[cnt], ZclDataType.Get(DataType.N_X_BINDING_TABLE));
            }
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            // Create lists
            BindingTableList = new List<List<BindingTable>>();

            Status = (ZdoStatus)deserializer.Deserialize(ZclDataType.Get(DataType.ZDO_STATUS));

            if (Status != ZdoStatus.SUCCESS)
            {
                // Don't read the full response if we have an error
                return;
            }

            BindingTableEntries = (int)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            int? bindingTableListCount = (int?)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));

            if (bindingTableListCount != null)
            {
                for (int cnt = 0; cnt < bindingTableListCount; cnt++)
                {
                    BindingTableList.Add((List<BindingTable>)deserializer.Deserialize(ZclDataType.Get(DataType.N_X_BINDING_TABLE)));
                }
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("BindRegisterResponse [")
                   .Append(base.ToString())
                   .Append(", status=")
                   .Append(Status)
                   .Append(", bindingTableEntries=")
                   .Append(BindingTableEntries)
                   .Append(", bindingTableList=")
                   .Append(BindingTableList)
                   .Append(']');

            return builder.ToString();
        }

    }
}