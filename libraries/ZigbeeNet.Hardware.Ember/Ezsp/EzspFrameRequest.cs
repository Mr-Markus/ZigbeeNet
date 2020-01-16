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
    /// An EZSP V4 Frame is made up as follows -:
    /// 
    ///  - Sequence : 1 byte sequence number
    ///  - Frame Control: 1 byte
    ///  - Frame ID : 1 byte
    ///  - Parameters : variable length
    /// 
    /// 
    /// An EZSP V5+ Frame is made up as follows -:
    /// 
    ///  - Sequence : 1 byte sequence number
    ///  - Frame Control: 1 byte
    ///  - Legacy Frame ID : 1 byte
    ///  - Extended Frame Control : 1 byte
    ///  - Frame ID : 1 byte
    ///  - Parameters : variable length
    /// 
    /// </summary>
    public abstract class EzspFrameRequest : EzspFrame
    {
        private static int sequence = 0;

        /**
         * Constructor used to create an outgoing frame
         */
        protected EzspFrameRequest()
        {
            _sequenceNumber = Interlocked.Increment(ref sequence) & 0xff;
        }

        protected void SerializeHeader(EzspSerializer serializer) 
        {
            // Output sequence number
            serializer.SerializeUInt8(_sequenceNumber);

            // Output Frame Control Byte
            serializer.SerializeUInt8(EZSP_FC_REQUEST);

            if (ezspVersion > 4) 
            {
                serializer.SerializeUInt8(EZSP_LEGACY_FRAME_ID);
                serializer.SerializeUInt8(0x00);
            }

            // Output Frame ID
            serializer.SerializeUInt8(_frameId);
        }

        public virtual int[] Serialize() 
        {
            EzspSerializer serializer = new EzspSerializer();
            SerializeHeader(serializer);

            return serializer.GetPayload();
        }
    }
}
