using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZigBeeNet.Extensions;
using ZigBeeNet.Hardware.TI.CC2531.Util;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet
{
    public class ZToolPacket
    {
        public const int PAYLOAD_START_INDEX = 4;

        public const int START_BYTE = 0xFE;

        public enum CommandType : byte
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

        public enum CommandSubsystem : byte
        {
            RES = 0x00,
            SYS = 0x01,
            MAC = 0x02,
            NWK = 0x03,
            AF = 0x04,
            ZDO = 0x05,
            SAPI = 0x06,
            UTIL = 0x07,
            DBG = 0x08,
            APP = 0x09,
            RCAF = 0x0a,
            RCN = 0x0b,
            RCN_CLIENT = 0x0c,
            BOOT = 0x0d,
            ZIPTEST = 0x0e,
            DEBUG = 0x0f,
            PERIPHERALS = 0x10,
            NFC = 0x11,
            PB_NWK_MGR = 0x12,
            PB_GW = 0x13,
            PB_OTA_MGR = 0x14,
            BLE_SPNP = 0x15,
            BLE_HCI = 0x16,
            RESV01 = 0x17,
            RESV02 = 0x18,
            RESV03 = 0x19,
            RESV04 = 0x1a,
            RESV05 = 0x1b,
            RESV06 = 0x1c,
            RESV07 = 0x1d,
            RESV08 = 0x1e,
            SRV_CTR = 0x1f
        }

        public byte[] Packet { get; set; }

        public int LEN { get; set; }

        public DoubleByte CMD { get; set; }

        public bool Error { get; set; } = false;

        public byte FCS { get; set; }

        public string ErrorMsg { get; set; }

        public CommandType Type
        {
            get
            {
                return (CommandType)((Packet[2] & 0x60) >> 5);
            }
        }

        public CommandSubsystem Subsystem
        {
            get
            {
                return (CommandSubsystem)(Packet[2] & 0x1F);
            }
        }

        /// <summary>
        /// I started off using bytes but quickly realized that java bytes are signed, so effectively only 7 bits.
        /// We should be able to use int instead.
        ///
        /// </summary> // PROTECTED?
        public ZToolPacket()
        {
        }

        // PROTECTED?
        public ZToolPacket(DoubleByte ApiId, byte[] frameData)
        {
            BuildPacket(ApiId, frameData);
        }

        public void BuildPacket(DoubleByte ApiId, byte[] frameData)
        {
            // packet size is start byte + len byte + 2 cmd bytes + data + checksum byte
            Packet = new byte[frameData.Length + 5];
            Packet[0] = START_BYTE;

            // note: if checksum is not correct, XBee won't send out packet or return error. ask me how I know.
            // checksum is always computed on pre-escaped packet
            Checksum checksum = new Checksum();
            // Packet length does not include escape bytes
            LEN = frameData.Length;
            Packet[1] = (byte)LEN;
            checksum.AddByte(Packet[1]);
            // msb Cmd0 -> Type & Subsystem
            Packet[2] = ApiId.Msb;
            checksum.AddByte(Packet[2]);
            // lsb Cmd1 -> PROFILE_ID_HOME_AUTOMATION
            Packet[3] = ApiId.Lsb;
            checksum.AddByte(Packet[3]);
            CMD = ApiId;
            // data
            for (int i = 0; i < frameData.Length; i++)
            {
                if (!ByteUtils.IsByteValue(frameData[i]))
                {
                    throw new Exception("Value is greater than one byte: " + frameData[i] + " (" + string.Format("{0:X}", frameData[i]) + ")");
                }
                Packet[PAYLOAD_START_INDEX + i] = frameData[i];
                checksum.AddByte(Packet[PAYLOAD_START_INDEX + i]);
            }
            // set last byte as checksum
            FCS = checksum.Value;
            Packet[Packet.Length - 1] = FCS;
        }

        /// <summary>
        /// Gets a hex dump of the packet data
        ///
        /// <returns><see cref="String"> containing the packet data</returns>
        /// </summary>
        public string PacketString
        {
            get
            {
                StringBuilder builder = new StringBuilder();

                bool first = true;
                foreach (byte value in Packet)
                {
                    if (!first)
                    {
                        builder.Append(' ');
                    }
                    first = false;
                    builder.Append(value.ToString("X2"));
                }
                return builder.ToString();
            }
        }

        public ushort CommandId
        {
            get
            {
                return ByteHelper.ShortFromBytes(Packet, 2, 3);
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("Packet: subsystem=")
               .Append(Subsystem)
               .Append(", length=")
               .Append(LEN)
               .Append(", apiId=")
               .Append(ByteUtils.ToBase16(CMD.Msb))
               .Append(" ")
               .Append(ByteUtils.ToBase16(CMD.Lsb))
               .Append(", data=")
               .Append(ByteUtils.ToBase16(Packet))
               .Append(", checksum=")
               .Append(ByteUtils.ToBase16(FCS))
               .Append(", error=")
               .Append(Error);
            if (Error)
            {
                builder.Append(", errorMessage=");
                builder.Append(ErrorMsg);
            }

            return builder.ToString();
        }
    }
}
