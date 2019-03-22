namespace ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol
{
    public enum TransmitOptions
    {
        /// <summary>
        /// Default unknown value
        /// </summary>
        UNKNOWN = -1,

        /// <summary>
        /// The disable retries [1]
        /// </summary>
        DISABLE_RETRIES = 1,

        /// <summary>
        /// The multicast addressing [8]
        /// </summary>
        MULTICAST_ADDRESSING = 8,

        /// <summary>
        /// The enable aps encryption
        /// </summary>
        ENABLE_APS_ENCRYPTION = 32,

        /// <summary>
        /// The extended transmission timeout
        /// </summary>
        EXTENDED_TRANSMISSION_TIMEOUT = 64
    }
}
