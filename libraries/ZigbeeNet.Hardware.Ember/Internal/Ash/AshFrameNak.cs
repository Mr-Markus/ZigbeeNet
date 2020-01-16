using Serilog;
using System;
using System.Text;

namespace ZigBeeNet.Hardware.Ember.Internal.Ash
{

    /// <summary>
    /// ASH NAK Frame
    /// </summary>
    public class AshFrameNak : AshFrame
    {
        /**
         * Constructor to create an ASH NAK frame.
         *
         * @param ackNum the frame number to acknowledge
         */
        public AshFrameNak(int ackNum) 
        {
            this._frameType = FrameType.NAK;
            this._ackNum = ackNum;
        }

        /**
         * Constructor taking an incoming data buffer
         *
         * @param frameBuffer the incoming data buffer
         */
        public AshFrameNak(int[] frameBuffer) 
        {
            this._frameType = FrameType.NAK;
            ProcessHeader(frameBuffer);
        }

        public override string ToString() 
        {
            return "AshFrameNak [ackNum=" + _ackNum + ", notRdy=" + _nRdy + "]";
        }
    }
}
