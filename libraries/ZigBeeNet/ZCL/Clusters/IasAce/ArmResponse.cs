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
    /// Arm Response value object class.
    ///
    /// Cluster: IAS ACE. Command ID 0x00 is sent FROM the server.
    /// This command is a specific command used for the IAS ACE cluster.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ArmResponse : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0501;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x00;

        /// <summary>
        /// Arm Notification command message field.
        /// </summary>
        public byte ArmNotification { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ArmResponse()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(ArmNotification, ZclDataType.Get(DataType.ENUMERATION_8_BIT));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            ArmNotification = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.ENUMERATION_8_BIT));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("ArmResponse [");
            builder.Append(base.ToString());
            builder.Append(", ArmNotification=");
            builder.Append(ArmNotification);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
