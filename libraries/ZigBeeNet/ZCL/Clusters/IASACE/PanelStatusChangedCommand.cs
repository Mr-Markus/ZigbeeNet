// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.IASACE;


namespace ZigBeeNet.ZCL.Clusters.IASACE
{
    /// <summary>
    /// Panel Status Changed Command value object class.
    /// <para>
    /// Cluster: IAS ACE. Command is sent FROM the server.
    /// This command is a specific command used for the IAS ACE cluster.
    ///
    /// This command updates ACE clients in the system of changes to panel status recorded by the ACE server (e.g., IAS CIE
    /// device).Sending the Panel Status Changed command (vs.the Get Panel Status and Get Panel Status Response method) is
    /// generally useful only when there are IAS ACE clients that data poll within the retry timeout of the network (e.g., less than
    /// 7.68 seconds).
    /// <br>
    /// An IAS ACE server SHALL send a Panel Status Changed command upon a change to the IAS CIE’s panel status (e.g.,
    /// Disarmed to Arming Away/Stay/Night, Arming Away/Stay/Night to Armed, Armed to Disarmed) as defined in the Panel Status field.
    /// <br>
    /// When Panel Status is Arming Away/Stay/Night, an IAS ACE server SHOULD send Panel Status Changed commands every second in order to
    /// update the Seconds Remaining. In some markets (e.g., North America), the final 10 seconds of the Arming Away/Stay/Night sequence
    /// requires a separate audible notification (e.g., a double tone).
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class PanelStatusChangedCommand : ZclCommand
    {
        /// <summary>
        /// Panel Status command message field.
        ///
        /// Indicates the number of seconds remaining for  the server to be in the state indicated in the PanelStatus parameter.
        /// The SecondsRemaining parameter SHALL be provided if the PanelStatus parameter has a value of 0x04 (Exit delay) or 0x05 (Entry delay).
        /// <p>
        /// The default value SHALL be 0x00.
        /// </summary>
        public byte PanelStatus { get; set; }

        /// <summary>
        /// Seconds Remaining command message field.
        /// </summary>
        public byte SecondsRemaining { get; set; }

        /// <summary>
        /// Audible Notification command message field.
        /// </summary>
        public byte AudibleNotification { get; set; }

        /// <summary>
        /// Alarm Status command message field.
        /// </summary>
        public byte AlarmStatus { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public PanelStatusChangedCommand()
        {
            GenericCommand = false;
            ClusterId = 1281;
            CommandId = 4;
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

            builder.Append("PanelStatusChangedCommand [");
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
