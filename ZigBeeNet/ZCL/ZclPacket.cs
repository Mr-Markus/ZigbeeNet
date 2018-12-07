using BinarySerialization;
using System;
using System.IO;

namespace ZigBeeNet.ZCL
{
    public class ZclPacket
    {
        public ZclPacket(byte cmdId)
        {
            Header = new ZclHeader();
        }

        public ZclPacket(ZclHeader header)
        {
            Header = header;
        }

        public ZclPacket(ZclHeader header, byte[] payload)
        {
            Header = header;
            Payload = payload;
        }

        [FieldOrder(0)]
        public ZclHeader Header { get; set; }

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
