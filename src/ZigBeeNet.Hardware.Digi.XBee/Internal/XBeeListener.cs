using ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol;

namespace ZigBeeNet.Hardware.Digi.XBee.Internal
{
    public class XBeeListener : IXBeeListener
    {
        #region fields

        private readonly object _lockObject = new object();

        #endregion fields

        #region properties

        /// <summary>
        /// Gets or sets our frame identifier.
        /// </summary>
        /// <value>
        /// Our frame identifier.
        /// </value>
        public int OurFrameId { get; set; } = 0;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IXBeeListener"/> is complete.
        /// </summary>
        /// <value>
        ///   <c>true</c> if complete; otherwise, <c>false</c>.
        /// </value>
        public bool Complete { get; set; }

        /// <summary>
        /// Gets or sets the completion response.
        /// </summary>
        /// <value>
        /// The completion response.
        /// </value>
        public IXBeeResponse CompletionResponse { get; set; } = null;

        #endregion properties

        #region methods

        /// <summary>
        /// Transactions the event.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        public bool TransactionEvent(IXBeeResponse response)
        {
            // Check if this response completes our transaction
            int frameId = response.GetFrameId();
            if (response.GetFrameId() != OurFrameId)
            {
                return false;
            }

            lock (_lockObject)
            {
                CompletionResponse = response;
                Complete = true;
            }

            return true;
        }

        #endregion methods
    }
}
