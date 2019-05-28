using System;

namespace ZigBeeNet.Hardware.Digi.XBee.Test.Internal.Protocol
{
    public class XBeeFrameBaseTest
    {
        protected int[] GetPacketData(string stringData)
        {
            string[] split = stringData.Split(" ");

            int[] response = new int[split.Length];
            int cnt = 0;
            foreach (string val in split)
            {
                response[cnt++] = Convert.ToInt32(val, 16);
            }

            return response;
        }
    }
}
