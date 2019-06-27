using ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol;

namespace ZigBeeNet.Hardware.Digi.XBee.Internal
{
    public interface IXBeeListener
    {
        /// <summary>
        /// Gets or sets our frame identifier.
        /// </summary>
        /// <value>
        /// Our frame identifier.
        /// </value>
        int OurFrameId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IXBeeListener"/> is complete.
        /// </summary>
        /// <value>
        ///   <c>true</c> if complete; otherwise, <c>false</c>.
        /// </value>
        bool Complete { get; set; }

        /// <summary>
        /// Gets or sets the completion response.
        /// </summary>
        /// <value>
        /// The completion response.
        /// </value>
        IXBeeResponse CompletionResponse { get; set; }

        /// <summary>
        /// Transactions the event.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        bool TransactionEvent(IXBeeResponse response);
    }
}
