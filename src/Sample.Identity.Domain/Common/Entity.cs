using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Sample.Identity.Domain.Common
{
    public class Entity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}