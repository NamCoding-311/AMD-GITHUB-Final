using MongoDB.Driver;
using URLShortener.API.Models;

namespace URLShortener.API.Services
{
    public class MongoDbService
    {
        private readonly IMongoCollection<MongoShortUrl> _collection;

        public MongoDbService(IConfiguration config)
        {
            var settings = config.GetSection("MongoDB").Get<MongoDbSettings>();

            var client = new MongoClient(settings.ConnectionString);
            var db = client.GetDatabase(settings.DatabaseName);

            _collection = db.GetCollection<MongoShortUrl>(settings.CollectionName);
        }

        public async Task SaveAsync(MongoShortUrl data) =>
            await _collection.InsertOneAsync(data);

        public async Task<MongoShortUrl?> GetByCodeAsync(string code) =>
            await _collection.Find(x => x.Code == code).FirstOrDefaultAsync();

        public async Task<List<MongoShortUrl>> GetAllAsync() =>
            await _collection.Find(_ => true).ToListAsync();

        public async Task<bool> DeleteAsync(string code)
        {
            var result = await _collection.DeleteOneAsync(x => x.Code == code);
            return result.DeletedCount > 0;
        }

        public async Task IncreaseClickAsync(string code)
        {
            var update = Builders<MongoShortUrl>.Update.Inc(x => x.Clicks, 1);
            await _collection.UpdateOneAsync(x => x.Code == code, update);
        }
    }
}
