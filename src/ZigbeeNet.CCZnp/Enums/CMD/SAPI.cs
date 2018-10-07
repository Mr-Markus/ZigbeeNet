using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC
{
    public enum SAPI {
        systemReset = 9,
        startRequest = 0,
        bindDevice = 1,
        allowBind = 2,
        sendDataRequest = 3,
        readConfiguration = 4,
        writeConfiguration = 5,
        getDeviceInfo = 6,
        findDeviceRequest = 7,
        permitJoiningRequest = 8,
        startConfirm = 128,
        bindConfirm = 129,
        allowBindConfirm = 130,
        sendDataConfirm = 131,
        findDeviceConfirm = 133,
        receiveDataIndication = 135
    }
}
