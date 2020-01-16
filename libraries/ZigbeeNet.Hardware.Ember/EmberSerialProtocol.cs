
namespace ZigBeeNet.Hardware.Ember
{
    /// <summary>
    /// Definition of the low level communication protocol types used to communicate with the Ember NCP.
    /// </summary>
    public enum EmberSerialProtocol
    {
        /**
         * No protocol - used for testing
         */
        NONE,
        /**
         * Serial Peripheral Interface - Not yet implemented
         */
        SPI,
        /**
         * Asynchronous Serial Handler V2
         */
        ASH2
    }
}
