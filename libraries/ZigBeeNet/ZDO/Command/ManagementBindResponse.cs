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
    /// Management Bind Response value object class.
    ///
    ///
    /// The Mgmt_Bind_rsp is generated in response to a Mgmt_Bind_req. If this management
    /// command is not supported, a status of NOT_SUPPORTED shall be returned and all parameter
    /// fields after the Status field shall be omitted. Otherwise, the Remote Device shall
    /// implement the following processing.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ManagementBindResponse : ZdoResponse
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x8033;

        /// <summary>
        /// Binding Table Entries command message field.
        /// </summary>
        public byte BindingTableEntries { get; set; }

        /// <summary>
        /// Start Index command message field.
        /// </summary>
        public byte StartIndex { get; set; }

        /// <summary>
        /// Binding Table List command message field.
        /// </summary>
        public List<BindingTable> BindingTableList { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ManagementBindResponse()
        {
            ClusterId = CLUSTER_ID;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(Status, DataType.ZDO_STATUS);
            serializer.Serialize(BindingTableEntries, DataType.UNSIGNED_8_BIT_INTEGER);
            serializer.Serialize(StartIndex, DataType.UNSIGNED_8_BIT_INTEGER);
            serializer.Serialize(BindingTableList.Count, DataType.UNSIGNED_8_BIT_INTEGER);
            for (int cnt = 0; cnt < BindingTableList.Count; cnt++)
            {
                serializer.Serialize(BindingTableList[cnt], DataType.BINDING_TABLE);
            }
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            // Create lists
            BindingTableList = new List<BindingTable>();

            Status = deserializer.Deserialize<ZdoStatus>(DataType.ZDO_STATUS);
            if (Status != ZdoStatus.SUCCESS)
            {
                // Don't read the full response if we have an error
                return;
            }
            BindingTableEntries = deserializer.Deserialize<byte>(DataType.UNSIGNED_8_BIT_INTEGER);
            StartIndex = deserializer.Deserialize<byte>(DataType.UNSIGNED_8_BIT_INTEGER);
            byte? bindingTableListCount = (byte?) deserializer.Deserialize(DataType.UNSIGNED_8_BIT_INTEGER);
            if (bindingTableListCount != null)
            {
                for (int cnt = 0; cnt < bindingTableListCount; cnt++)
                {
                    BindingTableList.Add((BindingTable) deserializer.Deserialize(DataType.BINDING_TABLE));
                }
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ManagementBindResponse [");
            builder.Append(base.ToString());
            builder.Append(", Status=");
            builder.Append(Status);
            builder.Append(", BindingTableEntries=");
            builder.Append(BindingTableEntries);
            builder.Append(", StartIndex=");
            builder.Append(StartIndex);
            builder.Append(", BindingTableList=");
            builder.Append(BindingTableList == null? "" : string.Join(", ", BindingTableList));
            builder.Append(']');

            return builder.ToString();
        }
    }
}
