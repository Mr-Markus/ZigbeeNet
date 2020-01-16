using Serilog;
using System;
using System.Text;

namespace ZigBeeNet.Hardware.Ember.Internal.Ash
{

    /// <summary>
    /// ASH Reset ACK Frame.
    /// Informs the Host that the NCP has reset and the reason for the reset.
    /// </summary>
    public class AshFrameRstAck : AshFrame
    {
        private int _version;
        private int _resetCode;
        private AshErrorCode _errorCode;

        /**
         * Constructor taking an incoming data buffer
         *
         * @param frameBuffer the incoming data buffer
         */
        public AshFrameRstAck(int[] frameBuffer) 
        {
            this._frameType = FrameType.RSTACK;

            this._version = frameBuffer[1];
            this._resetCode = frameBuffer[2];
            this._errorCode = AshErrorCode.GetAshErrorCode(this._resetCode);
        }

        public int GetVersion() {
            return _version;
        }

        public int GetResetCode() {
            return _resetCode;
        }

        public AshErrorCode GetResetType() 
        {
            return _errorCode;
        }

        public override string ToString() 
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("AshFrameRstAck [version=");
            builder.Append(_version);
            builder.Append(", resetCode=");
            builder.Append(_resetCode);
            builder.Append(", ");
            AshErrorCode ashError = AshErrorCode.GetAshErrorCode(_resetCode);
            builder.Append(ashError.GetDescription());
            builder.Append(']');
            return builder.ToString();
        }
    }
}
