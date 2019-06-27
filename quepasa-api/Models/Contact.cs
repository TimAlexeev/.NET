using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace quepasa_api.Models
{
    public class Contact
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public MongoDBRef Organization { get; set; }

        public MongoDBRef RelatedPerson { get; set; }
    }
}