using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.IASACE;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.IASACE
{
    /// <summary>
    /// Get Panel Status Response value object class.
    ///
    /// Cluster: IAS ACE. Command ID 0x05 is sent FROM the server.
    /// This command is a specific command used for the IAS ACE cluster.
    ///
    /// This command updates requesting IAS ACE clients in the system of changes to the security
    /// panel status recorded by the ACE server (e.g., IAS CIE device).
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetPanelStatusResponse : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0501;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x05;

        /// <summary>
        /// Panel Status command message field.
        /// 
        /// Defines the current status of the alarm panel.
        /// </summary>
        public byte PanelStatus { get; set; }

        /// <summary>
        /// Seconds Remaining command message field.
        /// 
        /// Indicates the number of seconds remaining for the server to be in the state
        /// indicated in the PanelStatus parameter. The SecondsRemaining parameter shall be
        /// provided if the PanelStatus parameter has a value of 0x04 (Exit delay) or 0x05
        /// (Entry delay).
        /// The default value shall be 0x00.
        /// </summary>
        public byte SecondsRemaining { get; set; }

        /// <summary>
        /// Audible Notification command message field.
        /// 
        /// Provide the ACE client with information on which type of audible notification it
        /// should make for the zone status change. This field is useful for telling the ACE
        /// client to play a standard chime or other audio indication or to mute and not sound an
        /// audible notification at all. This field also allows manufacturers to create
        /// additional audible alert types (e.g., dog barking, windchimes, conga drums) to
        /// enable users to customise their system.
        /// </summary>
        public byte AudibleNotification { get; set; }

        /// <summary>
        /// Alarm Status command message field.
        /// 
        /// Provides the ACE client with information on the type of alarm the panel is in if its
        /// Panel Status field indicates it is “in alarm.” This field may be useful for ACE
        /// clients to display or otherwise initiate notification for users.
        /// </summary>
        public byte AlarmStatus { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetPanelStatusResponse()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(PanelStatus, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(SecondsRemaining, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(AudibleNotification, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(AlarmStatus, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            PanelStatus = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            SecondsRemaining = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            AudibleNotification = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            AlarmStatus = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetPanelStatusResponse [");
            builder.Append(base.ToString());
            builder.Append(", PanelStatus=");
            builder.Append(PanelStatus);
            builder.Append(", SecondsRemaining=");
            builder.Append(SecondsRemaining);
            builder.Append(", AudibleNotification=");
            builder.Append(AudibleNotification);
            builder.Append(", AlarmStatus=");
            builder.Append(AlarmStatus);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
