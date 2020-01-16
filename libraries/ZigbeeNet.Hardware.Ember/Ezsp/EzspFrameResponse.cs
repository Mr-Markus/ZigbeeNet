using Serilog;
using System;
using System.Threading;
using ZigBeeNet.Hardware.Ember.Internal.Serializer;

namespace ZigBeeNet.Hardware.Ember.Ezsp
{
    /// <summary>
    /// The EmberZNet Serial Protocol (EZSP) is the protocol used by a host application processor to interact with the
    /// EmberZNet PRO stack running on a Network CoProcessor(NCP).
    ///
    /// Reference: UG100: EZSP Reference Guide
    /// 
    /// An EZSP Frame is made up as follows -:
    /// 
    ///  - Sequence : 1 byte sequence number
    ///  - Frame Control: 1 byte
    ///  - Legacy Frame ID : 1 byte
    ///  - Extended Frame Control : 1 byte
    ///  - Frame ID : 1 byte
    ///  - Parameters : variable length
    /// 
    /// The Frame Control byte is as follows -:
    /// 
    ///   bit 7 : 1 for Response
    ///   bit 6 : networkIndex[1]
    ///   bit 5 : networkIndex[0]
    ///   bit 4 : callbackType[1]
    ///   bit 3 : callbackType[0]
    ///   bit 2 : callbackPending
    ///   bit 1 : truncated
    ///   bit 0 : overflow
    /// 
    /// </summary>
    public abstract class EzspFrameResponse : EzspFrame
    {
        protected EzspDeserializer deserializer;

        private const int EZSP_FC_CB_PENDING = 0x04;

        private bool _callbackPending = false;

        /**
         * Constructor used to create a received frame. The constructor reads the header fields from the incoming message.
         *
         * @param inputBuffer the input array to deserialize
         */
        protected EzspFrameResponse(int[] inputBuffer)
        {
            deserializer = new EzspDeserializer(inputBuffer);

            _sequenceNumber = deserializer.DeserializeUInt8();
            _frameControl = deserializer.DeserializeUInt8();
            _frameId = deserializer.DeserializeUInt8();
            if (_frameId == EZSP_LEGACY_FRAME_ID)
            {
                deserializer.DeserializeUInt8();
                _frameId = deserializer.DeserializeUInt8();
            }
            _isResponse = (_frameControl & EZSP_FC_RESPONSE) != 0;
            _callbackPending = (_frameControl & EZSP_FC_CB_PENDING) != 0;
        }

        /**
         * Returns true if the frame control byte indicates that a callback is pending for this response frame
         *
         * @return true if a callback is pending
         */
        public bool IsCallbackPending()
        {
            return _callbackPending;
        }
    }

}
