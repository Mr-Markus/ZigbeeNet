using System;

namespace ZigbeeNet
{
    public class Frame
    {
        public FrameHeader Header { get; set; }

        public byte[] Payload { get; set; }
    }
}
