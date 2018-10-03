using BinarySerialization;
using System;

namespace ZigbeeNet.ZCL
{
    public class Frame
    {
        public Frame(FrameHeader header)
        {
            Header = header;
        }

        public Frame(FrameHeader header, byte[] payload)
        {
            Header = header;
            Payload = payload;
        }

        [FieldOrder(0)]
        [FieldLength(5)]
        public FrameHeader Header { get; set; }

        [FieldOrder(1)]
        public byte[] Payload { get; set; }
    }
}
