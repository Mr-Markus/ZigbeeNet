using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.Basic
{
    /**
     * Reset to Factory Defaults Command value object class.
     * <p>
     * Cluster: <b>Basic</b>. Command is sent <b>TO</b> the server.
     * This command is a <b>specific</b> command used for the Basic cluster.
     * <p>
     * On receipt of this command, the device resets all the attributes of all its clusters
     * to their factory defaults. Note that ZigBee networking functionality,bindings, groups
     * or other persistent data are not affected by this command
     * <p>
     * Code is auto-generated. Modifications may be overwritten!
     */
    class ResetToFactoryDefaultsCommand : ZclCommand
    {
        /**
        * Default constructor.
        */
        public ResetToFactoryDefaultsCommand()
        {
            GenericCommand = false;
            ClusterId = 0;
            CommandId = 0;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(32);
            builder.Append("ResetToFactoryDefaultsCommand [");
            builder.Append(base.ToString());
            builder.Append(']');
            return builder.ToString();
        }

    }
}
