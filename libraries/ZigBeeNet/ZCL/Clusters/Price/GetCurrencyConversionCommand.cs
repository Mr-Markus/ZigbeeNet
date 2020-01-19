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
    /// Get Currency Conversion Command value object class.
    ///
    /// Cluster: Price. Command ID 0x0F is sent TO the server.
    /// This command is a specific command used for the Price cluster.
    ///
    /// This command initiates a PublishCurrencyConversion command for the currency
    /// conversion factor updates. A server shall be capable of storing both the old and the new
    /// currencies. <br> A ZCL Default response with status NOT_FOUND shall be returned if
    /// there are no currency conversion factor updates available
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class GetCurrencyConversionCommand : ZclCommand
    {
        /// <summary>
        /// The cluster ID to which this command belongs.
        /// </summary>
        public const ushort CLUSTER_ID = 0x0700;

        /// <summary>
        /// The command ID.
        /// </summary>
        public const byte COMMAND_ID = 0x0F;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GetCurrencyConversionCommand()
        {
            ClusterId = CLUSTER_ID;
            CommandId = COMMAND_ID;
            GenericCommand = false;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("GetCurrencyConversionCommand [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
