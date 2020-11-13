using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.IasZone;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.IasZone
{
    /// <summary>
    /// Initiate Normal Operation Mode Command value object class.
    ///
    /// Cluster: IAS Zone. Command ID 0x01 is sent TO the server.
    /// This command is a specific command used for the IAS Zone cluster.
    ///
    /// Used to tell the IAS Zone server to commence normal operation mode. <br> Upon receipt,
    /// the IAS Zone server shall commence normal operational mode. <br> Any configurations
    /// and changes made (e.g., CurrentZoneSensitivityLevel attribute) to the IAS Zone
    /// server shall be retained. <br> Upon commencing normal operation mode, the IAS Zone
    /// server shall send a Zone Status Change Notification command updating the ZoneStatus
    /// attribute Test bit to zero (i.e., “operation mode”).
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class InitiateNormalOperationModeCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0500;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x01;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public InitiateNormalOperationModeCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("InitiateNormalOperationModeCommand [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
