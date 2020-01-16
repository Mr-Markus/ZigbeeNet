using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.Hardware.Ember.Ezsp;
using ZigBeeNet.Hardware.Ember.Ezsp.Structure;

namespace ZigBeeNet.Hardware.Ember.Transaction
{
    /// <summary>
    /// Interface for EZSP protocol transaction.
    /// The transaction looks for a {@link EzspFrameResponse} that matches the {@link EzspFrameRequest}.
    /// The{ @link EzspFrameResponse } and {@link EzspFrameRequest} classes are provided when the transaction is created.
    /// </summary>
    public interface IEzspTransaction
    {
        /**
         * Matches request and response.
         *
         * @param response the response {@link EzspFrameResponse}
         * @return true if response matches the request
         */
        bool IsMatch(EzspFrameResponse response);

        /**
         * Gets the {@link EzspFrameRequest} associated with this transaction
         *
         * @return the {@link EzspFrameRequest}
         */
        EzspFrameRequest GetRequest();

        /**
         * Gets the {@link EzspFrameResponse} for the transaction. If multiple responses are returned, this will return the
         * last response, indicating the final response used to complete the transaction.
         *
         * @return {@link EzspFrameResponse} to complete the transaction or null if no response received
         */
        EzspFrameResponse GetResponse();

        /**
         * Gets a {@link List} of the {@link EzspFrameResponse}s received for the transaction. This is used for transactions
         * returning multiple responses - for single response transactions, use {@link #getResponse}.
         *
         * @return {@link EzspFrameResponse} to complete the transaction or null if no response received
         */
        List<EzspFrameResponse> GetResponses();

        /**
         * Get the {@link EmberStatus} of the transaction. If multiple responses are returned, this will return the last
         * response indicating the final state of the transaction.
         *
         * @return {@link EmberStatus} indicating the transaction completion state or
         *         {@link EmberStatus#EMBED_UNKNOWN_STATUS} on error.
         */
        EmberStatus GetStatus();
    }
}
