// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.RSSILocation;


namespace ZigBeeNet.ZCL.Clusters.RSSILocation
{
    /// <summary>
    /// Compact Location Data Notification Command value object class.
    /// <para>
    /// Cluster: RSSI Location. Command is sent FROM the server.
    /// This command is a specific command used for the RSSI Location cluster.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class CompactLocationDataNotificationCommand : ZclCommand
    {

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CompactLocationDataNotificationCommand()
        {
            GenericCommand = false;
            ClusterId = 11;
            CommandId = 3;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("CompactLocationDataNotificationCommand [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
