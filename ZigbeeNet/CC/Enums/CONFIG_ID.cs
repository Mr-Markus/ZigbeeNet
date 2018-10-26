﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC
{
    public enum CONFIG_ID : byte
    {
        ZCD_NV_STARTUP_OPTION = 0x03,
        ZCD_NV_LOGICAL_TYPE = 0x87,
        ZCD_NV_POLL_RATE = 0x24,
        ZCD_NV_QEUED_POL_RATE = 0x25,
        ZCD_NV_RESPONSE_POLL_RATE = 0x26,
        ZCD_NV_POLL_FAILURE_RETRIES = 0x29,
        ZCD_NV_INDIRECT_MSG_TIMEOUT = 0x2B,
        ZCD_NV_APS_FRAME_RETRIES = 0x43,
        ZCD_NV_APS_ACK_WAIT_DURATION = 0x44,
        ZCD_NV_BINDING_TIME = 0x46,
        ZCD_NV_USERDESC = 0x81,
        ZCD_NV_PANID = 0x83,
        ZCD_NV_CHANLIST = 0x84,
        ZCD_NV_PRECFGKEY = 0x62,
        ZCD_NV_PRECFGKEYS_ENABLE = 0x63,
        ZCD_NV_SECURITY_MODE = 0x64,
        ZCD_NV_BCAST_RETRIES = 0x2E,
        ZCD_NV_PASSIVE_ACK_TIMEOUT = 0x2F,
        ZCD_NV_BCAST_DELIVERY_TIME = 0x30,
        ZCD_NV_ROUTE_EXPIRY_TIME = 0x2C
    }
}
