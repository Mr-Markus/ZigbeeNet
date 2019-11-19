using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.IASACE;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.IASACE
{
    /// <summary>
    /// Get Zone ID Map Response value object class.
    ///
    /// Cluster: IAS ACE. Command ID 0x01 is sent FROM the server.
    /// This command is a specific command used for the IAS ACE cluster.
    ///
    /// The 16 fields of the payload indicate whether each of the Zone IDs from 0x00 to 0xff is
    /// allocated or not. If bit n of Zone ID Map section N is set to 1, then Zone ID (16 x N + n ) is
    /// allocated, else it is not allocated.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetZoneIdMapResponse : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0501;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x01;

        /// <summary>
        /// Zone ID Map Section 0 command message field.
        /// </summary>
        public ushort ZoneIdMapSection0 { get; set; }

        /// <summary>
        /// Zone ID Map Section 1 command message field.
        /// </summary>
        public ushort ZoneIdMapSection1 { get; set; }

        /// <summary>
        /// Zone ID Map Section 2 command message field.
        /// </summary>
        public ushort ZoneIdMapSection2 { get; set; }

        /// <summary>
        /// Zone ID Map Section 3 command message field.
        /// </summary>
        public ushort ZoneIdMapSection3 { get; set; }

        /// <summary>
        /// Zone ID Map Section 4 command message field.
        /// </summary>
        public ushort ZoneIdMapSection4 { get; set; }

        /// <summary>
        /// Zone ID Map Section 5 command message field.
        /// </summary>
        public ushort ZoneIdMapSection5 { get; set; }

        /// <summary>
        /// Zone ID Map Section 6 command message field.
        /// </summary>
        public ushort ZoneIdMapSection6 { get; set; }

        /// <summary>
        /// Zone ID Map Section 7 command message field.
        /// </summary>
        public ushort ZoneIdMapSection7 { get; set; }

        /// <summary>
        /// Zone ID Map Section 8 command message field.
        /// </summary>
        public ushort ZoneIdMapSection8 { get; set; }

        /// <summary>
        /// Zone ID Map Section 9 command message field.
        /// </summary>
        public ushort ZoneIdMapSection9 { get; set; }

        /// <summary>
        /// Zone ID Map Section 10 command message field.
        /// </summary>
        public ushort ZoneIdMapSection10 { get; set; }

        /// <summary>
        /// Zone ID Map Section 11 command message field.
        /// </summary>
        public ushort ZoneIdMapSection11 { get; set; }

        /// <summary>
        /// Zone ID Map Section 12 command message field.
        /// </summary>
        public ushort ZoneIdMapSection12 { get; set; }

        /// <summary>
        /// Zone ID Map Section 13 command message field.
        /// </summary>
        public ushort ZoneIdMapSection13 { get; set; }

        /// <summary>
        /// Zone ID Map Section 14 command message field.
        /// </summary>
        public ushort ZoneIdMapSection14 { get; set; }

        /// <summary>
        /// Zone ID Map Section 15 command message field.
        /// </summary>
        public ushort ZoneIdMapSection15 { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetZoneIdMapResponse()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(ZoneIdMapSection0, ZclDataType.Get(DataType.BITMAP_16_BIT));
            serializer.Serialize(ZoneIdMapSection1, ZclDataType.Get(DataType.BITMAP_16_BIT));
            serializer.Serialize(ZoneIdMapSection2, ZclDataType.Get(DataType.BITMAP_16_BIT));
            serializer.Serialize(ZoneIdMapSection3, ZclDataType.Get(DataType.BITMAP_16_BIT));
            serializer.Serialize(ZoneIdMapSection4, ZclDataType.Get(DataType.BITMAP_16_BIT));
            serializer.Serialize(ZoneIdMapSection5, ZclDataType.Get(DataType.BITMAP_16_BIT));
            serializer.Serialize(ZoneIdMapSection6, ZclDataType.Get(DataType.BITMAP_16_BIT));
            serializer.Serialize(ZoneIdMapSection7, ZclDataType.Get(DataType.BITMAP_16_BIT));
            serializer.Serialize(ZoneIdMapSection8, ZclDataType.Get(DataType.BITMAP_16_BIT));
            serializer.Serialize(ZoneIdMapSection9, ZclDataType.Get(DataType.BITMAP_16_BIT));
            serializer.Serialize(ZoneIdMapSection10, ZclDataType.Get(DataType.BITMAP_16_BIT));
            serializer.Serialize(ZoneIdMapSection11, ZclDataType.Get(DataType.BITMAP_16_BIT));
            serializer.Serialize(ZoneIdMapSection12, ZclDataType.Get(DataType.BITMAP_16_BIT));
            serializer.Serialize(ZoneIdMapSection13, ZclDataType.Get(DataType.BITMAP_16_BIT));
            serializer.Serialize(ZoneIdMapSection14, ZclDataType.Get(DataType.BITMAP_16_BIT));
            serializer.Serialize(ZoneIdMapSection15, ZclDataType.Get(DataType.BITMAP_16_BIT));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            ZoneIdMapSection0 = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
            ZoneIdMapSection1 = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
            ZoneIdMapSection2 = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
            ZoneIdMapSection3 = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
            ZoneIdMapSection4 = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
            ZoneIdMapSection5 = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
            ZoneIdMapSection6 = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
            ZoneIdMapSection7 = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
            ZoneIdMapSection8 = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
            ZoneIdMapSection9 = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
            ZoneIdMapSection10 = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
            ZoneIdMapSection11 = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
            ZoneIdMapSection12 = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
            ZoneIdMapSection13 = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
            ZoneIdMapSection14 = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
            ZoneIdMapSection15 = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetZoneIdMapResponse [");
            builder.Append(base.ToString());
            builder.Append(", ZoneIdMapSection0=");
            builder.Append(ZoneIdMapSection0);
            builder.Append(", ZoneIdMapSection1=");
            builder.Append(ZoneIdMapSection1);
            builder.Append(", ZoneIdMapSection2=");
            builder.Append(ZoneIdMapSection2);
            builder.Append(", ZoneIdMapSection3=");
            builder.Append(ZoneIdMapSection3);
            builder.Append(", ZoneIdMapSection4=");
            builder.Append(ZoneIdMapSection4);
            builder.Append(", ZoneIdMapSection5=");
            builder.Append(ZoneIdMapSection5);
            builder.Append(", ZoneIdMapSection6=");
            builder.Append(ZoneIdMapSection6);
            builder.Append(", ZoneIdMapSection7=");
            builder.Append(ZoneIdMapSection7);
            builder.Append(", ZoneIdMapSection8=");
            builder.Append(ZoneIdMapSection8);
            builder.Append(", ZoneIdMapSection9=");
            builder.Append(ZoneIdMapSection9);
            builder.Append(", ZoneIdMapSection10=");
            builder.Append(ZoneIdMapSection10);
            builder.Append(", ZoneIdMapSection11=");
            builder.Append(ZoneIdMapSection11);
            builder.Append(", ZoneIdMapSection12=");
            builder.Append(ZoneIdMapSection12);
            builder.Append(", ZoneIdMapSection13=");
            builder.Append(ZoneIdMapSection13);
            builder.Append(", ZoneIdMapSection14=");
            builder.Append(ZoneIdMapSection14);
            builder.Append(", ZoneIdMapSection15=");
            builder.Append(ZoneIdMapSection15);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
