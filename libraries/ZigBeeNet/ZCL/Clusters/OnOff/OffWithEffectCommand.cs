// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.OnOff;


namespace ZigBeeNet.ZCL.Clusters.OnOff
{
    /// <summary>
    /// Off With Effect Command value object class.
    /// <para>
    /// Cluster: On/Off. Command is sent TO the server.
    /// This command is a specific command used for the On/Off cluster.
    ///
    /// The Off With Effect command allows devices to be turned off using enhanced ways of fading.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class OffWithEffectCommand : ZclCommand
    {
        /// <summary>
        /// Effect Identifier command message field.
        /// </summary>
        public byte EffectIdentifier { get; set; }

        /// <summary>
        /// Effect Variant command message field.
        /// </summary>
        public byte EffectVariant { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public OffWithEffectCommand()
        {
            GenericCommand = false;
            ClusterId = 6;
            CommandId = 64;
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
