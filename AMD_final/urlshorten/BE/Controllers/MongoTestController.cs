using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace URLShortener.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MongoTestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                // Chuỗi kết nối MongoDB (khi chạy bằng Docker Compose sẽ đổi sau)
                var connectionString = "mongodb://localhost:27017";
                var client = new MongoClient(connectionString);

                // Kết nối thử đến MongoDB
                var databaseList = client.ListDatabaseNames().ToList();

                return Ok(new
                {
                    Message = "✅ MongoDB Connected successfully!",
                    Databases = databaseList
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"❌ MongoDB connection failed: {ex.Message}");
            }
        }
    }
}
