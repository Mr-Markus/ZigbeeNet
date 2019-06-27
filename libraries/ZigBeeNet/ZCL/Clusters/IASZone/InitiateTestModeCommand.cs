// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.IASZone;


namespace ZigBeeNet.ZCL.Clusters.IASZone
{
    /// <summary>
    /// Initiate Test Mode Command value object class.
    /// <para>
    /// Cluster: IAS Zone. Command is sent TO the server.
    /// This command is a specific command used for the IAS Zone cluster.
    ///
    /// Certain IAS Zone servers MAY have operational configurations that could be configured OTA or locally on the device. This command enables
    /// them to be remotely placed into a test mode so that the user or installer MAY configure their field of view, sensitivity, and other
    /// operational parameters. They MAY also verify the placement and proper operation of the IAS Zone server, which MAY have been placed in a
    /// difficult to reach location (i.e., making a physical input on the device impractical to trigger).
    /// <br>
    /// Another use case for this command is large deployments, especially commercial and industrial, where placing the entire IAS system into
    /// test mode instead of a single IAS Zone server is infeasible due to the vulnerabilities that might arise. This command enables only a single
    /// IAS Zone server to be placed into test mode.
    /// <br>
    /// The biggest limitation of this command is that most IAS Zone servers today are battery-powered sleepy nodes that cannot reliably receive
    /// commands. However, implementers MAY decide to program an IAS Zone server by factory default to maintain a limited duration of normal
    /// polling upon initialization/joining to a new network. Some IAS Zone servers MAY also have AC mains power and are able to receive commands.
    /// Some types of IAS Zone servers that MAY benefit from this command are: motion sensors and fire sensor/smoke alarm listeners (i.e., a device
    /// that listens for a non-communicating fire sensor to alarm and communicates this to the IAS CIE).
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class InitiateTestModeCommand : ZclCommand
    {
        /// <summary>
        /// Test Mode Duration command message field.
        /// </summary>
        public byte TestModeDuration { get; set; }

        /// <summary>
        /// Current Zone Sensitivity Level command message field.
        /// </summary>
        public byte CurrentZoneSensitivityLevel { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public InitiateTestModeCommand()
        {
            GenericCommand = false;
            ClusterId = 1280;
            CommandId = 2;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(TestModeDuration, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(CurrentZoneSensitivityLevel, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            TestModeDuration = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            CurrentZoneSensitivityLevel = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("InitiateTestModeCommand [");
            builder.Append(base.ToString());
            builder.Append(", TestModeDuration=");
            builder.Append(TestModeDuration);
            builder.Append(", CurrentZoneSensitivityLevel=");
            builder.Append(CurrentZoneSensitivityLevel);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
