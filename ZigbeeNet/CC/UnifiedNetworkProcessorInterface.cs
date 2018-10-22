using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// TODO
// Files regaring UNPI is just cramped in here. Should not be tied too strict to
// the project as UNPI is intended to be used on other systems as well.
//
// SerialPacket could be abstracted to a general serialpacket and overloaded here to add the stuff we need
// to handle our MT CMD and queue stuff.... 

namespace ZigbeeNet.CC
{
    

    public class SerialPacket
    {

        public static byte SOF = 0xfe;

        public SerialPacket()
        {

        }

        public byte Length => (byte)Payload.Length;

        public SerialPacket(MessageType type, SubSystem subSystem, byte commandId, byte[] payload = null)
        {
            Type = type;
            SubSystem = subSystem;
            Cmd1 = commandId;
            Payload = payload != null ? payload : new byte[0];
        }

        public Task WriteAsync(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            var buffer = new List<byte>();
            buffer.Add(SOF);
            buffer.Add((byte)Payload.Length);
            buffer.Add(Cmd0);
            buffer.Add(Cmd1);
            buffer.AddRange(Payload);
            buffer.Add(buffer.Skip(1).Aggregate((byte)0xFF, (total, next) => total ^= next));

            return stream.WriteAsync(buffer.ToArray(), 0, buffer.Count);
        }

        public static async Task<SerialPacket> ReadAsync(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            var buffer = new byte[1024];
            await stream.ReadAsyncExact(buffer, 0, 1);

            if (buffer[0] == SOF)
            {
                await stream.ReadAsyncExact(buffer, 1, 1);
                var length = buffer[1];
                await stream.ReadAsyncExact(buffer, 2, length + 3);


                var type = (MessageType)(buffer[2] >> 5 & 0x07);
                var subsystem = (SubSystem)(buffer[2] & 0x1f);
                var cmd1 = buffer[3];
                var payload = buffer.Skip(4).Take(length - 2).ToArray();

                if (buffer.Skip(1).Take(length + 3).Aggregate((byte)0x00, (total, next) => (byte)(total ^ next)) != buffer[length + 4])
                    throw new InvalidDataException("checksum error");

                if (type == MessageType.SRSP)
                {
                    return new SynchronousResponse(subsystem, cmd1, payload);
                }
                else if (type == MessageType.SREQ)
                {
                    return new SynchronousRequest(subsystem, cmd1, payload);
                }
                else if (type == MessageType.AREQ)
                {
                    return new AsynchronousRequest(subsystem, cmd1, payload);
                }

                throw new InvalidDataException($"unknown message type: {type}");
            }

            throw new InvalidDataException("unable to decode packet");
        }

        public MessageType Type { get; set; }

        public SubSystem SubSystem { get; set; }

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
                Type = (MessageType)(value & 0xE0);
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
    }
    

    static partial class StreamExtensions
    {
        public static async Task ReadAsyncExact(this Stream stream, byte[] buffer, int offset, int count)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            if (buffer.Length == 0) throw new ArgumentException("Buffer length should not be 0");

            var read = 0;
            while (read < count)
            {
                read += await stream.ReadAsync(buffer, offset + read, count - read);
            }
        }
    }



    public class UnifiedNetworkProcessorInterface 
    {
        public SerialPort Port { get; set; }
        public int LenBytes;

        public Stream InputStream
        {
            get { return Port.BaseStream; }
        }

        public Stream OutputStream
        {
            get { return Port.BaseStream; }
        }

        /// <summary>
        /// Create a new instance of the unpi class.
        /// </summary>
        /// <param name="lenBytes">1 or 2 to indicate the width of length field. Default is 2.</param>
        /// <param name="stream">The transceiver instance, i.e. serial port, spi. It should be a duplex stream.</param>
        public UnifiedNetworkProcessorInterface(string port, int baudrate = 115200, int lenBytes = 2)
        {
            Port = new SerialPort(port, baudrate);

            LenBytes = lenBytes;
        }

        public void Open()
        {
            Port.Open();

            Port.DiscardInBuffer();
            Port.DiscardOutBuffer();
        }

        public void Close()
        {
            Port.Close();
        }


        /// <summary>
        /// Sending isn't really implmented, but the correct method is to put it on the transmit queue on CCZnp
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public byte[] Send(int areq, int zpiObjectSubSystem, byte zpiObjectCommandId, byte[] zpiObjectFrame)
        {
            throw new NotImplementedException();
        }
    }
}
