using Serilog;
using System;

namespace ZigBeeNet.Hardware.Ember.Internal.Ash
{
    /// <summary>
    /// ASH Frame Handling: Asynchronous Serial Host (ASH) protocol. The ASH protocol
    /// is a data-link layer protocol below EZSP and above the serial device (or
    /// UART) driver.
    /// 
    /// UG101: UART GATEWAY PROTOCOL REFERENCE: FOR THE EMBER® EZSP NETWORK CO-PROCESSOR
    ///
    /// </summary>
    public class AshFrame
    {

        protected int _frmNum;
        protected int _ackNum;
        protected bool _reTx;
        protected bool _nRdy;

        protected FrameType _frameType;
        protected int[] _dataBuffer;

        protected AshFrame() {
        }

        /**
         * Returns the data to be sent to the NCP
         * <ol>
         * <li>The Control Byte is added before the Data Field. The frmNum field is set to the last frame transmitted plus
         * one, and the ackNum field is set to the number of the next frame expected to be received. The reTx flag is clear.
         * <li>The Data Field is exclusive-OR’ed with a pseudo-random sequence (see Data randomization).
         * <li>The two-byte CRC of the Control Byte plus the Data Field is computed and appended after the Data Field.
         * <li>The frame is byte stuffed to remove reserved byte values (see Reserved bytes and byte stuffing).
         * <li>A Flag Byte is added after the CRC.
         * </ol>
         *
         * @return integer array of data to be sent
         */
        public virtual int[] GetOutputBuffer() 
        {
            if (_frmNum > 7 || _frmNum < 0) 
            {
                Log.Debug("Invalid frmNum {frmNum}. Assuming 0", _frmNum);
                _frmNum = 0;
            }
            if (_ackNum > 7 || _ackNum < 0) 
            {
                Log.Debug("Invalid ackNum {ackNum}. Assuming 0", _ackNum);
                _ackNum = 0;
            }

            int[] outputData = new int[250];
            int outputPos = 0;

            switch (_frameType) 
            {
                case FrameType.ACK:
                    outputData[outputPos++] = 0x80 + _ackNum;
                    break;
                case FrameType.DATA:
                    outputData[outputPos++] = (_frmNum << 4) + _ackNum + (_reTx ? 0x08 : 0x00);
                    break;
                case FrameType.ERROR:
                    break;
                case FrameType.NAK:
                    break;
                case FrameType.RST:
                    outputData[outputPos++] = 0xC0;
                    break;
                case FrameType.RSTACK:
                    break;
                default:
                    break;
            }
            if (_dataBuffer != null) 
            {
                for (int cnt = 0; cnt < _dataBuffer.Length; cnt++) 
                {
                    outputData[outputPos++] = _dataBuffer[cnt];
                }
                if (_frameType == FrameType.DATA) 
                {
                    DataRandomise(outputData, 1, _dataBuffer.Length + 1);
                }
            }

            int crc = CheckCRC(outputData, outputPos);
            outputData[outputPos++] = (crc >> 8) & 0xFF;
            outputData[outputPos++] = crc & 0xFF;

            int[] stuffedOutputData = new int[250];
            int stuffedOutputPos = 0;
            for (int cnt = 0; cnt < outputPos; cnt++) 
            {
                switch (outputData[cnt]) 
                {
                    case 0x7E:
                        stuffedOutputData[stuffedOutputPos++] = 0x7D;
                        stuffedOutputData[stuffedOutputPos++] = 0x5E;
                        break;
                    case 0x7D:
                        stuffedOutputData[stuffedOutputPos++] = 0x7D;
                        stuffedOutputData[stuffedOutputPos++] = 0x5D;
                        break;
                    case 0x11:
                        stuffedOutputData[stuffedOutputPos++] = 0x7D;
                        stuffedOutputData[stuffedOutputPos++] = 0x31;
                        break;
                    case 0x13:
                        stuffedOutputData[stuffedOutputPos++] = 0x7D;
                        stuffedOutputData[stuffedOutputPos++] = 0x33;
                        break;
                    case 0x18:
                        stuffedOutputData[stuffedOutputPos++] = 0x7D;
                        stuffedOutputData[stuffedOutputPos++] = 0x38;
                        break;
                    case 0x1A:
                        stuffedOutputData[stuffedOutputPos++] = 0x7D;
                        stuffedOutputData[stuffedOutputPos++] = 0x3A;
                        break;
                    default:
                        stuffedOutputData[stuffedOutputPos++] = outputData[cnt];
                        break;
                }
            }
            stuffedOutputData[stuffedOutputPos++] = 0x7E;

            int[] finalOutput = new int[stuffedOutputPos];
            Array.Copy(stuffedOutputData, 0, finalOutput, 0, stuffedOutputPos);
            return finalOutput;
        }

