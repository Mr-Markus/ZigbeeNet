using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet
{
    /// <summary>
    /// Represents a 128 bit ZigBee key
    /// </summary>
    public class ZigBeeKey
    {
        public byte[] Value { get; private set; }

        public ZigBeeKey()
        {
            Value = new byte[16];
        }

        public ZigBeeKey(byte[] key)
        {
            if (key.Length != 16)
            {
                throw new ArgumentException("NetworkKey array length must be 16 hex bytes");
            }

            Array.Copy(key, Value, 16);
        }

        public ZigBeeKey(string keyString)
        {
            if (keyString == null)
            {
                throw new ArgumentException("Key string must not be null");
            }

            String hexString = keyString.Replace("0x", "");
            hexString = hexString.Replace(",", "");
            hexString = hexString.Replace(" ", "");

            if (hexString.Length != 32)
            {
                throw new ArgumentException("Key string must contain an array of 32 hexadecimal numbers");
            }

            Value = new byte[16];
            char[] enc = hexString.ToCharArray();
            for (int i = 0; i < enc.Length; i += 2)
            {
                StringBuilder curr = new StringBuilder(2);
                curr.Append(enc[i]).Append(enc[i + 1]);
                Value[i / 2] = byte.Parse(curr.ToString());
            }
        }

        public bool IsValid()
        {
            if (Value == null || Value.Length != 16)
            {
                return false;
            }

            int cnt0 = 0;
            foreach (byte val in Value)
            {
                if (val == 0x00)
                {
                    cnt0++;
                }
            }

            return (cnt0 != 16);
        }

        public override int GetHashCode()
        {
            int prime = 31;
            int result = 1;
            result = prime * result + Value.GetHashCode();
            return result;
        }

        public static ZigBeeKey createRandom()
        {
            byte[] key = new byte[16];
            for (int cnt = 0; cnt < 16; cnt++)
            {
                key[cnt] = (byte)Math.Floor((new Random().NextDouble() * 255));
            }

            return new ZigBeeKey(key);
        }
    }
}
