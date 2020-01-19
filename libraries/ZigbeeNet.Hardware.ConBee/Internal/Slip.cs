/*
The MIT License (MIT)

Copyright (c) 2017 Marcin Borowicz <marcinbor85@gmail.com>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

using System;

namespace ZigbeeNet.Hardware.ConBee
{
    class Slip
    {
        const byte SLIP_SPECIAL_BYTE_END = 0xC0;
        const byte SLIP_SPECIAL_BYTE_ESC = 0xDB;

        const byte SLIP_ESCAPED_BYTE_END = 0xDC;
        const byte SLIP_ESCAPED_BYTE_ESC = 0xDD;

        slip_state_t state;
        int size;
        Descriptor descriptor;

        enum slip_state_t
        {
            SLIP_STATE_NORMAL = 0x00,
            SLIP_STATE_ESCAPED
        }


        public enum slip_error_t
        {
            SLIP_NO_ERROR = 0x00,
            SLIP_ERROR_BUFFER_OVERFLOW,
            SLIP_ERROR_UNKNOWN_ESCAPED_BYTE
        }

        public struct Descriptor
        {
            public byte[] buf;
            public uint buf_size;

            public Action<byte[], int> recv_message;// (byte[] data, uint size);
            public Action<byte[], int, int> write_byte;
        }

        void reset_rx()
        {
            state = slip_state_t.SLIP_STATE_NORMAL;
            size = 0;
        }

        public Slip(Descriptor descriptor)
        {
            //TODO: Check descriptor values

            this.descriptor = descriptor;
            reset_rx();
        }

        slip_error_t put_byte_to_buffer(byte val)
        {
            slip_error_t error = slip_error_t.SLIP_NO_ERROR;

            if (this.size >= this.descriptor.buf_size)
            {
                error = slip_error_t.SLIP_ERROR_BUFFER_OVERFLOW;
                reset_rx();
            }
            else
            {
                this.descriptor.buf[this.size++] = val;
                this.state = slip_state_t.SLIP_STATE_NORMAL;
            }

            return error;
        }

        public slip_error_t slip_read_byte(byte val)
        {
            slip_error_t error = slip_error_t.SLIP_NO_ERROR;

            switch (this.state)
            {
                case slip_state_t.SLIP_STATE_NORMAL:
                    switch (val)
                    {
                        case SLIP_SPECIAL_BYTE_END:
                            if (this.size > 0)
                            {
                                this.descriptor.recv_message(
                                        this.descriptor.buf,
                                        this.size
                                        );
                            }
                            reset_rx();
                            break;
                        case SLIP_SPECIAL_BYTE_ESC:
                            this.state = slip_state_t.SLIP_STATE_ESCAPED;
                            break;
                        default:
                            error = put_byte_to_buffer(val);
                            break;
                    }
                    break;

                case slip_state_t.SLIP_STATE_ESCAPED:
                    switch (val)
                    {
                        case SLIP_ESCAPED_BYTE_END:
                            val = SLIP_SPECIAL_BYTE_END;
                            break;
                        case SLIP_ESCAPED_BYTE_ESC:
                            val = SLIP_SPECIAL_BYTE_ESC;
                            break;
                        default:
                            error = slip_error_t.SLIP_ERROR_UNKNOWN_ESCAPED_BYTE;
                            reset_rx();
                            break;
                    }

                    if (error != slip_error_t.SLIP_NO_ERROR)
                        break;

                    error = put_byte_to_buffer(val);
                    break;
            }

            return error;
        }

        void write_encoded_byte(byte val, byte[] data, ref int writeOffset)
        {
            switch (val)
            {
                case SLIP_SPECIAL_BYTE_END:
                    data[writeOffset++] = SLIP_SPECIAL_BYTE_ESC;
                    data[writeOffset++] = SLIP_ESCAPED_BYTE_END;
                    break;
                case SLIP_SPECIAL_BYTE_ESC:
                    data[writeOffset++] = SLIP_SPECIAL_BYTE_ESC;
                    data[writeOffset++] = SLIP_ESCAPED_BYTE_ESC;
                    break;
                default:
                    data[writeOffset++] = val;
                    break;
            }
        }

        public slip_error_t slip_send_message(byte[] data, int size)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            int i;
            byte val;
            var writeBuff = new byte[
                size * 2/*multiple by 2 if every char needs escaping*/+
                2/*END bytes*/];
            var writeOffset = 0;

            writeBuff[writeOffset++] = SLIP_SPECIAL_BYTE_END;


            for (i = 0; i < size; i++)
            {
                val = data[i];
                write_encoded_byte(val, writeBuff, ref writeOffset);
            }

            writeBuff[writeOffset++] = SLIP_SPECIAL_BYTE_END;
            descriptor.write_byte(writeBuff, 0, writeOffset);
            return slip_error_t.SLIP_NO_ERROR;
        }

    }
}
