using System;
using System.Collections.Generic;
using ZigBeeNet.Hardware.Ember.Ezsp;
using ZigBeeNet.Hardware.Ember.Ezsp.Structure;

namespace ZigBeeNet.Hardware.Ember.Transaction
{
    /// <summary>
    /// Single EZSP transaction response handling. This matches a {@link EzspFrameRequest} with a single
    /// {@link EzspFrameResponse}. {@link EzspFrame#frameId} must also match.
    /// </summary>
    public class EzspSingleResponseTransaction : IEzspTransaction
    {
        private EzspFrameRequest _request;
        private EzspFrameResponse _response;
        private Type _requiredResponse;

        public EzspSingleResponseTransaction(EzspFrameRequest request, Type requiredResponse) 
        {
            this._request = request;
            this._requiredResponse = requiredResponse;
        }

        public bool IsMatch(EzspFrameResponse response) 
        {
            if (response.GetType() == _requiredResponse && _request.GetSequenceNumber() == response.GetSequenceNumber()) 
            {
                this._response = response;
                return true;
            } 
            else 
            {
                return false;
            }
        }

        public EzspFrameRequest GetRequest() 
        {
            return _request;
        }

        public EmberStatus GetStatus() 
        {
            if (_response == null) 
                return EmberStatus.UNKNOWN;

            // TODO: Fix the response status!
            return EmberStatus.UNKNOWN;
        }

        public EzspFrameResponse GetResponse() 
        {
            return _response;
        }

        public List<EzspFrameResponse> GetResponses() 
        {
            if (_response == null) 
                return null;

            // This transaction only allows a single response
            return new List<EzspFrameResponse>() { _response };
        }
    }
}
