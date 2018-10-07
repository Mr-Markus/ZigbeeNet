using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC
{
    public enum SYS  {
        resetReq = 0,
        ping = 1,
        version = 2,
        setExtAddr = 3,
        getExtAddr = 4,
        ramRead = 5,
        ramWrite = 6,
        osalNvItemInit = 7,
        osalNvRead = 8,
        osalNvWrite = 9,
        osalStartTimer = 10,
        osalStopTimer = 11,
        random = 12,
        adcRead = 13,
        gpio = 14,
        stackTune = 15,
        setTime = 16,
        getTime = 17,
        osalNvDelete = 18,
        osalNvLength = 19,
        setTxPower = 20,
        jammerParameters = 21,
        snifferParameters = 22,
        zdiagsInitStats = 23,
        zdiagsClearStats = 24,
        zdiagsGetStats = 25,
        zdiagsRestoreStatsNv = 26,
        zdiagsSaveStatsToNv = 27,
        osalNvReadExt = 28,
        osalNvWriteExt = 29,
        nvCreate = 48,
        nvDelete = 49,
        nvLength = 50,
        nvRead = 51,
        nvWrite = 52,
        nvUpdate = 53,
        nvCompact = 54,
        resetInd = 128,
        osalTimerExpired = 129,
        jammerInd = 130
    }
}
