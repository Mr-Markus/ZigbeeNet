using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet
{
    public enum SubSystem : byte
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
}
