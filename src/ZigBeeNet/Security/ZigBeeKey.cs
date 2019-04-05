using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Security
{
    /// <summary>
 /// Represents a 128 bit ZigBee key
 ///
 /// @author Chris Jackson
 ///
 /// </summary>
    public class ZigBeeKey
    {
        public byte[] Key { get; set; }
        public byte IncomingFrameCounter { get; set; }
        public byte OutgoingFrameCounter { get; set; }
        public byte SequenceNumber { get; set; }
        public IeeeAddress address { get; set; }

        /// <summary>
         /// Default constructor. Creates a network key of 0
         /// </summary>
        public ZigBeeKey()
        {
            Key = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        }

        /// <summary>
         /// Create an <see cref="ZigBeeKey"> from a <see cref="String">. The string must contain 32 hexadecimal numbers
         /// to make up a 16 byte key. Numbers can be formatted in various ways -:
         /// <ul>
         /// <li>1234ABCD ...
         /// <li>12 34 AB CD ...
         /// <li>12,34,AB,CD ...
         /// <li>12:34:AB:CD ...
         /// <li>0x12 0x34 0xAB 0xCD ...
         /// <li>0x12,0x34,0xAB,0xCD ...
         /// <li>0x12:0x34:0xAB:0xCD ...
         /// </ul>
         ///
         /// <param name="key">the key as a <see cref="String"></param>
         /// @throws IllegalArgumentException
         /// </summary>
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

            Key = new byte[16];
            char[] enc = hexString.ToCharArray();
            for (int i = 0; i < enc.Length; i += 2)
            {
                StringBuilder curr = new StringBuilder(2);
                curr.Append(enc[i]).Append(enc[i + 1]);
                Key[i / 2] = byte.Parse(curr.ToString());
            }
        }

        /// <summary>
         /// Create a <see cref="ZigBeeKey"> from an int array
         ///
         /// <param name="key">the key as an int array. Array length must be 16.</param>
         /// @throws IllegalArgumentException
         /// </summary>
        public ZigBeeKey(byte[] key)
        {
            if (key.Length != 16)
            {
                throw new ArgumentException("NetworkKey array length must be 16 hex bytes");
            }

            Key = new byte[key.Length];

            Array.Copy(key, Key, 16);
        }
      
        /// <summary>
         /// Returns true if this key has an address associated with it
         ///
         /// <returns>true if this key has an address associated with it</returns>
         /// </summary>
        public bool HasAddress()
        {
            return address != null;
        }

        /// <summary>
         /// Check if the NetworkKey is valid. This checks the length of the ID, and checks
         /// it is not 00000000000000000000000000000000.
         ///
         /// <returns>true if the key is valid</returns>
         /// </summary>
        public bool IsValid()
        {
            if (Key == null || Key.Length != 16)
            {
                return false;
            }

            int cnt0 = 0;
            foreach (byte val in Key)
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
            result = prime * result + Key.GetHashCode();
            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if(obj is ZigBeeKey zKey)
            {
                return Key.Equals(zKey.Key);
            }

            return false;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            for (int cnt = 0; cnt < 16; cnt++)
            {
                builder.Append(Key[cnt].ToString("x2"));
            }

            return builder.ToString();
        }

        /// <summary>
         /// Create a <see cref="ZigBeeKey"> with a random key
         ///
         /// <returns><see cref="ZigBeeKey"> containing a random 128 bit key</returns>
         /// </summary>
        public static ZigBeeKey CreateRandom()
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
