using BinarySerialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ZigbeeNet.CC;
using ZigbeeNet.CC.AF;
using ZigbeeNet.ZCL;

namespace ZigbeeNet
{
    public class ZclBridge
    {
        public event EventHandler AF_DataConfirm;
        public event EventHandler AF_ReflectError;
        public event EventHandler AF_IncomingMsg;
        public event EventHandler AF_IncomingMsgExt;
        public event EventHandler ZCL_IncomingMsg;

        private byte _seqNumber = 0;

        public ZigbeeController Controller { get; private set; }

        public ZclBridge(ZigbeeController controller)
        {
            Controller = controller;
        }

        public void Send(ZclCommand zclCommand, Endpoint srcEndpoint, Endpoint dstEndpoint, Cluster cluster, byte[] payload)
        {
            DataRequest dataRequest = new DataRequest()
            {
                DestinationAddress = dstEndpoint.Device.NwkAdress,
                SourceEndpoint = 0, //TODO: senderEp = srcEp.isLocal() ? srcEp : controller.getCoord().getDelegator(profId);
                DestinationEndpoint = dstEndpoint.Id,
                Cluster = (ushort)cluster,
                TransactionSeqNumber = Controller.NextTransId(),
                Options = (byte)(AFOptions.ACK_REQUEST | AFOptions.DISCV_ROUTE),
                Radius = 0x1e,
                Length = (byte)payload.Length,
                Data = payload
            };

            dataRequest.OnResponse += (object sender, ZpiObject zpiObject) =>
            {
                //TODO: parse zpiObject payload to ZclPacket and it's payload to ZclCommand
                // AF:IncommingMsg --> AREQ Id: 129
                // 1. AF_DataConfirm 2. AF_IncommingMsg ???
                // Call ZclCommand Response() with parsed object as value

                throw new NotImplementedException();
                zclCommand.Response(null);
            };

            dataRequest.Request(Controller.Znp);
        }

        public void ZclGlobal(Endpoint srcEndpoint, Endpoint dstEndpoint, Cluster cluster, ZclCommand zclCommand)
        {
            ZclPacket packet = new ZclPacket(zclCommand.Id);
            packet.Header.TransactionSequenceNumber = nextZclSeqNum();
            packet.Payload = zclCommand.Frame;

            if (packet.Header.FrameControl.Direction == Direction.ClientToServer) // client-to-server, thus require getting the feedback response
            {
                //TODO: Register event for response
            }

            BinarySerializer serializer = new BinarySerializer();
            using (MemoryStream stream = new MemoryStream())
            {
                serializer.Serialize(stream, packet);

                Send(zclCommand, srcEndpoint, dstEndpoint, cluster, stream.ToArray());
            }
        }

        public byte nextZclSeqNum()
        {
            _seqNumber += 1; // seqNumber is a private var on the top of this module

            if (_seqNumber > 255 || _seqNumber < 0)
                _seqNumber = 0;

            return _seqNumber;
        }
    }
}
