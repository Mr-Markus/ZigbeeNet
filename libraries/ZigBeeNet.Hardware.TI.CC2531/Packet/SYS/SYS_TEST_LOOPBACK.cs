using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SYS
{
    public class SYS_TEST_LOOPBACK : ZToolPacket
    {
        public byte[] TestData;

        public SYS_TEST_LOOPBACK()
        {

        }

        public SYS_TEST_LOOPBACK(byte[] buffer1)
        {
            this.TestData = new byte[buffer1.Length];
            this.TestData = buffer1;
            byte[] framedata = new byte[buffer1.Length];
            framedata = this.TestData;
            BuildPacket(new DoubleByte((ushort)ZToolCMD.SYS_TEST_LOOPBACK), framedata);
        }
    }
}
