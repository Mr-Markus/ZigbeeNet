/*
The MIT License (MIT)

Copyright (c) 2019 David Karlaš <david.karlas@gmail.com>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

using System;
namespace ZigbeeNet.Hardware.ConBee
{
    enum StatusCodes
    {
        SUCCESS,
        FAILURE,
        BUSY,
        TIMEOUT,
        UNSUPPORTED,
        ERROR,
        NO_NETWORK,
        INVALID_VALUE
    }

    enum NetworkStates
    {
        NET_OFFLINE,
        NET_JOINING,
        NET_CONNECTED,
        NET_LEAVING
    }

    enum Commands
    {
        DEVICE_STATE = 0x07,
        CHANGE_NETWORK_STATE = 0x08,
        READ_PARAMETER = 0x0A,
        WRITE_PARAMETER = 0x0B,
        DEVICE_STATE_CHANGED = 0x0E,
        VERSION = 0x0D,
        APS_DATA_REQUEST = 0x12,
        APS_DATA_CONFIRM = 0x04,
        APS_DATA_INDICATION = 0x17
    }

    enum Parameters
    {
        MacAddress = 0x01,
        NwkPanId = 0x05,
        NwkAddress = 0x07,
        NwkExtendedPanId = 0x08,
        ApsDesignedCoordinator = 0x09,
        ChannelMask = 0x0A,
        ApsExtendedPanId = 0x0B,
        TrustCenterAddress = 0x0E,
        SecurityMode = 0x10,
        NetworkKey = 0x18,
        CurrentChannel = 0x1C,
        ProtocolVersion = 0x22,
        NwkUpdateId = 0x24,
        WatchdogTTL = 0x26
    }

    enum DestinationAddressMode
    {
        GroupAddress = 0x01,
        NwkAddress = 0x02,
        IeeeAddress = 0x03,
        NwkAddressAndIeeeAddress = 0x04
    }
}
