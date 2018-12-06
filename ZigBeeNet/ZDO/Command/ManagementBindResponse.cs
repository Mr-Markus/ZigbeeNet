﻿using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZDO;
using ZigBeeNet.ZDO.Field;

namespace ZigBeeNet.ZDO.Command
{
    /**
     * Management Bind Response value object class.
     * 
     * The Mgmt_Bind_rsp is generated in response to a Mgmt_Bind_req. If this
     * management command is not supported, a status of NOT_SUPPORTED shall be
     * returned and all parameter fields after the Status field shall be omitted. Otherwise,
     * the Remote Device shall implement the following processing.
     */
    public class ManagementBindResponse : ZdoResponse
    {
        /**
         * BindingTableEntries command message field.
         */
        public int BindingTableEntries { get; set; }

        /**
         * StartIndex command message field.
         */
        public int StartIndex { get; set; }

        /**
         * BindingTableList command message field.
         */
        public List<BindingTable> BindingTableList { get; set; }

        /**
         * Default constructor.
         */
        public ManagementBindResponse()
        {
            ClusterId = 0x8033;
        }


        public override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(Status, ZclDataType.Get(DataType.ZDO_STATUS));
            serializer.Serialize(BindingTableEntries, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(StartIndex, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(BindingTableList.Count, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            for (int cnt = 0; cnt < BindingTableList.Count; cnt++)
            {
                serializer.Serialize(BindingTableList[cnt], ZclDataType.Get(DataType.BINDING_TABLE));
            }
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            // Create lists
            BindingTableList = new List<BindingTable>();

            Status = (ZdoStatus)deserializer.Deserialize(ZclDataType.Get(DataType.ZDO_STATUS));
            if (Status != ZdoStatus.SUCCESS)
            {
                // Don't read the full response if we have an error
                return;
            }

            BindingTableEntries = (int)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            StartIndex = (int)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            int? bindingTableListCount = (int?)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));

            if (bindingTableListCount != null)
            {
                for (int cnt = 0; cnt < bindingTableListCount; cnt++)
                {
                    BindingTableList.Add((BindingTable)deserializer.Deserialize(ZclDataType.Get(DataType.BINDING_TABLE)));
                }
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("ManagementBindResponse [")
                   .Append(base.ToString())
                   .Append(", status=")
                   .Append(Status)
                   .Append(", bindingTableEntries=")
                   .Append(BindingTableEntries)
                   .Append(", startIndex=")
                   .Append(StartIndex)
                   .Append(", bindingTableList=")
                   .Append(BindingTableList)
                   .Append(']');

            return builder.ToString();
        }

    }
}
