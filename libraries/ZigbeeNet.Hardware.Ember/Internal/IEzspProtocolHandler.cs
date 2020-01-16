using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.Hardware.Ember.Ezsp;
using ZigBeeNet.Hardware.Ember.Transaction;
using ZigBeeNet.Transport;

namespace ZigBeeNet.Hardware.Ember.Internal
{
    /// <summary>
    /// Interface for the EZSP protocol handler. The protocol handler manages the low level data transfer of EZSP frames.
    /// </summary>

    public interface IEzspProtocolHandler
    {

        /**
         * Starts the handler. Sets input stream where the packet is read from the and
         * handler which further processes the received packet.
         *
         * @param port the {@link ZigBeePort}
         */
        void Start(IZigBeePort port);

        /**
         * Set the close flag to true.
         */
        void SetClosing();

        /**
         * Requests parser thread to shutdown.
         */
        void Close();

        /**
         * Checks if parser thread is alive.
         *
         * @return true if parser thread is alive.
         */
        bool IsAlive();

        /**
         * Add an EZSP frame to the send queue. The sendQueue is a FIFO queue.
         * This method queues a {@link EzspFrameRequest} frame without waiting for a response and
         * no transaction management is performed.
         *
         * @param request {@link EzspFrameRequest}
         */
        void QueueFrame(EzspFrameRequest request);

        /**
         * Connect to the ASH/EZSP NCP
         */
        void Connect();

        /**
         * Sends an EZSP request to the NCP without waiting for the response.
         *
         * @param ezspTransaction Request {@link EzspTransaction}
         * @return response {@link Future} {@link EzspFrame}
         */
        Task<EzspFrame> SendEzspRequestAsync(IEzspTransaction ezspTransaction);

        /**
         * Sends an EZSP request to the NCP and waits for the response. The response is correlated with the request and the
         * returned {@link EzspTransaction} contains the request and response data.
         *
         * @param ezspTransaction Request {@link EzspTransaction}
         * @return response {@link EzspTransaction}
         */
        IEzspTransaction SendEzspTransaction(IEzspTransaction ezspTransaction);

        /**
         * Wait for the requested {@link EzspFrameResponse} to be received
         *
         * @param eventClass Request {@link EzspFrameResponse} to wait for
         * @return response {@link Future} {@link EzspFrameResponse}
         */
        Task<EzspFrameResponse> EventWaitAsync(Type eventClass);

        /**
         * Wait for the requested {@link EzspFrameResponse} to be received
         *
         * @param eventClass Request {@link EzspFrameResponse} to wait for
         * @param timeout the time in milliseconds to wait for the response
         * @return the {@link EzspFrameResponse} once received, or null on exception
         */
        EzspFrameResponse EventWait(Type eventClass, int timeout);

        /**
         * Get a map of statistics counters from the handler
         *
         * @return map of counters
         */
        Dictionary<string, long> GetCounters();
    }
}
