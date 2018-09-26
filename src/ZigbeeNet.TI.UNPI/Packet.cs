using BinarySerialization;
using System;

namespace ZigbeeNet.TI.UNPI
{
    /// <summary>
    /// TI Unified NPI Packet Format
    /// SOF(1) + Length(2/1) + Type/Sub(1) + Cmd(1) + Payload(N) + FCS(1)
    ///  
    /// Source: http://processors.wiki.ti.com/index.php/Unified_Network_Processor_Interface
    /// </summary>
    public class Packet
    {

        public Packet()
        {

        }

        public Packet(MessageTypes type, SubSystems subSystem, byte commandId, byte[] payload)
        {
            Type = type;
            SubSystem = subSystem;
            Cmd1 = commandId;
            Payload = payload;
        }

        /// <summary>
        /// Start of Frame(SOF) is set to be 0xFE (254)
        /// </summary>
        [FieldOrder(0)]
        public byte SOF => 0xfe;

        /// <summary>
        /// Length field is 2 bytes long in little-endian format (so LSB first).
        /// </summary>
        [FieldOrder(1)]
        public ushort Length { get; set; }

        [Ignore()]
        public MessageTypes Type { get; set; }

        [Ignore()]
        public SubSystems SubSystem { get; set; }

        /// <summary>
        /// CMD0 is a 1 byte field that contains both message type and subsystem information 
        /// Bits[8-6]: Message type, see the message type section for more info.
        /// Bits[5-1]: Subsystem ID field, used to help NPI route the message to the appropriate place.
        /// 
        /// Source: http://processors.wiki.ti.com/index.php/NPI_Type_SubSystem
        /// </summary>
        public byte Cmd0
        {
            get
            {
                return (byte)((byte)(((byte)Type << 5) & 0xE0) | (byte)(((byte)SubSystem) & 0x1F));
            }
        }

        /// <summary>
        /// CMD1 is a 1 byte field that contains the opcode of the command being sent
        /// </summary>
        [FieldOrder(3)]
        public byte Cmd1 { get; set; }

        /// <summary>
        /// Payload is a variable length field that contains the parameters defined by the 
        /// command that is selected by the CMD1 field. The length of the payload is defined by the length field.
        /// </summary>
        [FieldOrder(4)]
        [FieldLength(nameof(Length))]
        [FieldChecksum(nameof(FrameCheckSequence), Mode = ChecksumMode.Xor)]
        public byte[] Payload { get; set; }

        /// <summary>
        /// Frame Check Sequence (FCS) is calculated by doing a XOR on each bytes of the frame in the order they are 
        /// send/receive on the bus (the SOF byte is always excluded from the FCS calculation): 
        ///     FCS = LEN_LSB XOR LEN_MSB XOR D1 XOR D2...XOR Dlen
        /// </summary>
        [FieldOrder(5)]
        public byte FrameCheckSequence
        {
            get
            {
                byte[] preBuffer = new byte[5];

                //TODO: Check if length is two bytes long
                byte[] lengthBytes = BitConverter.GetBytes(Payload.Length);
                preBuffer[0] = lengthBytes[0]; //(byte)(packet.Length >> 8);
                preBuffer[1] = lengthBytes[1]; //(byte)(packet.Length  & 0xff);

                preBuffer[2] = (byte)Type;
                preBuffer[3] = (byte)SubSystem;

                //Do not include sof in fcs calculation
                return checksum(preBuffer, Payload);
            }
        }

        private byte checksum(byte[] buf1, byte[] buf2)
        {
            var fcs = (byte)0x00;
            var buf1_len = buf1.Length;
            var buf2_len = buf2.Length;

            for (int i = 0; i < buf1_len; i += 1)
            {
                fcs ^= buf1[i];
            }

            if (buf2 != null)
            {
                for (int i = 0; i < buf2_len; i += 1)
                {
                    fcs ^= buf2[i];
                }
            }

            return fcs;
        }
    }
}
