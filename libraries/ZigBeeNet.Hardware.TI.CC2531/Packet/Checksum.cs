using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet
{
    public class Checksum
    {

        // private final static Logger log = Logger.getLogger(Checksum.class);

        public byte Value { get; private set; }

        public Checksum()
        {
            Value = 0;
        }

        /// <summary>
        /// Don't add Checksum byte when computing checksum!!
        ///
        /// <param name="val"></param>
        /// </summary>
        public void AddByte(byte val)
        {
            // checksum+= val;
            Value = (byte)(Value ^ val);
        }
    }
}
