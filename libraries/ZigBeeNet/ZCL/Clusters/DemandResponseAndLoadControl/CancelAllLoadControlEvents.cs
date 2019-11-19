using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.DemandResponseAndLoadControl;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.DemandResponseAndLoadControl
{
    /// <summary>
    /// Cancel All Load Control Events value object class.
    ///
    /// Cluster: Demand Response And Load Control. Command ID 0x02 is sent FROM the server.
    /// This command is a specific command used for the Demand Response And Load Control cluster.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class CancelAllLoadControlEvents : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0701;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x02;

        /// <summary>
        /// Cancel Control command message field.
        /// </summary>
        public byte CancelControl { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CancelAllLoadControlEvents()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        internal override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(CancelControl, ZclDataType.Get(DataType.BITMAP_8_BIT));
        }

        internal override void Deserialize(ZclFieldDeserializer deserializer)
        {
            CancelControl = deserializer.Deserialize<byte>(ZclDataType.Get(DataType.BITMAP_8_BIT));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("CancelAllLoadControlEvents [");
            builder.Append(base.ToString());
            builder.Append(", CancelControl=");
            builder.Append(CancelControl);
            builder.Append(']');

            return builder.ToString();
        }
    }
}
