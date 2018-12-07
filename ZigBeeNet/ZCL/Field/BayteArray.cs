using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZigBeeNet.ZCL.Field
{
    public class ByteArray
    {
        /**
         * The base byte array.
         */
        private byte[] _value;

        /**
         * Constructor taking part of an existing integer array
         *
         * @param payload the existing integer array
         * @param from the start offset of the array (inclusive)
         * @param to the end offset of the array (exclusive)
         */
        public ByteArray(byte[] payload, int start, int finish)
        {
            _value = new byte[finish - start];
            int outCnt = 0;
            for (int cnt = start; cnt < finish; cnt++)
            {
                _value[outCnt++] = (byte)(payload[cnt] & 0xFF);
            }
        }

        /**
         * Constructor taking an existing integer array
         *
         * @param payload the existing integer array
         * @param from the start offset of the array (inclusive)
         * @param to the end offset of the array (exclusive)
         */
        public ByteArray(byte[] payload)
            : this(payload, 0, payload.Length)
        {
        }

        /**
         * Gets the byte array value.
         *
         * @return the value
         */
        public byte[] Get()
        {
            return _value;
        }

        /**
         * Gets the byte array as an array of integers
         *
         * @return the integer array
         */
        public int[] GetAsIntArray()
        {
            int[] intArray = new int[_value.Length];
            for (int cnt = 0; cnt < _value.Length; cnt++)
            {
                intArray[cnt] = _value[cnt] & 0xFF;
            }

            return intArray;
        }

        /**
         * Get the length of the underlying byte array
         *
         * @return the length of the data in the array
         */
        public int Size()
        {
            return _value.Length;
        }

        /**
         * Sets the byte array value.
         *
         * @param value the value as a byte array
         */
        public void Set(byte[] value)
        {
            this._value = value;
        }

        /**
         * Sets the byte array value from an integer array.
         *
         * @param value the value as an integer array
         */
        public void Set(int[] value)
        {
            this._value = new byte[value.Length];
            int outCnt = 0;
            foreach (int intValue in value)
            {
                this._value[outCnt++] = (byte)(intValue & 0xFF);
            }
        }

        public override int GetHashCode()
        {
            int prime = 31;
            int result = 1;
            result = prime * result + _value.GetHashCode();
            return result;
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }
            if (obj == null)
            {
                return false;
            }
            if (this.GetType() != obj.GetType())
            {
                return false;
            }
            ByteArray other = (ByteArray)obj;
            return _value.SequenceEqual(other._value);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("ByteArray [value=");
            bool first = true;
            foreach (byte val in _value)
            {
                if (!first)
                {
                    builder.Append(' ');
                }
                first = false;
                builder.Append(string.Format("{0}2X", val & 0xFF));
            }
            builder.Append(']');

            return builder.ToString();
        }

    }
}
