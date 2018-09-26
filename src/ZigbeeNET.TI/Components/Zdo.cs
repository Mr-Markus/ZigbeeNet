//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace ZigbeeNet.TI.Components
//{
//    public class Zdo
//    {
//        private const string SUBSYSTEM = "ZDO";

//        private ZdoHelper _zdoHelper;
//        private Controller _controller;

//        public Zdo(Controller controller)
//        {
//            _zdoHelper = new ZdoHelper();
//            _controller = controller;
//        }

//        public void Request(string apiName, object valObj)
//        {
//            string requestType = _zdoHelper.GetRequestType(apiName);

//            if (requestType == "rspless")
//                this.rsplessRequest(apiName, valObj);
//            else if (requestType == "generic")
//                this.genericRequest(apiName, valObj);
//            else if (requestType == "concat")
//                this.concatRequest(apiName, valObj);
//            else if (requestType == "special")
//                this.specialRequest(apiName, valObj);
//            else
//                throw new Exception("Unknown request type.");
//        }

//        private void sendZdoRequestViaZnp(string apiName, object valObj)
//        {
//            _controller.Request(SUBSYSTEM, apiName, valObj);
//        }

//        private void rsplessRequest(string apiName, object valObj)
//        {
//            this.sendZdoRequestViaZnp(apiName, valObj);
//        }

//        private void genericRequest(string apiName, object valObj)
//        {
//            //var deferred = Q.defer(),
//            //    areq = this._areq,
//            //    areqEvtKey = zdoHelper.generateEventOfRequest(apiName, valObj);

//            //if (areqEvtKey)
//            //    areq.register(areqEvtKey, deferred, function(payload) {
//            //    areq.resolve(areqEvtKey, payload);
//            //});

//            //TODO: handle async requests like areq library

//            this.sendZdoRequestViaZnp(apiName, valObj);
//        }

//        private void specialRequest(string apiName, object valObj)
//        {
//            if (apiName == "serverDiscReq")
//            {
//                // broadcast, remote device may not response when no bits match in mask
//                // listener at controller.on("ZDO:serverDiscRsp")
//                this.rsplessRequest("serverDiscReq", valObj);
//            }
//            else if (apiName == "bindReq")
//            {
//                if (valObj.dstaddrmode == ZSC.AF.addressMode.ADDR_16BIT)
//                    throw new Exception("TI not support address 16bit mode.");
//                else
//                    this.genericRequest("bindReq", valObj);
//            }
//            else if (apiName == "mgmtPermitJoinReq")
//            {
//                if (valObj.dstaddr == 0xFFFC)  // broadcast to all routers (and coord), no waiting for AREQ rsp
//                    this.rsplessRequest("mgmtPermitJoinReq", valObj);
//                else
//                    this.genericRequest("mgmtPermitJoinReq", valObj);
//            }
//            else
//            {
//                throw new Exception("No such request.");
//            }
//        }

//        private concatRequest(string apiName, object valObj)
//        {
//            if (apiName == "nwkAddrReq" || apiName == "ieeeAddrReq")
//                return this._concatAddrRequest(apiName, valObj);
//            else if (apiName == "mgmtNwkDiscReq")
//                return this._concatListRequest(apiName, valObj, {
//                entries: "networkcount",
//            listcount: "networklistcount",
//            list: "networklist"
//                });
//    else if (apiName == "mgmtLqiReq")
//                return this._concatListRequest(apiName, valObj, {
//                entries: "neighbortableentries",
//            listcount: "neighborlqilistcount",
//            list: "neighborlqilist"
//                });
//    else if (apiName == "mgmtRtgReq")
//                return this._concatListRequest(apiName, valObj, {
//                entries: "routingtableentries",
//            listcount: "routingtablelistcount",
//            list: "routingtablelist"
//                });
//    else if (apiName == "mgmtBindRsp")
//                return this._concatListRequest(apiName, valObj, {
//                entries: "bindingtableentries",
//            listcount: "bindingtablelistcount",
//            list: "bindingtablelist"
//                });
//    else
//        throw new Exception("No such request.");
//        }

//    }

