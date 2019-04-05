using System;
using System.Collections.Generic;
using System.Text;
using Serilog;

namespace ZigBeeNet.Hardware.TI.CC2531.Util
{
    public class ByteUtils
    {
        private ByteUtils()
        {
        }

        /// <summary>
         /// There is a slight problem with this method that you might have noticed; a Java int is signed, so we can't make
         /// use of the 32nd bit. This means we this method does not support a four byte value with msb greater than 01111111
         /// ((2^7-1) or 127).
         /// <p/>
         /// TODO use long instead of int to support 4 bytes values. note that long assignments are not atomic.
         /// </summary>
        public static int ConvertMultiByteToInt(byte[] bytes)
        {

            if (bytes.Length > 4)
            {
                throw new FormatException("Value too big");
            }
            else if (bytes.Length == 4 && ((bytes[0] & 0x80) == 0x80))
            {
                // 0x80 == 10000000, 0x7e == 01111111
                throw new FormatException("Java int can't support a four byte value, with msb byte greater than 7e");
            }

            int val = 0;

            for (int i = 0; i < bytes.Length; i++)
            {

                if (bytes[i] > 0xFF)
                {
                    throw new FormatException("Values exceeds byte range: " + bytes[i]);
                }

                if (i == (bytes.Length - 1))
                {
                    val += bytes[i];
                }
                else
                {
                    val += bytes[i] << ((bytes.Length - i - 1) * 8);
                }
            }

            return val;
        }

        public static long ConvertMultiByteToLong(byte[] bytes)
        {

            if (bytes.Length > 8)
            {
                throw new ArgumentException("too many bytes can't be converted to long");
            }
            else if (bytes.Length == 8 && ((bytes[0] & 0x80) == 0x80))
            {
                // 0x80 == 10000000, 0x7e == 01111111
                throw new FormatException("Java long can't support a 8 bytes value, where msb byte greater than 7e");
            }

            long val = 0;

            for (int i = 0; i < bytes.Length; i++)
            {
                val += 0x000000FF & bytes[i];
                val = val << 8;
            }

            return val;
        }

        public static byte[] ConvertLongtoMultiByte(ulong val)
        {

            int size;

            if ((val >> 56) > 0)
            {
                size = 8;
            }
            else if ((val >> 48) > 0)
            {
                size = 7;
            }
            else if ((val >> 40) > 0)
            {
                size = 6;
            }
            else if ((val >> 32) > 0)
            {
                size = 5;
            }
            else if ((val >> 24) > 0)
            {
                size = 4;
            }
            else if ((val >> 16) > 0)
            {
                size = 3;
            }
            else if ((val >> 8) > 0)
            {
                size = 2;
            }
            else
            {
                size = 1;
            }

            byte[] data = new byte[size];

            for (int i = 0; i < size; i++)
            {
                data[i] = (byte)((val >> (size - i - 1) * 8) & 0xFF);
            }

            return data;
        }

        public static string ToBase16(byte[] arr)
        {
            return ToBase16(arr, 0, arr.Length);
        }

        private static string ToBase16(byte[] arr, int start, int end)
        {

            StringBuilder sb = new StringBuilder();

            for (int i = start; i < end; i++)
            {
                sb.Append(ToBase16(arr[i]));

                if (i < arr.Length - 1)
                {
                    sb.Append(" ");
                }
            }

            return sb.ToString();
        }

        private static string PadBase2(string s)
        {

            for (int i = s.Length; i < 8; i++)
            {
                s = "0" + s;
            }

            return s;
        }

        /// <summary>
         /// <param name="b">the int value to check if it contains a byte representable value</param>
         /// <returns>true</returns>
         /// </summary>
        public static bool IsByteValue(byte b)
        {
            bool valid = ((b & 0xffffff00) == 0 || (b & 0xffffff00) == 0xffffff00);
            
            return valid;
        }

        public static string ToBase16(byte b)
        {
            if (!IsByteValue(b))
            {
                throw new ArgumentException(
                        "Error converting " + b + " input value to hex string it is larger than a byte");
            }
            if (b < 0)
            {
                return string.Format("{0:X}", b).Substring(6).ToUpper();
            }
            else if (b < 0x10)
            {
                return "0" + string.Format("{0:X}", b).ToUpper();
            }
            else if (b >= 0x10)
            {
                return string.Format("{0:X}", b).ToUpper();
            }
            else
            {
                throw new ArgumentException("Unable to recognize the value " + b);
            }
        }

        private static string ToBase2(byte b)
        {

            if (b > 0xff)
            {
                throw new ArgumentException("input value is larger than a byte");
            }

            return PadBase2(string.Format("{0:X}", b));
        }

        public static string FormatByte(byte b)
        {
            return "base10=" + b + ",base16=" + ToBase16(b) + ",base2=" + ToBase2(b);
        }

    }
}
