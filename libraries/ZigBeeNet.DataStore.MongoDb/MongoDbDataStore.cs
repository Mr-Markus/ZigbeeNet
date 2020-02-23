using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Serilog;
using ZigBeeNet.Database;

namespace ZigBeeNet.DataStore.MongoDb
{
    public class MongoDbDataStore : IZigBeeNetworkDataStore
    {
        private readonly IMongoCollection<ZigBeeNodeDao> _nodes;

        public MongoDbDataStore(MongoDbDatabaseSettings settings)
        {
            BsonClassMap.RegisterClassMap<ZigBeeNodeDao>(cm =>
            {
                cm.AutoMap();
                // Do this so that id field from mongoDb does not going in conflict with existing class
                cm.SetIgnoreExtraElements(true);
            });

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _nodes = database.GetCollection<ZigBeeNodeDao>(settings.NodesCollectionName);

        }

        public ISet<IeeeAddress> ReadNetworkNodes()
        {
            ISet<IeeeAddress> ieeeAddresses = new HashSet<IeeeAddress>();

            var nodes = _nodes.Find(node => true).ToList();

            try
            {
                nodes.ForEach(node =>
                {
                    try
                    {
                        ieeeAddresses.Add(node.IeeeAddress);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "Error");
                    }
                });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error");
            }

            return ieeeAddresses;
        }

        public ZigBeeNodeDao ReadNode(IeeeAddress address)
        {
            return _nodes.Find<ZigBeeNodeDao>(device => device.IeeeAddress == address).FirstOrDefault();
        }

        public void RemoveNode(IeeeAddress address)
        {
            _nodes.DeleteOne(node => node.IeeeAddress == address);
        }

        public void WriteNode(ZigBeeNodeDao node)
        {
            var nodeFound = ReadNode(node.IeeeAddress);

            if (nodeFound != null)
            {
                _nodes.ReplaceOne(n => n.IeeeAddress == nodeFound.IeeeAddress, node);
            }
            else
            {
                _nodes.InsertOne(node);
            }
        }
    }
}
