using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.OnOff
{
    /**
     * On With Recall Global Scene Command value object class.
     * <p>
     * Cluster: <b>On/Off</b>. Command is sent <b>TO</b> the server.
     * This command is a <b>specific</b> command used for the On/Off cluster.
     * <p>
     * The On With Recall Global Scene command allows the recall of the settings when the device was turned off.
     * <p>
     * Code is auto-generated. Modifications may be overwritten!
     */
    public class OnWithRecallGlobalSceneCommand : ZclCommand
    {
        /**
         * Default constructor.
         */
        public OnWithRecallGlobalSceneCommand()
        {
            GenericCommand = false;
            ClusterId = 6;
            CommandId = 65;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder()
                .Append("OnWithRecallGlobalSceneCommand [")
                .Append(base.ToString())
                .Append(']');
            return builder.ToString();
        }
    }
}
