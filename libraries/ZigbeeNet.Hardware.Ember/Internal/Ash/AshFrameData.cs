using Serilog;
using System;
using System.Text;
using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.Ember.Internal.Ash
{

    /// <summary>
    /// ASH Data Frame
    /// </summary>
    public class AshFrameData : AshFrame
    {
        /**
         * Constructor to create an ASH Data frame for sending.
         *
         * @param ezspRequestFrame the {@link EzspFrameRequest} to send
         */
        public AshFrameData(EzspFrameRequest ezspRequestFrame) 
        {
            _frameType = FrameType.DATA;
            _dataBuffer = ezspRequestFrame.Serialize();
        }

        /**
         * Constructor taking an incoming data buffer
         *
         * @param frameBuffer the incoming data buffer
         */
        public AshFrameData(int[] frameBuffer) 
        {
            _frameType = FrameType.DATA;

            ProcessHeader(frameBuffer);
            _dataBuffer = new int[frameBuffer.Length - 3];
            Array.Copy(frameBuffer, 1, _dataBuffer, 0, frameBuffer.Length - 3);
        }

        public void SetReTx() 
        {
            _reTx = true;
        }

        public bool GetReTx() 
        {
            return _reTx;
        }

        public int[] GetDataBuffer() 
        {
            return _dataBuffer;
        }

        public override string ToString() 
        {
            StringBuilder result = new StringBuilder();
            result.Append("AshFrameData [frmNum=");
            result.Append(_frmNum);
            result.Append(", ackNum=");
            result.Append(_ackNum);
            result.Append(", reTx=");
            result.Append(_reTx);
            result.Append(", data=");

            for (int i = 0; i < _dataBuffer.Length; i++) 
            {
                if (i != 0) 
                {
                    result.Append(' ');
                }
                result.Append(_dataBuffer[i].ToString("X2"));
            }
            result.Append(']');

            return result.ToString();
        }
    }
}
