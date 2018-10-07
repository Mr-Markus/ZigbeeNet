using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC
{
    public enum UTIL
    {
        getDeviceInfo = 0,
        getNvInfo = 1,
        setPanid = 2,
        setChannels = 3,
        setSeclevel = 4,
        setPrecfgkey = 5,
        callbackSubCmd = 6,
        keyEvent = 7,
        timeAlive = 9,
        ledControl = 10,
        testLoopback = 16,
        dataReq = 17,
        srcMatchEnable = 32,
        srcMatchAddEntry = 33,
        srcMatchDelEntry = 34,
        srcMatchCheckSrcAddr = 35,
        srcMatchAckAllPending = 36,
        srcMatchCheckAllPending = 37,
        addrmgrExtAddrLookup = 64,
        addrmgrNwkAddrLookup = 65,
        apsmeLinkKeyDataGet = 68,
        apsmeLinkKeyNvIdGet = 69,
        assocCount = 72,
        assocFindDevice = 73,
        assocGetWithAddress = 74,
        apsmeRequestKeyCmd = 75,
        zclKeyEstInitEst = 128,
        zclKeyEstSign = 129,
        syncReq = 224,
        zclKeyEstablishInd = 225,
        gpioSetDirection = 20,
        gpioRead = 21,
        gpioWrite = 22,
        srngGen = 76,
        bindAddEntry = 77
    }
}
