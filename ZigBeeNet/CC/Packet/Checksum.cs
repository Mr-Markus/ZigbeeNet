using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CC.Packet
{
    public class Checksum
    {

        // private final static Logger log = Logger.getLogger(Checksum.class);

        public byte Value { get; private set; }

        public Checksum()
        {
            Value = 0;
        }

        /**
         * Don't add Checksum byte when computing checksum!!
         *
         * @param val
         */
        public void AddByte(byte val)
        {
            // checksum+= val;
            Value = (byte)(Value ^ val);
        }
    }
}
