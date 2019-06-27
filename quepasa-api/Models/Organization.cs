using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace quepasa_api.Models
{
    public class Organization
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        public string Address { get; set; }
        public Organization()
        {
        }
    }
}
