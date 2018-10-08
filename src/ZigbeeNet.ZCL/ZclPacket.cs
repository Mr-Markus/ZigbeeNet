using BinarySerialization;
using System;
using System.IO;

namespace ZigbeeNet.ZCL
{
    public class ZclPacket
    {
        public ZclPacket()
        {

        }

        public ZclPacket(FrameHeader header)
        {
            Header = header;
        }

        public ZclPacket(FrameHeader header, byte[] payload)
        {
            Header = header;
            Payload = payload;
        }

        [FieldOrder(0)]
        [FieldLength(5)]
        public FrameHeader Header { get; set; }

        [FieldOrder(1)]
        public byte[] Payload { get; set; }

        public void Parse(byte[] data, Action callback = null)
        {
            var stream = new MemoryStream(data);
            var serializer = new BinarySerializer();

            ZclPacket zclPacket = serializer.Deserialize<ZclPacket>(stream);

            Header = zclPacket.Header;
            Payload = zclPacket.Payload;
        }
    }
}
