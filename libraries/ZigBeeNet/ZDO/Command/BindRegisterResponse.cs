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
    /// Bind Register Response value object class.
    ///
    ///
    /// The Bind_Register_rsp is generated from a primary binding table cache device in
    /// response to a Bind_Register_req and contains the status of the request. This command
    /// shall be unicast to the requesting device.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class BindRegisterResponse : ZdoResponse
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x8023;

        /// <summary>
        /// Binding Table Entries command message field.
        /// </summary>
        public ushort BindingTableEntries { get; set; }

        /// <summary>
        /// Binding Table List command message field.
        /// </summary>
        public List<BindingTable> BindingTableList { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BindRegisterResponse()
        {
            ClusterId = CLUSTER_ID;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(Status, ZclDataType.Get(DataType.ZDO_STATUS));
            serializer.Serialize(BindingTableEntries, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(BindingTableList.Count, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            for (int cnt = 0; cnt < BindingTableList.Count; cnt++)
            {
                serializer.Serialize(BindingTableList[cnt], ZclDataType.Get(DataType.BINDING_TABLE));
            }
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            // Create lists
            BindingTableList = new List<BindingTable>();

            Status = deserializer.Deserialize<ZdoStatus>(ZclDataType.Get(DataType.ZDO_STATUS));
            if (Status != ZdoStatus.SUCCESS)
            {
                // Don't read the full response if we have an error
                return;
            }
            BindingTableEntries = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            ushort? bindingTableListCount = (ushort?) deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            if (bindingTableListCount != null)
            {
                for (int cnt = 0; cnt < bindingTableListCount; cnt++)
                {
                    BindingTableList.Add((BindingTable) deserializer.Deserialize(ZclDataType.Get(DataType.BINDING_TABLE)));
                }
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("BindRegisterResponse [");
            builder.Append(base.ToString());
            builder.Append(", Status=");
            builder.Append(Status);
            builder.Append(", BindingTableEntries=");
            builder.Append(BindingTableEntries);
            builder.Append(", BindingTableList=");
            builder.Append(string.Join(", ", BindingTableList));
            builder.Append(']');

            return builder.ToString();
        }
    }
}
