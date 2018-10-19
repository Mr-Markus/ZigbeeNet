using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.MAC
{
    public enum MacCommand {
        resetReq = 1,
        init = 2,
        startReq = 3,
        syncReq = 4,
        dataReq = 5,
        associateReq = 6,
        disassociateReq = 7,
        getReq = 8,
        setReq = 9,
        scanReq = 12,
        pollReq = 13,
        purgeReq = 14,
        setRxGainReq = 15,
        securityGetReq = 48,
        securitySetReq = 49,
        associateRsp = 80,
        orphanRsp = 81,
        syncLossInd = 128,
        associateInd = 129,
        associateCnf = 130,
        beaconNotifyInd = 131,
        dataCnf = 132,
        dataInd = 133,
        disassociateInd = 134,
        disassociateCnf = 135,
        orphanInd = 138,
        pollCnf = 139,
        scanCnf = 140,
        commStatusInd = 141,
        startCnf = 142,
        rxEnableCnf = 143,
        purgeCnf = 144
    }
}
