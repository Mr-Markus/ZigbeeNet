// License text here
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.OnOff;


namespace ZigBeeNet.ZCL.Clusters.OnOff
{
    /// <summary>
    /// On With Recall Global Scene Command value object class.
    /// <para>
    /// Cluster: On/Off. Command is sent TO the server.
    /// This command is a specific command used for the On/Off cluster.
    ///
    /// The On With Recall Global Scene command allows the recall of the settings when the device was turned off.
    /// </para>
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class OnWithRecallGlobalSceneCommand : ZclCommand
    {

        /// <summary>
        /// Default constructor.
        /// </summary>
        public OnWithRecallGlobalSceneCommand()
        {
            GenericCommand = false;
            ClusterId = 6;
            CommandId = 65;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("OnWithRecallGlobalSceneCommand [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
