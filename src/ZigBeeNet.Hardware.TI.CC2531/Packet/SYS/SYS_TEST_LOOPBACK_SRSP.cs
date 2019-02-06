using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.SYS
{
    public class SYS_TEST_LOOPBACK_SRSP : ZToolPacket
    {
        public byte[] TestData;

        public SYS_TEST_LOOPBACK_SRSP()
        {

        }

        public SYS_TEST_LOOPBACK_SRSP(byte[] framedata)
        {
            this.TestData = new byte[framedata.Length];
            this.TestData = framedata;
            BuildPacket(new DoubleByte((ushort)ZToolCMD.SYS_TEST_LOOPBACK_SRSP), framedata);
        }
    }
}
