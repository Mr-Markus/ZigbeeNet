using Serilog;
using System;
using System.Text;

namespace ZigBeeNet.Hardware.Ember.Internal.Ash
{

    /// <summary>
    /// ASH Reset Frame
    /// </summary>
    public class AshFrameRst : AshFrame
    {
        /**
         * Constructor to create an ASH Reset frame.
         */
        public AshFrameRst()
        {
            this._frameType = FrameType.RST;
        }

        /**
         * Constructor taking an incoming data buffer
         *
         * @param frameBuffer the incoming data buffer
         */
        public AshFrameRst(int[] frameBuffer)
            : this()
        {
        }

        public override int[] GetOutputBuffer() 
        {
            int[] rstFrame = base.GetOutputBuffer();

            int[] outFrame = new int[rstFrame.Length + 1];
            outFrame[0] = 0x1A;
            for (int cnt = 0; cnt < rstFrame.Length; cnt++) 
            {
                outFrame[cnt + 1] = rstFrame[cnt];
            }

            return outFrame;
        }

        public override string ToString() 
        {
            return "AshFrameRst []";
        }
    }
}
