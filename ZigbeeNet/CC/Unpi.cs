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
    public enum UnpiMessageType : byte
    {
        POLL = 0x00,
        /// <summary>
        /// Synchronous Messages A Synchronous Request (SREQ) is a frame, defined by data content instead of 
        /// the ordering of events of the physical interface, which is sent from the Host to NP where the 
        /// next frame sent from NP to Host must be the Synchronous Response (SRESP) to that SREQ. 
        /// 
        /// Note that once a SREQ is sent, the NPI interface blocks until a corresponding response(SRESP) is received.
        /// </summary>
        SREQ = 0x01,
        /// <summary>
        /// Asynchronous Messages Asynchronous Request – transfer initiated by Host Asynchronous Indication – transfer initiated by NP. 
        /// </summary>
        AREQ = 0x02,
        /// <summary>
        /// Synchronous Response
        /// </summary>
        SRSP = 0x03,
        RES0 = 0x04,
        RES1 = 0x05,
        RES2 = 0x06,
        RES3 = 0x07
    }

    public class SerialPacket
    {

        public static byte SOF = 0xfe;

        public SerialPacket()
        {

        }

        public byte Length => (byte)Payload.Length;

        public SerialPacket(UnpiMessageType type, UnpiSubSystem subSystem, byte commandId, byte[] payload = null)
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
                await stream.ReadAsyncExact(buffer, 2, length + 2);


                var type = (UnpiMessageType)(buffer[2] & 0xe0);
                var subsystem = (UnpiSubSystem)(buffer[2] & 0x1f);
                var cmd1 = buffer[3];
                var payload = buffer.Skip(4).Take(length - 2).ToArray();

                if (buffer.Skip(1).Take(buffer.Length - 2).Aggregate((byte)0xFF, (total, next) => (byte)(total ^ next)) != buffer[buffer.Length - 1])
                    throw new InvalidDataException("checksum error");

                return new SerialPacket(type, subsystem, cmd1, payload);
            }

            throw new InvalidDataException("unable to decode packet");
        }

        public UnpiMessageType Type { get; set; }

        public UnpiSubSystem SubSystem { get; set; }

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
                Type = (UnpiMessageType)(value & 0xE0);
                SubSystem = (UnpiSubSystem)(value & 0x1F);
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

    public enum UnpiSubSystem : byte
    {
        RPC_SYS_RES = 0x00,
        RPC_SYS_SYS = 0x01,
        RPC_SYS_MAC = 0x02,
        RPC_SYS_NWK = 0x03,
        RPC_SYS_AF = 0x04,
        RPC_SYS_ZDO = 0x05,
        RPC_SYS_SAPI = 0x06,
        RPC_SYS_UTIL = 0x07,
        RPC_SYS_DBG = 0x08,
        RPC_SYS_APP = 0x09,
        RPC_SYS_RCAF = 0x0a,
        RPC_SYS_RCN = 0x0b,
        RPC_SYS_RCN_CLIENT = 0x0c,
        RPC_SYS_BOOT = 0x0d,
        RPC_SYS_ZIPTEST = 0x0e,
        RPC_SYS_DEBUG = 0x0f,
        RPC_SYS_PERIPHERALS = 0x10,
        RPC_SYS_NFC = 0x11,
        RPC_SYS_PB_NWK_MGR = 0x12,
        RPC_SYS_PB_GW = 0x13,
        RPC_SYS_PB_OTA_MGR = 0x14,
        RPC_SYS_BLE_SPNP = 0x15,
        RPC_SYS_BLE_HCI = 0x16,
        RPC_SYS_RESV01 = 0x17,
        RPC_SYS_RESV02 = 0x18,
        RPC_SYS_RESV03 = 0x19,
        RPC_SYS_RESV04 = 0x1a,
        RPC_SYS_RESV05 = 0x1b,
        RPC_SYS_RESV06 = 0x1c,
        RPC_SYS_RESV07 = 0x1d,
        RPC_SYS_RESV08 = 0x1e,
        RPC_SYS_SRV_CTR = 0x1f
    }

    static partial class StreamExtensions
    {
        public static async Task ReadAsyncExact(this Stream stream, byte[] buffer, int offset, int count)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            var read = 0;
            while (read < count)
            {
                read += await stream.ReadAsync(buffer, offset + read, count - read);
            }
        }
    }



    public class Unpi 
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
        /// Create a new instance of the Unpi class.
        /// </summary>
        /// <param name="lenBytes">1 or 2 to indicate the width of length field. Default is 2.</param>
        /// <param name="stream">The transceiver instance, i.e. serial port, spi. It should be a duplex stream.</param>
        public Unpi(string port, int baudrate = 115200, int lenBytes = 2)
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
