using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Price;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters.Price
{
    /// <summary>
    /// Get Tariff Cancellation Command value object class.
    ///
    /// Cluster: Price. Command ID 0x10 is sent TO the server.
    /// This command is a specific command used for the Price cluster.
    ///
    /// This command initiates the return of the last CancelTariff command held on the
    /// associated server. <br> A ZCL Default response with status NOT_FOUND shall be returned
    /// if there is no CancelTariff command available.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetTariffCancellationCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0700;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x10;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetTariffCancellationCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetTariffCancellationCommand [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
