using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Clusters;
using ZigBeeNet.ZCL.Clusters.General;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.App.Basic
{
    public class ZclBasicServer : IZigBeeCommandListener
    {
        private ZigBeeNetworkManager _networkManager;
        private ConcurrentDictionary<ushort, ZclAttribute> _attributes = new ConcurrentDictionary<ushort, ZclAttribute>();

        private const int DEFAULT_ZCLVERSION = 2;
        private const int DEFAULT_STACKVERSION = 2;

        public ZclBasicServer(ZigBeeNetworkManager networkManager)
        {
            ZclBasicCluster cluster = new ZclBasicCluster(null);

            foreach (ZclAttribute attribute in cluster.GetAttributes())
            {
                _attributes.TryAdd(attribute.Id, attribute);
            }

            SetAttribute(ZclBasicCluster.ATTR_STACKVERSION, DEFAULT_STACKVERSION);
            SetAttribute(ZclBasicCluster.ATTR_ZCLVERSION, DEFAULT_ZCLVERSION);

            _networkManager = networkManager;
            networkManager.AddCommandListener(this);
        }

        public void Shutdown()
        {
            _networkManager.RemoveCommandListener(this);
        }

        public void CommandReceived(ZigBeeCommand command)
        {
            // We are only interested in READ ATTRIBUTE commands
            if (!(command is ReadAttributesCommand))
            {
                return;
            }

            ReadAttributesCommand readCommand = (ReadAttributesCommand)command;

            if (readCommand.ClusterId != ZclBasicCluster.CLUSTER_ID && readCommand.CommandDirection != ZclCommandDirection.SERVER_TO_CLIENT)
            {
                return;
            }

            List<ReadAttributeStatusRecord> attributeRecords = new List<ReadAttributeStatusRecord>();
            foreach (ushort attributeId in readCommand.Identifiers)
            {
                attributeRecords.Add(GetAttributeRecord(attributeId));
            }

            ReadAttributesResponse readResponse = new ReadAttributesResponse();

            readResponse.Records = attributeRecords;
            readResponse.DestinationAddress = readCommand.SourceAddress;
            readResponse.CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
            readResponse.TransactionId = command.TransactionId;
            _networkManager.SendCommand(readResponse);
        }

        ///<summary>
        ///Sets an attribute value in the basic server.
        ///
        ///<param name="attributeId">the attribute identifier to set</param> 
        ///<param name="attributeValue">the value related to the attribute ID</param> 
        ///<returns>true if the attribute was set</returns> 
        ///</summary>
        public bool SetAttribute(ushort attributeId, object attributeValue)
        {
            if (!_attributes.ContainsKey(attributeId))
            {
                return false;
            }

            _attributes.TryGetValue(attributeId, out ZclAttribute attr);
            attr?.UpdateValue(attributeValue);

            return true;
        }

        private ReadAttributeStatusRecord GetAttributeRecord(ushort attributeId)
        {
            ReadAttributeStatusRecord record = new ReadAttributeStatusRecord(attributeId);

            _attributes.TryGetValue(attributeId, out ZclAttribute attribute);

            if (attribute != null)
            {
                record.Status = ZclStatus.SUCCESS;
                record.AttributeDataType = attribute.ZclDataType;
                record.AttributeValue = attribute.LastValue;
            }
            else
            {
                record.Status = ZclStatus.UNSUPPORTED_ATTRIBUTE;
            }

            return record;
        }
    }
}
