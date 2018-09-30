using BinarySerialization;
using System;

namespace UnpiNet
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

        public Packet(MessageType type, SubSystem subSystem, byte commandId, byte[] payload)
        {
            Type = type;
            SubSystem = subSystem;
            Cmd1 = commandId;
            Payload = payload;
        }

        [Ignore()]
        public int LenBytes { get; set; }

        [Ignore()]
        public int Length { get; set; }

        /// <summary>
        /// Start of Frame(SOF) is set to be 0xFE (254)
        /// </summary>
        [FieldOrder(0)]
        public byte SOF => 0xfe;

        /// <summary>
        /// Length field is 2 bytes long in little-endian format (so LSB first).
        /// </summary>
        [FieldOrder(1)]
        [SerializeWhen(nameof(LenBytes), 2)]
        public ushort LengthUShort { get; set; }

        /// <summary>
        /// Length field is 2 bytes long in little-endian format (so LSB first).
        /// </summary>
        [FieldOrder(2)]
        [SerializeWhen(nameof(LenBytes), 1)]
        public byte LengthByte { get; set; }

        [Ignore()]
        public MessageType Type { get; set; }

        [Ignore()]
        public SubSystem SubSystem { get; set; }

        /// <summary>
        /// CMD0 is a 1 byte field that contains both message type and subsystem information 
        /// Bits[8-6]: Message type, see the message type section for more info.
        /// Bits[5-1]: Subsystem ID field, used to help NPI route the message to the appropriate place.
        /// 
        /// Source: http://processors.wiki.ti.com/index.php/NPI_Type_SubSystem
        /// </summary>
        [FieldOrder(3)]
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
        [FieldOrder(4)]
        public byte Cmd1 { get; set; }

        /// <summary>
        /// Payload is a variable length field that contains the parameters defined by the 
        /// command that is selected by the CMD1 field. The length of the payload is defined by the length field.
        /// </summary>
        [FieldOrder(5)]
        [FieldLength(nameof(Length))]
        [FieldChecksum(nameof(FrameCheckSequence), Mode = ChecksumMode.Xor)]
        public byte[] Payload { get; set; }

        /// <summary>
        /// Frame Check Sequence (FCS) is calculated by doing a XOR on each bytes of the frame in the order they are 
        /// send/receive on the bus (the SOF byte is always excluded from the FCS calculation): 
        ///     FCS = LEN_LSB XOR LEN_MSB XOR D1 XOR D2...XOR Dlen
        /// </summary>
        [FieldOrder(6)]
        public byte FrameCheckSequence { get; set; }
    }
}
