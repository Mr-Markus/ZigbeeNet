﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.ZDO
{
    public enum ZdoCommand : byte
    {
        nwkAddrReq = 0,
        ieeeAddrReq = 1,
        nodeDescReq = 2,
        powerDescReq = 3,
        simpleDescReq = 4,
        activeEpReq = 5,
        matchDescReq = 6,
        complexDescReq = 7,
        userDescReq = 8,
        endDeviceAnnce = 10,
        userDescSet = 11,
        serverDiscReq = 12,
        endDeviceBindReq = 32,
        bindReq = 33,
        unbindReq = 34,
        setLinkKey = 35,
        removeLinkKey = 36,
        getLinkKey = 37,
        nwkDiscoveryReq = 38,
        joinReq = 39,
        mgmtNwkDiscReq = 48,
        mgmtLqiReq = 49,
        mgmtRtgReq = 50,
        mgmtBindReq = 51,
        mgmtLeaveReq = 52,
        mgmtDirectJoinReq = 53,
        mgmtPermitJoinReq = 54,
        mgmtNwkUpdateReq = 55,
        msgCbRegister = 62,
        msgCbRemove = 63,
        startupFromApp = 64,
        autoFindDestination = 65,
        nwkAddrRsp = 128,
        ieeeAddrRsp = 129,
        nodeDescRsp = 130,
        powerDescRsp = 131,
        simpleDescRsp = 132,
        activeEpRsp = 133,
        matchDescRsp = 134,
        complexDescRsp = 135,
        userDescRsp = 136,
        userDescConf = 137,
        serverDiscRsp = 138,
        endDeviceBindRsp = 160,
        bindRsp = 161,
        unbindRsp = 162,
        mgmtNwkDiscRsp = 176,
        mgmtLqiRsp = 177,
        mgmtRtgRsp = 178,
        mgmtBindRsp = 179,
        mgmtLeaveRsp = 180,
        mgmtDirectJoinRsp = 181,
        mgmtPermitJoinRsp = 182,
        stateChangeInd = 192,
        endDeviceAnnceInd = 193,
        matchDescRspSent = 194,
        statusErrorRsp = 195,
        srcRtgInd = 196,
        beacon_notify_ind = 197,
        joinCnf = 198,
        nwkDiscoveryCnf = 199,
        leaveInd = 201,
        setRejoinParametersReq = 204,
        msgCbIncoming = 255,
        endDeviceTimeoutReq = 13,
        sendData = 40,
        nwkAddrOfInterestReq = 41,
        secAddLinkKey = 66,
        secEntryLookupExt = 67,
        secDeviceRemove = 68,
        extRouteDisc = 69,
        extRouteCheck = 70,
        extRemoveGroup = 71,
        extRemoveAllGroup = 72,
        extFindAllGroupsEndpoint = 73,
        extFindGroup = 74,
        extAddGroup = 75,
        extCountAllGroups = 76,
        extRxIdle = 77,
        extUpdateNwkKey = 78,
        extSwitchNwkKey = 79,
        extNwkInfo = 80,
        extSecApsRemoveReq = 81,
        forceConcentratorChange = 82,
        extSetParams = 83,
        tcDeviceInd = 202,
        permitJoinInd = 203
    }
}