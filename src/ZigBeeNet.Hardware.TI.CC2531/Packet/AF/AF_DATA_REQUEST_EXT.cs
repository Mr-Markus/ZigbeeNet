using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Extensions;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.AF
{
    public class AF_DATA_REQUEST_EXT : ZToolPacket
    {

        public AF_DATA_REQUEST_EXT(ushort groupdId, short srcEndPoint, int j, int k, byte bitmapOpt, byte radius,
                byte[] payload)
        {

            if (payload.Length > 230)
            {
                throw new ArgumentException("Payload is too big, maxium is 230");
            }

            byte[] framedata = new byte[payload.Length + 20];
            framedata[0] = 0x01; // Destination address mode 1 (group addressing)
            framedata[1] = groupdId.GetByte(0); // Source address
            framedata[2] = groupdId.GetByte(1); // Source address
            framedata[3] = 0x00; // Source address
            framedata[4] = 0x00; // Source address
            framedata[5] = 0x00; // Source address
            framedata[6] = 0x00; // Source address
            framedata[7] = 0x00; // Source address
            framedata[8] = 0x00; // Source address
            framedata[9] = 0x00; // Destination Endpoint
            framedata[10] = 0x00; // Destination PAN ID
            framedata[11] = 0x00; // Destination PAN ID
            framedata[12] = (byte)(srcEndPoint & 0xFF);
            framedata[13] = j.GetByte(0);
            framedata[14] = j.GetByte(1);
            framedata[15] = (byte)(k & 0xFF);
            framedata[16] = (byte)(bitmapOpt & 0xFF);
            framedata[17] = (byte)(radius & 0xFF);
            framedata[18] = payload.Length.GetByte(0);
            framedata[19] = payload.Length.GetByte(1);
            for (int i = 0; i < payload.Length; i++)
            {
                framedata[20 + i] = payload[i];
            }
            BuildPacket(new DoubleByte((ushort)ZToolCMD.AF_DATA_REQUEST_EXT), framedata);
        }
    }
}