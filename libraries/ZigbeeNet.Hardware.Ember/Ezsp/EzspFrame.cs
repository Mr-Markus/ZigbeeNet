using Serilog;
using System;

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
    public abstract partial class EzspFrame 
    {
    
        /**
         * The minimum supported version of EZSP
         */
        private const int EZSP_MIN_VERSION = 4;

        /**
         * The maximum supported version of EZSP
         */
        private const int EZSP_MAX_VERSION = 7;

        /**
         * The current version of EZSP being used
         */
        protected static int ezspVersion = EZSP_MIN_VERSION;

        /**
         * Legacy frame ID for EZSP 5+
         */
        protected const int EZSP_LEGACY_FRAME_ID = 0xFF;

        /**
         * EZSP Frame Control Request flag
         */
        protected const int EZSP_FC_REQUEST = 0x00;

        /**
         * EZSP Frame Control Response flag
         */
        protected const int EZSP_FC_RESPONSE = 0x80;

        protected int _sequenceNumber;
        protected int _frameControl;
        protected int _frameId = 0;
        protected bool _isResponse = false;

        /**
         * Sets the 8 bit transaction sequence number
         *
         * @param sequenceNumber
         */
        public void SetSequenceNumber(int sequenceNumber) 
        {
            this._sequenceNumber = sequenceNumber;
        }

        /**
         * Gets the 8 bit transaction sequence number
         *
         * @return sequence number
         */
        public int GetSequenceNumber() 
        {
            return _sequenceNumber;
        }

        /**
         * Checks if this frame is a response frame
         *
         * @return true if this is a response
         */
        public bool IsResponse() 
        {
            return _isResponse;
        }

        /**
         * Gets the Ember frame ID for this frame
         *
         * @return the Ember frame Id
         */
        public int GetFrameId() 
        {
            return _frameId;
        }

        /**
         * Creates and {@link EzspFrameResponse} from the incoming data.
         *
         * @param data the int[] containing the EZSP data from which to generate the frame
         * @return the {@link EzspFrameResponse} or null if the response can't be created.
         */
        public static EzspFrameResponse CreateHandler(int[] data) 
        {
            Type ezspClass = null;
            EzspFrameResponse ezspFrame = null;

            try 
            {
                if (data[2] != 0xFF) 
                {
                    ezspClass = _ezspHandlerDict[data[2]];
                } 
                else 
                {
                    ezspClass = _ezspHandlerDict[data[4]];
                }
            } 
            catch (Exception e) 
            {
                Log.Debug(e, "Error detecting the EZSP frame type");
            }

            if (ezspClass == null) 
            {
                return null;
            }

            try 
            {
                ezspFrame = (EzspFrameResponse) Activator.CreateInstance(ezspClass, new object[] { data });
            } 
            catch (Exception e) 
            {
                Log.Debug(e, "Error creating instance of EzspFrame");
            }

            return ezspFrame;
        }

        /**
         * Set the EZSP version to use
         *
         * @param ezspVersion the EZSP protocol version
         * @return true if the version is supported
         */
        public static bool SetEzspVersion(int ezspVersion) 
        {
            if (ezspVersion <= EZSP_MAX_VERSION && ezspVersion >= EZSP_MIN_VERSION) 
            {
                EzspFrame.ezspVersion = ezspVersion;
                return true;
            }

            return false;
        }

        /**
         * Gets the current version of EZSP that is in use. This will default to the minimum supported version on startup
         *
         * @return the current version of EZSP
         */
        public static int GetEzspVersion() 
        {
            return EzspFrame.ezspVersion;
        }
    }
}
