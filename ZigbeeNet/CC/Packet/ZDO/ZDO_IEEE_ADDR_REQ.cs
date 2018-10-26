using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Packet.ZDO
{
    public class ZDO_IEEE_ADDR_REQ : SynchronousRequest
    {
        public ZAddress16 ShortAddress { get; set; }

        public RequestType ReqType { get; set; }

        public byte StartIndex { get; set; }

        public ZDO_IEEE_ADDR_REQ(ZAddress16 shortAddress, RequestType reqType, byte startIndex)
        {
            ShortAddress = shortAddress;
            ReqType = reqType;
            StartIndex = startIndex;

            List<byte> data = new List<byte>();
            data.AddRange(ShortAddress.ToByteArray());
            data.Add((byte)ReqType);
            data.Add(StartIndex);

            BuildPacket(CommandType.ZDO_IEEE_ADDR_REQ, data.ToArray());
        }

        public enum RequestType : byte
        {
            SingleDeviceResponse = 0x00,
            ExtendedIncludeAssociatedDevices  = 0x01
        }
    }
}
