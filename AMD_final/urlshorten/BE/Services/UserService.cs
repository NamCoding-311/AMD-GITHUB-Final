using MongoDB.Driver;
using URLShortener.API.Models;

namespace URLShortener.API.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IConfiguration config)
        {
            var client = new MongoClient(config["MongoDB:ConnectionString"]);
            var db = client.GetDatabase(config["MongoDB:DatabaseName"]);
            _users = db.GetCollection<User>("users");
        }

        public User? GetByUsername(string username) =>
            _users.Find(u => u.Username == username).FirstOrDefault();

        public void Create(User user) =>
            _users.InsertOne(user);
    }
}