        protected void ProcessHeader(int[] inputBuffer) 
        {
            switch (_frameType) {
                case FrameType.DATA:
                    _ackNum = (inputBuffer[0] & 0x07);
                    _frmNum = (inputBuffer[0] & 0x70) >> 4;
                    _reTx = (inputBuffer[0] & 0x08) != 0;
                    break;
                case FrameType.ACK:
                case FrameType.NAK:
                    _nRdy = (inputBuffer[0] & 0x08) != 0;
                    _ackNum = (inputBuffer[0] & 0x07);
                    break;
                default:
                    break;
            }
        }

        public static AshFrame CreateFromInput(int[] buffer) 
        {
            // A frame must be at least 3 bytes long
            if (buffer.Length < 3) 
            {
                return null;
            }

            // Remove byte stuffing
            int[] unstuffedData = new int[buffer.Length];
            int outLength = 0;
            bool escape = false;
            int d = 0;
            foreach (int data in buffer) 
            {
                d = data;
                if (escape) 
                {
                    escape = false;
                    if ((d & 0x20) == 0)
                        d = (byte)(d + 0x20);
                    else
                        d = (byte)(d & 0xDF);
                } 
                else if (d == 0x7D) 
                {
                    escape = true;
                    continue;
                }
                unstuffedData[outLength++] = d;
            }

            // Check CRC
            if (CheckCRC(unstuffedData, outLength) != 0) 
            {
                return null;
            }

            FrameType? frameType = GetFrameType(unstuffedData);
            if (frameType == null) 
            {
                Log.Debug("Invalid ASH frame type {Type}", unstuffedData[0].ToString("X2"));
                return null;
            }

            int[] frameBuffer = new int[outLength];
            Array.Copy(unstuffedData, 0, frameBuffer, 0, outLength);

            switch (frameType) 
            {
                case FrameType.ACK:
                    return new AshFrameAck(frameBuffer);
                case FrameType.DATA:
                    DataRandomise(frameBuffer, 1, frameBuffer.Length);
                    return new AshFrameData(frameBuffer);
                case FrameType.ERROR:
                    return new AshFrameError(frameBuffer);
                case FrameType.NAK:
                    return new AshFrameNak(unstuffedData);
                case FrameType.RST:
                    return new AshFrameRst();
                case FrameType.RSTACK:
                    return new AshFrameRstAck(frameBuffer);
                default:
                    break;
            }
            return null;
        }

        private static void DataRandomise(int[] buffer, int start, int length) 
        {
            // Randomise the data
            int rand = 0x42;
            for (int cnt = start; cnt < length; cnt++) 
            {
                buffer[cnt] = buffer[cnt] ^ rand;

                if ((rand & 0x01) == 0) 
                {
                    rand = rand >> 1;
                } 
                else 
                {
                    rand = (rand >> 1) ^ 0xb8;
                }
            }
        }

        private static FrameType? GetFrameType(int[] buffer) {
            if (buffer == null) {
                return null;
            }

            if ((buffer[0] & 0x80) == 0) {
                return FrameType.DATA;
            } else if ((buffer[0] & 0x60) == 0x00) {
                return FrameType.ACK;
            } else if ((buffer[0] & 0x60) == 0x20) {
                return FrameType.NAK;
            } else if (buffer[0] == 0xC0) {
                return FrameType.RST;
            } else if (buffer[0] == 0xC1) {
                return FrameType.RSTACK;
            } else if (buffer[0] == 0xC2) {
                return FrameType.ERROR;
            }

            return null;
        }

        public int GetFrmNum() 
        {
            return _frmNum;
        }

        public void SetFrmNum(int frmNum) 
        {
            this._frmNum = frmNum;
        }

        public int GetAckNum() 
        {
            return _ackNum;
        }

        public void SetAckNum(int ackNum) 
        {
            this._ackNum = ackNum;
        }

        private static int CheckCRC(int[] buffer, int length) 
        {
            int crc = 0xFFFF; // initial value
            int polynomial = 0x1021; // 0001 0000 0010 0001 (0, 5, 12)

            for (int cnt = 0; cnt < length; cnt++) {
                for (int i = 0; i < 8; i++) {
                    bool bit = ((buffer[cnt] >> (7 - i) & 1) == 1);
                    bool c15 = ((crc >> 15 & 1) == 1);
                    crc <<= 1;
                    if (c15 ^ bit) {
                        crc ^= polynomial;
                    }
                }
            }

            crc &= 0xffff;

            return crc;
        }

        public enum FrameType {
            DATA,
            ACK,
            NAK,
            RST,
            RSTACK,
            ERROR
        }

        public FrameType GetFrameType() 
        {
            return _frameType;
        }
    }
}
