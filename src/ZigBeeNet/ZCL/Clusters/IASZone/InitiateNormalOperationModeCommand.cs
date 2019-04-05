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
    /// Initiate Normal Operation Mode Command value object class.
    /// <para>
    /// Cluster: IAS Zone. Command is sent TO the server.
    /// This command is a specific command used for the IAS Zone cluster.
    ///
    /// Used to tell the IAS Zone server to commence normal operation mode.
    /// <br>
    /// Upon receipt, the IAS Zone server SHALL commence normal operational mode.
    /// <br>
    /// Any configurations and changes made (e.g., CurrentZoneSensitivityLevel attribute) to the IAS Zone server SHALL be retained.
    /// <br>
    /// Upon commencing normal operation mode, the IAS Zone server SHALL send a Zone Status Change Notification command updating the ZoneStatus
    /// attribute Test bit to zero (i.e., “operation mode”).
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class InitiateNormalOperationModeCommand : ZclCommand
    {

        /// <summary>
        /// Default constructor.
        /// </summary>
        public InitiateNormalOperationModeCommand()
        {
            GenericCommand = false;
            ClusterId = 1280;
            CommandId = 1;
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
