using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.OnOff
{
    /**
     * Off Command value object class.
     * <p>
     * Cluster: <b>On/Off</b>. Command is sent <b>TO</b> the server.
     * This command is a <b>specific</b> command used for the On/Off cluster.
     * <p>
     * Code is auto-generated. Modifications may be overwritten!
     */
    public class OffCommand : ZclCommand
    {
        /**
         * Default constructor.
         */
        public OffCommand()
        {
            GenericCommand = false;
            ClusterId = 6;
            CommandId = 0;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder()
                .Append("OffCommand [")
                .Append(base.ToString())
                .Append(']');

            return builder.ToString();
        }
    }
}
