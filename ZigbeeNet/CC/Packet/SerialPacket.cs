using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZigbeeNet.CC.Extensions;

namespace ZigbeeNet.CC.Packet
{
    public class SerialPacket
    {
        public static byte SOF = 0xfe;

        public byte Length { get; set; }

        public MessageType Type { get; set; }

        public SubSystem SubSystem { get; set; }

        public CommandType Cmd { get; set; }

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
                return (byte)(((int)Type << 5) | ((int)SubSystem));
            }
            private set
            {
                Type = (MessageType)(value >> 5);
                SubSystem = (SubSystem)(value & 0x1F);
            }
        }

        /// <summary>
        /// CMD1 is a 1 byte field that contains the opcode of the command being sent
        /// </summary>

        public byte Cmd1 { get; set; }

        /// <summary>
        /// Payload is a variable length field that contains the parameters defined by the 
        /// command that is selected by the CMD1 field. The length of the payload is defined by the length field.
        /// </summary>
        public byte[] Payload { get; set; }

        /// <summary>
        /// Frame Check Sequence (FCS) is calculated by doing a XOR on each bytes of the frame in the order they are 
        /// send/receive on the bus (the SOF byte is always excluded from the FCS calculation): 
        ///     FCS = LEN_LSB XOR LEN_MSB XOR D1 XOR D2...XOR Dlen
        /// </summary>
        public byte FrameCheckSequence { get; set; }

        /// <summary>
        /// Do not use! Should be handled internally
        /// </summary>
        /// <value>The checksum.</value>
        public byte Checksum { get; set; }

        public SerialPacket()
        {

        }

        public SerialPacket(CommandType commandType, byte[] data)
        {

        }

        public SerialPacket(MessageType type, SubSystem subSystem, byte commandId, byte[] payload = null)
        {
            Type = type;
            SubSystem = subSystem;
            Cmd1 = commandId;
            Payload = payload != null ? payload : new byte[0];
        }

        public async Task<byte[]> ToFrame()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                await WriteAsync(stream).ConfigureAwait(false);

                return stream.ToArray();
            }
        }

        public async Task WriteAsync(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            var buffer = new List<byte>();
            buffer.Add(SOF);
            buffer.Add((byte)Payload.Length);
            buffer.Add(Cmd0);
            buffer.Add(Cmd1);
            buffer.AddRange(Payload);
            buffer.Add(buffer.Skip(1).Aggregate((byte)0x00, (total, next) => total ^= next));

            await stream.WriteAsync(buffer.ToArray(), 0, buffer.Count);
        }

        public void BuildPacket(CommandType commandType, byte[] data)
        {
            Length = (byte)data.Length;

            Payload = new byte[Length];
            
            DoubleByte apiId = new DoubleByte((ushort)commandType);

            Cmd = commandType;
            Cmd0 = apiId.High;
            Cmd1 = apiId.Low;

            Payload = data.ToArray();

            FrameCheckSequence = CalcChecksum(Length, Cmd0, Cmd1, Payload);
        }

        public override string ToString()
        {
            return $"{{ SubSys: {SubSystem}, Type: {Type}, Cmd1: {Cmd1}, Length: {Length} }}";
        }

        
        
        public static byte CalcChecksum(byte length, byte cmd0, byte cmd1, byte[] payload)
        {
            var buffer = new List<byte>();
            buffer.Add(length);
            buffer.Add(cmd0);
            buffer.Add(cmd1);
            buffer.AddRange(payload);

            return buffer.Aggregate((byte)0x00, (total, next) => total ^= next);
        }
    }
}
