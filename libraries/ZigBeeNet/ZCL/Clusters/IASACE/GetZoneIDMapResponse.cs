// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.IASACE;


namespace ZigBeeNet.ZCL.Clusters.IASACE
{
    /// <summary>
    /// Get Zone ID Map Response value object class.
    /// <para>
    /// Cluster: IAS ACE. Command is sent FROM the server.
    /// This command is a specific command used for the IAS ACE cluster.
    ///
    /// The 16 fields of the payload indicate whether each of the Zone IDs from 0 to 0xff is allocated or not. If bit n
    /// of Zone ID Map section N is set to 1, then Zone ID (16 x N + n ) is allocated, else it is not allocated
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetZoneIDMapResponse : ZclCommand
    {
        /// <summary>
        /// Zone ID Map section 0 command message field.
        /// </summary>
        public ushort ZoneIDMapSection0 { get; set; }

        /// <summary>
        /// Zone ID Map section 1 command message field.
        /// </summary>
        public ushort ZoneIDMapSection1 { get; set; }

        /// <summary>
        /// Zone ID Map section 2 command message field.
        /// </summary>
        public ushort ZoneIDMapSection2 { get; set; }

        /// <summary>
        /// Zone ID Map section 3 command message field.
        /// </summary>
        public ushort ZoneIDMapSection3 { get; set; }

        /// <summary>
        /// Zone ID Map section 4 command message field.
        /// </summary>
        public ushort ZoneIDMapSection4 { get; set; }

        /// <summary>
        /// Zone ID Map section 5 command message field.
        /// </summary>
        public ushort ZoneIDMapSection5 { get; set; }

        /// <summary>
        /// Zone ID Map section 6 command message field.
        /// </summary>
        public ushort ZoneIDMapSection6 { get; set; }

        /// <summary>
        /// Zone ID Map section 7 command message field.
        /// </summary>
        public ushort ZoneIDMapSection7 { get; set; }

        /// <summary>
        /// Zone ID Map section 8 command message field.
        /// </summary>
        public ushort ZoneIDMapSection8 { get; set; }

        /// <summary>
        /// Zone ID Map section 9 command message field.
        /// </summary>
        public ushort ZoneIDMapSection9 { get; set; }

        /// <summary>
        /// Zone ID Map section 10 command message field.
        /// </summary>
        public ushort ZoneIDMapSection10 { get; set; }

        /// <summary>
        /// Zone ID Map section 11 command message field.
        /// </summary>
        public ushort ZoneIDMapSection11 { get; set; }

        /// <summary>
        /// Zone ID Map section 12 command message field.
        /// </summary>
        public ushort ZoneIDMapSection12 { get; set; }

        /// <summary>
        /// Zone ID Map section 13 command message field.
        /// </summary>
        public ushort ZoneIDMapSection13 { get; set; }

        /// <summary>
        /// Zone ID Map section 14 command message field.
        /// </summary>
        public ushort ZoneIDMapSection14 { get; set; }

        /// <summary>
        /// Zone ID Map section 15 command message field.
        /// </summary>
        public ushort ZoneIDMapSection15 { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetZoneIDMapResponse()
        {
            GenericCommand = false;
            ClusterId = 1281;
            CommandId = 1;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(ZoneIDMapSection0, ZclDataType.Get(DataType.BITMAP_16_BIT));
            serializer.Serialize(ZoneIDMapSection1, ZclDataType.Get(DataType.BITMAP_16_BIT));
            serializer.Serialize(ZoneIDMapSection2, ZclDataType.Get(DataType.BITMAP_16_BIT));
            serializer.Serialize(ZoneIDMapSection3, ZclDataType.Get(DataType.BITMAP_16_BIT));
            serializer.Serialize(ZoneIDMapSection4, ZclDataType.Get(DataType.BITMAP_16_BIT));
            serializer.Serialize(ZoneIDMapSection5, ZclDataType.Get(DataType.BITMAP_16_BIT));
            serializer.Serialize(ZoneIDMapSection6, ZclDataType.Get(DataType.BITMAP_16_BIT));
            serializer.Serialize(ZoneIDMapSection7, ZclDataType.Get(DataType.BITMAP_16_BIT));
            serializer.Serialize(ZoneIDMapSection8, ZclDataType.Get(DataType.BITMAP_16_BIT));
            serializer.Serialize(ZoneIDMapSection9, ZclDataType.Get(DataType.BITMAP_16_BIT));
            serializer.Serialize(ZoneIDMapSection10, ZclDataType.Get(DataType.BITMAP_16_BIT));
            serializer.Serialize(ZoneIDMapSection11, ZclDataType.Get(DataType.BITMAP_16_BIT));
            serializer.Serialize(ZoneIDMapSection12, ZclDataType.Get(DataType.BITMAP_16_BIT));
            serializer.Serialize(ZoneIDMapSection13, ZclDataType.Get(DataType.BITMAP_16_BIT));
            serializer.Serialize(ZoneIDMapSection14, ZclDataType.Get(DataType.BITMAP_16_BIT));
            serializer.Serialize(ZoneIDMapSection15, ZclDataType.Get(DataType.BITMAP_16_BIT));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            ZoneIDMapSection0 = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
            ZoneIDMapSection1 = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
            ZoneIDMapSection2 = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
            ZoneIDMapSection3 = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
            ZoneIDMapSection4 = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
            ZoneIDMapSection5 = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
            ZoneIDMapSection6 = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
            ZoneIDMapSection7 = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
            ZoneIDMapSection8 = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
            ZoneIDMapSection9 = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
            ZoneIDMapSection10 = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
            ZoneIDMapSection11 = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
            ZoneIDMapSection12 = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
            ZoneIDMapSection13 = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
            ZoneIDMapSection14 = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
            ZoneIDMapSection15 = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.BITMAP_16_BIT));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetZoneIDMapResponse [");
            builder.Append(base.ToString());
            builder.Append(", ZoneIDMapSection0=");
            builder.Append(ZoneIDMapSection0);
            builder.Append(", ZoneIDMapSection1=");
            builder.Append(ZoneIDMapSection1);
            builder.Append(", ZoneIDMapSection2=");
            builder.Append(ZoneIDMapSection2);
            builder.Append(", ZoneIDMapSection3=");
            builder.Append(ZoneIDMapSection3);
            builder.Append(", ZoneIDMapSection4=");
            builder.Append(ZoneIDMapSection4);
            builder.Append(", ZoneIDMapSection5=");
            builder.Append(ZoneIDMapSection5);
            builder.Append(", ZoneIDMapSection6=");
            builder.Append(ZoneIDMapSection6);
            builder.Append(", ZoneIDMapSection7=");
            builder.Append(ZoneIDMapSection7);
            builder.Append(", ZoneIDMapSection8=");
            builder.Append(ZoneIDMapSection8);
            builder.Append(", ZoneIDMapSection9=");
            builder.Append(ZoneIDMapSection9);
            builder.Append(", ZoneIDMapSection10=");
            builder.Append(ZoneIDMapSection10);
            builder.Append(", ZoneIDMapSection11=");
            builder.Append(ZoneIDMapSection11);
            builder.Append(", ZoneIDMapSection12=");
            builder.Append(ZoneIDMapSection12);
            builder.Append(", ZoneIDMapSection13=");
            builder.Append(ZoneIDMapSection13);
            builder.Append(", ZoneIDMapSection14=");
            builder.Append(ZoneIDMapSection14);
            builder.Append(", ZoneIDMapSection15=");
            builder.Append(ZoneIDMapSection15);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