//    private void concatAddrRequest(string apiName, object valObj)
//    {
//        int totalToGet = 0;
//        int accum = 0;
//        int nextIndex = valObj.startindex;
//        var reqObj = new {
//            reqtype = valObj.reqtype,
//            startindex = valObj.startindex    // start from 0
//        };
//        var finalRsp = new {
//            status = null,
//            ieeeaddr = null,
//            nwkaddr = null,
//            startindex = valObj.startindex,
//            numassocdev = null,
//            assocdevlist = []
//        };

//    if (apiName == "nwkAddrReq")
//        reqObj.ieeeaddr = valObj.ieeeaddr;
//    else
//        reqObj.shortaddr = valObj.shortaddr;

//    var recursiveRequest = function() {
//        self._genericRequest(apiName, reqObj, function (err, rsp) {
//            if (err) {
//                callback(err, finalRsp);
//            } else if (rsp.status !== 0) {
//                callback(new Error("request unsuccess: " + rsp.status), finalRsp);
//            } else {
//                finalRsp.status = rsp.status;
//                finalRsp.ieeeaddr = finalRsp.ieeeaddr || rsp.ieeeaddr;
//                finalRsp.nwkaddr = finalRsp.nwkaddr || rsp.nwkaddr;
//                finalRsp.numassocdev = finalRsp.numassocdev || rsp.numassocdev;
//                finalRsp.assocdevlist = finalRsp.assocdevlist.concat(rsp.assocdevlist);

//                totalToGet = totalToGet || (finalRsp.numassocdev - finalRsp.startindex);    // compute at 1st rsp back
//                accum = accum + rsp.assocdevlist.length;

//                if (valObj.reqtype == 1 && accum<totalToGet) {  // extended, include associated devices
//                    nextIndex = nextIndex + rsp.assocdevlist.length;
//                    reqObj.startindex = nextIndex;
//                    recursiveRequest();
//                } else {
//                    callback(null, finalRsp);
//                }
//            }
//        });
//    };

//    recursiveRequest();
//};

//Zdo.prototype._concatListRequest = function(apiName, valObj, listKeys, callback)
//{
//    // valObj = { dstaddr[, scanchannels, scanduration], startindex }
//    // listKeys = { entries: "networkcount", listcount: "networklistcount", list: "networklist" };
//    var self = this,
//        totalToGet = null,
//        accum = 0,
//        nextIndex = valObj.startindex,
//        reqObj = {
//            dstaddr: valObj.dstaddr,
//            scanchannels: valObj.scanchannels,
//            scanduration: valObj.scanduration,
//            startindex: valObj.startindex    // starts from 0
//        },
//        finalRsp = {
//            srcaddr: null,
//            status: null,
//            startindex: valObj.startindex
//        };

//    finalRsp[listKeys.entries] = null;       // finalRsp.networkcount = null
//    finalRsp[listKeys.listcount] = null;     // finalRsp.networklistcount = null
//    finalRsp[listKeys.list] = [];            // finalRsp.networklist = []

//    if (apiName == "mgmtNwkDiscReq") {
//        reqObj.scanchannels = valObj.scanchannels;
//        reqObj.scanduration = valObj.scanduration;
//    }

//    var recursiveRequest = function() {
//        self._genericRequest(apiName, reqObj, function (err, rsp) {
//            if (err) {
//                callback(err, finalRsp);
//            } else if (rsp.status !== 0) {
//                callback(new Error("request unsuccess: " + rsp.status), finalRsp);
//            } else {
//                finalRsp.status = rsp.status;
//                finalRsp.srcaddr = finalRsp.srcaddr || rsp.srcaddr;
//                finalRsp[listKeys.entries] = finalRsp[listKeys.entries] || rsp[listKeys.entries];
//                finalRsp[listKeys.listcount] = rsp[listKeys.listcount];
//                finalRsp[listKeys.list] = finalRsp[listKeys.list].concat(rsp[listKeys.list]);

//totalToGet = totalToGet || (finalRsp[listKeys.entries] - finalRsp.startindex);
//                accum = accum + rsp[listKeys.list].length;

//                if (accum<totalToGet) {
//                    nextIndex = nextIndex + rsp[listKeys.list].length;
//                    reqObj.startindex = nextIndex;
//                    recursiveRequest();
//                } else {
//                    callback(null, finalRsp);
//                }
//            }
//        });
//    };

//    recursiveRequest();
//};

//}
