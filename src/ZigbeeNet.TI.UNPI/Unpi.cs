using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BinarySerialization;

namespace ZigbeeNet.TI.UNPI
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
        private Stream _stream;

        /// <summary>
        /// Create a new instance of the Unpi class.
        /// </summary>
        /// <param name="lenBytes">1 or 2 to indicate the width of length field. Default is 2.</param>
        /// <param name="stream">The transceiver instance, i.e. serial port, spi. It should be a duplex stream.</param>
        public Unpi(int lenBytes, Stream stream)
        {
            //TODO: send stream directly to serial port or just write to stream and let caller send it to a serial port?
            _stream = stream;
        }

        public byte[] Send(Packet packet)
        {
            var stream = new MemoryStream();
            var serializer = new BinarySerializer();

            serializer.Serialize(stream, packet);

            stream.Flush();

            stream.CopyTo(_stream);

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

            ushort length = BitConverter.ToUInt16(new byte[] { buffer[1], buffer[2] }, 0);
            byte[] payload = new byte[length];
            Array.Copy(buffer, payload, length);

            Packet packet = new Packet()
            {
                Type = (MessageTypes)(byte)(buffer[3] & 0xe0),
                SubSystem = (SubSystems)(byte)(buffer[3] & 0x1f),
                Cmd1 = buffer[4],
                Payload = payload
            };

            byte receivedFcs = buffer[5 + length + 1];

            if(packet.FrameCheckSequence.Equals(receivedFcs) == false)
            {
                throw new Exception("Received FCS is not equal with new packet");
            }

            // forward data to stream
            _stream.Write(buffer, 0, buffer.Length);

            return packet;
        }
    }
}
