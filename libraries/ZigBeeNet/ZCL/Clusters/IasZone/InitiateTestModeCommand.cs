using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.IASZone;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.IASZone
{
    /// <summary>
    /// Initiate Test Mode Command value object class.
    ///
    /// Cluster: IAS Zone. Command ID 0x02 is sent TO the server.
    /// This command is a specific command used for the IAS Zone cluster.
    ///
    /// Certain IAS Zone servers may have operational configurations that could be configured
    /// OTA or locally on the device. This command enables them to be remotely placed into a test
    /// mode so that the user or installer may configure their field of view, sensitivity, and
    /// other operational parameters. They may also verify the placement and proper operation
    /// of the IAS Zone server, which may have been placed in a difficult to reach location (i.e.,
    /// making a physical input on the device impractical to trigger). <br> Another use case for
    /// this command is large deployments, especially commercial and industrial, where
    /// placing the entire IAS system into test mode instead of a single IAS Zone server is
    /// infeasible due to the vulnerabilities that might arise. This command enables only a
    /// single IAS Zone server to be placed into test mode. <br> The biggest limitation of this
    /// command is that most IAS Zone servers today are battery-powered sleepy nodes that
    /// cannot reliably receive commands. However, implementers may decide to program an IAS
    /// Zone server by factory default to maintain a limited duration of normal polling upon
    /// initialization/joining to a new network. Some IAS Zone servers may also have AC mains
    /// power and are able to receive commands. Some types of IAS Zone servers that may benefit
    /// from this command are: motion sensors and fire sensor/smoke alarm listeners (i.e., a
    /// device that listens for a non-communicating fire sensor to alarm and communicates this
    /// to the IAS CIE).
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class InitiateTestModeCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0500;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x02;

        /// <summary>
        /// Test Mode Duration command message field.
        /// 
        /// Specifies the duration, in seconds, for which the IAS Zone server shall operate in
        /// its test mode.
        /// </summary>
        public byte TestModeDuration { get; set; }

        /// <summary>
        /// Current Zone Sensitivity Level command message field.
        /// 
        /// Specifies the sensitivity level the IAS Zone server shall use for the duration of
        /// the Test Mode and with which it must update its CurrentZoneSensitivityLevel
        /// attribute.
        /// The permitted values of Current Zone Sensitivity Level are shown defined for the
        /// CurrentZoneSensitivityLevel Attribute.
        /// </summary>
        public byte CurrentZoneSensitivityLevel { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public InitiateTestModeCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
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
