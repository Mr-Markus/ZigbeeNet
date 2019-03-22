namespace ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol
{
    /// <summary>
    /// 
    /// </summary>
    public enum ReceiveOptions
    {
        /// <summary>
        /// Default unknown value
        /// </summary>
        UNKNOWN = -1,

        /// <summary>
        /// The packet acknowledged
        /// </summary>
        PACKET_ACKNOWLEDGED = 1,

        /// <summary>
        /// The packet broadcast
        /// </summary>
        PACKET_BROADCAST = 2,

        /// <summary>
        /// The aps encryption
        /// </summary>
        APS_ENCRYPTION = 32
    }
}
