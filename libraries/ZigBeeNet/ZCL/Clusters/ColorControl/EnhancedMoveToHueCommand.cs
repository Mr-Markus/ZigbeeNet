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
    /// Enhanced Move To Hue Command value object class.
    ///
    /// Cluster: Color Control. Command ID 0x40 is sent TO the server.
    /// This command is a specific command used for the Color Control cluster.
    ///
    /// The Enhanced Move to Hue command allows lamps to be moved in a smooth continuous
    /// transition from their current hue to a target hue.
    /// On receipt of this command, a device shall set the ColorMode attribute to 0x00 and set the
    /// EnhancedColorMode attribute to the value 0x03. The device shall then move from its
    /// current enhanced hue to the value given in the Enhanced Hue field.
    /// The movement shall be continuous, i.e., not a step function, and the time taken to move to
    /// the new en- hanced hue shall be equal to the Transition Time field.
    ///     ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class EnhancedMoveToHueCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0300;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x40;

        /// <summary>
        /// Enhanced Hue command message field.
        /// </summary>
        public ushort EnhancedHue { get; set; }

        /// <summary>
        /// Direction command message field.
        /// </summary>
        public byte Direction { get; set; }

        /// <summary>
        /// Transition Time command message field.
        /// </summary>
        public ushort TransitionTime { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public EnhancedMoveToHueCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(EnhancedHue, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(Direction, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(TransitionTime, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            EnhancedHue = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            Direction = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            TransitionTime = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("EnhancedMoveToHueCommand [");
            builder.Append(base.ToString());
            builder.Append(", EnhancedHue=");
            builder.Append(EnhancedHue);
            builder.Append(", Direction=");
            builder.Append(Direction);
            builder.Append(", TransitionTime=");
            builder.Append(TransitionTime);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
