using System;
using System.Collections.Generic;
using System.Text;

namespace UnpiNet
{
    /// <summary>
    /// Source: http://processors.wiki.ti.com/index.php/NPI_Type_SubSystem
    /// </summary>
    public enum SubSystem
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
}
