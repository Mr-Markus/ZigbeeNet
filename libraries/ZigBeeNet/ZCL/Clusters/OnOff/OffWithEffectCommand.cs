using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.OnOff;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.OnOff
{
    /// <summary>
    /// Off With Effect Command value object class.
    ///
    /// Cluster: On/Off. Command ID 0x40 is sent TO the server.
    /// This command is a specific command used for the On/Off cluster.
    ///
    /// The Off With Effect command allows devices to be turned off using enhanced ways of
    /// fading.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class OffWithEffectCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0006;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x40;

        /// <summary>
        /// Effect Identifier command message field.
        /// 
        /// The Effect Identifier field is 8-bits in length and specifies the fading effect to
        /// use when switching the device off.
        /// </summary>
        public byte EffectIdentifier { get; set; }

        /// <summary>
        /// Effect Variant command message field.
        /// 
        /// The Effect Variant field is 8-bits in length and is used to indicate which variant of
        /// the effect, indicated in the Effect Identifier field, should be triggered. If a
        /// device does not support the given variant, it shall use the default variant. This
        /// field is dependent on the value of the Effect Identifier field.
        /// </summary>
        public byte EffectVariant { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public OffWithEffectCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(EffectIdentifier, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(EffectVariant, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            EffectIdentifier = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            EffectVariant = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("OffWithEffectCommand [");
            builder.Append(base.ToString());
            builder.Append(", EffectIdentifier=");
            builder.Append(EffectIdentifier);
            builder.Append(", EffectVariant=");
            builder.Append(EffectVariant);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
