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
    /// Arm Response value object class.
    /// <para>
    /// Cluster: IAS ACE. Command is sent FROM the server.
    /// This command is a specific command used for the IAS ACE cluster.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ArmResponse : ZclCommand
    {
        /// <summary>
        /// Arm Notification command message field.
        /// </summary>
        public byte ArmNotification { get; set; }


        /// <summary>
        /// Default constructor.
        /// </summary>
        public ArmResponse()
        {
            GenericCommand = false;
            ClusterId = 1281;
            CommandId = 0;
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
