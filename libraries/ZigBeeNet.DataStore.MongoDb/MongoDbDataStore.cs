using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using ZigBeeNet.Database;
using ZigBeeNet.Util;
using Microsoft.Extensions.Logging;

namespace ZigBeeNet.DataStore.MongoDb
{
    public class MongoDbDataStore : IZigBeeNetworkDataStore
    {
        static private readonly ILogger _logger = LogManager.GetLog<MongoDbDataStore>();

        private readonly IMongoCollection<ZigBeeNodeDao> _nodes;

        public MongoDbDataStore(MongoDbDatabaseSettings settings)
        {
            BsonClassMap.RegisterClassMap<ZigBeeNodeDao>(cm =>
            {
                cm.AutoMap();
                // Do this so that id field from mongoDb does not going in conflict with existing class
                cm.SetIgnoreExtraElements(true);
                cm.MapMember( c => c.IeeeAddress).SetSerializer(new IeeAddressSerializer());
            });

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _nodes = database.GetCollection<ZigBeeNodeDao>(settings.NodesCollectionName);
            // Create index on IeeeAddress
            CreateIndexModel<ZigBeeNodeDao> model = new CreateIndexModel<ZigBeeNodeDao>(
                                                            Builders<ZigBeeNodeDao>.IndexKeys.Ascending(n => n.IeeeAddress),
                                                            new CreateIndexOptions { Unique = true }
                                                        ); 
            _nodes.Indexes.CreateOne( model );

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
                        _logger.LogError(ex, "Error: {Exception}", ex.Message);
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {Exception}", ex.Message);
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
    
        private class IeeAddressSerializer : SerializerBase<IeeeAddress>
        {
            public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, IeeeAddress value)
            {
                context.Writer.WriteString(value.ToString());
            }

            public override IeeeAddress Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
            {
                var type = context.Reader.GetCurrentBsonType();
                if (type is BsonType.String)
                    return new IeeeAddress(context.Reader.ReadString());
                throw new NotSupportedException($"Cannot convert a {type} to IeeAddress");
            }
        }
    }

}
