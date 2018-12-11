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

        public ulong Value {
            get
            {
                return BitConverter.ToUInt64(PanId, 0);
            }
        }

        /**
         * Default constructor. Creates a PAN Id of 0
         */
        public ExtendedPanId()
        {
            PanId = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        }

        /**
         * Create an {@link ExtendedPanId} from a {@link BigInteger}
         *
         * @param panId the panId as a {@link BigInteger}
         */
        public ExtendedPanId(BigInteger panId)
        {
            PanId = new byte[8];

            long longVal = (long)panId;

            PanId[0] = (byte)(longVal & 0xff);
            PanId[1] = (byte)((longVal >> 8) & 0xff);
            PanId[2] = (byte)((longVal >> 16) & 0xff);
            PanId[3] = (byte)((longVal >> 24) & 0xff);
            PanId[4] = (byte)((longVal >> 32) & 0xff);
            PanId[5] = (byte)((longVal >> 40) & 0xff);
            PanId[6] = (byte)((longVal >> 48) & 0xff);
            PanId[7] = (byte)((longVal >> 56) & 0xff);
        }

        /**
         * Create an {@link ExtendedPanId} from a {@link String} defined in hexadecimal notation.
         *
         * @param panId the panId as a {@link String}
         */
        public ExtendedPanId(string panId)
            : this(BigInteger.Parse(panId, System.Globalization.NumberStyles.HexNumber))
        {

        }

        /**
         * Create an {@link ExtendedPanId} from an int array
         *
         * @param panId the panId as an int array. Array length must be 8.
         * @throws InvalidParameterException
         */
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



        /**
         * Check if the ExtendedPanId is valid. This checks the length of the ID, and checks
         * it is not 0000000000000000 or FFFFFFFFFFFFFFFF.
         *
         * @return true if the extended PAN ID is valid
         */
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
