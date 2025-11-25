using Microsoft.AspNetCore.Mvc;
using URLShortener.API.Services;
using URLShortener.API.Models;

namespace URLShortener.API.Controllers
{
    [ApiController]
    public class RedirectController : ControllerBase
    {
        private readonly MongoDbService _mongoDb;

        public RedirectController(MongoDbService mongoDb)
        {
            _mongoDb = mongoDb;
        }

        // GET /{code}
[HttpGet("{code}")]
public async Task<IActionResult> RedirectToOriginal(string code)
    {
    var record = await _mongoDb.GetByCodeAsync(code);
    if (record == null)
        return NotFound("URL not found");

    // Tăng 1 lượt click
    await _mongoDb.IncreaseClickAsync(code);

    // Nếu request đến từ Swagger thì trả JSON
    string referer = Request.Headers["Referer"].ToString();
    if (referer.Contains("swagger", StringComparison.OrdinalIgnoreCase))
        return Ok(record);

    return Redirect(record.OriginalUrl);
}
    }
}
