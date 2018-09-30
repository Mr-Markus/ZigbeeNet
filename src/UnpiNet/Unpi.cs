using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Text;
using BinarySerialization;

namespace UnpiNet
{
    /// <summary>
    /// The unpi is the packet builder and parser for Texas Instruments Unified Network Processor Interface (UNPI) 
    /// used in RF4CE, BluetoothSmart, and ZigBee wireless SoCs. As stated in TI's wiki page:
    ///     TI's Unified Network Processor Interface (NPI) is used for establishing a serial data link between a TI SoC and 
    ///     external MCUs or PCs. This is mainly used by TI's network processor solutions.
    /// 
    /// The UNPI packet consists of sof, length, cmd0, cmd1, payload, and fcs fields.The description of each field 
    /// can be found in Unified Network Processor Interface.
    /// 
    /// It is noted that UNPI defines the length field with 2 bytes wide, but some SoCs use NPI in their real transmission (physical layer), 
    /// the length field just occupies a single byte. (The length field will be normalized to 2 bytes in the transportation layer of NPI stack.)
    /// 
    /// Source: http://processors.wiki.ti.com/index.php/Unified_Network_Processor_Interface?keyMatch=Unified%20Network%20Processor%20Interface&tisearch=Search-EN-Support
    /// 
    /// /*************************************************************************************************/
    /// /*** TI Unified NPI Packet Format                                                              ***/
    /// /***     SOF(1) + Length(2/1) + Type/Sub(1) + Cmd(1) + Payload(N) + FCS(1)                     ***/
    /// /*************************************************************************************************/
    /// </summary>
    public class Unpi
    {
        public SerialPort Port { get; set; }
        public int LenBytes;

        public event EventHandler<Packet> DataReceived;
        public event EventHandler<SerialErrorReceivedEventArgs> ErrorReceived;

        public event EventHandler Opened;
        public event EventHandler Closed;

        /// <summary>
        /// Create a new instance of the Unpi class.
        /// </summary>
        /// <param name="lenBytes">1 or 2 to indicate the width of length field. Default is 2.</param>
        /// <param name="stream">The transceiver instance, i.e. serial port, spi. It should be a duplex stream.</param>
        public Unpi(string port, int baudrate = 115200, int lenBytes = 2)
        {
            Port = new SerialPort(port, baudrate);

            Port.DataReceived += Port_DataReceived;
            Port.ErrorReceived += Port_ErrorReceived;

            LenBytes = lenBytes;

            Open();
        }

        private void Port_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            ErrorReceived(sender, e);
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] data = new byte[Port.BytesToRead];
            Port.Read(data, 0, Port.BytesToRead);

            Receive(data);
        }

        public void Open()
        {
            if (Port != null)
            {
                Port.Open();

                Opened(this, EventArgs.Empty);
            } else
            {
                throw new NullReferenceException("Port is not created");
            }
        }

        public void Close()
        {
            if (Port.IsOpen)
            {
                Port.Close();

                Closed(this, EventArgs.Empty);
            }
        }

        public byte[] Send(int type, int subSystem, byte commandId, byte[] payload)
        {
            return Send((MessageType)type, (SubSystem)subSystem, commandId, payload);
        }

        public byte[] Send(MessageType type, SubSystem subSystem, byte commandId, byte[] payload)
        {
            Packet packet = new Packet(type, subSystem, commandId, payload);

            return Send(packet);
        }

        public byte[] Send(Packet packet)
        {
            byte[] preBuffer = BuildPreBuffer(packet.Payload.Length, packet.Cmd0, packet.Cmd1);

            packet.FrameCheckSequence = checksum(preBuffer, packet.Payload);

            var stream = new MemoryStream();
            var serializer = new BinarySerializer();

            serializer.Serialize(stream, packet);

            stream.Flush();

            if(Port.IsOpen == false)
            {
                Port.Open();
            }

            byte[] data = stream.ToArray();
            //Port.Write(data, 0, data.Length);

            return stream.ToArray();
        }

        public Packet Receive(byte[] buffer)
        {
            if(buffer == null || buffer.Length == 0)
            {
                throw new NullReferenceException("Buffer is empty");
            }

            if(buffer[0] != 0xfe)
            {
                throw new FormatException("Buffer is not a vailid frame");
            }

            Packet packet = new Packet();
            
            if (LenBytes == 1)
            {
                packet.LenBytes = 1;
                packet.LengthByte = buffer[1];
                packet.Length = buffer[1];

                packet.Payload = new byte[packet.Length];
                Array.Copy(buffer, packet.Payload, packet.Length);

                packet.Type = (MessageType)(byte)(buffer[2] & 0xe0);
                packet.SubSystem = (SubSystem)(byte)(buffer[2] & 0x1f);
                packet.Cmd1 = buffer[3];

                packet.FrameCheckSequence = buffer[4 + packet.Length + 1];
            }
            else if (LenBytes == 2)
            {
                packet.LenBytes = 2;
                packet.LengthUShort = BitConverter.ToUInt16(new byte[] { buffer[1], buffer[2] }, 0);
                packet.Length = packet.LengthUShort;

                packet.Payload = new byte[packet.Length];
                Array.Copy(buffer, packet.Payload, packet.Length);

                packet.Type = (MessageType)(byte)(buffer[3] & 0xe0);
                packet.SubSystem = (SubSystem)(byte)(buffer[3] & 0x1f);
                packet.Cmd1 = buffer[4];

                packet.FrameCheckSequence = buffer[5 + packet.Length + 1];
            }

            byte[] preBuffer = BuildPreBuffer(packet.Length, packet.Cmd0, packet.Cmd1);

            byte csum = checksum(preBuffer, packet.Payload);
            
                if(packet.FrameCheckSequence.Equals(csum) == false)
            {
                throw new Exception("Received FCS is not equal with new packet");
            }

            // forward data to stream
            //Stream.Write(buffer, 0, buffer.Length);

            DataReceived(this, packet);

            return packet;
        }

        private byte[] BuildPreBuffer(int length, byte cmd0, byte cmd1)
        {
            if (LenBytes == 1)
            {
                byte[] preBuffer = new byte[LenBytes + 2];

                preBuffer[0] = (byte)length;
                preBuffer[1] = cmd0;
                preBuffer[2] = cmd1;

                return preBuffer;
            }
            else if (LenBytes == 2)
            {
                byte[] preBuffer = new byte[LenBytes + 2];

                //TODO: Check if length is two bytes long
                byte[] lengthBytes = BitConverter.GetBytes((ushort)length);
                preBuffer[0] = lengthBytes[0]; //(byte)(packet.Length >> 8);
                preBuffer[1] = lengthBytes[1]; //(byte)(packet.Length  & 0xff);

                preBuffer[2] = cmd0;
                preBuffer[3] = cmd1;

                return preBuffer;
            } else
            {
                throw new ArgumentOutOfRangeException("Length field can only be 1 or 2 bytes long");
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
