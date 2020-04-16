using System;
using System.IO;
using System.Linq;
using Xunit;
using ZigBeeNet.Hardware.TI.CC2531.Packet;
using ZigBeeNet.Hardware.TI.CC2531.Util;
using ZigBeeNet.Transport;

namespace ZigBeeNet.Hardware.TI.CC2531.Test
{
    public class Cc2351TestPacket
    {
        protected byte[] GetPacketData(string stringData)
        {
            string hex = stringData.Replace(" ", "");

            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        protected ZToolPacket GetPacket(string stringData)
        {
            byte[] packet = GetPacketData(stringData);

            byte[] byteArray = new byte[packet.Length - 1];
            for (int c = 1; c < packet.Length; c++)
            {
                byteArray[c - 1] = (byte)packet[c];
            }

            MemoryStream stream = new MemoryStream(byteArray);
            IZigBeePort port = new TestPort(stream, null);

            try
            {
                ZToolPacket ztoolPacket = new ZToolPacketStream(port).ParsePacket();

                Assert.False(ztoolPacket.Error);

                return ztoolPacket;
            }
            catch (IOException)
            {
                return null;
            }
        }


    }
}
