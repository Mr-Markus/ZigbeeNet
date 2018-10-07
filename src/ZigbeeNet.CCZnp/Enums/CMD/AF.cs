using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC
{
    public enum AF {
        register = 0,
        dataRequest = 1,
        dataRequestExt = 2,
        dataRequestSrcRtg = 3,
        delete = 4,
        interPanCtl = 16,
        dataStore = 17,
        dataRetrieve = 18,
        apsfConfigSet = 19,
        apsfConfigGet = 20,
        dataConfirm = 128,
        incomingMsg = 129,
        incomingMsgExt = 130,
        reflectError = 131
    }
}
