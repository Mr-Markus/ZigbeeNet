using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet
{
    public enum ZigBeeStatus
    {
        /// <summary>
        /// The operation completed successfully
        /// </summary>
        SUCCESS,
        /// <summary>
        /// The operation failed and no further information on the reason is available
        /// </summary>
        FAILURE,
        /// <summary>
        /// The caller provided invalid arguments for the requested function
        /// </summary>
        INVALID_ARGUMENTS,
        /// <summary>
        /// The transport has not set a result for this request. This could mean it did not process the request.
        /// </summary>
        NO_RESPONSE,
        /// <summary>
        /// The system was not in the correct state to process the request
        /// </summary>
        INVALID_STATE,
        /// <summary>
        /// A request was made that is not supported by the function
        /// </summary>
        UNSUPPORTED,
        /// <summary>
        /// The system could not communicate. Depending on the method returning this error, the cause may be one of the
        /// following -:
        ///     Could not communicate with the transport layer
        COMMUNICATION_ERROR,
        /// <summary>
        /// An unexpected response was received
        /// </summary>
        BAD_RESPONSE
    }
}
