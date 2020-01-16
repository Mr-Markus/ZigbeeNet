using Serilog;
using System;

namespace ZigBeeNet.Hardware.Ember.Internal.Ash
{

    /// <summary>
    /// ASH Frame Error
    /// </summary>
    public class AshFrameAck : AshFrame
    {
        /**
         * Constructor to create an ASH ACK frame.
         *
         * @param ackNum the frame number to acknowledge
         */
        public AshFrameAck(int ackNum)
        {
            this._frameType = FrameType.ACK;
            this._ackNum = ackNum;
        }

        /**
         * Constructor taking an incoming data buffer
         * 
         * @param frameBuffer the incoming data buffer
         */
        public AshFrameAck(int[] frameBuffer) 
        {
            this._frameType = FrameType.ACK;
            ProcessHeader(frameBuffer);
        }

        public override string ToString() 
        {
            return "AshFrameAck [ackNum=" + _ackNum + ", notRdy=" + _nRdy + "]";
        }
    }
}
