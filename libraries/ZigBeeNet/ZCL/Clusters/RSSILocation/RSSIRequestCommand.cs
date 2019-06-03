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
    /// RSSI Request Command value object class.
    /// <para>
    /// Cluster: RSSI Location. Command is sent FROM the server.
    /// This command is a specific command used for the RSSI Location cluster.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class RSSIRequestCommand : ZclCommand
    {

        /// <summary>
        /// Default constructor.
        /// </summary>
        public RSSIRequestCommand()
        {
            GenericCommand = false;
            ClusterId = 11;
            CommandId = 5;
            CommandDirection = ZclCommandDirection.SERVER_TO_CLIENT;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("RSSIRequestCommand [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
