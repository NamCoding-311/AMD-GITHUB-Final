using Microsoft.AspNetCore.Mvc;
using URLShortener.API.Services;
using URLShortener.API.Models;

namespace URLShortener.API.Controllers
{
    [ApiController]
    [Route("api/url")]
    public class UrlController : ControllerBase
    {
        private readonly UrlService _urlService;
        private readonly MongoDbService _mongoDb;

        public UrlController(UrlService urlService, MongoDbService mongoDb)
        {
            _urlService = urlService;
            _mongoDb = mongoDb;
        }

        // POST /api/url/shorten
        [HttpPost("shorten")]
        public async Task<IActionResult> Shorten([FromBody] UrlShortenRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.OriginalUrl))
                return BadRequest("OriginalUrl is required");

            string code = _urlService.GenerateCode();

            var data = new MongoShortUrl
            {
                OriginalUrl = req.OriginalUrl,
                Code = code,
                CreatedAt = DateTime.UtcNow,   // thêm ngày tạo
                Clicks = 0                     // thêm lượt click
            };

            await _mongoDb.SaveAsync(data);

            return Ok(new
            {
                id = data.Id,
                code = code,
                originalUrl = data.OriginalUrl,
                createdAt = data.CreatedAt,
                clicks = data.Clicks
            });
        }

        // ⭐⭐ GET /api/url/{code} — FE cần API này để lấy detail ⭐⭐
        [HttpGet("{code}")]
        public async Task<IActionResult> GetOne(string code)
        {
            var result = await _mongoDb.GetByCodeAsync(code);

            if (result == null)
                return NotFound("Not found");

            return Ok(result);
        }

        // GET /api/url/all
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _mongoDb.GetAllAsync();
            return Ok(list);
        }

        // DELETE /api/url/{code}
        [HttpDelete("{code}")]
        public async Task<IActionResult> Delete(string code)
        {
            bool ok = await _mongoDb.DeleteAsync(code);
            if (!ok)
                return NotFound();

            return Ok("Deleted");
        }
    }
}
