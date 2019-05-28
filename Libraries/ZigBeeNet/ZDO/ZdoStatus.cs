using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZDO
{
    public enum ZdoStatus : byte
    {
        UNKNOWN = 0xFF,

        /// <summary>
         /// Completed successfully
         /// </summary>
        SUCCESS = 0x00,

        /// <summary>
         /// The supplied request type was invalid.
         /// </summary>
        INV_REQUESTTYPE = 0x80,
        /// <summary>
         /// The requested device did not exist on a device following a child descriptor request to a parent.
         /// </summary>
        DEVICE_NOT_FOUND = 0x81,
        /// <summary>
         /// The supplied endpoint was equal to 0x00 or between 0xf1 and 0xff.
         /// </summary>
        INVALID_EP = 0x82,
        /// <summary>
         /// The requested endpoint is not described by a simple descriptor.
         /// </summary>
        NOT_ACTIVE = 0x83,
        /// <summary>
         /// The requested optional feature is not supported on the target device.
         /// </summary>
        NOT_SUPPORTED = 0x84,
        /// <summary>
         /// A timeout has occurred with the requested operation.
         /// </summary>
        TIMEOUT = 0x85,
        /// <summary>
         /// The end device bind request was unsuccessful due to a failure to match any suitable clusters.
         /// </summary>
        NO_MATCH = 0x86,
        /// <summary>
         /// The unbind request was unsuccessful due to the coordinator or source device not having an entry in its binding
         /// table to unbind.
         /// </summary>
        NO_ENTRY = 0x88,
        /// <summary>
         /// A child descriptor was not available following a discovery request to a parent.
         /// </summary>
        NO_DESCRIPTOR = 0x89,
        /// <summary>
         /// The device does not have storage space to support the requested operation.
         /// </summary>
        INSUFFICIENT_SPACE = 0x8A,
        /// <summary>
         /// The device is not in the proper state to support the requested operation.
         /// </summary>
        NOT_PERMITTED = 0x8B,
        /// <summary>
         /// The device does not have table space to support the operation.
         /// </summary>
        TABLE_FULL = 0x8C,
        /// <summary>
         /// The permissions configuration table on the target indicates that the request is not authorized from this device.
         /// </summary>
        NOT_AUTHORIZED = 0x8D
    }
}
