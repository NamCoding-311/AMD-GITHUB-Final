using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace URLShortener.API.Models
{
    [BsonIgnoreExtraElements]
    public class ShortUrl
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("code")]
        public string Code { get; set; } = string.Empty;

        [BsonElement("originalUrl")]
        public string OriginalUrl { get; set; } = string.Empty;

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("clicks")]
        public int Clicks { get; set; } = 0;
    }
}
