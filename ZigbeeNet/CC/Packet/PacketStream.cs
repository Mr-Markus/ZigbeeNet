using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZigbeeNet.CC.Extensions;
using ZigbeeNet.CC.Packet.SimpleAPI;
using ZigbeeNet.CC.Packet.ZDO;
using ZigbeeNet.Logging;

namespace ZigbeeNet.CC.Packet
{
    public class PacketStream
    {
        private static readonly ILog _logger = LogProvider.For<PacketStream>();

        public static async Task<SerialPacket> ReadAsync(Stream stream)
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

                var type = (MessageType)(buffer[2] >> 5 & 0x07);
                var subsystem = (SubSystem)(buffer[2] & 0x1f);
                var cmd1 = buffer[3];
                var payload = buffer.Skip(4).Take(length).ToArray();

                if (buffer.Skip(1).Take(length + 3).Aggregate((byte)0x00, (total, next) => (byte)(total ^ next)) != buffer[length + 4])
                    throw new InvalidDataException("checksum error");

                DoubleByte cmd = new DoubleByte(buffer[3], buffer[2]);

                return ParsePayload((CommandType)cmd.Get16BitValue(), buffer.Skip(4).Take(length).ToArray());
            }

            throw new InvalidDataException("unable to decode packet");
        }

        public static SerialPacket ParsePayload(CommandType cmd, byte[] payload)
        {
            switch (cmd)
            {
                //case CommandType.SYS_RESET_RESPONSE:
                //    return new SYS_RESET_RESPONSE(payload);
                //case CommandType.SYS_VERSION_RESPONSE:
                //    return new SYS_VERSION_RESPONSE(payload);
                //case CommandType.SYS_PING_RESPONSE:
                //    return new SYS_PING_RESPONSE(payload);
                //case CommandType.SYS_RPC_ERROR:
                //    return new SYS_RPC_ERROR(payload);
                //case CommandType.SYS_TEST_LOOPBACK_SRSP:
                //    return new SYS_TEST_LOOPBACK_SRSP(payload);
                //case CommandType.AF_DATA_CONFIRM:
                //    return new AF_DATA_CONFIRM(payload);
                //case CommandType.AF_DATA_SRSP:
                //    return new AF_DATA_SRSP(payload);
                //case CommandType.AF_DATA_SRSP_EXT:
                //    return new AF_DATA_SRSP_EXT(payload);
                //case CommandType.AF_INCOMING_MSG:
                //    return new AF_INCOMING_MSG(payload);
                //case CommandType.AF_REGISTER_SRSP:
                //    return new AF_REGISTER_SRSP(payload);
                //case CommandType.ZB_ALLOW_BIND_CONFIRM:
                //    return new ZB_ALLOW_BIND_CONFIRM(payload);
                //case CommandType.ZB_ALLOW_BIND_RSP:
                //    return new ZB_ALLOW_BIND_RSP(payload);
                //case CommandType.ZB_APP_REGISTER_RSP:
                //    return new ZB_APP_REGISTER_RSP(payload);
                //case CommandType.ZB_BIND_CONFIRM:
                //    return new ZB_BIND_CONFIRM(payload);
                case CommandType.ZB_BIND_DEVICE_RSP:
                    return new ZB_BIND_DEVICE_RSP(payload);
                case CommandType.ZB_FIND_DEVICE_CONFIRM:
                    return new ZB_FIND_DEVICE_CONFIRM(payload);
                case CommandType.ZB_FIND_DEVICE_REQUEST_RSP:
                    return new ZB_FIND_DEVICE_REQUEST_RSP();
                case CommandType.ZB_GET_DEVICE_INFO_RSP:
                    return new ZB_GET_DEVICE_INFO_RSP(payload);
                case CommandType.ZB_PERMIT_JOINING_REQUEST_RSP:
                    return new ZB_PERMIT_JOINING_REQUEST_RSP(payload);
                case CommandType.ZB_READ_CONFIGURATION_RSP:
                    return new ZB_READ_CONFIGURATION_RSP(payload);
                case CommandType.ZB_RECEIVE_DATA_INDICATION:
                    return new ZB_RECEIVE_DATA_INDICATION(payload);
                case CommandType.ZB_SEND_DATA_CONFIRM:
                    return new ZB_SEND_DATA_CONFIRM(payload);
                case CommandType.ZB_SEND_DATA_REQUEST_RSP:
                    return new ZB_SEND_DATA_REQUEST_RSP(payload);
                case CommandType.ZB_START_CONFIRM:
                    return new ZB_START_CONFIRM(payload);
                case CommandType.ZB_START_REQUEST_RSP:
                    return new ZB_START_REQUEST_RSP(payload);
                case CommandType.ZB_WRITE_CONFIGURATION_RSP:
                    return new ZB_WRITE_CONFIGURATION_RSP(payload);

                #region not implemented but posible
                // case CommandType.ZDO_ACTIVE_EP_REQ_SRSP:
                // return new ZDO_ACTIVE_EP_REQ_SRSP(payload);
                // case CommandType.ZDO_ACTIVE_EP_RSP:
                // return new ZDO_ACTIVE_EP_RSP(payload);
                // case CommandType.ZDO_BIND_REQ_SRSP:
                // return new ZDO_BIND_REQ_SRSP(payload);
                // case CommandType.ZDO_BIND_RSP:
                // return new ZDO_BIND_RSP(payload);
                case CommandType.ZDO_END_DEVICE_ANNCE_IND:
                    return new ZDO_END_DEVICE_ANNCE_IND(payload);
                // case CommandType.ZDO_END_DEVICE_ANNCE_SRSP:
                // return new ZDO_END_DEVICE_ANNCE_SRSP(payload);
                // case CommandType.ZDO_END_DEVICE_BIND_REQ_SRSP:
                // return new ZDO_END_DEVICE_BIND_REQ_SRSP(payload);
                // case CommandType.ZDO_END_DEVICE_BIND_RSP:
                // return new ZDO_END_DEVICE_BIND_RSP(payload);
                // case CommandType.ZDO_IEEE_ADDR_REQ_SRSP:
                // return new ZDO_IEEE_ADDR_REQ_SRSP(payload);
                // case CommandType.ZDO_IEEE_ADDR_RSP:
                // return new ZDO_IEEE_ADDR_RSP(payload);
                // case CommandType.ZDO_MATCH_DESC_REQ_SRSP:
                // return new ZDO_MATCH_DESC_REQ_SRSP(payload);
                // case CommandType.ZDO_MATCH_DESC_RSP:
                // return new ZDO_MATCH_DESC_RSP(payload);
                // case CommandType.ZDO_MGMT_LEAVE_REQ_SRSP:
                // return new ZDO_MGMT_LEAVE_REQ_SRSP(payload);
                // case CommandType.ZDO_MGMT_LEAVE_RSP:
                // return new ZDO_MGMT_LEAVE_RSP(payload);
                // case CommandType.ZDO_MGMT_LQI_REQ_SRSP:
                // return new ZDO_MGMT_LQI_REQ_SRSP(payload);
                // case CommandType.ZDO_MGMT_LQI_RSP:
                // return new ZDO_MGMT_LQI_RSP(payload);
                // case CommandType.ZDO_MGMT_NWK_UPDATE_REQ_SRSP:
                // return new ZDO_MGMT_NWK_UPDATE_REQ_SRSP(payload);
                case CommandType.ZDO_MGMT_PERMIT_JOIN_REQ_SRSP:
                    return new ZDO_MGMT_PERMIT_JOIN_REQ_SRSP(payload);
                case CommandType.ZDO_MGMT_PERMIT_JOIN_RSP:
                    return new ZDO_MGMT_PERMIT_JOIN_RSP(payload);
                // case CommandType.ZDO_MGMT_RTG_RSP:
                // return new ZDO_MGMT_RTG_RSP(payload);
                // case CommandType.ZDO_MSG_CB_INCOMING:
                // ZDO_MSG_CB_INCOMING incoming = new ZDO_MSG_CB_INCOMING(payload);
                // return incoming.translate();
                case CommandType.ZDO_NODE_DESC_REQ_SRSP:
                    return new ZDO_NODE_DESC_REQ_SRSP(payload);
                case CommandType.ZDO_NODE_DESC_RSP:
                    return new ZDO_NODE_DESC_RSP(payload);
                // case CommandType.ZDO_POWER_DESC_REQ_SRSP:
                // return new ZDO_POWER_DESC_REQ_SRSP(payload);
                // case CommandType.ZDO_POWER_DESC_RSP:
                // return new ZDO_POWER_DESC_RSP(payload);
                // case CommandType.ZDO_NWK_ADDR_REQ_SRSP:
                // return new ZDO_NWK_ADDR_REQ_SRSP(payload);
                // case CommandType.ZDO_NWK_ADDR_RSP:
                // return new ZDO_NWK_ADDR_RSP(payload);
                // case CommandType.ZDO_SIMPLE_DESC_REQ_SRSP:
                // return new ZDO_SIMPLE_DESC_REQ_SRSP(payload);
                // case CommandType.ZDO_SIMPLE_DESC_RSP:
                // return new ZDO_SIMPLE_DESC_RSP(payload);
                // case CommandType.ZDO_TC_DEVICE_IND:
                // return new ZDO_TC_DEVICE_IND(payload);
                // case CommandType.ZDO_UNBIND_REQ_SRSP:
                // return new ZDO_UNBIND_REQ_SRSP(payload);
                // case CommandType.ZDO_UNBIND_RSP:
                // return new ZDO_UNBIND_RSP(payload);
                // case CommandType.ZDO_USER_DESC_REQ_SRSP:
                // return new ZDO_USER_DESC_REQ_SRSP(payload);
                // case CommandType.ZDO_USER_DESC_RSP:
                // return new ZDO_USER_DESC_RSP(payload);
                // case CommandType.ZDO_USER_DESC_CONF:
                // return new ZDO_USER_DESC_CONF(payload);
                // case CommandType.ZDO_USER_DESC_SET_SRSP:
                // return new ZDO_USER_DESC_SET_SRSP(payload);
                #endregion

                case CommandType.ZDO_STATE_CHANGE_IND:
                    return new ZDO_STATE_CHANGE_IND(payload);
                case CommandType.ZDO_STATUS_ERROR_RSP:
                    return new ZDO_STATUS_ERROR_RSP(payload);
                //case CommandType.ZDO_MSG_CB_REGISTER_SRSP:
                //    return new ZDO_MSG_CB_REGISTER_SRSP(payload);
                //case CommandType.ZDO_STARTUP_FROM_APP_SRSP:
                //    return new ZDO_STARTUP_FROM_APP_SRSP(payload);
                //case CommandType.UTIL_SET_PANID_RESPONSE:
                //    return new UTIL_SET_PANID_RESPONSE(payload);
                //case CommandType.UTIL_SET_CHANNELS_RESPONSE:
                //    return new UTIL_SET_CHANNELS_RESPONSE(payload);
                //case CommandType.UTIL_GET_DEVICE_INFO_RESPONSE:
                //    return new UTIL_GET_DEVICE_INFO_RESPONSE(payload);
                default:
                    _logger.Warn("Unknown command ID: {Command}", cmd);
                    throw new NotImplementedException();
                    return new SerialPacket(cmd, payload);
            }
        }
    }
}
