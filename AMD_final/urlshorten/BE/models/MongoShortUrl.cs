using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace URLShortener.API.Models
{
    public class MongoShortUrl
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonElement("originalUrl")]
        public string OriginalUrl { get; set; } = null!;

        [BsonElement("code")]
        public string Code { get; set; } = null!;

        [BsonElement("clicks")]
        public int Clicks { get; set; } = 0;

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
