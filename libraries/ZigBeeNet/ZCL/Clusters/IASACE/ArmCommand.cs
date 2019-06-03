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
    /// Arm Command value object class.
    /// <para>
    /// Cluster: IAS ACE. Command is sent TO the server.
    /// This command is a specific command used for the IAS ACE cluster.
    ///
    /// On receipt of this command, the receiving device sets its arm mode according to the value of the Arm Mode field. It
    /// is not guaranteed that an Arm command will succeed. Based on the current state of
    /// the IAS CIE, and its related devices, the command can be rejected. The device SHALL generate an Arm Response command
    /// to indicate the resulting armed state
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ArmCommand : ZclCommand
    {
        /// <summary>
        /// Arm Mode command message field.
        /// </summary>
        public byte ArmMode { get; set; }

        /// <summary>
        /// Arm/Disarm Code command message field.
        ///
        /// The Arm/DisarmCode SHALL be a code entered into the ACE client (e.g., security keypad) or system by the
        /// user upon arming/disarming. The server MAY validate the Arm/Disarm Code received from the IAS ACE client
        /// in Arm command payload before arming or disarming the system. If the client does not have the capability
        /// to input an Arm/Disarm Code (e.g., keyfob),or the system does not require one, the client SHALL a transmit
        /// a string with a length of zero.
        /// <p>
        /// There is no minimum or maximum length to the Arm/Disarm Code; however, the
        /// Arm/Disarm Code SHOULD be between four and eight alphanumeric characters in length.
        /// <p>
        /// The string encoding SHALL be UTF-8.
        /// </summary>
        public string ArmDisarmCode { get; set; }

        /// <summary>
        /// Zone ID command message field.
        ///
        /// Zone ID is the index of the Zone in the CIE's zone table. If none is programmed, the Zone
        /// ID default value SHALL be indicated in this field.
        /// </summary>
        public byte ZoneID { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public ArmCommand()
        {
            GenericCommand = false;
            ClusterId = 1281;
            CommandId = 0;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(ArmMode, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            serializer.Serialize(ArmDisarmCode, ZclDataType.Get(DataType.CHARACTER_STRING));
            serializer.Serialize(ZoneID, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            ArmMode = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
            ArmDisarmCode = deserializer.Deserialize<string>(ZclDataType.Get(DataType.CHARACTER_STRING));
            ZoneID = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ArmCommand [");
            builder.Append(base.ToString());
            builder.Append(", ArmMode=");
            builder.Append(ArmMode);
            builder.Append(", ArmDisarmCode=");
            builder.Append(ArmDisarmCode);
            builder.Append(", ZoneID=");
            builder.Append(ZoneID);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
