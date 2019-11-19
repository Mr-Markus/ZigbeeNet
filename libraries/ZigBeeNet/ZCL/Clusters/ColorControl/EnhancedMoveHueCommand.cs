using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.ColorControl;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.ColorControl
{
    /// <summary>
    /// Enhanced Move Hue Command value object class.
    ///
    /// Cluster: Color Control. Command ID 0x41 is sent TO the server.
    /// This command is a specific command used for the Color Control cluster.
    ///
    /// The Enhanced Move to Hue command allows lamps to be moved in a smooth continuous
    /// transition from their current hue to a target hue.
    /// On receipt of this command, a device shall set the ColorMode attribute to 0x00 and set the
    /// EnhancedColorMode attribute to the value 0x03. The device shall then move from its
    /// current enhanced hue in an up or down direction in a continuous fashion.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class EnhancedMoveHueCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0300;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x41;

        /// <summary>
        /// Move Mode command message field.
        /// </summary>
        public byte MoveMode { get; set; }

        /// <summary>
        /// Rate command message field.
        /// </summary>
        public ushort Rate { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public EnhancedMoveHueCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(MoveMode, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(Rate, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            MoveMode = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            Rate = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("EnhancedMoveHueCommand [");
            builder.Append(base.ToString());
            builder.Append(", MoveMode=");
            builder.Append(MoveMode);
            builder.Append(", Rate=");
            builder.Append(Rate);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
