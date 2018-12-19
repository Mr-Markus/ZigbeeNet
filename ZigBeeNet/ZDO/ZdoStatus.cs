using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZDO
{
    public enum ZdoStatus : byte
    {
        UNKNOWN = 0xFF,

        /**
         * Completed successfully
         */
        SUCCESS = 0x00,

        /**
         * The supplied request type was invalid.
         */
        INV_REQUESTTYPE = 0x80,
        /**
         * The requested device did not exist on a device following a child descriptor request to a parent.
         */
        DEVICE_NOT_FOUND = 0x81,
        /**
         * The supplied endpoint was equal to 0x00 or between 0xf1 and 0xff.
         */
        INVALID_EP = 0x82,
        /**
         * The requested endpoint is not described by a simple descriptor.
         */
        NOT_ACTIVE = 0x83,
        /**
         * The requested optional feature is not supported on the target device.
         */
        NOT_SUPPORTED = 0x84,
        /**
         * A timeout has occurred with the requested operation.
         */
        TIMEOUT = 0x85,
        /**
         * The end device bind request was unsuccessful due to a failure to match any suitable clusters.
         */
        NO_MATCH = 0x86,
        /**
         * The unbind request was unsuccessful due to the coordinator or source device not having an entry in its binding
         * table to unbind.
         */
        NO_ENTRY = 0x88,
        /**
         * A child descriptor was not available following a discovery request to a parent.
         */
        NO_DESCRIPTOR = 0x89,
        /**
         * The device does not have storage space to support the requested operation.
         */
        INSUFFICIENT_SPACE = 0x8A,
        /**
         * The device is not in the proper state to support the requested operation.
         */
        NOT_PERMITTED = 0x8B,
        /**
         * The device does not have table space to support the operation.
         */
        TABLE_FULL = 0x8C,
        /**
         * The permissions configuration table on the target indicates that the request is not authorized from this device.
         */
        NOT_AUTHORIZED = 0x8D
    }
}
