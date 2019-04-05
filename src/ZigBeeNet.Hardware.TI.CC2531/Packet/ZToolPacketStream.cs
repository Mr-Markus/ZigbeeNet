using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZigBeeNet.Hardware.TI.CC2531.Util;
using ZigBeeNet.Extensions;
using ZigBeeNet.Hardware.TI.CC2531.Packet.AF;
using ZigBeeNet.Hardware.TI.CC2531.Packet.SimpleAPI;
using ZigBeeNet.Hardware.TI.CC2531.Packet.SYS;
using ZigBeeNet.Hardware.TI.CC2531.Packet.ZDO;
using Serilog;
using ZigBeeNet.Transport;
using ZigBeeNet.Hardware.TI.CC2531.Packet.UTIL;
using ZigBeeNet.Hardware.TI.CC2531.Extensions;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet
{
    public class ZToolPacketStream : IByteArrayInputStream
    {
        private int _length;

        private bool _generic = false;

        private IZigBeePort _port;

        public bool Done { get; private set; }

        public int BytesRead { get; set; }

        public Checksum Checksum { get; private set; } = new Checksum();

        public ZToolPacketStream(IZigBeePort port)
        {
            _port = port;
        }

        public ZToolPacket ParsePacket()
        {

            Exception exception;
            Done = false;
            BytesRead = 0;
            try
            {
                ZToolPacket response;
                // int byteLength = this.read("Length");
                _length = Read("Length");
                // log.debug("data length is " + ByteUtils.formatByte(length.getLength()));
                byte[] frameData;
                byte apiIdMSB = Read("API PROFILE_ID_HOME_AUTOMATION MSB");
                byte apiIdLSB = Read("API PROFILE_ID_HOME_AUTOMATION LSB");
                DoubleByte apiId = new DoubleByte(apiIdMSB, apiIdLSB);
                // TODO Remove generic never used
                // generic = true;
                if (_generic)
                {
                    // Log.Information("Parsing data as generic");
                    int i = 0;
                    frameData = new byte[_length];
                    // Read all data bytes without parsing
                    while (i < frameData.Length)
                    {
                        frameData[i] = Read("Data " + i + "-th");
                        i++;
                    }

                    response = new ZToolPacket(apiId, frameData);
                }
                else
                {
                    frameData = ReadRemainingBytes();
                    response = ParsePayload(apiId, frameData);
                }
                // response.setFCS(this.read("Checksum"));
                int fcs = Read("Checksum");
                // setDone(true);
                if (fcs != response.FCS)
                {
                    // log.debug("Checksum of packet failed: received =" + fcs + " expected = " + response.getFCS());
                    throw new ZToolParseException("Packet checksum failed");
                }
                if (!Done)
                {
                    // TODO this is not the answer!
                    throw new ZToolParseException("Packet stream is not finished yet we seem to think it is");
                }
                return response;
            }
            catch (Exception e)
            {
                Log.Error("Packet parsing failed due to exception.", e);
                exception = e;
            }
            ZToolPacket exceptionResponse = new ErrorPacket();

            if (exception != null)
            {
                exceptionResponse.Error = true;
                exceptionResponse.ErrorMsg = exception.Message;
            }

            return exceptionResponse;
        }


        public static async Task<ZToolPacket> ReadAsync(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            var buffer = new byte[1024];
            await stream.ReadAsyncExact(buffer, 0, 1);

            if (buffer[0] == 0xFE)
            {
                await stream.ReadAsyncExact(buffer, 1, 1);
                var length = buffer[1];
                await stream.ReadAsyncExact(buffer, 2, length + 3);

                var type = (ZToolPacket.CommandType)(buffer[2] >> 5 & 0x07);
                var subsystem = (ZToolPacket.CommandSubsystem)(buffer[2] & 0x1f);
                var cmd1 = buffer[3];
                var payload = buffer.Skip(4).Take(length).ToArray();

                if (buffer.Skip(1).Take(length + 3).Aggregate((byte)0x00, (total, next) => (byte)(total ^ next)) != buffer[length + 4])
                    throw new InvalidDataException("checksum error");

                DoubleByte cmd = new DoubleByte(buffer[3], buffer[2]);

                return ParsePayload(cmd, buffer.Skip(4).Take(length).ToArray());
            }

            throw new InvalidDataException("unable to decode packet");
        }

        public static ZToolPacket ParsePayload(DoubleByte cmd, byte[] payload)
        {
            switch ((ZToolCMD)cmd.Value)
            {
                case ZToolCMD.SYS_RESET_RESPONSE:
                    return new SYS_RESET_RESPONSE(payload);
                case ZToolCMD.SYS_VERSION_RESPONSE:
                    return new SYS_VERSION_RESPONSE(payload);
                case ZToolCMD.SYS_PING_RESPONSE:
                    return new SYS_PING_RESPONSE(payload);
                case ZToolCMD.SYS_RPC_ERROR:
                    return new SYS_RPC_ERROR(payload);
                case ZToolCMD.SYS_TEST_LOOPBACK_SRSP:
                    return new SYS_TEST_LOOPBACK_SRSP(payload);
                case ZToolCMD.AF_DATA_CONFIRM:
                    return new AF_DATA_CONFIRM(payload);
                case ZToolCMD.AF_DATA_SRSP:
                    return new AF_DATA_REQUEST_SRSP(payload);
                case ZToolCMD.AF_DATA_SRSP_EXT:
                    return new AF_DATA_SRSP_EXT(payload);
                case ZToolCMD.AF_INCOMING_MSG:
                    return new AF_INCOMING_MSG(payload);
                case ZToolCMD.AF_REGISTER_SRSP:
                    return new AF_REGISTER_SRSP(payload);
                case ZToolCMD.ZB_ALLOW_BIND_CONFIRM:
                    return new ZB_ALLOW_BIND_CONFIRM(payload);
                case ZToolCMD.ZB_ALLOW_BIND_RSP:
                    return new ZB_ALLOW_BIND_RSP(payload);
                case ZToolCMD.ZB_APP_REGISTER_RSP:
                    return new ZB_APP_REGISTER_RSP(payload);
                case ZToolCMD.ZB_BIND_CONFIRM:
                    return new ZB_BIND_CONFIRM(payload);
                case ZToolCMD.ZB_BIND_DEVICE_RSP:
                    return new ZB_BIND_DEVICE_RSP(payload);
                case ZToolCMD.ZB_FIND_DEVICE_CONFIRM:
                    return new ZB_FIND_DEVICE_CONFIRM(payload);
                case ZToolCMD.ZB_FIND_DEVICE_REQUEST_RSP:
                    return new ZB_FIND_DEVICE_REQUEST_RSP();
                case ZToolCMD.ZB_GET_DEVICE_INFO_RSP:
                    return new ZB_GET_DEVICE_INFO_RSP(payload);
                case ZToolCMD.ZB_PERMIT_JOINING_REQUEST_RSP:
                    return new ZB_PERMIT_JOINING_REQUEST_RSP(payload);
                case ZToolCMD.ZB_READ_CONFIGURATION_RSP:
                    return new ZB_READ_CONFIGURATION_RSP(payload);
                case ZToolCMD.ZB_RECEIVE_DATA_INDICATION:
                    return new ZB_RECEIVE_DATA_INDICATION(payload);
                case ZToolCMD.ZB_SEND_DATA_CONFIRM:
                    return new ZB_SEND_DATA_CONFIRM(payload);
                case ZToolCMD.ZB_SEND_DATA_REQUEST_RSP:
                    return new ZB_SEND_DATA_REQUEST_RSP(payload);
                case ZToolCMD.ZB_START_CONFIRM:
                    return new ZB_START_CONFIRM(payload);
                case ZToolCMD.ZB_START_REQUEST_RSP:
                    return new ZB_START_REQUEST_RSP(payload);
                case ZToolCMD.ZB_WRITE_CONFIGURATION_RSP:
                    return new ZB_WRITE_CONFIGURATION_RSP(payload);
                //case ZToolCMD.ZDO_ACTIVE_EP_REQ_SRSP:
                //    return new ZDO_ACTIVE_EP_REQ_SRSP(payload);
                //case ZToolCMD.ZDO_ACTIVE_EP_RSP:
                //    return new ZDO_ACTIVE_EP_RSP(payload);
                //case ZToolCMD.ZDO_BIND_REQ_SRSP:
                //    return new ZDO_BIND_REQ_SRSP(payload);
                //case ZToolCMD.ZDO_BIND_RSP:
                //    return new ZDO_BIND_RSP(payload);
                //case ZToolCMD.ZDO_END_DEVICE_ANNCE_IND:
                //    return new ZDO_END_DEVICE_ANNCE_IND(payload);
                //case ZToolCMD.ZDO_END_DEVICE_ANNCE_SRSP:
                //    return new ZDO_END_DEVICE_ANNCE_SRSP(payload);
                //case ZToolCMD.ZDO_END_DEVICE_BIND_REQ_SRSP:
                //    return new ZDO_END_DEVICE_BIND_REQ_SRSP(payload);
                //case ZToolCMD.ZDO_END_DEVICE_BIND_RSP:
                //    return new ZDO_END_DEVICE_BIND_RSP(payload);
                //case ZToolCMD.ZDO_IEEE_ADDR_REQ_SRSP:
                //    return new ZDO_IEEE_ADDR_REQ_SRSP(payload);
                //case ZToolCMD.ZDO_IEEE_ADDR_RSP:
                //    return new ZDO_IEEE_ADDR_RSP(payload);
                //case ZToolCMD.ZDO_MATCH_DESC_REQ_SRSP:
                //    return new ZDO_MATCH_DESC_REQ_SRSP(payload);
                //case ZToolCMD.ZDO_MATCH_DESC_RSP:
                //    return new ZDO_MATCH_DESC_RSP(payload);
                //case ZToolCMD.ZDO_MGMT_LEAVE_REQ_SRSP:
                //    return new ZDO_MGMT_LEAVE_REQ_SRSP(payload);
                //case ZToolCMD.ZDO_MGMT_LEAVE_RSP:
                //    return new ZDO_MGMT_LEAVE_RSP(payload);
                //case ZToolCMD.ZDO_MGMT_LQI_REQ_SRSP:
                //    return new ZDO_MGMT_LQI_REQ_SRSP(payload);
                //case ZToolCMD.ZDO_MGMT_LQI_RSP:
                //    return new ZDO_MGMT_LQI_RSP(payload);
                //case ZToolCMD.ZDO_MGMT_NWK_UPDATE_REQ_SRSP:
                //    return new ZDO_MGMT_NWK_UPDATE_REQ_SRSP(payload);
                //case ZToolCMD.ZDO_MGMT_PERMIT_JOIN_REQ_SRSP:
                //    return new ZDO_MGMT_PERMIT_JOIN_REQ_SRSP(payload);
                //case ZToolCMD.ZDO_MGMT_PERMIT_JOIN_RSP:
                //    return new ZDO_MGMT_PERMIT_JOIN_RSP(payload);
                //case ZToolCMD.ZDO_MGMT_RTG_RSP:
                //    return new ZDO_MGMT_RTG_RSP(payload);
                //case ZToolCMD.ZDO_MSG_CB_INCOMING:
                //    ZDO_MSG_CB_INCOMING incoming = new ZDO_MSG_CB_INCOMING(payload);
                //    return incoming.Translate();
                //case ZToolCMD.ZDO_NODE_DESC_REQ_SRSP:
                //    return new ZDO_NODE_DESC_REQ_SRSP(payload);
                //case ZToolCMD.ZDO_NODE_DESC_RSP:
                //    return new ZDO_NODE_DESC_RSP(payload);
                //case ZToolCMD.ZDO_POWER_DESC_REQ_SRSP:
                //    return new ZDO_POWER_DESC_REQ_SRSP(payload);
                //case ZToolCMD.ZDO_POWER_DESC_RSP:
                //    return new ZDO_POWER_DESC_RSP(payload);
                //case ZToolCMD.ZDO_NWK_ADDR_REQ_SRSP:
                //    return new ZDO_NWK_ADDR_REQ_SRSP(payload);
                //case ZToolCMD.ZDO_NWK_ADDR_RSP:
                //    return new ZDO_NWK_ADDR_RSP(payload);
                //case ZToolCMD.ZDO_SIMPLE_DESC_REQ_SRSP:
                //    return new ZDO_SIMPLE_DESC_REQ_SRSP(payload);
                //case ZToolCMD.ZDO_SIMPLE_DESC_RSP:
                //    return new ZDO_SIMPLE_DESC_RSP(payload);
                //case ZToolCMD.ZDO_TC_DEVICE_IND:
                //    return new ZDO_TC_DEVICE_IND(payload);
                //case ZToolCMD.ZDO_UNBIND_REQ_SRSP:
                //    return new ZDO_UNBIND_REQ_SRSP(payload);
                //case ZToolCMD.ZDO_UNBIND_RSP:
                //    return new ZDO_UNBIND_RSP(payload);
                //case ZToolCMD.ZDO_USER_DESC_REQ_SRSP:
                //    return new ZDO_USER_DESC_REQ_SRSP(payload);
                //case ZToolCMD.ZDO_USER_DESC_RSP:
                //    return new ZDO_USER_DESC_RSP(payload);
                //case ZToolCMD.ZDO_USER_DESC_CONF:
                //    return new ZDO_USER_DESC_CONF(payload);
                //case ZToolCMD.ZDO_USER_DESC_SET_SRSP:
                //    return new ZDO_USER_DESC_SET_SRSP(payload);
                case ZToolCMD.ZDO_STATE_CHANGE_IND:
                    return new ZDO_STATE_CHANGE_IND(payload);
                case ZToolCMD.ZDO_STATUS_ERROR_RSP:
                    return new ZDO_STATUS_ERROR_RSP(payload);
                case ZToolCMD.ZDO_MSG_CB_REGISTER_SRSP:
                    return new ZDO_MSG_CB_REGISTER_SRSP(payload);
                case ZToolCMD.ZDO_STARTUP_FROM_APP_SRSP:
                    return new ZDO_STARTUP_FROM_APP_SRSP(payload);
                case ZToolCMD.UTIL_SET_PANID_RESPONSE:
                    return new UTIL_SET_PANID_RESPONSE(payload);
                case ZToolCMD.UTIL_SET_CHANNELS_RESPONSE:
                    return new UTIL_SET_CHANNELS_RESPONSE(payload);
                case ZToolCMD.UTIL_GET_DEVICE_INFO_RESPONSE:
                    return new UTIL_GET_DEVICE_INFO_RESPONSE(payload);
                case ZToolCMD.UTIL_LED_CONTROL_RESPONSE:
                    return new UTIL_LED_CONTROL_RESPONSE(payload);
                default:
                    Log.Warning("Unknown command ID: {Command}", cmd);
                    return new ZToolPacket(cmd, payload);
            }
        }

        public byte Read(string context)
        {
            byte b = Read();
            Log.Verbose("Read {Context}  byte, val is {Byte}", context, b);
            return b;
        }

        /// <summary>
        /// TODO implement as class that extends inputstream?
        /// 
        /// This method reads bytes from the underlying input stream and performs the following tasks: keeps track of how
        /// many bytes we've read, un-escapes bytes if necessary and verifies the checksum.
        /// </summary>
        public byte Read()
        {

            byte? b = _port.Read();

            if (b == null)
            {
                throw new ZToolParseException("Read -1 from input stream while reading packet!");
            }

            BytesRead++;

            // when verifying checksum you must add the checksum that we are verifying
            // when computing checksum, do not include start byte; when verifying, include checksum
            Checksum.AddByte(b.Value);
            // log.debug("Read byte " + ByteUtils.formatByte(b) + " at position " + bytesRead + ", data length is " +
            // this.length.getLength() + ", #escapeBytes is " + escapeBytes + ", remaining bytes is " +
            // this.getRemainingBytes());

            if (GetFrameDataBytesRead() >= (_length + 1))
            {
                // this is checksum and final byte of packet
                Done = true;

                // log.debug("Checksum byte is " + b);
                ////
                /// if (!checksum.verify()) {/////////////Maybe expected in ZTool is 0x00, not FF//////////////////// throw
                /// new ZToolParseException("Checksum is incorrect.  Expected 0xff, but got " + checksum.getChecksum()); }
                /// </summary>
            }

            return b.Value;
        }

        // TODO remove it seems useless, we can replace with a reading of all the bytes of the payload
        private byte[] ReadRemainingBytes()
        {
            byte[] value = new byte[_length - GetFrameDataBytesRead()];

            for (int i = 0; i < value.Length; i++)
            {
                value[i] = Read("Remaining bytes " + (value.Length - i));
                // log.debug("byte " + i + " is " + value[i]);
            }

            return value;
        }

        /// <summary>
        /// Returns number of bytes remaining, relative to the stated packet length (not including checksum).
        ///
        /// <returns>number of bytes remaining to be read excluding checksum</returns>
        /// </summary>
        public int GetFrameDataBytesRead()
        {
            // subtract out the 1 length bytes and API PROFILE_ID_HOME_AUTOMATION 2 bytes
            return BytesRead - 3;
        }

        /// <summary>
        /// Number of bytes remaining to be read, including the checksum
        ///
        /// <returns>number of bytes remaining to be read including checksum</returns>
        /// </summary>
        public int GetRemainingBytes()
        {
            // add one for checksum byte (not included) in packet length
            return _length - GetFrameDataBytesRead() + 1;
        }

        // get unescaped packet length
        // get escaped packet length
    }
}
