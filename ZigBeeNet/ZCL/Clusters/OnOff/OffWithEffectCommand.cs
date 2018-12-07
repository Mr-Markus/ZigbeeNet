using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.OnOff
{
    /**
     * Off With Effect Command value object class.
     * <p>
     * Cluster: <b>On/Off</b>. Command is sent <b>TO</b> the server.
     * This command is a <b>specific</b> command used for the On/Off cluster.
     * <p>
     * The Off With Effect command allows devices to be turned off using enhanced ways of fading.
     * <p>
     * Code is auto-generated. Modifications may be overwritten!
     */
    public class OffWithEffectCommand : ZclCommand
    {
        /**
         * Effect Identifier command message field.
         */
        public byte EffectIdentifier { get; set; }

        /**
         * Effect Variant command message field.
         */
        public byte EffectVariant { get; set; }

        /**
         * Default constructor.
         */
        public OffWithEffectCommand()
        {
            GenericCommand = false;
            ClusterId = 6;
            CommandId = 64;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(EffectIdentifier, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(EffectVariant, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            EffectIdentifier = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            EffectVariant = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public String toString()
        {
            StringBuilder builder = new StringBuilder()
                .Append("OffWithEffectCommand [")
                .Append(base.ToString())
                .Append(", effectIdentifier=")
                .Append(EffectIdentifier)
                .Append(", effectVariant=")
                .Append(EffectVariant)
                .Append(']');

            return builder.ToString();
        }
    }
}
