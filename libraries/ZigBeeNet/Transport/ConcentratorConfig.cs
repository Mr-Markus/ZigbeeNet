using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Transport
{
    /// <summary>
    /// Defines a the configuration for the ZigBee Concentrator
    /// </summary>
    public class ConcentratorConfig
    {
        /// <summary>
        /// Defines the {@link ConcentratorType}
        /// </summary>
        public ConcentratorType Type { get; set; }

        /// <summary>
        /// The minimum time between MTORR transmissions
        /// </summary>
        public int RefreshMinimum { get; set; }

        /// <summary>
        /// The maximum time between MTORR transmissions
        /// </summary>
        public int RefreshMaximum { get; set; }

        /// <summary>
        /// The maximum number of hops the MTORR will be sent to
        /// </summary>
        public int MaxHops { get; set; }

        /// <summary>
        /// Maximum number of errors that will trigger a re-broadcast of the MTORR
        /// </summary>
        public int MaxFailures { get; set; }

    }

}
