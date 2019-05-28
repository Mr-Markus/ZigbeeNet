using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ZigBeeNet
{
    public class ExtendedPanId
    {
        public byte[] PanId { get; private set; }

        public ulong Value
        {
            get
            {
                return BitConverter.ToUInt64(PanId, 0);
            }
        }

        /// <summary>
        /// Default constructor. Creates a PAN Id of 0
        /// </summary>
        public ExtendedPanId()
        {
            PanId = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        }

        /// <summary>
        /// Create an <see cref="ExtendedPanId"> from a <see cref="BigInteger">
        ///
        /// <param name="panId">the panId as a <see cref="long"></param>
        /// </summary>
        public ExtendedPanId(long panId)
        {
            PanId = new byte[8];

            long longVal = panId;

            PanId[0] = (byte)(longVal & 0xff);
            PanId[1] = (byte)((longVal >> 8) & 0xff);
            PanId[2] = (byte)((longVal >> 16) & 0xff);
            PanId[3] = (byte)((longVal >> 24) & 0xff);
            PanId[4] = (byte)((longVal >> 32) & 0xff);
            PanId[5] = (byte)((longVal >> 40) & 0xff);
            PanId[6] = (byte)((longVal >> 48) & 0xff);
            PanId[7] = (byte)((longVal >> 56) & 0xff);
        }

        /// <summary>
        /// Create an <see cref="ExtendedPanId"> from a <see cref="String"> defined in hexadecimal notation.
        ///
        /// <param name="panId">the panId as a <see cref="String"></param>
        /// </summary>
        public ExtendedPanId(string panId)
            : this(long.Parse(panId, System.Globalization.NumberStyles.HexNumber))
        {

        }

        /// <summary>
        /// Create an <see cref="ExtendedPanId"> from an int array
        ///
        /// <param name="panId">the panId as an int array. Array length must be 8.</param>
        /// @throws InvalidParameterException
        /// </summary>
        public ExtendedPanId(byte[] panId)
        {
            if (panId == null)
            {
                PanId = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
                return;
            }

            if (panId.Length != 8)
            {
                throw new ArgumentException("ExtendedPanId array length must be 8");
            }

            PanId = panId;//panId.Take(8).ToArray();
        }



        /// <summary>
        /// Check if the ExtendedPanId is valid. This checks the length of the ID, and checks
        /// it is not 0000000000000000 or FFFFFFFFFFFFFFFF.
        ///
        /// <returns>true if the extended PAN ID is valid</returns>
        /// </summary>
        public bool IsValid()
        {
            if (PanId == null || PanId.Length != 8)
            {
                return false;
            }

            int cnt0 = 0;
            int cntF = 0;
            foreach (int val in PanId)
            {
                if (val == 0x00)
                {
                    cnt0++;
                }
                if (val == 0xFF)
                {
                    cntF++;
                }
            }

            return !(cnt0 == 8 || cntF == 8);
        }

        public override int GetHashCode()
        {
            int prime = 31;
            int result = 1;
            result = prime * result + Util.Hash.CalcHashCode(PanId); // TODO: Or just PanId.GetHashCode() ?
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
            if (GetType() != obj.GetType())
            {
                return false;
            }

            ExtendedPanId other = (ExtendedPanId)obj;

            return PanId.SequenceEqual(other.PanId);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            for (int cnt = 7; cnt >= 0; cnt--)
            {
                builder.Append(PanId[cnt].ToString("X2"));
            }

            return builder.ToString();
        }
    }
}
