using Serilog;
using System;
using System.Text;

namespace ZigBeeNet.Hardware.Ember.Internal.Ash
{

    /// <summary>
    /// ASH Error Frame
    /// </summary>
    public class AshFrameError : AshFrame
    {
        private int _version;
        private int _errorCode;

        /**
         * Constructor taking an incoming data buffer
         *
         * @param frameBuffer the incoming data buffer
         */
        public AshFrameError(int[] frameBuffer) 
        {
            this._frameType = FrameType.ERROR;
            ProcessHeader(frameBuffer);

            this._version = frameBuffer[1];
            this._errorCode = frameBuffer[2];
        }

        public int GetVersion() 
        {
            return _version;
        }

        public int GetErrorCode() {
            return _errorCode;
        }

        public override string ToString() 
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("AshFrameError [version=");
            builder.Append(_version);
            builder.Append(", errorCode=");
            builder.Append(_errorCode);
            builder.Append(", ");
            AshErrorCode ashError = AshErrorCode.GetAshErrorCode(_errorCode);
            builder.Append(ashError.GetDescription());
            builder.Append(']');
            return builder.ToString();
        }

    }
}
