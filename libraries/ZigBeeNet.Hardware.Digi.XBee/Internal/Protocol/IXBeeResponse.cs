namespace ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol
{
    /// <summary>
    /// Interface for the XBee receive event
    /// </summary>
    public interface IXBeeResponse
    {
        /// <summary>
        /// Gets the frame Id used to correct the request and response.
        /// </summary>
        /// <returns>Frame ID</returns>
        int GetFrameId();

        /// <summary>
        /// Deserialize an incoming data packet.
        /// <p>
        /// A command handler may require multiple data packets to be processed before it is complete.True should only be
        /// returned once it has received all data.
        /// <p>
        /// A prompt handler will always process the data and should always return true since it can not handle multiple
        /// responses.
        /// </summary>
        /// <param name="data">The data to deserialize.</param>
        void Deserialize(int[] data);
    }
}
