// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.RSSILocation;


namespace ZigBeeNet.ZCL.Clusters.RSSILocation
{
    /// <summary>
    /// Set Absolute Location Command value object class.
    /// <para>
    /// Cluster: RSSI Location. Command is sent TO the server.
    /// This command is a specific command used for the RSSI Location cluster.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class SetAbsoluteLocationCommand : ZclCommand
    {
        /// <summary>
        /// Coordinate 1 command message field.
        /// </summary>
        public short Coordinate1 { get; set; }

        /// <summary>
        /// Coordinate 2 command message field.
        /// </summary>
        public short Coordinate2 { get; set; }

        /// <summary>
        /// Coordinate 3 command message field.
        /// </summary>
        public short Coordinate3 { get; set; }

        /// <summary>
        /// Power command message field.
        /// </summary>
        public short Power { get; set; }

        /// <summary>
        /// Path Loss Exponent command message field.
        /// </summary>
        public ushort PathLossExponent { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public SetAbsoluteLocationCommand()
        {
            GenericCommand = false;
            ClusterId = 11;
            CommandId = 0;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Coordinate1, ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            serializer.Serialize(Coordinate2, ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            serializer.Serialize(Coordinate3, ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            serializer.Serialize(Power, ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            serializer.Serialize(PathLossExponent, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Coordinate1 = deserializer.Deserialize<short>(ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            Coordinate2 = deserializer.Deserialize<short>(ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            Coordinate3 = deserializer.Deserialize<short>(ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            Power = deserializer.Deserialize<short>(ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER));
            PathLossExponent = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("SetAbsoluteLocationCommand [");
            builder.Append(base.ToString());
            builder.Append(", Coordinate1=");
            builder.Append(Coordinate1);
            builder.Append(", Coordinate2=");
            builder.Append(Coordinate2);
            builder.Append(", Coordinate3=");
            builder.Append(Coordinate3);
            builder.Append(", Power=");
            builder.Append(Power);
            builder.Append(", PathLossExponent=");
            builder.Append(PathLossExponent);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
