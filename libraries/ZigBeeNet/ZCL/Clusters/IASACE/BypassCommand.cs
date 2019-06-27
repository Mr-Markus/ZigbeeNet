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
    /// Bypass Command value object class.
    /// <para>
    /// Cluster: IAS ACE. Command is sent TO the server.
    /// This command is a specific command used for the IAS ACE cluster.
    ///
    /// Provides IAS ACE clients with a method to send zone bypass requests to the IAS ACE server.
    /// Bypassed zones MAYbe faulted or in alarm but will not trigger the security system to go into alarm.
    /// For example, a user MAYwish to allow certain windows in his premises protected by an IAS Zone server to
    /// be left open while the user leaves the premises. The user could bypass the IAS Zone server protecting
    /// the window on his IAS ACE client (e.g., security keypad), and if the IAS ACE server indicates that zone is
    /// successfully by-passed, arm his security system while he is away.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class BypassCommand : ZclCommand
    {
        /// <summary>
        /// Number of Zones command message field.
        /// </summary>
        public byte NumberOfZones { get; set; }

        /// <summary>
        /// Zone IDs command message field.
        /// </summary>
        public List<byte> ZoneIDs { get; set; }

        /// <summary>
        /// Arm/Disarm Code command message field.
        ///
        /// The Arm/DisarmCode SHALL be a code entered into the ACE client (e.g., security keypad) or system by the
        /// user upon arming/disarming. The server MAY validate the Arm/Disarm Code received from the IAS ACE client
        /// in Arm command payload before arming or disarming the system. If the client does not have the capability
        /// to input an Arm/Disarm Code (e.g., keyfob),or the system does not require one, the client SHALL a transmit
        /// a string with a length of zero.
        /// </summary>
        public string ArmDisarmCode { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public BypassCommand()
        {
            GenericCommand = false;
            ClusterId = 1281;
            CommandId = 1;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(NumberOfZones, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(ZoneIDs, ZclDataType.Get(DataType.N_X_UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(ArmDisarmCode, ZclDataType.Get(DataType.CHARACTER_STRING));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            NumberOfZones = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            ZoneIDs = deserializer.Deserialize<List<byte>>(ZclDataType.Get(DataType.N_X_UNSIGNED_8_BIT_INTEGER));
            ArmDisarmCode = deserializer.Deserialize<string>(ZclDataType.Get(DataType.CHARACTER_STRING));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("BypassCommand [");
            builder.Append(base.ToString());
            builder.Append(", NumberOfZones=");
            builder.Append(NumberOfZones);
            builder.Append(", ZoneIDs=");
            builder.Append(ZoneIDs);
            builder.Append(", ArmDisarmCode=");
            builder.Append(ArmDisarmCode);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
