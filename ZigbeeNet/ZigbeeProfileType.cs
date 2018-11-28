using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet
{
    public enum ZigbeeProfileType
    {
        /// <summary>
        /// ZigBee Home Automation
        /// </summary>
        ZIGBEE_HOME_AUTOMATION = 0x0104,
        /// <summary>
        /// ZigBee Smart Energy
        /// </summary>
        ZIGBEE_SMART_ENERGY = 0x0109,
        /// <summary>
        /// ZigBee Green Power
        /// </summary>
        ZIGBEE_GREEN_POWER = 0xA10E,
        /// <summary>
        /// Manufacturer Telegesis
        /// </summary>
        MANUFACTURER_TELEGESIS = 0xC059,
        /// <summary>
        /// ZigBee Light Link
        /// </summary>
        ZIGBEE_LIGHT_LINK = 0xC05E,
        /// <summary>
        /// Manufacturer Digi
        /// </summary>
        MANUFACTURER_DIGI = 0xC105
    }
}
