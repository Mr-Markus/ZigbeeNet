// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.IASWD;


namespace ZigBeeNet.ZCL.Clusters.IASWD
{
    /// <summary>
    /// Start Warning Command value object class.
    /// <para>
    /// Cluster: IAS WD. Command is sent TO the server.
    /// This command is a specific command used for the IAS WD cluster.
    ///
    /// This command starts the WD operation. The WD alerts the surrounding area by
    /// audible (siren) and visual (strobe) signals.
    /// <br>
    /// A Start Warning command shall always terminate the effect of any previous
    /// command that is still current.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class StartWarningCommand : ZclCommand
    {
        /// <summary>
        /// Header command message field.
        /// </summary>
        public byte Header { get; set; }

        /// <summary>
        /// Warning duration command message field.
        /// </summary>
        public ushort WarningDuration { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public StartWarningCommand()
        {
            GenericCommand = false;
            ClusterId = 1282;
            CommandId = 0;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(Header, ZclDataType.Get(DataType.DATA_8_BIT));
            serializer.Serialize(WarningDuration, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            Header = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.DATA_8_BIT));
            WarningDuration = deserializer.Deserialize<ushort>(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("StartWarningCommand [");
            builder.Append(base.ToString());
            builder.Append(", Header=");
            builder.Append(Header);
            builder.Append(", WarningDuration=");
            builder.Append(WarningDuration);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
